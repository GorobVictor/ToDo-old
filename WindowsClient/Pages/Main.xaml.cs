using Core.Dto.Tasks;
using Core.Entities;
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

namespace WindowsClient.Pages
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        List<Tasks> tasks { get; set; }

        public Main(User user)
        {
            InitializeComponent();

            tasks = user.Tasks;

            grid_tasks.ItemsSource = tasks.Where(x => !x.Status);
            grid_tasks_close.ItemsSource = tasks.Where(x => x.Status);
        }

        private void MyGotFocus(object sender, RoutedEventArgs e)
        {
            MyAction.MyGotFocus(sender as TextBox);
        }

        private void MyLostFocus(object sender, RoutedEventArgs e)
        {
            MyAction.MyLostFocus(sender as TextBox);
        }

        private async void check_status_Checked(object sender, RoutedEventArgs e)
        {
            var check = sender as CheckBox;

            var task = check.DataContext as Tasks;

            await MyRestClient.UpdateTaskStatusAsync(task.Id, true);

            task.Status = true;

            grid_tasks.ItemsSource = tasks.Where(x => !x.Status);
            grid_tasks_close.ItemsSource = tasks.Where(x => x.Status);
        }

        private async void check_status_Unchecked(object sender, RoutedEventArgs e)
        {
            var check = sender as CheckBox;

            var task = check.DataContext as Tasks;

            await MyRestClient.UpdateTaskStatusAsync(task.Id, false);

            task.Status = false;

            grid_tasks.ItemsSource = tasks.Where(x => !x.Status);
            grid_tasks_close.ItemsSource = tasks.Where(x => x.Status);
        }

        private async void btn_addTask_Click(object sender, RoutedEventArgs e)
        {
            tasks.Add(await MyRestClient.AddTask(new CreateTaskDto(txt_newTask.Text, string.Empty)));

            grid_tasks.ItemsSource = tasks.Where(x => !x.Status);
            grid_tasks_close.ItemsSource = tasks.Where(x => x.Status);

            txt_newTask.Text = string.Empty;

            MyAction.MyLostFocus(txt_newTask);
        }
    }
}
