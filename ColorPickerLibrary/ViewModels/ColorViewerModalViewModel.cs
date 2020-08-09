using ColorPicker.ViewModels;
using ColorPickerLibrary.ViewModels;
using ColorPickerLibrary.Views;
using Shared;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ColorPickerLibrary.ViewModels
{
    public class ColorViewerModalViewModel : BindableBase
    {
        public ICommand ApplyCommand { get; set; }
        public ICommand ColorDialog { get; set; }
        //Setting Color from Settings File
        static SolidColorBrush colorBrush = Brushes.AliceBlue;
        public ColorViewerModalViewModel() 
        {
            PopulateComboBox();
            CurrentView = new CMYK() { DataContext = new CMYKViewModel(this) };
        }
        
        private SolidColorBrush mysolidColorBrush = colorBrush;
        public SolidColorBrush MySolidColorBrush
        {
            get { return mysolidColorBrush; }
            set { mysolidColorBrush = value; OnPropertyChanged(); }
        }
        private string selectedModel = "CMYK";
        public string SelectedModel
        {
            get => selectedModel;
            set
            {
                selectedModel = value;
                OnPropertyChanged();
                DynamicallyLoadViews();
               // SetBackground(mysolidColorBrush);
            }
        }
       
        private UIElement _currentView;
        public UIElement CurrentView
        {
            get => _currentView;
            set { SetProperty(ref _currentView, value); }
        }

        private ObservableCollection<string> colorScale;
        public ObservableCollection<string> ColorScale
        {
            get { return colorScale; }
            set { colorScale = value; }
        }

        private void PopulateComboBox()
        {
            colorScale = new ObservableCollection<string>();
            colorScale.Add("CMYK");
            colorScale.Add("HSV");
            colorScale.Add("RGBA");
            colorScale.Add("Gray Scale");
        }
        private void DynamicallyLoadViews()
        {
            if (selectedModel == "RGBA")
            {
                CurrentView = new RGBA()
                {
                    DataContext = new RGBViewModel(this)
                };
            }
            else if (selectedModel == "CMYK")
            {
                CurrentView = new CMYK()
                {
                    DataContext = new CMYKViewModel(this)
                };
            }
            else if (selectedModel == "Gray Scale")
            {
                CurrentView = new GrayScale()
                {
                    DataContext = new GrayScaleViewModel(this)
                };

            }
            else if (selectedModel == "HSV")
            {
                CurrentView = new HSV()
                {
                    DataContext = new HSLViewModel(this)
                };
            }

        }

    }
}
