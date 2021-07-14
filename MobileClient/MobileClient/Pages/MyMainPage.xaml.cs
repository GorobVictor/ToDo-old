using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileClient.Pages
{
    public partial class MyMainPage : Shell
    {
        public MyMainPage(User user)
        {
            InitializeComponent();

            flyout_group. = user.TaskGroups.ToList();

            listView.ItemsSource = user.TaskGroups.Select(x => x.Name == "ToDo").ToList();
        }
    }
}