using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ColorMine.ColorSpaces;
using ColorPickerLibrary.ViewModels;

namespace ColorPicker.ViewModels
{
    public class GrayScaleViewModel : ColorModelBase
    {
        public GrayScaleViewModel(ColorViewerModalViewModel viewModel):base(viewModel)
        {

        }
        private byte _red;
        private byte _green;
        private byte _blue;
        private byte _alpha = 255;

        public byte Red
        {
            get => _red;
            set
            {
                _red = value;
                OnPropertyChanged();
                UpdateColorFromRGB();
            }
        }

        public byte Green
        {
            get => _green;
            set
            {
                _green = value;
                OnPropertyChanged();
                UpdateColorFromRGB();
            }
        }

        public byte Blue
        {
            get => _blue;
            set
            {
                _blue = value;
                OnPropertyChanged();
                UpdateColorFromRGB();
            }
        }

        public byte Alpha
        {
            get => _alpha;
            set
            {
                _alpha = value;
                OnPropertyChanged();
                UpdateColorFromRGB();
            }
        }
        private byte black;

        public byte Black
        {
            get { return black; }
            set { black = value; OnPropertyChanged();UpdateColorFromRGB(); }
        }


        public override void ReadOurColorFromRGB()
        {
            var selectedColor = mainVm.MySolidColorBrush;
            byte r = selectedColor.Color.R;
            byte g = selectedColor.Color.G;
            byte b = selectedColor.Color.B;
            byte a = selectedColor.Color.A;
            Red = (byte)(0.33 * r);
            Green = (byte)(0.33 * g);
            Blue = (byte)(0.33 * b);
            Black = (byte)(Red + Green + Blue);
           
        }

        public override void UpdateColorFromRGB()
        {
             
            byte R = (byte)Red;
            byte G = (byte)Green;
            byte B = (byte)Blue;

            mainVm.MySolidColorBrush = new SolidColorBrush(Color.FromRgb(R, G, B));
        }
    }
}
