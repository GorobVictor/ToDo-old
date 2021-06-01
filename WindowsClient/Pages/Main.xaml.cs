using Core.Dto.Tasks;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
        ObservableCollection<Tasks> tasksFalse { get; set; }
        ObservableCollection<Tasks> tasksTrue { get; set; }

        public Main(User user)
        {
            InitializeComponent();

            tasksFalse = new ObservableCollection<Tasks>(user.Tasks.Where(x => !x.Status));
            tasksTrue = new ObservableCollection<Tasks>(user.Tasks.Where(x => x.Status));

            grid_tasksFalse.ItemsSource = tasksFalse;
            grid_tasksTrue.ItemsSource = tasksTrue;
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

            tasksFalse.Remove(task);
            tasksTrue.Add(task);
        }

        private async void check_status_Unchecked(object sender, RoutedEventArgs e)
        {
            var check = sender as CheckBox;

            var task = check.DataContext as Tasks;

            await MyRestClient.UpdateTaskStatusAsync(task.Id, false);

            task.Status = false;

            tasksTrue.Remove(task);
            tasksFalse.Add(task);
        }

        private async void btn_addTask_Click(object sender, RoutedEventArgs e)
        {
            tasksFalse.Add(await MyRestClient.AddTask(new CreateTaskDto(txt_newTask.Text, string.Empty)));

            txt_newTask.Text = string.Empty;

            MyAction.MyLostFocus(txt_newTask);
        }

        private async void grid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var tasks = e.EditingElement.DataContext as Tasks;

            var textBox = e.EditingElement as TextBox;

            if (tasks.Name != textBox.Text && !string.IsNullOrWhiteSpace(textBox.Text))
            {
                await MyRestClient.UpdateTaskNameAsync(tasks.Id, textBox.Text);
            }
            else if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = tasks.Name;
            }
        }

        private async void grid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var grid = sender as DataGrid;
            if (Key.Delete == e.Key)
            {
                var tasks = new List<Tasks>();

                foreach (var row in grid.SelectedItems)
                {
                    tasks.Add(row as Tasks);
                }

                if (tasks.Count() > 0)
                    await MyRestClient.DeleteTask(tasks.Select(x => x.Id).ToList());
            }
        }
    }
}
