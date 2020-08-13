using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ColorPickerLibrary.Views
{
    /// <summary>
    /// Interaction logic for CustomSpinner.xaml
    /// </summary>
    public partial class CustomSpinner : UserControl
    {

         private readonly Regex _numMatch;
        public CustomSpinner()
        {
            InitializeComponent();
            _numMatch = new Regex(@"^-?\d+$");
        }
        private void Increase_Click(object sender, RoutedEventArgs e)
        {
            if (Value < Maximum)
            {
                Value++;
                RaiseEvent(new RoutedEventArgs(IncreaseClickedEvent));
            }
        }

        private void Decrease_Click(object sender, RoutedEventArgs e)
        {
            if (Value > Minimum)
            {
                Value--;
                RaiseEvent(new RoutedEventArgs(DecreaseClickedEvent));
            }
        }     

        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set
            {
                SetValue(ValueProperty, value);
            }
        }

        public static readonly DependencyProperty ValueProperty =  DependencyProperty.Register("Value", typeof(double), typeof(CustomSpinner), new PropertyMetadata(0, new PropertyChangedCallback(OnSomeValuePropertyChanged)));


        private static void OnSomeValuePropertyChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            CustomSpinner numericBox = target as CustomSpinner;
            numericBox.Value = (int)e.NewValue;
        }

        public int Maximum
        {
            get { return (int)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof(int), typeof(CustomSpinner), new FrameworkPropertyMetadata(360));

        public int Minimum
        {
            get { return (int)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof(int), typeof(CustomSpinner), new FrameworkPropertyMetadata(0));

        private static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CustomSpinner));

        public event RoutedEventHandler ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }

        //Increase button clicked
        private static readonly RoutedEvent IncreaseClickedEvent = EventManager.RegisterRoutedEvent("IncreaseClicked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CustomSpinner));

        /// <summary>The IncreaseClicked event is called when the Increase button clicked</summary>
        public event RoutedEventHandler IncreaseClicked
        {
            add { AddHandler(IncreaseClickedEvent, value); }
            remove { RemoveHandler(IncreaseClickedEvent, value); }
        }

        //Increase button clicked
        private static readonly RoutedEvent DecreaseClickedEvent = EventManager.RegisterRoutedEvent("DecreaseClicked", RoutingStrategy.Bubble,typeof(RoutedEventHandler), typeof(CustomSpinner));

        /// <summary>The DecreaseClicked event is called when the Decrease button clicked</summary>
        public event RoutedEventHandler DecreaseClicked
        {
            add { AddHandler(DecreaseClickedEvent, value); }
            remove { RemoveHandler(DecreaseClickedEvent, value); }
        }
        
        private void value_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.IsDown && e.Key == Key.Up && Value < Maximum)
            {
                Value++;
                RaiseEvent(new RoutedEventArgs(IncreaseClickedEvent));
            }
            else if (e.IsDown && e.Key == Key.Down && Value > Minimum)
            {
                Value--;
                RaiseEvent(new RoutedEventArgs(DecreaseClickedEvent));
            }
        }

        private void ResetText(TextBox tb)
        {
            bool stateOftxtValue = double.TryParse(tb.Text, out double result);
            if (stateOftxtValue)
            {
                tb.Text = result < Minimum ? Minimum.ToString() : "0";
            }
            tb.SelectAll();
        }
        private void ColorValue_TextChanged(object sender, TextChangedEventArgs e)
       {
            if(ColorValue.Text != string.Empty)
            {
                string input = ColorValue.Text;
                try
                {
                    Value = int.Parse(input);
                    if (Value < Minimum) Value = Minimum;
                    if (Value > Maximum) Value = Maximum;
                    RaiseEvent(new RoutedEventArgs(ValueChangedEvent));
                }
                catch (Exception)
                {
                   MessageBox.Show("Wrong Input");
                }
                
            }
            
        }
        private void ColorValue_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var text = ColorValue.Text.Insert(ColorValue.CaretIndex, e.Text);
            e.Handled = !_numMatch.IsMatch(text);
        }
    }
}
