using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyWeather
{
    public partial class MainWindow : Window
    {
        public ICommand Weath { get; set; }
        public ICommand LogList { get; set; }
        public ICommand WeathHist { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            //NotificationService.Instance.Initialize(this);
            //Instance = this;

            Weath = new RelayCommand(Weather);
            LogList = new RelayCommand(LogEvt);
            WeathHist = new RelayCommand(Hist);
            this.DataContext = this;
        }

        private void Weather()
        {
            cntFrame.Navigate(new CurrWeather());
        }

        private void LogEvt()
        {
            cntFrame.Navigate(new Logs());
        }

        private void Hist()
        {
            cntFrame.Navigate(new WeatherHist());
        }

        public class RelayCommand : ICommand
        {
            private readonly Action _execute;
            public event EventHandler CanExecuteChanged;

            public RelayCommand(Action execute)
            {
                _execute = execute;
            }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                _execute?.Invoke();
            }
        }
    }
}