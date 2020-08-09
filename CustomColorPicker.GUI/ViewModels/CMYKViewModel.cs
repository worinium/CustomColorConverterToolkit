using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ColorMine.ColorSpaces;

namespace ColorPicker.ViewModels
{
    public class CMYKViewModel : ColorModelBase
    {
        private double cyan;

        public double Cyan
        {
            get { return cyan; }
            set { cyan = value; OnPropertyChanged(); UpdateColorFromRGB(); }
        }
        private double magenta;

        public double Magenta
        {
            get { return magenta; }
            set { magenta = value; OnPropertyChanged(); UpdateColorFromRGB(); }
        }
        private double yellow;

        public double Yellow
        {
            get { return yellow; }
            set { yellow = value; OnPropertyChanged(); UpdateColorFromRGB(); }
        }
        private double black;

        public double Black
        {
            get { return black; }
            set { black = value; OnPropertyChanged(); UpdateColorFromRGB(); }
        }
        public override void ReadOurColorFromRGB()
        {
            Color color = mainVm.MySolidColorBrush.Color;
            byte _red = color.R;
            byte _green = color.G;
            byte _blue = color.B;
            byte _alpha = color.A;
            //var rgb = new Rgb { R = _red, G = _green, B = _blue };
            //var cmyk = rgb.To<Cmyk>();
            //black = cmyk.K * 100;
            //cyan = cmyk.C * 100;
            //magenta = cmyk.M * 100;
            //yellow = cmyk.Y * 100;

            double Rc = _red / 255.0;
            double Gc = _green / 255.0;
            double Bc = _blue / 255.0;
            int max = 100;
            double b = (double)(1 - Math.Max(Math.Max(Rc, Gc), Bc)) * 100;
            if (b > max)
                Black = max;
            else
                Black = b;
            double c = (double)((1 - Rc - Black) / (1 - Black)) * 100;
            if (c > max)
                Cyan = max;
            else
                Cyan = c;
            double m = (double)((1 - Gc - Black) / (1 - black)) * 100;
            if (m > max)
                Magenta = max;
            else
                Magenta = m;
            double y = (double)((1 - Bc - Black) / (1 - Black)) * 100;
            if (y > max)
                Yellow = max;
            else
                Yellow = y;
        }

        public override void UpdateColorFromRGB()
        {
            //byte R = (byte)((255 * (1 - Cyan) * (1 - Black)) / 100);
            //byte G = (byte)((255 * (1 - Magenta) * (1 - Black)) / 100);
            //byte B = (byte)((255 * (1 - Yellow) * (1 - Black)) / 100);

            byte R = (byte)((255 * (1 - Cyan/100) * (1 - Black/100)));
            byte G = (byte)((255 * (1 - Magenta / 100) * (1 - Black / 100)));
            byte B = (byte)((255 * (1 - Yellow / 100) * (1 - Black / 100)));

            //double Red = 255 × (1 - Cyan ÷ 100 ) × (1 - Black ÷ 100 )
            //Green = 255 × (1 - Magenta ÷ 100 ) × (1 - Black ÷ 100 )
            //Blue = 255 × (1 - Yellow ÷ 100 ) × (1 - Black ÷ 100 )
            //var cmyk = new Cmyk { C = cyan, M = magenta, Y = yellow, K = black };
            //var rgb = cmyk.To<Rgb>();
            //byte R = (byte)rgb.R;
            //byte G = (byte)rgb.G;
            //byte B = (byte)rgb.B;

            mainVm.MySolidColorBrush = new SolidColorBrush(Color.FromRgb(R, G, B));
            
        }
        public CMYKViewModel(ColorViewerModalViewModel mainVM)
            : base(mainVM)
        {
        }
    }
}
