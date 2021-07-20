using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Dto.UserDto;
using Core.Utils;
using MobileClient.Pages.MainPage;
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

        private bool SaveToken { get; set; }

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
                if (SaveToken)
                {
                    if (App.Current.Properties.ContainsKey("token"))
                        App.Current.Properties.Remove("token");

                    App.Current.Properties.Add("token", response.Token);
                }

                Application.Current.MainPage = new MyMainPage(response.User);
            }
            else
            {
                await DisplayAlert("Error", "Неверный логин или пароль", "Ok");
            }
        }

        private async void btn_signUp_Click(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new SignUp());
        }

        private void show_Password(object sender, ToggledEventArgs e)
        {
            txt_password.IsPassword = !e.Value;
        }

        private void save_Token(object sender, ToggledEventArgs e)
        {
            SaveToken = e.Value;
        }
    }
}