using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ColorPicker.ViewModels;
using ColorPicker.Views;

namespace CustomColorPicker.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

           // // Add choices to combo box
           // skinComboBox.Items.Add("AliceBlue");
           // skinComboBox.Items.Add("Yellow");
           //// skinComboBox.Items.Add("CustomLoad");
           // skinComboBox.SelectedIndex = 0;

           // // Set initial skin
           // Application.Current.Resources = (ResourceDictionary)Application.Current.Properties["AliceBlue"];

           // // Detect when skin changes
           // skinComboBox.SelectionChanged += skinComboBox_SelectionChanged;
        }


        ////private void skinComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        ////{
        ////    // Change the skin
        ////    var selectedValue = (string)e.AddedItems[0];
        ////    Application.Current.Resources = (ResourceDictionary)Application.Current.Properties[selectedValue];
        ////}

        private void Task2_Click(object sender, RoutedEventArgs e)
        {
            
            TaskTwoWindow window = new TaskTwoWindow();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();
        }

        private void Task3_Click(object sender, RoutedEventArgs e)
        {
            TaskThreeWindow window = new TaskThreeWindow();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();
        }

        private void Task4_Click(object sender, RoutedEventArgs e)
        {
            TaskFourWindow window = new TaskFourWindow();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();
        }

        private void Task1_Click(object sender, RoutedEventArgs e)
        {
            ColorViewerModal window = new ColorViewerModal();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ApplyBtn.Visibility = Visibility.Hidden;
            window.ShowDialog();
        }
    }
}
