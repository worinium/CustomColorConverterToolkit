using ColorPicker.Views;
using ColorPickerLibrary.ViewModels;
using CustomColorPicker.GUI.Helpers;
using Shared;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CustomColorPicker.GUI.ViewModels
{
    public class TaskThreeViewModel : BindableBase
    {
        private LinearGradientBrush myLinearGradientBrush = new LinearGradientBrush();
        TaskThreeWindow windowThreeData;

        IList<Thumb> children { get; set; }

        public TaskThreeViewModel() 
        {
            LoadColorDialogWindowCommand = new RelayCommand(LoadFormData);
            AddSlider = new RelayCommand(AddSliderToCanvas);
            RemoveSlider = new RelayCommand(RemoveSelectedThum_Click); 
        }
        public TaskThreeViewModel(TaskThreeWindow taskThreeWindow) : this()
        {
            windowThreeData = taskThreeWindow;
            
        }
        public void PositionValue_KeyUp()
        {
            if (windowThreeData.Posiion.Value > 0)
            {
                Canvas.SetTop(selectedThumb, ConvertPositionValueToThumbHeightOnCanvas(windowThreeData.Posiion.Value));
            }
        }

        SolidColorBrush mySolidColorBrush;
        private void LoadFormData()
        {
            colorViewmodal = new ColorViewerModal();
            var colorViewModel = colorViewmodal.DataContext as ColorViewerModalViewModel;
            colorViewmodal.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            colorViewModel.MySolidColorBrush = (SolidColorBrush)selectedThumb.Background;
            colorViewmodal.ShowDialog();
            mySolidColorBrush = colorViewModel.MySolidColorBrush;
            var selectedThumbOffset = GetThumbOffset(windowThreeData.canvas.ActualHeight, Canvas.GetTop(selectedThumb));
            var selectedThumbGradientStop = myLinearGradientBrush.GradientStops.FirstOrDefault(x => x.Offset == selectedThumbOffset);
            if (selectedThumbGradientStop != null)
            {
                selectedThumbGradientStop.Color = mySolidColorBrush.Color;
                selectedThumb.Background = mySolidColorBrush;
                ThumbFillAndStrokeColor(selectedThumb, mySolidColorBrush, 1);
            }
        }
        # region Properties
        private Thumb selectedThumb;

        public Thumb SelectedThum
        {
            get { return selectedThumb; }
            set { selectedThumb = value; OnPropertyChanged(); }
        }
        private int transparency = 100;

        public int Transparency
        {
            get { return transparency; }
            set { transparency = value; OnPropertyChanged(); SetTransparencyOfCanvas(); }
        }
        private int brightness = 100;

        public int Brightness
        {
            get { return brightness; }
            set { brightness = value; OnPropertyChanged(); SetBrightness(); }
        }
        private void SetTransparencyOfCanvas()
        {
            double transparency = windowThreeData.tranTXT.Value;
            if (transparency > 0)
            windowThreeData.canvas.Opacity = transparency / 100.0;
        }
        private void SetBrightness()
        {
            if (transparency > 0)
                windowThreeData.canvas.Opacity = windowThreeData.txtslider2.Value/100.0;
        }
        private int rangenumber = 3500;

        public int RangeNumber
        {
            get { return rangenumber; }
            set { rangenumber = value; OnPropertyChanged(); ConvertMinMaxScaleToOnePercent(); }
        }

        private int max = 7000;
        public int Max
        {
            get { return max; }
            set { max = value; OnPropertyChanged(); ConvertMinMaxScaleToOnePercent(); }
           
        }
        private int min = 100;
        public int Min
        {
            get { return min; }
            set { min = value; OnPropertyChanged(); ConvertMinMaxScaleToOnePercent(); }
        }
        private SolidColorBrush generateColorFromPosition;
        public SolidColorBrush GenerateColorFromPosition
        { 
            get { return generateColorFromPosition; }
            set { generateColorFromPosition = value; OnPropertyChanged(); }
        }
        #endregion 
        private void ConvertMinMaxScaleToOnePercent()
        {
            double offset = (((RangeNumber * 100)/ (Max - Min) ))/100.0;
            var color = ColorHelper.GetColorByOffset(myLinearGradientBrush.GradientStops, offset);
            GenerateColorFromPosition = new SolidColorBrush(color);
        }
        public void OnStartUpLoadMe()
        {
            CreateCanvasThumb(Colors.Red,true);
            CreateCanvasThumb(Colors.Purple,false, windowThreeData.canvas.ActualHeight-1);
            myLinearGradientBrush.StartPoint = new Point(0, 0);
            myLinearGradientBrush.EndPoint = new Point(1, 1);
            windowThreeData.canvas.Background = myLinearGradientBrush;
            ConvertMinMaxScaleToOnePercent();
        }
        public void CreateCanvasThumb(Color color, bool active=false, double num = 0)
        {
            Thumb thumb = new Thumb();
            var Data = Geometry.Parse("M 0,0 V10 H30 L35,5 L30,0 Z");
            ControlTemplate thCt = new ControlTemplate();
            var path = new FrameworkElementFactory(typeof(Path));
            path.Name = "NewThumb";
            var brush = new SolidColorBrush(color);
            path.SetValue(Path.FillProperty, brush);
            path.SetValue(Path.StrokeThicknessProperty, 1.0);
            path.SetValue(Path.MinWidthProperty, 22.0);
            path.SetValue(Path.DataProperty, Data);
            thCt.VisualTree = path;
            thumb.Template = thCt;
            thumb.BorderBrush = Brushes.Black;
            thumb.Background = brush;
            thumb.BorderThickness = new Thickness(2);
            thumb.IsEnabled = true;
            thumb.MinWidth = 22.0;
            thumb.MinHeight = 12.0;
            Canvas.SetTop(thumb, num);
            myLinearGradientBrush.GradientStops.Add(new GradientStop(color, GetThumbOffset(windowThreeData.canvas.ActualHeight, num)));
            thumb.DragDelta += Thumb_DragDelta;
            thumb.PreviewMouseLeftButtonDown += Thumb_PreviewMouseLeftButtonDown;
            windowThreeData.canvas.Children.Add(thumb);
            selectedThumb = thumb;
            if (active == true)
            {
                AddOrRemoveThumbFillAndStrokeColor(selectedThumb, Brushes.LightGray, 2);
            }

        }

        private void Thumb_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Thumb thumb = sender as Thumb;
            if(selectedThumb != thumb)
            {
                AddOrRemoveThumbFillAndStrokeColor(selectedThumb,Brushes.Transparent,0);
                SetThumbPositionOnCustomSpinner();
                selectedThumb = thumb;
                AddOrRemoveThumbFillAndStrokeColor(selectedThumb, Brushes.LightGray, 2);
            }
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Thumb thumb = (Thumb)sender;
            var thumbTop = Canvas.GetTop(thumb);
            var canvasheight = windowThreeData.canvas.ActualHeight;
            var number1 = thumbTop + e.VerticalChange;
            var number2 = thumbTop + thumb.ActualHeight + e.VerticalChange;
            if (number1 > 0 && number2 < canvasheight)
            {
                try
                {
                    var newThumbTop = thumbTop + e.VerticalChange;
                    Canvas.SetTop(thumb, newThumbTop);
                    RemoveGraidientColorFromSelectedThumb(thumbTop);
                    var newOffset = GetThumbOffset(windowThreeData.canvas.ActualHeight, newThumbTop);
                    var number = GetThumbNumber(windowThreeData.canvas.ActualHeight, newThumbTop);
                    windowThreeData.Posiion.Value = (int)number;
                    myLinearGradientBrush.GradientStops.Add(new GradientStop(((SolidColorBrush)selectedThumb.Background).Color, newOffset));
                }
                catch (System.Exception) { return; }
            }

        }

        private void RemoveGraidientColorFromSelectedThumb(double thumbTop)
        {
            var oldOffset = GetThumbOffset(windowThreeData.canvas.ActualHeight, thumbTop);
            var oldGradientStop = myLinearGradientBrush.GradientStops.First(x => x.Offset == oldOffset);
            myLinearGradientBrush.GradientStops.Remove(oldGradientStop);
        }

        public double GetThumbPosition(Thumb selectedThumb)
        {
            children = windowThreeData.canvas.Children.OfType<Thumb>().ToList();
            var res = children.IndexOf(selectedThumb);
            var top = Canvas.GetTop(children[res]);
            return top;
        }
        public void ResizeCanvasSize(SizeChangedEventArgs e)
        {
            double canvasNewSizeHeight = e.NewSize.Height;
            double canvasPreviousSizeHeight = e.PreviousSize.Height;
            double slackHeight = 0;
            var children = windowThreeData.canvas.Children.OfType<Thumb>().ToList();
            if (canvasNewSizeHeight < canvasPreviousSizeHeight)
            {
                slackHeight = canvasPreviousSizeHeight - canvasNewSizeHeight;
                for (int i = 0; i < children.Count - 1; i++)
                {
                    Canvas.SetTop(windowThreeData.canvas.Children[i + 1], Canvas.GetTop(windowThreeData.canvas.Children[i + 1]) - slackHeight);
                }
            }
            else if (canvasNewSizeHeight > canvasPreviousSizeHeight)
            {
                slackHeight = canvasNewSizeHeight - canvasPreviousSizeHeight;
                for (int i = 0; i < children.Count - 1; i++)
                {
                    Canvas.SetTop(windowThreeData.canvas.Children[i + 1], Canvas.GetTop(windowThreeData.canvas.Children[i + 1]) + slackHeight);
                }
            }
        }
        public Thumb DetermineSelectedThumb(Thumb thumb_SelectedThumb)
        {
            children = windowThreeData.canvas.Children.OfType<Thumb>().ToList();
            Thumb newThumb = null;
            if(children.Count >= 2) {
                if (children.Count == 2){
                    newThumb = selectedThumb = children[0];
                }
                else {
                    var index = children.IndexOf(thumb_SelectedThumb);
                    newThumb = index == children.Count - 1 ? children[index - 1] : children[index + 1];
                }
                AddOrRemoveThumbFillAndStrokeColor(newThumb, Brushes.LightGray, 1);
            }
            return newThumb;
        }

        private void AddOrRemoveThumbFillAndStrokeColor(Thumb NewSelectedThumb, SolidColorBrush fillColor, int borderThickness)
        {
            var path = NewSelectedThumb.Template.FindName("NewThumb", NewSelectedThumb) as Path;
            if (path != null)
            {
               path.Stroke = fillColor;
                path.StrokeThickness = borderThickness;
            }
        }
        private void ThumbFillAndStrokeColor(Thumb NewSelectedThumb, SolidColorBrush fillColor, int borderThickness)
        {
            var path = NewSelectedThumb.Template.FindName("NewThumb", NewSelectedThumb) as Path;
            if (path != null)
            {
                path.Fill = fillColor;
                path.Stroke = Brushes.LightGray;
                path.StrokeThickness = borderThickness;
            }
        }

        public double GetThumbOffset(double canvasHeight, double thumbPosition)
        {
            var offset = ((thumbPosition / canvasHeight) * 100) /100;
            return offset;
        }
        public double GetThumbNumber(double canvasHeight, double thumbPosition)
        {
            var height = (thumbPosition / canvasHeight) * 100;
            return height;
        }
        public double ConvertPositionValueToThumbHeightOnCanvas(double textboxValue)
        {
            var position = windowThreeData.canvas.ActualHeight;
            var height = (textboxValue * position) / 100.0;
            return height;
        }

       
        private double GetPositionOfNewThumb()
        {
            double firstThumb = GetThumbPosition(selectedThumb);
            double nextThumb = GetNextThumbPosition(selectedThumb);
            double newThumbPosition = (firstThumb + nextThumb) / 2.0;
            return newThumbPosition;
        }

        private void AddSliderToCanvas()
        {
            double position = GetPositionOfNewThumb();
            double offset = GetThumbOffset(windowThreeData.canvas.ActualHeight, position);
            var color = ColorHelper.GetColorByOffset(myLinearGradientBrush.GradientStops, offset);
            CreateCanvasThumb(color,true, position);
            windowThreeData.canvas.Background = myLinearGradientBrush;
            windowThreeData.colorBtn.IsEnabled = true; ConvertMinMaxScaleToOnePercent();
        }

        private void SetThumbPositionOnCustomSpinner()
        {
            var newThumbTop = Canvas.GetTop(selectedThumb);
            var number = GetThumbNumber(windowThreeData.canvas.ActualHeight, newThumbTop);
            windowThreeData.Posiion.Value = (int)number;
        }
        private void RemoveSelectedThum_Click()
        {
            Thumb demo = null;
            if (windowThreeData.canvas.Children.Count == 2)
                return;
            else {
                demo = DetermineSelectedThumb(selectedThumb);
                var selectedThumbTopValue = Canvas.GetTop(selectedThumb);
                RemoveGraidientColorFromSelectedThumb(selectedThumbTopValue); 
                windowThreeData.canvas.Children.Remove(selectedThumb); ConvertMinMaxScaleToOnePercent();
            }
            selectedThumb = demo;
        }
       
        private static void Canvas_SizeChanged(Canvas MyCanvas, SizeChangedEventArgs e)
        {
            double canvasNewSizeHeight = e.NewSize.Height;
            double canvasPreviousSizeHeight = e.PreviousSize.Height;
            double slackHeight = 0;
            var children = MyCanvas.Children.OfType<Thumb>().ToList();
            if (canvasNewSizeHeight < canvasPreviousSizeHeight)
            {
                slackHeight = canvasPreviousSizeHeight - canvasNewSizeHeight;
                for (int i = 0; i < children.Count - 1; i++)
                {
                    Canvas.SetTop(MyCanvas.Children[i + 1], Canvas.GetTop(MyCanvas.Children[i + 1]) - slackHeight);
                }
            }
            else if (canvasNewSizeHeight > canvasPreviousSizeHeight)
            {
                slackHeight = canvasNewSizeHeight - canvasPreviousSizeHeight;
                for (int i = 0; i < children.Count - 1; i++)
                {
                    Canvas.SetTop(MyCanvas.Children[i + 1], Canvas.GetTop(MyCanvas.Children[i + 1]) + slackHeight);
                }
            }
        }
        
        public double GetNextThumbPosition(Thumb selectedThumb)
        {
            var data = new List<double>();
            var canvasChildren = windowThreeData.canvas.Children.OfType<Thumb>().ToList();
            double result = 0;
            for (int i = 0; i < canvasChildren.Count; i++)
            {
                data.Add(Canvas.GetTop(canvasChildren[i]));
            }
            data.Sort();
            for (int i = 0; i < data.Count; i++)
            {
                if (Canvas.GetTop(selectedThumb) == data[i])
                {
                    result = i == (data.Count - 1) ? data[i - 1] : data[i + 1];
                }
            }
            return result;

        }
        ColorViewerModal colorViewmodal = null;
        public ICommand LoadColorDialogWindowCommand { get; set; }
        public ICommand AddSlider { get; set; }
        public ICommand RemoveSlider { get; set; }
    }
}
