// ***********************************************************************
// Assembly         : FlightSimulatorApp
// Author           : Modi Esawi , Hosny ganaim
// Created          : 03-31-2020
//
// Last Modified By : Modi Esawi
// Last Modified On : 04-14-2020
// ***********************************************************************
// <copyright file="ViewModel.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using FlightSimulatorApp.Model;
using FlightSimulatorApp.ViewModel.Commands;
using System;
using System.ComponentModel;
using System.Configuration;

namespace FlightSimulatorApp.ViewModel
{
    /// <summary>
    /// Class MainViewModel.
    /// Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The model
        /// </summary>
        private ISimulatorModel Model;

        /// <summary>
        /// Gets or sets the connect command.
        /// </summary>
        /// <value>The connect command.</value>
        public ConnectCommand ConnectCommand { set; get; }

        /// <summary>
        /// Gets or sets the disconnect command.
        /// </summary>
        /// <value>The disconnect command.</value>
        public DisconnectCommand DisconnectCommand { set; get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public MainViewModel(ISimulatorModel model)
        {
            this.Model = model;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged(e.PropertyName + "ViewModel");
            };
            this.ConnectCommand = new ConnectCommand(this);
            this.ConnectCommand.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged(e.PropertyName);
            };
            this.DisconnectCommand = new DisconnectCommand(this);
            this.DisconnectCommand.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged(e.PropertyName);
            };
        }

        /// <summary>
        /// Gets the time view model.
        /// </summary>
        /// <value>The time view model.</value>
        public string TimeViewModel
        {
            get
            {
                return Model.Time;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="MainViewModel"/> is connected.
        /// </summary>
        /// <value><c>true</c> if connected; otherwise, <c>false</c>.</value>
        public bool Connected
        {
            get { return Model.IsConnect(); }
        }

        /// <summary>
        /// Notifies the property changed.
        /// </summary>
        /// <param name="propName">Name of the property.</param>
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        /// <summary>
        /// Connects to server.
        /// </summary>
        [Obsolete]
        public void ConnectToServer()
        {
            string ip = ConfigurationSettings.AppSettings.Get("Simulator IP");
            int port = int.Parse(ConfigurationSettings.AppSettings.Get("Simulator Port"));
            this.Model.Connect(ip, port);
        }

        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        public void Disconnect()
        {
            this.Model.Disconnect();
        }

        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <returns>ISimulatorModel.</returns>
        public ISimulatorModel GetModel()
        {
            return this.Model;
        }
    }
}