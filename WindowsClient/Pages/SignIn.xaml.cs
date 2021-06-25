using Core;
using Core.Dto.UserDto;
using Core.Model;
using Newtonsoft.Json;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.IO;
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

            if (File.Exists(Constant.SettingsFileName))
                using (var stream = new StreamReader(Constant.SettingsFileName, Encoding.UTF8))
                {
                    var settings = JsonConvert.DeserializeObject<Settings>(stream.ReadToEnd());

                    if (!string.IsNullOrWhiteSpace(settings.Token))
                        AuthByToken(settings.Token);
                }
        }

        async void AuthByToken(string token)
        {
            MyRestClient.Client.Authenticator = new JwtAuthenticator(token);

            var user = await MyRestClient.ProfileAsync();

            if (user == null)
                return;

            new Main(user).Show();
            this.Close();
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
                txt_email
            });

            if (!check)
            {
                MessageBox.Show("Нужно заполнить поля", "Ошибка");
                return;
            }

            var response = await MyRestClient.LoginAsync(new UserAuth(txt_email.Text, txt_password.Password));

            if (response != null)
            {
                if (check_save.IsChecked.HasValue && check_save.IsChecked.Value)
                    using (var stream = new StreamWriter(Constant.SettingsFileName, false, Encoding.UTF8))
                        stream.Write(JsonConvert.SerializeObject(new Settings(response.Token)));

                new Main(response.User).Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка");
            }
        }

        private void btn_signUp_Click(object sender, RoutedEventArgs e)
        {
            new SignUp().Show();
            this.Close();
        }
    }
}
