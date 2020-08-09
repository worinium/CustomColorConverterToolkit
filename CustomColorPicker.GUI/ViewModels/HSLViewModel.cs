using ColorMine.ColorSpaces;
using System.Windows.Media;

namespace ColorPicker.ViewModels
{
    public class HSLViewModel : ColorModelBase
    {
        private double _hue;
        private double _saturation;
        private double _brightness;
        public HSLViewModel(ColorViewerModalViewModel viewModel) : base(viewModel)
        {

        }
        public double Hue
        {
            get => _hue;
            set
            {
                 _hue = value;
                OnPropertyChanged();
                UpdateColorFromRGB();
            }
        }

        public double Saturation
        {
            get => _saturation;
            set
            {
                _saturation = value;
                OnPropertyChanged();
                UpdateColorFromRGB();
            }
        }

        public double Brightness
        {
            get => _brightness;
            set
            {
               _brightness = value;
                OnPropertyChanged();
                UpdateColorFromRGB();
            }
        }

        public override void ReadOurColorFromRGB()
        {
            var color = this.mainVm.MySolidColorBrush.Color;
            byte _red = color.R;
            byte _green = color.G;
            byte _blue = color.B;
            byte _alpha = color.A;
            var rgb = new Rgb { R = _red, G = _green, B = _blue };
            var hsv = rgb.To<Hsv>();
            _hue = hsv.H;
            _saturation = hsv.S * 100;
            _brightness = hsv.V * 100;
            //_hue = color.GetHue();
            //_saturation = color.GetSaturation();
            //_brightness = color.GetBrightness();
        }
        public override void UpdateColorFromRGB()
        {
            var hsv = new Hsv { H = _hue, S = _saturation, V = _brightness };
            var rgb = hsv.To<Rgb>();
            byte r = (byte)rgb.R;
            byte g = (byte)rgb.G;
            byte b = (byte)rgb.B;
            mainVm.MySolidColorBrush = new SolidColorBrush(Color.FromRgb(r, g, b));

        }
    }
}
