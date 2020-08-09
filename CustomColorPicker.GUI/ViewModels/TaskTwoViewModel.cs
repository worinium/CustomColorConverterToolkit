using ColorPicker.Views;
using ColorPickerLibrary.ViewModels;
using CustomColorPicker.GUI.Helpers;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace CustomColorPicker.GUI.ViewModels
{
    public class TaskTwoViewModel: BindableBase
    {
        ColorViewerModal colorViewmodal = null;
        static System.Drawing.Color color = Properties.Settings.Default.SettingColor;
     
        public TaskTwoViewModel()
        {
            OpenCommand1 = new RelayCommand(LoadFirstGradientColor);
            OpenCommand2 = new RelayCommand(LoadSecondGradientColor);
        }
        private void LoadFirstGradientColor()
        {
            colorViewmodal = new ColorViewerModal();
            colorViewmodal.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            var colorViewModel = colorViewmodal.DataContext as ColorViewerModalViewModel;
            colorViewmodal.ShowDialog();
            SolidColorBrush myFirstGradientSolidColorBrush = colorViewModel.MySolidColorBrush;
            MyFirstColor = myFirstGradientSolidColorBrush.Color;
        }
        private void LoadSecondGradientColor()
        {
            colorViewmodal = new ColorViewerModal();
            colorViewmodal.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            var colorViewModel = colorViewmodal.DataContext as ColorViewerModalViewModel;
            colorViewmodal.ShowDialog();
            SolidColorBrush mySecondGradientSolidColorBrush = colorViewModel.MySolidColorBrush;
            MySecondColor = mySecondGradientSolidColorBrush.Color;
        }

        private Color myFirstColor = Colors.Red;/*= Color.FromArgb(color.A, color.R, color.G, color.B)*/

        public Color MyFirstColor
        {
            get { return myFirstColor; }
            set { myFirstColor = value; OnPropertyChanged(); }
        }
        private Color mySecondColor = Colors.BlueViolet;/*= Color.FromRgb(222, 22, 33)*/
        public Color MySecondColor
        {
            get { return mySecondColor; }
            set { mySecondColor = value; OnPropertyChanged(); }
        }

        public ICommand OpenCommand1 { get; set; }
        public ICommand OpenCommand2 { get; set; }
       
    }
}
