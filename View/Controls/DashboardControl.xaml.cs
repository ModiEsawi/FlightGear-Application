// ***********************************************************************
// Assembly         : FlightSimulatorApp
// Author           : Modi Esawi , Hosny ganaim
// Created          : 03-31-2020
//
// Last Modified By : Modi Esawi
// Last Modified On : 03-20-2020
// ***********************************************************************
// <copyright file="DashboardControl.xaml.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using FlightSimulatorApp.Model;
using FlightSimulatorApp.ViewModel;
using System.Windows.Controls;

namespace FlightSimulatorApp.View.Controls
{
    /// <summary>
    /// Class DashboardControl.
    /// Implements the <see cref="System.Windows.Controls.UserControl" />
    /// Implements the <see cref="System.Windows.Markup.IComponentConnector" />
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class DashboardControl : UserControl
    {
        /// <summary>
        /// The model
        /// </summary>
        private ISimulatorModel Model;
        /// <summary>
        /// The DVM
        /// </summary>
        private DashboardVM DashBoardViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardControl"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public DashboardControl(ISimulatorModel model)
        {
            InitializeComponent();
            this.Model = model;
            DashBoardViewModel = new DashboardVM(model);
            this.DataContext = DashBoardViewModel;
        }
    }
}