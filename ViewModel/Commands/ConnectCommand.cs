// ***********************************************************************
// Assembly         : FlightSimulatorApp
// Author           : Modi Esawi , Hosny ganaim
// Created          : 03-31-2020
//
// Last Modified By : Modi Esawi
// Last Modified On : 04-17-2020
// ***********************************************************************
// <copyright file="ConnectCommand.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace FlightSimulatorApp.ViewModel.Commands
{
    /// <summary>
    /// Class ConnectCommand.
    /// Implements the <see cref="System.Windows.Input.ICommand" />
    /// Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
    /// </summary>
    /// <seealso cref="System.Windows.Input.ICommand" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class ConnectCommand : ICommand, INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the vm.
        /// </summary>
        /// <value>The vm.</value>
        public MainViewModel ViewModel { set; get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectCommand" /> class.
        /// </summary>
        /// <param name="vm">The vm.</param>
        public ConnectCommand(MainViewModel vm)
        {
            ViewModel = vm;
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
        /// <returns><see langword="true" /> if this command can be executed; otherwise, <see langword="false" />.</returns>
        public bool CanExecute(object parameter)
        {
            return !ViewModel.GetModel().IsConnect();
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
        [Obsolete]
        public void Execute(object parameter)
        {
            ViewModel.ConnectToServer();
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            CanExecuteChanged.Invoke(this, EventArgs.Empty);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }

        /// <summary>
        /// conniction between the conniction to the dissconiction.
        /// </summary>
        public void ConnectFullFill()
        {
            DisconnectCommand disconnectCommand = new DisconnectCommand(ViewModel);
            disconnectCommand.CanExecuteChanged += (given, args) =>
             {
                 disconnectCommand.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e){};
             };
            disconnectCommand.Start();
        }
    }
}