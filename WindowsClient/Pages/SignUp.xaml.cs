using Core.Dto.UserDto;
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
using Core.Utils;

namespace WindowsClient.Pages
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private async void btn_signUp_Click(object sender, RoutedEventArgs e)
        {
            if (
                string.IsNullOrWhiteSpace(txt_firstName.Text) ||
                string.IsNullOrWhiteSpace(txt_lastName.Text) ||
                string.IsNullOrWhiteSpace(txt_email.Text) ||
                string.IsNullOrWhiteSpace(txt_phone.Text)
                )
            {
                MessageBox.Show("Fields are not filled");
                return;
            }

            if (pass_password.Password != pass_passwordRepeat.Password)
            {
                MessageBox.Show("Password mismatch");
                return;
            }

            var user = await MyRestClient.SignUpAsync(new UserSignUp()
            {
                Email = txt_email.Text,
                FirstName = txt_firstName.Text,
                LastName = txt_lastName.Text,
                Phone = txt_phone.Text,
                Password = pass_password.Password
            });

            if (user != null)
            {
                var window = new Main(user.User);
                window.Show();
                this.Close();
            }
        }

        private void MyGotFocus(object sender, RoutedEventArgs e)
        {
            MyAction.MyGotFocus(sender as TextBox);
        }

        private void MyLostFocus(object sender, RoutedEventArgs e)
        {
            MyAction.MyLostFocus(sender as TextBox);
        }
    }
}
