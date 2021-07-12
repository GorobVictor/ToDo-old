using Core.Entities;
using Core.Utils;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileClient.Pages.MainPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyMainPage : FlyoutPage
    {
        User User { get; set; }

        public MyMainPage(string token)
        {
            InitializeComponent();
            //FlyoutPage.ListView.ItemSelected += ListView_ItemSelected;


            AuthByToken(token);
        }

        public MyMainPage(User user)
        {
            InitializeComponent();
            FlyoutPage.ListView.ItemSelected += ListView_ItemSelected;

            FlyoutPage.User = user;

            User = user;
        }

        async void AuthByToken(string token)
        {
            MyRestClient.Client.Authenticator = new JwtAuthenticator(token);

            User = await MyRestClient.ProfileAsync();

            FlyoutPage.User = User;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MyMainPageFlyoutMenuItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            Detail = new NavigationPage(page);
            IsPresented = false;

            FlyoutPage.ListView.SelectedItem = null;
        }
    }
}