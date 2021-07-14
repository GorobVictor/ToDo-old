using Core.Dto.UserDto;
using Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileClient.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUp : ContentPage
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private async void btn_signUp_Click(object sender, EventArgs e)
        {
            if (
                string.IsNullOrWhiteSpace(txt_firstName.Text) ||
                string.IsNullOrWhiteSpace(txt_lastName.Text) ||
                string.IsNullOrWhiteSpace(txt_email.Text) ||
                string.IsNullOrWhiteSpace(txt_phone.Text)
                )
            {
                await DisplayAlert("Error","Fields are not filled", "Ok");
                return;
            }

            if (pass_password.Text != pass_passwordRepeat.Text)
            {
                await DisplayAlert("Error", "Password mismatch", "Ok");
                return;
            }

            var user = await MyRestClient.SignUpAsync(new UserSignUp()
            {
                Email = txt_email.Text,
                FirstName = txt_firstName.Text,
                LastName = txt_lastName.Text,
                Phone = txt_phone.Text,
                Password = pass_password.Text
            });

            if (user != null)
            {
                await Navigation.PushAsync(new MyMainPage(user.User));
            }
        }

    }
}