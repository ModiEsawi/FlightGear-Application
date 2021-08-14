// ***********************************************************************
// Assembly         : FlightSimulatorApp
// Author           : Modi Esawi , Hosny ganaim
// Created          : 03-31-2020
//
// Last Modified By : Modi Esawi
// Last Modified On : 04-01-2020
// ***********************************************************************
// <copyright file="BingMap.xaml.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using FlightSimulatorApp.Model;
using FlightSimulatorApp.ViewModel;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace FlightSimulatorApp.View.Controls
{
    /// <summary>
    /// Interaction logic for BingMap.xaml
    /// </summary>
    public partial class BingMap : UserControl
    {
        /// <summary>
        /// Delegate EmptyDelegate
        /// </summary>
        private delegate void EmptyDelegate();

        public event PropertyChangedEventHandler PropertyChanged;

        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="name">The name.</param>
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BingMap"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public BingMap(ISimulatorModel model)
        {
            InitializeComponent();
            MapVM mapViewModel = new MapVM(model);
            mapViewModel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "leftEarth")
                {
                    Coordinates.Visibility = Visibility.Hidden;
                    OutOfRange.Visibility = Visibility.Visible;
                }
                if (e.PropertyName == "backToEarth")
                {
                    Coordinates.Visibility = Visibility.Visible;
                    OutOfRange.Visibility = Visibility.Hidden;
                }
                if (e.PropertyName == "PushPinColor")
                {
                    PlanePin.Background = new SolidColorBrush(Color.FromRgb(mapViewModel.PinRedColor, 0, 0));
                }
            };
            this.DataContext = mapViewModel;

            new Thread(delegate ()
            {
                while (true)
                {
                    Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Background, new EmptyDelegate(delegate { }));

                    Thread.Sleep(650);
                }
            }).Start();
        }
    }
}