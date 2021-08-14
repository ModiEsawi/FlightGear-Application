// ***********************************************************************
// Assembly         : FlightSimulatorApp
// Author           : Modi Esawi , Hosny Ganaim
// Created          : 03-31-2020
//
// Last Modified By : Modi Esawi
// Last Modified On : 03-31-2020
// ***********************************************************************
// <copyright file="Joystick.xaml.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using FlightSimulatorApp.ViewModel;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace FlightSimulatorApp.View.Controls
{
    /// <summary>
    /// Class Joystick.
    /// Implements the <see cref="System.Windows.Controls.UserControl" />
    /// Implements the <see cref="System.Windows.Markup.IComponentConnector" />
    /// Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public partial class Joystick : UserControl, INotifyPropertyChanged
    {
        /// <summary>
        /// Delegate EmptyJoystickEventHandler
        /// </summary>
        /// <param name="sender">The sender.</param>
        public delegate void EmptyJoystickEventHandler(Joystick sender);

        // public delegate void OnScreenJoystickEventHandler(Joystick sender, VirtualJoystickEventArgs args);
        public event EmptyJoystickEventHandler Released;

        public event EmptyJoystickEventHandler Captured;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The SVM
        /// </summary>
        private SteeringVM SteeringViewModel;

        /// <summary>
        /// The starting position
        /// </summary>
        private Point StartingPosition;

        /// <summary>
        /// The canvas width
        /// </summary>
        private double CanvasWidth, CanvasHeight;

        /// <summary>
        /// The center knob
        /// </summary>
        private readonly Storyboard CenterKnob;

        /// <summary>
        /// The aileron
        /// </summary>
        private double PrivateAileron, PrivateElevator;

        /// <summary>
        /// Gets or sets the aileron.
        /// </summary>
        /// <value>The aileron.</value>
        public double Aileron
        {
            set
            {
                PrivateAileron = value;
                SteeringViewModel.AileronViewModel = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Aileron"));
            }
            get
            {
                return PrivateAileron;
            }
        }

        /// <summary>
        /// Invokes the specified joystick.
        /// </summary>
        /// <param name="joystick">The joystick.</param>
        /// <param name="propertyChangedEventArgs">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        /// <exception cref="NotImplementedException"></exception>
        private void Invoke(Joystick joystick, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets or sets the elevator.
        /// </summary>
        /// <value>The elevator.</value>
        public double Elevator
        {
            set
            {
                PrivateElevator = value;
                SteeringViewModel.ElevatorViewModel = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Elevator"));
            }
            get
            {
                return PrivateElevator;
            }
        }

        /// <summary>
        /// The temporary elevator
        /// </summary>
        private double TempElevator = 10;

        /// <summary>
        /// The temporary aileron
        /// </summary>
        private double TempAileron = 10;

        /// <summary>
        /// Initializes a new instance of the <see cref="Joystick"/> class.
        /// </summary>
        /// <param name="svm">The SVM.</param>
        public Joystick(SteeringVM svm)
        {
            InitializeComponent();
            this.SteeringViewModel = svm;
            Knob.MouseMove += KnobMoved;
            Knob.MouseLeftButtonUp += KnobLeft;
            Knob.MouseLeftButtonDown += KnobPressed;

            CenterKnob = Knob.Resources["CenterKnob"] as Storyboard;
        }

        /// <summary>
        /// Knobs the pressed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void KnobPressed(object sender, MouseButtonEventArgs e)
        {
            StartingPosition = e.GetPosition(Base);
            CanvasHeight = Base.ActualHeight - KnobBase.ActualHeight;
            CanvasWidth = Base.ActualWidth - KnobBase.ActualWidth;
            Captured?.Invoke(this); 
            Knob.CaptureMouse();
            CenterKnob.Stop();
        }

        /// <summary>
        /// Knobs the left.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void KnobLeft(object sender, MouseButtonEventArgs e)
        {
            Knob.ReleaseMouseCapture();
            CenterKnob.Begin();
        }

        /// <summary>
        /// Knobs the moved.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void KnobMoved(object sender, MouseEventArgs e)
        {
            if (Knob.IsMouseCaptured == false)
            {
                return;
            }

            Point newPosition = e.GetPosition(Base);

            Point toMove = new Point(newPosition.X - StartingPosition.X, newPosition.Y - StartingPosition.Y);

            double distance = Math.Round(Math.Sqrt(toMove.X * toMove.X + toMove.Y * toMove.Y));
            if (distance >= (CanvasHeight / 2) || distance >= (CanvasWidth / 2))
            {
                return;
            }
            double elevator = (170 - newPosition.Y) / (170 - KnobBase.Height / 2);
            double aileron = (newPosition.X - 170) / (170 - KnobBase.Height / 2);
            if (Math.Abs(elevator - TempElevator) >= 0.1 || Math.Abs(aileron - TempAileron) >= 0.1)
            {
                Elevator = elevator;
                Aileron = aileron;
                TempAileron = aileron;
                TempElevator = elevator;
            }

            knobPosition.Y = toMove.Y;
            knobPosition.X = toMove.X;
        }

        /// <summary>
        /// Handles the Completed event of the centerKnob control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void centerKnob_Completed(object sender, EventArgs e)
        {
            Elevator = 0.0;
            Aileron = 0.0;
            Released?.Invoke(this);
        }
    }
}