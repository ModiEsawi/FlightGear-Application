// ***********************************************************************
// Assembly         : FlightSimulatorApp
// Author           : Modi Esawi , Hosny Ganaim
// Created          : 03-31-2020
//
// Last Modified By : Modi Esawi
// Last Modified On : 03-29-2020
// ***********************************************************************
// <copyright file="SimulatorWindow.xaml.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using FlightSimulatorApp.Model;
using FlightSimulatorApp.View.Controls;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FlightSimulatorApp.View
{
    /// <summary>
    /// Class SimulatorWindow.
    /// Implements the <see cref="System.Windows.Window" />
    /// Implements the <see cref="System.Windows.Markup.IComponentConnector" />
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class SimulatorWindow : Window
    {
        private ViewModel.MainViewModel ViewModel;
        private DashboardControl DashBoard;
        private Steering2 Steering2;
        private ISimulatorModel Model;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimulatorWindow"/> class.
        /// </summary>
        [Obsolete]
        public SimulatorWindow()
        {
            InitializeComponent();
            Model = new SimulatorModel();
            ViewModel = new ViewModel.MainViewModel(Model);
            this.DataContext = ViewModel;
            BingMap bingMap = new BingMap(Model);
            MapBorder.Child = bingMap;
            ViewModel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "ConnectedViewModel")
                {
                    if (ViewModel.Connected)
                    {
                        this.Dispatcher.Invoke((Action)(() =>
                        {
                            //this refers to the form of WPF application
                            ConnectBtn.Background = Brushes.Red;
                            ConnectBtn.Content = "Disconnect";
                            ConnectBtn.Command = ViewModel.DisconnectCommand;
                            StatusTextBlock.Foreground = Brushes.Green;
                            StatusTextBlock.Text = "Connected";
                            bingMap.PlanePin.Visibility = Visibility.Visible;
                           
                        }));
                    }
                    else
                    {
                        this.Dispatcher.Invoke((Action)(() =>
                        {
                            ConnectBtn.Background = SettingsBtn.Background;
                            ConnectBtn.Content = "connect";
                            ConnectBtn.Command = ViewModel.ConnectCommand;
                            StatusTextBlock.Foreground = Brushes.Red;
                            StatusTextBlock.Text = "Disconnected";
                            bingMap.PlanePin.Visibility = Visibility.Hidden;

                        }));
                    }
                }
            };
            DashBoard = new DashboardControl(Model);
            Grid.SetColumn(DashBoard, 1);
            Grid.SetRow(DashBoard, 2);
            mainGrid.Children.Add(DashBoard);
            this.Steering2 = new Steering2(Model);
            SteeringViewBox.Child = Steering2;


        }

        /// <summary>
        /// Handles the Click event of the SettingsButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        [Obsolete]
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindw = new SettingsWindow();
            settingsWindw.ShowDialog();
        }
    }
}