using Microsoft.Extensions.Logging;
using MyWeather.Entities;
using Newtonsoft.Json;
using Syncfusion.Windows.Controls.RichTextBoxAdv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyWeather.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Readings>> GetWeatherDataAsync()
        {
            var response = await _httpClient.GetAsync("http://192.168.0.3:5000/GetWeatherInfo");
            response.EnsureSuccessStatusCode();
            string responseData = await response.Content.ReadAsStringAsync();

            List<Readings> readings = JsonConvert.DeserializeObject<List<Readings>>(responseData);
            return readings;

        }

        public async Task<List<LogEvents>> GetEventsAsync()
        {
            var response = await _httpClient.GetAsync("http://192.168.0.3:5000/read_events");
            response.EnsureSuccessStatusCode();
            string responseData = await response.Content.ReadAsStringAsync();

            List<LogEvents> events = JsonConvert.DeserializeObject<List<LogEvents>>(responseData);
            return events;
        }
    }
}
