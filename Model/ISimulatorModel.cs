// ***********************************************************************
// Assembly         : FlightSimulatorApp
// Author           : Modi Esawi , Hosny ganaim
// Created          : 03-31-2020
//
// Last Modified By : Modi Esawi
// Last Modified On : 03-29-2020
// ***********************************************************************
// <copyright file="ISimulatorModel.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel;

namespace FlightSimulatorApp.Model
{
    /// <summary>
    /// Interface ISimulatorModel
    /// Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public interface ISimulatorModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>The time.</value>
        string Time { set; get; }

        /// <summary>
        /// Connects the specified ip.
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <param name="port">The port.</param>
        void Connect(string ip, int port);

        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Sets the variable.
        /// </summary>
        /// <param name="varPath">The variable path.</param>
        /// <param name="value">The value.</param>
        void SetVariable(string varPath, string value);

        /// <summary>
        /// Adds to symbol table.
        /// </summary>
        /// <param name="varPath">The variable path.</param>
        void AddToSymbolTable(string varPath);

        /// <summary>
        /// Determines whether this instance is connect.
        /// </summary>
        /// <returns><c>true</c> if this instance is connect; otherwise, <c>false</c>.</returns>
        bool IsConnect();

        /// <summary>
        /// Gets or sets the heading.
        /// </summary>
        /// <value>The heading.</value>
        double Heading { set; get; }
        /// <summary>
        /// Gets or sets the vertical speed.
        /// </summary>
        /// <value>The vertical speed.</value>
        double VerticalSpeed { set; get; }
        /// <summary>
        /// Gets or sets the ground speed.
        /// </summary>
        /// <value>The ground speed.</value>
        double GroundSpeed { set; get; }
        /// <summary>
        /// Gets or sets the airspeed.
        /// </summary>
        /// <value>The airspeed.</value>
        double Airspeed { set; get; }
        /// <summary>
        /// Gets or sets the altitude.
        /// </summary>
        /// <value>The altitude.</value>
        double Altitude { set; get; }
        /// <summary>
        /// Gets or sets the roll deg.
        /// </summary>
        /// <value>The roll deg.</value>
        double RollDeg { set; get; }
        /// <summary>
        /// Gets or sets the pitch deg.
        /// </summary>
        /// <value>The pitch deg.</value>
        double PitchDeg { set; get; }
        /// <summary>
        /// Gets or sets the altimeter altitude.
        /// </summary>
        /// <value>The altimeter altitude.</value>
        double AltimeterAltitude { set; get; }
        /// <summary>
        /// Gets or sets the throttle.
        /// </summary>
        /// <value>The throttle.</value>
        double Throttle { set; get; }
        /// <summary>
        /// Gets or sets the rudder.
        /// </summary>
        /// <value>The rudder.</value>
        double Rudder { set; get; }
        /// <summary>
        /// Gets or sets the elevator.
        /// </summary>
        /// <value>The elevator.</value>
        double Elevator { set; get; }
        /// <summary>
        /// Gets or sets the aileron.
        /// </summary>
        /// <value>The aileron.</value>
        double Aileron { set; get; }

        /// <summary>
        /// Gets or sets the longtitude.
        /// </summary>
        /// <value>The longtitude.</value>
        double Longtitude { set; get; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>The latitude.</value>
        double Latitude { set; get; }
    }
}