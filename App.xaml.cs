// ***********************************************************************
// Assembly         : FlightSimulatorApp
// Author           : Modi Esawi . Hosny Ganaim
// Created          : 04-17-2020
//
// Last Modified By : Modi Esawi
// Last Modified On : 04-16-2020
// ***********************************************************************
// <copyright file="App.xaml.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Windows;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Class App.
    /// Implements the <see cref="System.Windows.Application" />
    /// </summary>
    /// <seealso cref="System.Windows.Application" />
    public partial class App : Application
    {
        /// <summary>
        /// The simulator vars
        /// </summary>
        public static Dictionary<string, string> SimulatorVars = new Dictionary<string, string>
        {
            {"Heading","/instrumentation/heading-indicator/indicated-heading-deg" },
            {"Vertical Speed","/instrumentation/gps/indicated-vertical-speed" },
            {"Ground Speed","/instrumentation/gps/indicated-ground-speed-kt" },
            {"Airspeed","/instrumentation/airspeed-indicator/indicated-speed-kt" },
            {"Altitude","/instrumentation/gps/indicated-altitude-ft" },
            {"Roll Degree","/instrumentation/attitude-indicator/internal-roll-deg" },
            {"Pitch Degree","/instrumentation/attitude-indicator/internal-pitch-deg" },
            {"Altimeter Altitude","/instrumentation/altimeter/indicated-altitude-ft" },
            {"Rudder","/controls/flight/rudder" },
            {"Throttle","/controls/engines/current-engine/throttle" },
            {"Elevator", "/controls/flight/elevator" },
            {"Aileron", "/controls/flight/aileron" },
            {"Latitude", "/position/latitude-deg"},
            {"Longitude","/position/longitude-deg"},
        };

        /// <summary>
        /// Pathes the of.
        /// </summary>
        /// <param name="varName">Name of the variable.</param>
        /// <returns>System.String.</returns>
        public static string PathOf(string varName)
        {
            return SimulatorVars[varName];
        }
    }
}