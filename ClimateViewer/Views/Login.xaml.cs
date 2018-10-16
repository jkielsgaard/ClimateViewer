using ClimateViewer.Handlers;
using LiveCharts;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ClimateViewer.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// Only function used by event handlers is described because that events should be self-explanatory
    /// </summary>
    public partial class Login : Window
    {
        public Login() { InitializeComponent(); }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AnimatedLogo();
            tb_mail.Focus();
        }

        #region Keypress events
        private void OnKeyDownHandler(object sender, KeyEventArgs e) { if (e.Key == Key.Return) { login(); } }
        #endregion

        #region Buttons
        private void btn_login_Click(object sender, RoutedEventArgs e) { login(); }

        private void lb_showPassword_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (pb_password.Visibility == Visibility.Visible)
            {
                pb_password.Visibility = Visibility.Hidden;
                tb_password.Visibility = Visibility.Visible;
                tb_password.Text = pb_password.Password;
            }
            else if (pb_password.Visibility == Visibility.Hidden)
            {
                pb_password.Visibility = Visibility.Visible;
                tb_password.Visibility = Visibility.Hidden;
                pb_password.Password = tb_password.Text;
            }
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e) { Application.Current.Shutdown(); }
        #endregion

        #region Functions
        /// <summary>
        /// Function for login fase used by Enterkey event and Login butten event
        /// </summary>
        private void login()
        {
            if (string.IsNullOrEmpty(tb_mail.Text) || string.IsNullOrEmpty(pb_password.Password) || string.IsNullOrEmpty(tb_mail.Text) && string.IsNullOrEmpty(pb_password.Password))
            {
                MessageBox.Show("Please fill out username and password");
            }
            else
            {
                if (tb_password.Visibility == Visibility.Visible) { pb_password.Password = tb_password.Text; }

                string JSONapikey = HttpApiRequest.ClimateLogin(tb_mail.Text, pb_password.Password);
                if (string.IsNullOrEmpty(JSONapikey)) { MessageBox.Show("Wrong username or password"); }
                else
                {
                    UserInformation.ApiKey = JsonDataConverter.deserializedApikey(JSONapikey);
                    UserInformation.Mail = tb_mail.Text;
                    UserInformation.Password = pb_password.Password;

                    Climate c = new Climate();
                    c.Show();
                    Close();
                }
            }
        }

        /// <summary>
        /// AnimatedLogo function is just a little gimmick, it has no value for the application, it just make a neww "logo" in the login screen every time the application is started
        /// </summary>
        private void AnimatedLogo()
        {
            var TempLineColor = Colors.Red;
            var HumiLineColor = Colors.Blue;

            SolidColorBrush TempLineColorfil = new SolidColorBrush();
            TempLineColorfil.Color = TempLineColor;
            TempLineColorfil.Opacity = 0.2;

            SolidColorBrush HumiLineColorfil = new SolidColorBrush();
            HumiLineColorfil.Color = HumiLineColor;
            HumiLineColorfil.Opacity = 0.2;

            List<double> TempValues = new List<double>();
            List<double> HumiValues = new List<double>();

            Random r = new Random();
            for (int i = 0; i < 6; i++)
            {
                TempValues.Add(r.Next(2, 10));
                HumiValues.Add(r.Next(2, 10));
            }

            SeriesCollection TempSeries = new SeriesCollection {
                new LineSeries
                {
                    Title = "Temperatur",
                    Values = TempValues.AsChartValues(),
                    LineSmoothness = 1,
                    PointGeometrySize = 0,
                    Stroke = Brushes.Red,
                    Fill = TempLineColorfil
                }
            };
            SeriesCollection HumiSeries = new SeriesCollection {
                new LineSeries
                {
                    Title = "Humidity",
                    Values = HumiValues.AsChartValues(),
                    LineSmoothness = 1,
                    PointGeometrySize = 0,
                    Stroke = Brushes.Blue,
                    Fill = HumiLineColorfil
                }
            };

            lc_Logo01.Series = TempSeries;
            lc_Logo02.Series = HumiSeries;
        }
        #endregion

    }
}
