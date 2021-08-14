// ***********************************************************************
// Assembly         : FlightSimulatorApp
// Author           : Modi Esawi , Hosny ganaim
// Created          : 03-31-2020
//
// Last Modified By : Modi Esawi
// Last Modified On : 04-13-2020
// ***********************************************************************
// <copyright file="SteeringVM.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using FlightSimulatorApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace FlightSimulatorApp.ViewModel
{
    /// <summary>
    /// Class SteeringVM.
    /// Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class SteeringVM : INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The model
        /// </summary>
        private ISimulatorModel Model;

        /// <summary>
        /// To be sampled
        /// </summary>
        private List<string> ToBeSampled = new List<string>()
        {
            App.PathOf("Rudder"),
            App.PathOf("Throttle"),
            App.PathOf("Elevator"),
            App.PathOf("Aileron"),
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="SteeringVM"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public SteeringVM(ISimulatorModel model)
        {
            this.Model = model;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged( e.PropertyName + "ViewModel");
            };
            foreach (var s in this.ToBeSampled)
                model.AddToSymbolTable(s);
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
        /// The vm rudder
        /// </summary>
        private double PrivateViewModelRudder;

        /// <summary>
        /// The vm throttle
        /// </summary>
        private double PrivateViewModelThrottle;

        /// <summary>
        /// The vm elevator
        /// </summary>
        private double PrivateViewModelElevator;

        /// <summary>
        /// The vm aileron
        /// </summary>
        private double PrivateViewModelAileron;

        /// <summary>
        /// Gets or sets the vm rudder.
        /// </summary>
        /// <value>The vm rudder.</value>
        public double RudderViewModel
        {
            set
            {
                PrivateViewModelRudder = value;
                Model.SetVariable(App.PathOf("Rudder"), value.ToString());
            }
            get
            {
                return Model.Rudder;
            }
        }

        /// <summary>
        /// Gets or sets the vm throttle.
        /// </summary>
        /// <value>The vm throttle.</value>
        public double ThrottleViewModel
        {
            set
            {
                PrivateViewModelThrottle = value;
                Model.SetVariable(App.PathOf("Throttle"), value.ToString());
            }
            get
            {
                return Model.Throttle;
            }
        }

        /// <summary>
        /// Gets or sets the vm elevator.
        /// </summary>
        /// <value>The vm elevator.</value>
        public double ElevatorViewModel
        {
            set
            {
                PrivateViewModelElevator = value;
                Model.SetVariable(App.PathOf("Elevator"), value.ToString());
            }
            get
            {
                return Model.Elevator;
            }
        }

        /// <summary>
        /// Gets or sets the aileron view model.
        /// </summary>
        /// <value>The aileron view model.</value>
        public double AileronViewModel
        {
            set
            {
                PrivateViewModelAileron = value;
                Model.SetVariable(App.PathOf("Aileron"), value.ToString());
            }
            get
            {
                return Model.Aileron;
            }
        }
    }
}