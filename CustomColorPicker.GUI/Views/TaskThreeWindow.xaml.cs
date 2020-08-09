using ColorPicker.ViewModels;
using CustomColorPicker.GUI.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ColorPicker.Views
{
  
    public partial class TaskThreeWindow : Window
    {
        TaskThreeViewModel viewModel = null;
        public TaskThreeWindow()
        {
            viewModel = new TaskThreeViewModel(this);
            InitializeComponent();
            DataContext = viewModel;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            canvas.SizeChanged += Canvas_SizeChanged;
            viewModel.OnStartUpLoadMe();
        }
        private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            viewModel.ResizeCanvasSize(e);
        }
        private void Posiion_KeyUp(object sender, KeyEventArgs e)
        {
            viewModel.PositionValue_KeyUp();
        }
    } 
}
