using Core.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileClient.Pages.MainPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyMainPageFlyout : ContentPage
    {
        public User User { get; set; }

        public ListView ListView;

        public MyMainPageFlyout()
        {
            InitializeComponent();

            BindingContext = new MyMainPageFlyoutViewModel();
            ListView = MenuItemsListView;
            label_userName.Text = User.FullName;
        }

        class MyMainPageFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MyMainPageFlyoutMenuItem> MenuItems { get; set; }

            public MyMainPageFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<MyMainPageFlyoutMenuItem>(new[]
                {
                    new MyMainPageFlyoutMenuItem { Id = 0, Title = "Page 1" },
                    new MyMainPageFlyoutMenuItem { Id = 1, Title = "Page 2" },
                    new MyMainPageFlyoutMenuItem { Id = 2, Title = "Page 3" },
                    new MyMainPageFlyoutMenuItem { Id = 3, Title = "Page 4" },
                    new MyMainPageFlyoutMenuItem { Id = 4, Title = "Page 5" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}