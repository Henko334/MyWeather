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

namespace MyWeather
{
    public partial class CurrWeather : Page
    {
        List<Readings> readings;

        public CurrWeather()
        {
            InitializeComponent();
            LoadData();
        }

        public async Task LoadData()
        {
            ApiService serv = new ApiService();
            readings = await serv.GetWeatherDataAsync();

            var todayStart = DateTime.Today.AddMinutes(1).AddDays(-1);
            List<Readings> dayReads = readings.Where(r => r.DateTime >= todayStart).ToList();

            ChartAdornmentInfo adornmentInfo = new ChartAdornmentInfo()
            {
                ShowMarker = true,
                SymbolStroke = new SolidColorBrush(Colors.Black),
                SymbolInterior = new SolidColorBrush(Colors.DarkGray),
                SymbolHeight = 5,
                SymbolWidth = 5,
                Symbol = ChartSymbol.Ellipse
            };

            var tempSeries = new LineSeries
            {
                ItemsSource = dayReads.Select(r => new { XValue = r.DateTime, YValue = r.Temperature }).ToList(),
                XBindingPath = "XValue",
                YBindingPath = "YValue",
                EnableAnimation = true,
                ShowTooltip = true,
                AdornmentsInfo = adornmentInfo,
                TooltipTemplate = (DataTemplate)Resources["TooltipTemplate"] // Assign the tooltip template
            };

            ChartAdornmentInfo adornmentInfo1 = new ChartAdornmentInfo()
            {
                ShowMarker = true,
                SymbolStroke = new SolidColorBrush(Colors.Black),
                SymbolInterior = new SolidColorBrush(Colors.DarkGray),
                SymbolHeight = 5,
                SymbolWidth = 5,
                Symbol = ChartSymbol.Ellipse
            };

            // Prepare data for humidity chart
            var humiditySeries = new LineSeries
            {
                ItemsSource = dayReads.Select(r => new { XValue = r.DateTime, YValue = r.Humidity }).ToList(),
                XBindingPath = "XValue",
                EnableAnimation = true,
                ShowTooltip = true,
                YBindingPath = "YValue",
                AdornmentsInfo = adornmentInfo1,
                TooltipTemplate = (DataTemplate)Resources["TooltipTemplate"] // Assign the tooltip template
            };

            ChartAdornmentInfo adornmentInfo2 = new ChartAdornmentInfo()
            {
                ShowMarker = true,
                SymbolStroke = new SolidColorBrush(Colors.Black),
                SymbolInterior = new SolidColorBrush(Colors.DarkGray),
                SymbolHeight = 5,
                SymbolWidth = 5,
                Symbol = ChartSymbol.Ellipse
            };
            // Prepare data for pressure chart
            var pressureSeries = new LineSeries
            {
                ItemsSource = dayReads.Select(r => new { XValue = r.DateTime, YValue = r.Preasure }).ToList(),
                XBindingPath = "XValue",
                EnableAnimation = true,
                ShowTooltip = true,
                YBindingPath = "YValue",
                AdornmentsInfo = adornmentInfo2,
                TooltipTemplate = (DataTemplate)Resources["TooltipTemplate"] // Assign the tooltip template
            };

            // Bind the series to the charts
            chtTemp.Series.Clear();
            chtTemp.Series.Add(tempSeries);

            chtHum.Series.Clear();
            chtHum.Series.Add(humiditySeries);

            chtPres.Series.Clear();
            chtPres.Series.Add(pressureSeries);
        }
    }
}
