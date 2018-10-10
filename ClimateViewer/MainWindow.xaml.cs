using ClimateViewer.Handlers;
using LiveCharts;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ClimateViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        string apikey;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dp_datestampfrom.SelectedDate = DateTime.Now;
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            string JSONapikey = HttpApiRequest.ClimateLogin(tb_Usermail.Text, pb_userpassword.Password);           
            if (string.IsNullOrEmpty(JSONapikey)) { MessageBox.Show("Wrong username or password"); }
            else
            {
                apikey = JsonDataConverter.deserializedApikey(JSONapikey);
                btn_userinfo.IsEnabled = true;
            }
        }

        List<Userunits> units = new List<Userunits>();
        private void btn_userinfo_Click(object sender, RoutedEventArgs e)
        {
            string JSONunits = HttpApiRequest.Userunits(apikey, tb_Usermail.Text, pb_userpassword.Password);
            units = JsonDataConverter.deserializedUnits(JSONunits);

            foreach (var unit in units)
            {
                cb_UnitID.Items.Add(unit.name);
            }

            btn_showdata.IsEnabled = true;
            dp_datestampfrom.IsEnabled = true;
            cb_UnitID.IsEnabled = true;
            cb_CompressionLVL.IsEnabled = true;
        }

        private void btn_Showdata_Click(object sender, RoutedEventArgs e)
        {
            PopulateCharts();
        }

        private void PopulateCharts()
        {
            DataContext = null;

            DateTime date = dp_datestampfrom.SelectedDate.Value;

            string clvl = "1";
            switch (cb_CompressionLVL.SelectedIndex)
            {
                case 0:
                    clvl = "1";
                    break;
                case 1:
                    clvl = "2";
                    break;
                case 2:
                    clvl = "3";
                    break;
                case 3:
                    clvl = "4";
                    break;
                default:
                    break;
            }

            string unitID = null;
            foreach (var unit in units)
            {
                if (unit.name == cb_UnitID.Text) { unitID = unit.id; }
            }
            
            string climatestringdata = HttpApiRequest.GetClimateData(apikey, tb_Usermail.Text, unitID, date.ToString("yyyy.MM.dd"), clvl);
            List<unitData> ClimateDataList = JsonDataConverter.deserializedClimateData(climatestringdata);

            string format = "HH:mm";

            int x = ClimateDataList.Count();
            TimeLabel = new string[x];
            List<double> TemperaturValues = new List<double>();
            List<double> HeatIndexValues = new List<double>();
            List<double> HumidityValues = new List<double>();

            for (int i = 0; i < x; i++)
            {
                TimeLabel[i] = UnixStampConvert.UnixTimeToDateTime(ClimateDataList[i].datestamp).ToString(format);
                TemperaturValues.Add(Math.Round(ClimateDataList[i].climatedata.temperature, 2));
                HeatIndexValues.Add(Math.Round(ClimateDataList[i].climatedata.heatindex, 2));
                HumidityValues.Add(Math.Round(ClimateDataList[i].climatedata.humidity, 2));
            }

            var TempLineColor = Colors.Red;
            var HeatLineColor = Colors.Yellow;
            var HumiLineColor = Colors.Blue;

            SolidColorBrush TempLineColorfil = new SolidColorBrush();
            TempLineColorfil.Color = TempLineColor;
            TempLineColorfil.Opacity = 0.2;

            SolidColorBrush HeatLineColorfil = new SolidColorBrush();
            HeatLineColorfil.Color = HeatLineColor;
            HeatLineColorfil.Opacity = 0.2;

            SolidColorBrush HumiLineColorfil = new SolidColorBrush();
            HumiLineColorfil.Color = HumiLineColor;
            HumiLineColorfil.Opacity = 0.2;

            TempSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Temperatur",
                    Values = TemperaturValues.AsChartValues(),
                    LineSmoothness = 1,
                    PointGeometrySize = 0,
                    Stroke = Brushes.Red,
                    Fill = TempLineColorfil
                },
                new LineSeries
                {
                    Title = "HeatIndex",
                    Values = HeatIndexValues.AsChartValues(),
                    LineSmoothness = 1,
                    PointGeometrySize = 0,
                    Stroke = Brushes.Yellow,
                    Fill = HeatLineColorfil
                }
            };

            HumiSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Humidity",
                    Values = HumidityValues.AsChartValues(),
                    LineSmoothness = 1,
                    PointGeometrySize = 0,
                    Stroke = Brushes.Blue,
                    Fill = HumiLineColorfil
                }
            };

            DataContext = this;
        }

        public string[] TimeLabel { get; set; }
        public SeriesCollection TempSeries { get; set; }
        public SeriesCollection HumiSeries { get; set; }
    }
}
