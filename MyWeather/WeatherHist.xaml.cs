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
using MyWeather.Entities;
using MyWeather.Services;
using Syncfusion.UI.Xaml.Charts;
using Syncfusion.XPS;

namespace MyWeather
{
    public partial class WeatherHist : Page
    {
        List<Readings> readings;

        public WeatherHist()
        {
            InitializeComponent();
            GetReads();
            LoadData(readings);
        }

        public async Task LoadData(List<Readings> reads)
        {
            ChartAdornmentInfo adornmentInfo = new ChartAdornmentInfo()
            {
                ShowMarker = true,
                SymbolStroke = new System.Windows.Media.SolidColorBrush(Colors.Black),
                SymbolInterior = new System.Windows.Media.SolidColorBrush(Colors.DarkGray),
                SymbolHeight = 5,
                SymbolWidth = 5,
                Symbol = ChartSymbol.Ellipse
            };

            var tempSeries = new LineSeries
            {
                ItemsSource = reads.Select(r => new { XValue = r.DateTime, YValue = r.Temperature }).ToList(),
                XBindingPath = "XValue",
                YBindingPath = "YValue",
                EnableAnimation = true,
                ShowTooltip = true,
                AdornmentsInfo = adornmentInfo
            };

            ChartAdornmentInfo adornmentInfo1 = new ChartAdornmentInfo()
            {
                ShowMarker = true,
                SymbolStroke = new System.Windows.Media.SolidColorBrush(Colors.Black),
                SymbolInterior = new System.Windows.Media.SolidColorBrush(Colors.DarkGray),
                SymbolHeight = 5,
                SymbolWidth = 5,
                Symbol = ChartSymbol.Ellipse
            };

            // Prepare data for humidity chart
            var humiditySeries = new LineSeries
            {
                ItemsSource = reads.Select(r => new { XValue = r.DateTime, YValue = r.Humidity }).ToList(),
                XBindingPath = "XValue",
                EnableAnimation = true,
                ShowTooltip = true,
                YBindingPath = "YValue",
                AdornmentsInfo = adornmentInfo1
            };

            ChartAdornmentInfo adornmentInfo2 = new ChartAdornmentInfo()
            {
                ShowMarker = true,
                SymbolStroke = new System.Windows.Media.SolidColorBrush(Colors.Black),
                SymbolInterior = new System.Windows.Media.SolidColorBrush(Colors.DarkGray),
                SymbolHeight = 5,
                SymbolWidth = 5,
                Symbol = ChartSymbol.Ellipse
            };
            // Prepare data for pressure chart
            var pressureSeries = new LineSeries
            {
                ItemsSource = reads.Select(r => new { XValue = r.DateTime, YValue = r.Preasure }).ToList(),
                XBindingPath = "XValue",
                EnableAnimation = true,
                ShowTooltip = true,
                YBindingPath = "YValue",
                AdornmentsInfo = adornmentInfo2
            };

            // Bind the series to the charts
            ClearCharts();
            chtTemp.Series.Add(tempSeries);            
            chtHum.Series.Add(humiditySeries);            
            chtPres.Series.Add(pressureSeries);
        }

        private void dtpFrom_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!String.IsNullOrEmpty(dtpFrom.Text) && !String.IsNullOrEmpty(dtpTo.Text))
            {
                if (Convert.ToDateTime(dtpFrom.Text) > Convert.ToDateTime(dtpTo.Text))
                {
                    MessageBox.Show("From Date Can Not Be After To Date");
                    ClearCharts();
                }

                List<Readings> reads = readings.Where(r => r.DateTime >= Convert.ToDateTime(dtpFrom.Text).AddMinutes(1) && r.DateTime <= Convert.ToDateTime(dtpTo.Text).AddHours(23).AddMinutes(59)).ToList();
                LoadData(reads);
            }
        }

        private async Task GetReads()
        {
            ApiService serv = new ApiService();
            readings = await serv.GetWeatherDataAsync();
        }

        private void ClearCharts()
        {
            chtTemp.Series.Clear();
            chtHum.Series.Clear();
            chtPres.Series.Clear();
        }
    }
}
