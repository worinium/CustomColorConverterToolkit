using System;
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

namespace CustomControls
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
    }
}
