using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using ColorPicker.ViewModels;
using ColorPickerLibrary.ViewModels;

namespace ColorPicker.Views
{
    /// <summary>
    /// Interaction logic for ColorViewerModal.xaml
    /// </summary>
    public partial class ColorViewerModal : Window
    {
        public ColorViewerModal()
        {
            InitializeComponent();
            DataContext = new ColorViewerModalViewModel();
        }
        private void Close_Command(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseButtonRectangle_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OK_Command(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
