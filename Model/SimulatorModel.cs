// ***********************************************************************
// Assembly         : FlightSimulatorApp
// Author           : Modi Esawi , Hosnt ganaim
// Created          : 03-31-2020
//
// Last Modified By : Modi Esawi
// Last Modified On : 04-13-2020
// ***********************************************************************
// <copyright file="SimulatorModel.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using FlightSimulatorApp.Model.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace FlightSimulatorApp.Model
{
    /// <summary>
    /// Class SimulatorModel.
    /// Implements the <see cref="FlightSimulatorApp.Model.ISimulatorModel" />
    /// </summary>
    /// <seealso cref="FlightSimulatorApp.Model.ISimulatorModel" />
    internal class SimulatorModel : ISimulatorModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The simulator handler
        /// </summary>
        private SimulatorHandler SimulatorHandler = null;
        /// <summary>
        /// The current time
        /// </summary>
        private CurrentTime CurrentTime = null;
        /// <summary>
        /// The time
        /// </summary>
        private string PrivateTime;

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>The time.</value>
        public string Time
        {
            get { return PrivateTime; }
            set
            {
                this.PrivateTime = value;
                NotifyPropertyChanged("Time");
            }
        }

        /// <summary>
        /// The heading
        /// </summary>
        private double PrivateHeading, PrivateVerticalSpeed, PrivateGroundSpeed, PrivateAirSpeed, PrivateAltitude, PrivateRollDeg, PrivatePitchDeg, PrivateAltimeterAltitude, PrivateRudder, PrivateThrottle, PrivateElevator, PrivateAileron, PrivateLongtitude, PrivateLatitude;

        /// <summary>
        /// Gets or sets the heading.
        /// </summary>
        /// <value>The heading.</value>
        public double Heading { get { return PrivateHeading; } set { PrivateHeading = value; NotifyPropertyChanged("Heading"); } }
        /// <summary>
        /// Gets or sets the vertical speed.
        /// </summary>
        /// <value>The vertical speed.</value>
        public double VerticalSpeed { get { return PrivateVerticalSpeed; } set { PrivateVerticalSpeed = value; NotifyPropertyChanged("VerticalSpeed"); } }
        /// <summary>
        /// Gets or sets the ground speed.
        /// </summary>
        /// <value>The ground speed.</value>
        public double GroundSpeed { get { return PrivateGroundSpeed; } set { PrivateGroundSpeed = value; NotifyPropertyChanged("GroundSpeed"); } }
        /// <summary>
        /// Gets or sets the airspeed.
        /// </summary>
        /// <value>The airspeed.</value>
        public double Airspeed { get { return PrivateAirSpeed; } set { PrivateAirSpeed = value; NotifyPropertyChanged("Airspeed"); } }
        /// <summary>
        /// Gets or sets the altitude.
        /// </summary>
        /// <value>The altitude.</value>
        public double Altitude { get { return PrivateAltitude; } set { PrivateAltitude = value; NotifyPropertyChanged("Altitude"); } }
        /// <summary>
        /// Gets or sets the roll deg.
        /// </summary>
        /// <value>The roll deg.</value>
        public double RollDeg { get { return PrivateRollDeg; } set { PrivateRollDeg = value; NotifyPropertyChanged("RollDeg"); } }
        /// <summary>
        /// Gets or sets the pitch deg.
        /// </summary>
        /// <value>The pitch deg.</value>
        public double PitchDeg { get { return PrivatePitchDeg; } set { PrivatePitchDeg = value; NotifyPropertyChanged("PitchDeg"); } }
        /// <summary>
        /// Gets or sets the altimeter altitude.
        /// </summary>
        /// <value>The altimeter altitude.</value>
        public double AltimeterAltitude { get { return PrivateAltimeterAltitude; } set { PrivateAltimeterAltitude = value; NotifyPropertyChanged("AltimeterAltitude"); } }
        /// <summary>
        /// Gets or sets the rudder.
        /// </summary>
        /// <value>The rudder.</value>
        public double Rudder { get { return PrivateRudder; } set { PrivateRudder = value; NotifyPropertyChanged("Rudder"); } }
        /// <summary>
        /// Gets or sets the throttle.
        /// </summary>
        /// <value>The throttle.</value>
        public double Throttle { get { return PrivateThrottle; } set { PrivateThrottle = value; NotifyPropertyChanged("Throttle"); } }
        /// <summary>
        /// Gets or sets the elevator.
        /// </summary>
        /// <value>The elevator.</value>
        public double Elevator { get { return PrivateElevator; } set { PrivateElevator = value; NotifyPropertyChanged("Elevator"); } }
        /// <summary>
        /// Gets or sets the aileron.
        /// </summary>
        /// <value>The aileron.</value>
        public double Aileron { get { return PrivateAileron; } set { PrivateAileron = value; NotifyPropertyChanged("Aileron"); } }
        /// <summary>
        /// Gets or sets the longtitude.
        /// </summary>
        /// <value>The longtitude.</value>
        public double Longtitude { get { return PrivateLongtitude; } set { PrivateLongtitude = value; NotifyPropertyChanged("Longtitude"); } }
        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>The latitude.</value>
        public double Latitude { get { return PrivateLatitude; } set { PrivateLatitude = value; NotifyPropertyChanged("Latitude"); } }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimulatorModel"/> class.
        /// </summary>
        [Obsolete]
        public SimulatorModel()
        {
            CurrentTime = new CurrentTime();
            CurrentTime.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e) { Time = CurrentTime.Time; };

            SimulatorHandler = new SimulatorHandler();
            SimulatorHandler.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "varsTable")
                {
                    Heading = SimulatorHandler.GetFromSymbolTable(App.PathOf("Heading"));
                    VerticalSpeed = SimulatorHandler.GetFromSymbolTable(App.PathOf("Vertical Speed"));
                    GroundSpeed = SimulatorHandler.GetFromSymbolTable(App.PathOf("Ground Speed"));
                    Airspeed = SimulatorHandler.GetFromSymbolTable(App.PathOf("Airspeed"));
                    Altitude = SimulatorHandler.GetFromSymbolTable(App.PathOf("Altitude"));
                    RollDeg = SimulatorHandler.GetFromSymbolTable(App.PathOf("Roll Degree"));
                    PitchDeg = SimulatorHandler.GetFromSymbolTable(App.PathOf("Pitch Degree"));
                    AltimeterAltitude = SimulatorHandler.GetFromSymbolTable(App.PathOf("Altimeter Altitude"));
                    Rudder = SimulatorHandler.GetFromSymbolTable(App.PathOf("Rudder"));
                    Throttle = SimulatorHandler.GetFromSymbolTable(App.PathOf("Throttle"));
                    Elevator = SimulatorHandler.GetFromSymbolTable(App.PathOf("Elevator"));
                    Aileron = SimulatorHandler.GetFromSymbolTable(App.PathOf("Aileron"));
                    Latitude = SimulatorHandler.GetFromSymbolTable(App.PathOf("Latitude"));
                    Longtitude = SimulatorHandler.GetFromSymbolTable(App.PathOf("Longitude"));
                }
                else if (e.PropertyName == "Connected")
                {
                    NotifyPropertyChanged("Connected");
                }
            };
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
        /// Connects the specified ip.
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <param name="port">The port.</param>
        [Obsolete]
        public void Connect(string ip, int port)
        {
            this.SimulatorHandler.Connect(ip, port);
        }

        /// <summary>
        /// Sets the variable.
        /// </summary>
        /// <param name="vapPath">The vap path.</param>
        /// <param name="value">The value.</param>
        public void SetVariable(string vapPath, string value)
        {
            if (this.SimulatorHandler.IsConnecting())
            {
                this.SimulatorHandler.SetVariable(vapPath, value);
            }
        }

        /// <summary>
        /// Adds to symbol table.
        /// </summary>
        /// <param name="varPath">The variable path.</param>
        public void AddToSymbolTable(string varPath)
        {
            this.SimulatorHandler.AddToSymbolTable(varPath);
        }

        /// <summary>
        /// Determines whether this instance is connect.
        /// </summary>
        /// <returns><c>true</c> if this instance is connect; otherwise, <c>false</c>.</returns>
        public bool IsConnect()
        {
            return this.SimulatorHandler.IsConnecting();
        }

        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        public void Disconnect()
        {
            this.SimulatorHandler.Disconnect();
        }
    }
}