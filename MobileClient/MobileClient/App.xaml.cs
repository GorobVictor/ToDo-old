using Core.Utils;
using MobileClient.Pages;
using MobileClient.Pages.MainPage;
using RestSharp.Authenticators;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileClient
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            App.Current.Properties.TryGetValue("token", out object token);

            if (token == null)
                MainPage = new SignIn();
            else
                MainPage = new MyMainPage(token.ToString());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
