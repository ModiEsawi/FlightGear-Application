// ***********************************************************************
// Assembly         : FlightSimulatorApp
// Author           : Modi Esawi
// Created          : 03-31-2020
//
// Last Modified By : Modi Esawi , Hosny ganaim
// Last Modified On : 04-01-2020
// ***********************************************************************
// <copyright file="SettingsWindow.xaml.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Configuration;
using System.Windows;

namespace FlightSimulatorApp.View
{
    /// <summary>
    /// Class SettingsWindow.
    /// Implements the <see cref="System.Windows.Window" />
    /// Implements the <see cref="System.Windows.Markup.IComponentConnector" />
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class SettingsWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsWindow"/> class.
        /// </summary>
        [Obsolete]
        public SettingsWindow()
        {
            InitializeComponent();
            SimIPtextBox.Text = ConfigurationSettings.AppSettings.Get("Simulator IP");
            SimPortTextBox.Text = ConfigurationSettings.AppSettings.Get("Simulator Port");
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        [Obsolete]
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (SimIPtextBox.Text == "" || SimPortTextBox.Text == "")
            {
                MessageBox.Show("field is Missing", "Confirmation");
            }
            else
            {
                ConfigurationSettings.AppSettings.Set("Simulator IP", SimIPtextBox.Text);
                ConfigurationSettings.AppSettings.Set("Simulator Port", SimPortTextBox.Text);
                this.Close();
            }
        }

        /// <summary>
        /// Handles the Click event of the ResetButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            SimIPtextBox.Text = "";
            SimPortTextBox.Text = "";
        }
    }
}