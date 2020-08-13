using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CustomColorPicker.GUI.CustomControl
{
    /// <summary>
    /// Interaction logic for RatingControl.xaml
    /// </summary>
    public partial class RatingControl : UserControl
    {
        
        public RatingControl()
        {
            InitializeComponent();
        }
        public int RatingValue
        {
            get { return (int)GetValue(RatingValueProperty); }
            set { SetValue(RatingValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RatingValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RatingValueProperty =
            DependencyProperty.Register("RatingValue", typeof(int), typeof(RatingControl), new FrameworkPropertyMetadata(0, new PropertyChangedCallback(RaisePropertyChangedCallBack)));

        private static void RaisePropertyChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ratingControl = d as RatingControl;
            ratingControl.setStarValue((int)e.NewValue);
        }

        public int Maximum
        {
            get { return (int)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof(int), typeof(RatingControl), new FrameworkPropertyMetadata(5));

        public int Minimum
        {
            get { return (int)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof(int), typeof(RatingControl), new FrameworkPropertyMetadata(1));

        public void setStarValue(int value)
        {
            //First star
            if (value > 0)
                starcell1.Visibility = Visibility.Visible;
            else
                starcell1.Visibility = Visibility.Hidden;
            //Second star
            if (value > 1)
                starcell2.Visibility = Visibility.Visible;
            else
                starcell2.Visibility = Visibility.Hidden;
            //Third star
            if (value > 2)
                starcell3.Visibility = Visibility.Visible;
            else
                starcell3.Visibility = Visibility.Hidden;
            //Fourth star
            if (value > 3)
                starcell4.Visibility = Visibility.Visible;
            else
                starcell4.Visibility = Visibility.Hidden;
            //Firfth star
            if (value > 4)
                starcell5.Visibility = Visibility.Visible;
            else
                starcell5.Visibility = Visibility.Hidden;
        }

        private void ColorValue_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (ColorValue.Text != string.Empty)
            {
                if (e.IsUp && RatingValue <= Minimum)
                {
                    RatingValue = 1;
                }
                else if (e.IsUp && RatingValue > Maximum)
                {
                    RatingValue = 5;
                }
            }
        }
    }
}
