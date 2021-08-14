// ***********************************************************************
// Assembly         : FlightSimulatorApp
// Author           : Modi Esawi , Hosny ganaim
// Created          : 03-31-2020
//
// Last Modified By : Modi Esawi
// Last Modified On : 04-01-2020
// ***********************************************************************
// <copyright file="MapVM.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.Maps.MapControl.WPF;
using FlightSimulatorApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace FlightSimulatorApp.ViewModel
{
    /// <summary>
    /// Class MapVM.
    /// Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    internal class MapVM : INotifyPropertyChanged
    {
        /// <summary>
        /// The model
        /// </summary>
        private ISimulatorModel Model;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The temporary location
        /// </summary>
        private Location TempLocation = null;

        /// <summary>
        /// To be sampled
        /// </summary>
        private List<string> ToBeSampled = new List<string>()
        {
            App.PathOf("Longitude"),
            App.PathOf("Latitude"),
        };

        /// <summary>
        /// Delegate EmptyDelegate
        /// </summary>
        private delegate void EmptyDelegate();

        /// <summary>
        /// The left earth
        /// </summary>
        private bool LeftEarth = false;

        /// <summary>
        /// The red color
        /// </summary>
        private byte RedColor = 0;

        /// <summary>
        /// Gets or sets the color of the pin red.
        /// </summary>
        /// <value>The color of the pin red.</value>
        public byte PinRedColor
        {
            get
            {
                return this.RedColor;
            }
            set
            {
                this.RedColor = value;
                NotifyPropertyChanged("PushPinColor");
            }
        }

        /// <summary>
        /// Gets the vm location.
        /// </summary>
        /// <value>The vm location.</value>
        public Location LocationViewModel
        {
            get
            {
                double latitude = Model.Latitude;
                double longtitude = Model.Longtitude;
                double altitude = Model.Altitude;
                if ((latitude <= 85 && latitude >= -85) || (longtitude <= 180 && longtitude >= -180))
                {
                    if (!LeftEarth)
                    {
                        PinRedColor = (byte)Math.Abs(((latitude + longtitude) / 265) * 255);
                    }
                }

                if ((latitude >= 85 || latitude <= -85) || (longtitude >= 180 || longtitude <= -180))
                {
                    if (!LeftEarth)
                    {
                        NotifyPropertyChanged("leftEarth");
                    }
                    this.LeftEarth = true;
                    return TempLocation;
                }
                else
                {
                    this.TempLocation = new Location(latitude, longtitude, altitude);
                    if (LeftEarth)
                    {
                        NotifyPropertyChanged("backToEarth");
                        LeftEarth = false;
                    }
                    return this.TempLocation;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MapVM"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public MapVM(ISimulatorModel model)
        {
            this.Model = model;

            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("LocationViewModel");
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
    }
}