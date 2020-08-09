using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ColorPickerLibrary.ViewModels;

namespace ColorPicker.ViewModels
{
    public class RGBViewModel: ColorModelBase
    {
        private double _red;
        private double _green;
        private double _blue;
        private double _alpha = 255;

        public double Red
        {
            get => _red;
            set
            {
                if (value == _red)
                {
                    return;
                }
                _red = value;
                OnPropertyChanged();
                UpdateColorFromRGB();
            }
        }

        public double Green
        {
            get => _green;
            set
            {
                if (value == _green)
                {
                    return;
                }
                _green = value;
                UpdateColorFromRGB();
                OnPropertyChanged();
                
            }
        }

        public double Blue
        {
            get => _blue;
            set
            {
                if (value == _blue)
                {
                    return;
                }

                _blue = value;
                UpdateColorFromRGB();
                OnPropertyChanged();
            }
        }

        public double Alpha
        {
            get => _alpha;
            set
            {
                if (value == _alpha)
                {
                    return;
                }
                _alpha = value;
                OnPropertyChanged();
                UpdateColorFromRGB();
            }
        }


        public RGBViewModel(ColorViewerModalViewModel mainVm):base(mainVm)
        {
        }

        public override void ReadOurColorFromRGB()
        {
            Color color = mainVm.MySolidColorBrush.Color;
            _red = color.R;
            _green = color.G;
            _blue = color.B;
            _alpha = color.A;
        }

        public override void UpdateColorFromRGB()
        {
            byte A = (byte)_alpha;
            byte R = (byte)_red;
            byte G = (byte)_green;
            byte B = (byte)_blue;
            Color color = Color.FromArgb(A, R, G, B);
            mainVm.MySolidColorBrush = new SolidColorBrush(color);
        }
    }
}
