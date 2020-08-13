using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Shapes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using Shared;

namespace CustomColorPicker.GUI.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        //private Thumb SelectedThumb = null;

        public MainWindowViewModel()
        {

        }
        public ICommand OpenCommand1 { get; set; }
        public ICommand OpenCommand2 { get; set; }
        public ICommand AddSlider { get; set; }
        public ICommand RemoveSlider { get; set; }
        public ICommand ColorDialog { get; set; }

    }
}