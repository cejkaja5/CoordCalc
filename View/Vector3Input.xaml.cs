﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CoordCalc.ClassLib;

namespace CoordCalc.View
{
    /// <summary>
    /// Interaction logic for Vector3Input.xaml
    /// </summary>
    public partial class Vector3Input : UserControl
    {

        public static readonly DependencyProperty TextBoxWidthProperty =
            DependencyProperty.Register(
                nameof(TextBoxWidth),
                typeof(double),
                typeof(Vector3Input),
                new PropertyMetadata(45.0)); // default value

        public double TextBoxWidth
        {
            get => (double)GetValue(TextBoxWidthProperty);
            set => SetValue(TextBoxWidthProperty, value);
        }
        public Vector3Input()
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(VectorType))
            {
                VectorType = "EulerAnglesDeg";
            }
        }

        public static readonly DependencyProperty VectorTypeProperty =
            DependencyProperty.Register(
                "VectorType",
                typeof(string),
                typeof(Vector3Input),
                new PropertyMetadata("Default"));

        public string VectorType
        {
            get { return (string)GetValue(VectorTypeProperty); }
            set { SetValue(VectorTypeProperty, value); }
        }

        public Vector3DisplayModel Vector
        {
            get { return (Vector3DisplayModel)GetValue(VectorProperty); }
            set { SetValue(VectorProperty, value); }
        }

        public static readonly DependencyProperty VectorProperty =
            DependencyProperty.Register(nameof(Vector), typeof(Vector3DisplayModel),
                typeof(Vector3Input), new PropertyMetadata(null));


        private void TextBox_SelectAllTextOnFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.SelectAll();
            }
        }

        private void TextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && !textBox.IsKeyboardFocusWithin)
            {
                e.Handled = true;
                textBox.Focus();
                textBox.SelectAll();
            }
        }
    }
}
