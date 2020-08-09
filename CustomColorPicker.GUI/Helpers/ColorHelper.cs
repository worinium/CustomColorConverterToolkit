using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ColorPicker.ViewModels;

namespace CustomColorPicker.GUI
{
    public class ColorHelper
    {
        public static Color GetColorByOffset(GradientStopCollection collection, double offset)
        {
            GradientStop[] stops = collection.OrderBy(x => x.Offset).ToArray();
            if (offset <= 0) return stops[0].Color;
            if (offset >= 1) return stops[stops.Length - 1].Color;
            GradientStop left = stops[0], right = null;
            foreach (GradientStop stop in stops)
            {
                if (stop.Offset >= offset)
                {
                    right = stop;
                    break;
                }
                left = stop;
            }
            offset = Math.Round((offset - left.Offset) / (right.Offset - left.Offset), 2);
            byte a = (byte)((right.Color.A - left.Color.A) * offset + left.Color.A);
            byte r = (byte)((right.Color.R - left.Color.R) * offset + left.Color.R);
            byte g = (byte)((right.Color.G - left.Color.G) * offset + left.Color.G);
            byte b = (byte)((right.Color.B - left.Color.B) * offset + left.Color.B);
            return Color.FromArgb(a, r, g, b);
        }
        //private void UpdateSelectedThumb()
        //{
        //    selectedThumb = null;
        //    var children = windowThreeData.canvas.Children.OfType<Thumb>().ToList();
        //    if (children.Count == 2)
        //    {
        //        selectedThumb = children[0];
        //    }
        //    var thumbTop = Canvas.GetTop(selectedThumb);
        //    var newOffset = GetThumbOffset(windowThreeData.canvas.ActualHeight, thumbTop);
        //    var path = selectedThumb.Template.FindName("NewThumb", selectedThumb) as Path;
        //    if (path != null)
        //    {
        //        var color = ColorHelper.GetColorByOffset(myLinearGradientBrush.GradientStops, newOffset);
        //        path.Fill = new SolidColorBrush(color);
        //        path.Stroke = Brushes.Gainsboro;
        //        path.StrokeThickness = 2;
        //    }
        //}
    }
}
