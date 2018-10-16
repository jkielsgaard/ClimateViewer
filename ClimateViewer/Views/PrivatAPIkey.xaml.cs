using ClimateViewer.Handlers;
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
using System.Windows.Shapes;

namespace ClimateViewer.Views
{
    /// <summary>
    /// Interaction logic for PrivatAPIkey.xaml
    /// Only function used by event handlers is described because that events should be self-explanatory
    /// </summary>
    public partial class PrivatAPIkey : Window
    {
        public PrivatAPIkey() { InitializeComponent(); }

        private void Window_Loaded(object sender, RoutedEventArgs e) { lb_apikey.Content = "Privat API key: " + UserInformation.ApiKey; }

        #region Buttons
        private void btn_copy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(UserInformation.ApiKey);
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion
    }
}
