using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Dto.UserDto;
using Core.Utils;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileClient.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignIn : ContentPage
    {
        public SignIn()
        {
            InitializeComponent();
        }

        private async void btn_login_Click(object sender, EventArgs args)
        {
            if (string.IsNullOrWhiteSpace(txt_email.Text) || string.IsNullOrWhiteSpace(txt_password.Text))
            {
                await DisplayAlert("Error", "Нужно заполнить поля", "Ok");
                return;
            }

            var response = await MyRestClient.LoginAsync(new UserAuth(txt_email.Text, txt_password.Text));

            if (response != null)
            {
                //if (check_save.IsChecked.HasValue && check_save..Value)
                //    using (var stream = new StreamWriter(Constant.SettingsFileName, false, Encoding.UTF8))
                //        stream.Write(JsonConvert.SerializeObject(new Settings(response.Token)));

                await DisplayAlert("Ok", response.User.FullName, "Ok");

                //new Main(response.User).Show();
                //this.Close();
            }
            else
            {
                await DisplayAlert("Error", "Неверный логин или пароль", "Ok");
            }
        }

        private void btn_signUp_Click(object sender, EventArgs args)
        {
            //new SignUp().Show();
            //this.Close();
        }

        private void show_Password(object sender, ToggledEventArgs e)
        {
            txt_password.IsPassword = !e.Value;
        }
    }
}