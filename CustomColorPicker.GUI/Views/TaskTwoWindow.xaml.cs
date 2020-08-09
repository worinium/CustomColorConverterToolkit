using ColorPicker.ViewModels;
using CustomColorPicker.GUI.ViewModels;
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
using System.Windows.Shapes;

namespace ColorPicker.Views
{
    /// <summary>
    /// Interaction logic for TaskTwoWindow.xaml
    /// </summary>
    public partial class TaskTwoWindow : Window
    {
        private TaskTwoViewModel viewModel = null;

        public TaskTwoWindow()
        {
            InitializeComponent();
            viewModel = new TaskTwoViewModel();
            DataContext = viewModel;
        }

       
    }
}
