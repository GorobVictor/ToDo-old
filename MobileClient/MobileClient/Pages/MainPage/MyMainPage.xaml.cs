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
    public partial class MyMainPage : Shell
    {
        public MyMainPage(User user)
        {
            InitializeComponent();

            foreach (var group in user.TaskGroups)
            {
                ShellSection shell_section = new ShellSection
                {
                    Title = group.Name,
                };

                shell_section.Items.Add(new ShellContent() { Content = new TablePage(group) });

                mainPage.Items.Add(shell_section);
            }
        }
    }
}