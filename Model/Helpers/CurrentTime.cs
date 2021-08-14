// ***********************************************************************
// Assembly         : FlightSimulatorApp
// Author           : Modi Esawi , Hosny ganaim
// Created          : 03-31-2020
//
// Last Modified By : Modi Esawi
// Last Modified On : 03-29-2020
// ***********************************************************************
// <copyright file="CurrentTime.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel;
using System.Threading;

namespace FlightSimulatorApp.Model.Helpers
{
    /// <summary>
    /// Class CurrentTime.
    /// Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class CurrentTime : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CurrentTime"/> is stop.
        /// </summary>
        /// <value><c>true</c> if stop; otherwise, <c>false</c>.</value>
        public bool Stop { set; get; }
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
            set
            {
                this.PrivateTime = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Time"));
            }
            get
            {
                return this.PrivateTime;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentTime"/> class.
        /// </summary>
        public CurrentTime()
        {
            Stop = false;
            new Thread(delegate ()
            {
                while (!Stop)
                {
                    this.Time = DateTime.Now.ToString();
                    Thread.Sleep(1000);
                }
            }).Start();
        }
    }
}