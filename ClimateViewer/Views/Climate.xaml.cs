using ClimateViewer.Handlers;
using ClimateViewer.Views;
using LiveCharts;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace ClimateViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Climate : Window
    {
        public Climate() { InitializeComponent(); }

        List<Userunits> units = new List<Userunits>();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dp_datestampfrom.SelectedDate = DateTime.Now;

            populateUnitBox();
            cb_CompressionLVL.SelectedIndex = 0;
        }

        private List<Userunits> GetUnits(bool FilterNull)
        {
            string JSONunits = HttpApiRequest.Userunits(UserInformation.ApiKey, UserInformation.Mail, UserInformation.Password);
            return JsonDataConverter.deserializedUnits(JSONunits, FilterNull);
        }

        private void populateUnitBox()
        {
            units = GetUnits(true);
            if (cb_UnitID.Items != null) { cb_UnitID.Items.Clear(); }
            foreach (var unit in units) { cb_UnitID.Items.Add(unit.name); }

            cb_UnitID.SelectedIndex = 0;
        }

        private void Menu_exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Menu_Getapikey_Click(object sender, RoutedEventArgs e)
        {
            PrivatAPIkey key = new PrivatAPIkey();
            key.Show();
        }

        private void Menu_privatunits_Click(object sender, RoutedEventArgs e)
        {
            PrivatUnits punit = new PrivatUnits(GetUnits(false));
            if (punit.ShowDialog() == true) { populateUnitBox(); }
        }

        private void Menu_changepass_Click(object sender, RoutedEventArgs e)
        {
            NewPassword np = new NewPassword();
            np.Show();
        }

        private void btn_Showdata_Click(object sender, RoutedEventArgs e) { PopulateCharts(); }

        private void PopulateCharts()
        {
            DataContext = null;
            DateTime date = dp_datestampfrom.SelectedDate.Value;

            string clvl = null;
            switch (cb_CompressionLVL.SelectedIndex)
            {
                case 0: clvl = "1"; break;
                case 1: clvl = "2"; break;
                case 2: clvl = "3"; break;
                case 3: clvl = "4"; break;
                default: break;
            }

            string unitID = null;
            foreach (var unit in units)
            {
                if (unit.name == cb_UnitID.Text) { unitID = unit.id; }
            }

            string climatestringdata = HttpApiRequest.GetClimateData(UserInformation.ApiKey, UserInformation.Mail, unitID, date.ToString("yyyy.MM.dd"), clvl);
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
