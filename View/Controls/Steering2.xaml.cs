// ***********************************************************************
// Assembly         : FlightSimulatorApp
// Author           : Modi Esawi , Hosny Ganaim
// Created          : 03-31-2020
//
// Last Modified By : Modi Esawi
// Last Modified On : 03-29-2020
// ***********************************************************************
// <copyright file="Steering2.xaml.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using FlightSimulatorApp.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace FlightSimulatorApp.View.Controls
{
    /// <summary>
    /// Interaction logic for Steering2.xaml
    /// </summary>
    public partial class Steering2 : UserControl
    {
        /// <summary>
        /// The model
        /// </summary>
        private ISimulatorModel Model;
        /// <summary>
        /// The SVM
        /// </summary>
        private ViewModel.SteeringVM SteeringViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="Steering2"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public Steering2(ISimulatorModel model)
        {
            InitializeComponent();
            this.Model = model;
            this.SteeringViewModel = new ViewModel.SteeringVM(model);
            Joystick joystick = new Joystick(SteeringViewModel);
            Grid.SetRow(joystick, 1);
            Grid.SetColumn(joystick, 1);
            MainGrid.Children.Add(joystick);
            this.DataContext = SteeringViewModel;

            Binding AileronBinding = new Binding();
            AileronBinding.Path = new PropertyPath("Aileron");
            AileronBinding.Source = joystick;

            AileronBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            BindingOperations.SetBinding(AileronTextBlock, TextBlock.TextProperty, AileronBinding);

            Binding ElevatorBinding = new Binding();
            ElevatorBinding.Path = new PropertyPath("Elevator");
            ElevatorBinding.Source = joystick;
            BindingOperations.SetBinding(ElevatorTextBlock, TextBlock.TextProperty, ElevatorBinding);

            Slider slider = new Slider();
        }
    }
}