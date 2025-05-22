using MyWeather.Entities;
using MyWeather.Services;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyWeather
{
    /// <summary>
    /// Interaction logic for Logs.xaml
    /// </summary>
    public partial class Logs : Page
    {
        List<LogEvents> logs;
        public Logs()
        {
            InitializeComponent();
            LoadLogs();
        }

        public async Task LoadLogs()
        {
            ApiService serv = new ApiService();
            logs = await serv.GetEventsAsync();

            grdLogs.ItemsSource = logs.OrderByDescending(r => r.DateTime);
        }
    }
}
