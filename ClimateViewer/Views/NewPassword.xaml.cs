﻿using ClimateViewer.Handlers;
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
    /// Interaction logic for NewPassword.xaml
    /// </summary>
    public partial class NewPassword : Window
    {
        public NewPassword()
        {
            InitializeComponent();
        }

        private void btn_change_Click(object sender, RoutedEventArgs e)
        {
            if (tb_newpassword.Visibility == Visibility.Visible) { pb_newpassword.Password = tb_newpassword.Text; }
            if (tb_confirmnewpassword.Visibility == Visibility.Visible) { pb_confirmnewpassword.Password = tb_confirmnewpassword.Text; }

            if (pb_newpassword.Password != pb_confirmnewpassword.Password)
            {
                MessageBox.Show("New Password and Comfirm New Password does not match");
            }
            else {
                HttpApiRequest.ChangePassword(UserInformation.ApiKey, UserInformation.Mail, pb_newpassword.Password);
                UserInformation.Password = pb_newpassword.Password;
                Close();
            }
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void lb_showNewPassword_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (tb_newpassword.Visibility == Visibility.Visible) {
                pb_newpassword.Visibility = Visibility.Visible;
                tb_newpassword.Visibility = Visibility.Hidden;
                pb_newpassword.Password = tb_newpassword.Text;
            }
            else if (pb_newpassword.Visibility == Visibility.Visible)
            {
                pb_newpassword.Visibility = Visibility.Hidden;
                tb_newpassword.Visibility = Visibility.Visible;
                tb_newpassword.Text = pb_newpassword.Password;
            }
        }

        private void lb_showconfirmNewPassword_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (tb_confirmnewpassword.Visibility == Visibility.Visible)
            {
                pb_confirmnewpassword.Visibility = Visibility.Visible;
                tb_confirmnewpassword.Visibility = Visibility.Hidden;
                pb_confirmnewpassword.Password = tb_confirmnewpassword.Text;
            }
            else if(pb_confirmnewpassword.Visibility == Visibility.Visible)
            {
                pb_confirmnewpassword.Visibility = Visibility.Hidden;
                tb_confirmnewpassword.Visibility = Visibility.Visible;
                tb_confirmnewpassword.Text = pb_confirmnewpassword.Password;
            }
        }
    }
}