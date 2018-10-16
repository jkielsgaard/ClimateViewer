using ClimateViewer.Handlers;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ClimateViewer.Views
{
    /// <summary>
    /// Interaction logic for Userunits.xaml
    /// Only function used by event handlers is described because that events should be self-explanatory
    /// </summary>
    public partial class PrivatUnits : Window
    {
        public PrivatUnits(List<Userunits> _units)
        {
            InitializeComponent();
            units = _units;
        }

        List<Userunits> units = new List<Userunits>();

        private void Window_Loaded(object sender, RoutedEventArgs e) { dg_units.ItemsSource = units; }

        #region Buttons
        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            HttpApiRequest.Changeunits(UserInformation.ApiKey, UserInformation.Mail, units);
            DialogResult = true;
            Close();
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
        #endregion
    }
}
