// ***********************************************************************
// Assembly         : FlightSimulatorApp
// Author           : Modi Esawi , Hosny Ganaim
// Created          : 03-31-2020
//
// Last Modified By : Modi Esawi
// Last Modified On : 04-13-2020
// ***********************************************************************
// <copyright file="DashboardVM.cs" company="">
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
    /// Class DashboardVM.
    /// Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    internal class DashboardVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The model
        /// </summary>
        private readonly ISimulatorModel Model;

        /// <summary>
        /// To be sampled
        /// </summary>
        private readonly List<string> ToBeSampled = new List<string>()
        {
            App.PathOf("Heading"),
            App.PathOf("Vertical Speed"),
            App.PathOf("Ground Speed"),
            App.PathOf("Airspeed"),
            App.PathOf("Altitude"),
            App.PathOf("Roll Degree"),
            App.PathOf("Pitch Degree"),
            App.PathOf("Altimeter Altitude"),
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardVM"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public DashboardVM(ISimulatorModel model)
        {
            this.Model = model;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged(e.PropertyName + "ViewModel");
            };
            foreach (var s in this.ToBeSampled)
                model.AddToSymbolTable(s);
        }

        /// <summary>
        /// Gets the vm time.
        /// </summary>
        /// <value>The vm time.</value>
        public string TimeViewModel
        {
            get { return Model.Time; }
        }

        /// <summary>
        /// Gets the vm heading.
        /// </summary>
        /// <value>The vm heading.</value>
        public double HeadingViewModel { get { return Model.Heading; } }

        /// <summary>
        /// Gets the vm vertical speed.
        /// </summary>
        /// <value>The vm vertical speed.</value>
        public double VerticalSpeedViewModel { get { return Model.VerticalSpeed; } }

        /// <summary>
        /// Gets the vm ground speed.
        /// </summary>
        /// <value>The vm ground speed.</value>
        public double GroundSpeedViewModel { get { return Model.GroundSpeed; } }

        /// <summary>
        /// Gets the vm airspeed.
        /// </summary>
        /// <value>The vm airspeed.</value>
        public double AirspeedViewModel { get { return Model.Airspeed; } }

        /// <summary>
        /// Gets the vm altitude.
        /// </summary>
        /// <value>The vm altitude.</value>
        public double AltitudeViewModel { get { return Model.Altitude; } }

        /// <summary>
        /// Gets the vm roll deg.
        /// </summary>
        /// <value>The vm roll deg.</value>
        public double RollDegViewModel { get { return Model.RollDeg; } }

        /// <summary>
        /// Gets the vm pitch deg.
        /// </summary>
        /// <value>The vm pitch deg.</value>
        public double PitchDegViewModel { get { return Model.PitchDeg; } }

        /// <summary>
        /// Gets the vm altimeter altitude.
        /// </summary>
        /// <value>The vm altimeter altitude.</value>
        public double AltimeterAltitudeViewModel { get { return Model.AltimeterAltitude; } }

        /// <summary>
        /// Notifies the property changed.
        /// </summary>
        /// <param name="propName">Name of the property.</param>
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}