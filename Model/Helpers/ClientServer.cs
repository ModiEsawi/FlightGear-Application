// ***********************************************************************
// Assembly         : FlightSimulatorApp
// Author           : Modi Esawi , Hosny ganaim
// Created          : 03-31-2020
//
// Last Modified By : Modi Esawi
// Last Modified On : 03-29-2020
// ***********************************************************************
// <copyright file="ClientServer.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;

namespace FlightSimulatorApp.Model.Helpers
{
    /// <summary>
    /// Class ClientServer.
    /// </summary>
    internal class ClientServer
    {
        /// <summary>
        /// The client
        /// </summary>
        private TcpClient Client = null;


        public NetworkStream NetworkStream = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientServer"/> class.
        /// </summary>
        public ClientServer()
        {
        }

        /// <summary>
        /// Connects the specified ip.
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <param name="port">The port.</param>
       
        [Obsolete]
        public bool Connect(string ip, int port)
        {
            try
            {
                this.Client = new TcpClient(ip, port);
                this.NetworkStream = Client.GetStream();
                return true;
            }
            catch (Exception) { return false; }
        }

        /// <summary>
        /// Determines whether this instance is connecting.
        /// </summary>
        /// <returns><c>true</c> if this instance is connecting; otherwise, <c>false</c>.</returns>
        public bool IsConnecting()
        {
            if (this.Client != null)
            {
                return this.Client.Connected;
            }
            return false;
        }

        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        public void Disconnect()
        {
            this.Client.Close();
            this.NetworkStream.Close();
        }

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void SendMessage(string message)
        {
            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(message);
            if (!IsConnecting())
                return;
            try
            {
                NetworkStream.Write(bytesToSend, 0, bytesToSend.Length);
            }
            catch (Exception)
            {
            }
        }
    }
}