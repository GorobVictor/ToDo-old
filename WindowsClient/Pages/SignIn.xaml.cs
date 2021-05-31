﻿using Core.Dto.UserDto;
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
using WindowsClient.Utils;

namespace WindowsClient.Pages
{
    /// <summary>
    /// Interaction logic for SignIn.xaml
    /// </summary>
    public partial class SignIn : Window
    {
        public SignIn()
        {
            InitializeComponent();
        }

        private void MyGotFocus(object sender, RoutedEventArgs e)
        {
            MyAction.MyGotFocus(sender as TextBox);
        }

        private void MyLostFocus(object sender, RoutedEventArgs e)
        {
            MyAction.MyLostFocus(sender as TextBox);
        }

        private async void btn_login_Click(object sender, RoutedEventArgs e)
        {
            var check = MyAction.CheckBox(new List<TextBox>()
            {
                txt_email,
                txt_password
            });

            if (!check)
            {
                MessageBox.Show("Нужно заполнить поля", "Ошибка");
                return;
            }

            var response = await MyRestClient.LoginAsync(new UserAuth(txt_email.Text, txt_password.Text));

            if (response != null)
            {
                new Main(response.User).Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка");
            }
        }
    }
}
