// ***********************************************************************
// Assembly         : FlightSimulatorApp
// Author           : Modi Esawi , Hosny ganaim
// Created          : 03-31-2020
//
// Last Modified By : Modi Esawi
// Last Modified On : 03-29-2020
// ***********************************************************************
// <copyright file="SimulatorHandler.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Model.Helpers
{
    /// <summary>
    /// Class SimulatorHandler.
    /// Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    internal class SimulatorHandler : INotifyPropertyChanged
    {
        /// <summary>
        /// The client
        /// </summary>
        private readonly ClientServer Client;

        /// <summary>
        /// The symbol table
        /// </summary>
        private readonly Dictionary<string, double> SymbolTable;

        /// <summary>
        /// To send
        /// </summary>
        private readonly Queue<string> ToSend;

        /// <summary>
        /// The mutex
        /// </summary>
        private static Mutex Mutex;

        /// <summary>
        /// To send at once
        /// </summary>
        private const int SendAtOnce = 100;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimulatorHandler"/> class.
        /// </summary>
        public SimulatorHandler()
        {
            this.Client = new ClientServer();
            this.SymbolTable = new Dictionary<string, double>();
            this.ToSend = new Queue<string>();
        }

        /// <summary>
        /// Connects the specified ip.
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <param name="port">The port.</param>
        [Obsolete]
        public void Connect(string ip, int port)
        {
            Mutex = new Mutex();
            if (!this.Client.Connect(ip, port))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ERR"));
                return;
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Connected"));
            StartUpdating();
            SendToServer();
        }

        /// <summary>
        /// Sends to server.
        /// </summary>
        private void SendToServer()
        {
            new Thread(delegate ()
            {
                while (IsConnecting())
                {
                    Mutex.WaitOne();
                    for (var i = 0; i < SendAtOnce; i++)
                    {
                        if (ToSend.Count != 0)
                            Client.SendMessage(this.ToSend.Dequeue());
                        else
                            break;
                    }
                    Mutex.ReleaseMutex();
                    Thread.Sleep(10);
                }
            }).Start();
        }

        /// <summary>
        /// Starts the updating.
        /// </summary>
        private void StartUpdating()
        {
            new Thread(delegate ()
            {
                while (IsConnecting())
                {
                    if (this.SymbolTable.Count == 0)
                    {
                        Thread.Sleep(500);
                        continue;
                    }
                    List<string> keys = new List<string>(SymbolTable.Keys);
                    string request = "";
                    Queue<string> varsQueue = new Queue<string>();
                    foreach (var key in keys)
                    {
                        varsQueue.Enqueue(key);
                        request += "get " + key + "\r\n";
                    }
                    Mutex.WaitOne();
                    Client.SendMessage(request);
                    Thread.Sleep(250);
                    Byte[] data = new Byte[4096];
                    Int32 bytes = -1;

                    var task = Task.Run(delegate ()
                    {
                        int byt = 0;
                        try
                        {
                            byt = Client.NetworkStream.Read(data, 0, data.Length);
                            return byt;
                        }
                        catch (Exception e)
                        {
                            Exception e1 = e;
                            return -1;
                        }
                    });
                    if (task.Wait(TimeSpan.FromSeconds(10)))
                    {
                        bytes = task.Result;
                    }
                    else
                    {
                        break;
                    }

                    if (bytes == -1)
                    {
                        break;
                    }

                    string fullResponse = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

                    String[] lines = fullResponse.Split('\n');
                    foreach (var l in lines)

                        if (lines.Length - 1 == varsQueue.Count)
                        {
                            foreach (string line in lines)
                            {
                                Double valueAsDouble = 0.0;
                                try
                                {
                                    valueAsDouble = double.Parse(line);
                                    SymbolTable[varsQueue.Dequeue()] = double.Parse(line);
                                }
                                catch (Exception)
                                {
                                    break;
                                }
                            }
                        }

                    Mutex.ReleaseMutex();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("varsTable"));
                }
                Mutex.ReleaseMutex();
                Disconnect();
            }).Start();
        }

        /// <summary>
        /// Gets from symbol table.
        /// </summary>
        /// <param name="varPath">The variable path.</param>
        /// <returns>System.Double.</returns>
        public double GetFromSymbolTable(string varPath)
        {
            return this.SymbolTable[varPath];
        }

        /// <summary>
        /// Adds to symbol table.
        /// </summary>
        /// <param name="varPath">The variable path.</param>
        public void AddToSymbolTable(string varPath)
        {
            this.SymbolTable.Add(varPath, 0);
        }

        /// <summary>
        /// Sets the variable.
        /// </summary>
        /// <param name="varPath">The variable path.</param>
        /// <param name="value">The value.</param>
        public void SetVariable(string varPath, string value)
        {
            this.ToSend.Enqueue(string.Format("set {0} {1}\r\n", varPath, value));
        }

        /// <summary>
        /// Determines whether this instance is connecting.
        /// </summary>
        /// <returns><c>true</c> if this instance is connecting; otherwise, <c>false</c>.</returns>
        public bool IsConnecting()
        {
            return this.Client.IsConnecting();
        }

        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        public void Disconnect()
        {
            this.Client.Disconnect();
            foreach (string k in SymbolTable.Keys.ToList<string>())
                SymbolTable[k] = 0.0;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Connected"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("varsTable"));
        }
    }
}