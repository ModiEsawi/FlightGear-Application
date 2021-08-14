// ***********************************************************************
// Assembly         : FlightSimulatorApp
// Author           : Modi Esawi , Hosny ganaim
// Created          : 03-31-2020
//
// Last Modified By : Modi Esawi
// Last Modified On : 03-28-2020
// ***********************************************************************
// <copyright file="DisconnectCommand.cs" company="">
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
    /// Class DisconnectCommand.
    /// Implements the <see cref="System.Windows.Input.ICommand" />
    /// Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
    /// </summary>
    /// <seealso cref="System.Windows.Input.ICommand" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class DisconnectCommand : ICommand, INotifyPropertyChanged
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
        /// Initializes a new instance of the <see cref="DisconnectCommand" /> class.
        /// </summary>
        /// <param name="vm">The view model.</param>
        public DisconnectCommand(MainViewModel vm)
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
            return ViewModel.GetModel().IsConnect();
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
        [Obsolete]
        public void Execute(object parameter)
        {
            ViewModel.Disconnect();
        }

        /// <summary>
        /// conniction between the conniction to the dissconiction.
        /// </summary>
        public void DisconnectFullFill()
        {
            ConnectCommand connectCommand = new ConnectCommand(ViewModel);
            connectCommand.CanExecuteChanged += (given, args) =>
              {
                  connectCommand.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e) {};
              };
            connectCommand.Start();
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            CanExecuteChanged.Invoke(this, EventArgs.Empty);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}