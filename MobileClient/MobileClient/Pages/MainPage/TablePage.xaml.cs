using Core.Entities;
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
    public partial class TablePage : ContentPage
    {
        public TablePage(TaskGroup taskGroup)
        {
            InitializeComponent();

            listView.ItemsSource = taskGroup.Tasks;
        }
    }
}