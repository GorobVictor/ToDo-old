using Core.Dto.TasksDto;
using Core.Entities;
using MaterialDesignThemes.Wpf;
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
using Core.Utils;
using WindowsClient.Pages.MainPageResource;

namespace WindowsClient.Pages
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        ObservableCollection<Tasks> tasksFalse { get; set; }
        ObservableCollection<Tasks> tasksTrue { get; set; }
        User User { get; set; }

        public Main(User user)
        {
            InitializeComponent();

            tasksFalse = new ObservableCollection<Tasks>(user.TaskGroups.FirstOrDefault(x => x.Name == "ToDo").Tasks.Where(x => !x.Status));
            tasksTrue = new ObservableCollection<Tasks>(user.TaskGroups.FirstOrDefault(x => x.Name == "ToDo").Tasks.Where(x => x.Status));

            UpdateTable();

            txt_Name.Content = user.FullName;

            txt_Email.Content = user.Email;

            if (!string.IsNullOrWhiteSpace(user.Photo))
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(user.Photo, UriKind.Absolute);
                bitmap.EndInit();

                img_Photo.ImageSource = bitmap;
            }

            user.TaskGroups.ForEach(x => listBox_groups.Items.Add(x));

            listBox_groups.SelectedItem = user.TaskGroups.FirstOrDefault(x => x.Name == "ToDo");

            User = user;

            var form = new GridResource(tasksFalse, tasksTrue);

            form.event_grid_UpdateClick += new EventHandler<RoutedEventArgs>(grid_UpdateClick);
            form.event_grid_CellEditEnding += new EventHandler<DataGridCellEditEndingEventArgs>(grid_CellEditEnding);
            form.event_grid_PreviewKeyDown += new EventHandler<KeyEventArgs>(grid_PreviewKeyDown);
            form.event_grid_DeleteClick += new EventHandler<RoutedEventArgs>(grid_DeleteClick);
            form.event_check_status_Checked += new EventHandler<RoutedEventArgs>(check_status_Checked);
            form.event_check_status_Unchecked += new EventHandler<RoutedEventArgs>(check_status_Unchecked);
            form.event_check_favorite_Checked += new EventHandler<RoutedEventArgs>(check_favorite_Checked);
            form.event_check_favorite_Unchecked += new EventHandler<RoutedEventArgs>(check_favorite_Unchecked);

            grid_TableFalse.Children.Add(form);
        }

        private async void check_status_Checked(object sender, RoutedEventArgs e)
        {
            var check = sender as CheckBox;

            var task = check.DataContext as Tasks;

            if (task != null)
            {
                await MyRestClient.UpdateTaskStatusAsync(task.Id, true);

                task.Status = true;

                tasksFalse.Remove(task);
                tasksTrue.Add(task);
            }
        }

        private async void check_status_Unchecked(object sender, RoutedEventArgs e)
        {
            var check = sender as CheckBox;

            var task = check.DataContext as Tasks;

            if (task != null)
            {
                await MyRestClient.UpdateTaskStatusAsync(task.Id, false);

                task.Status = false;

                tasksTrue.Remove(task);
                tasksFalse.Add(task);
            }
        }

        private async void check_favorite_Checked(object sender, RoutedEventArgs e)
        {
            var check = sender as CheckBox;

            var task = check.DataContext as Tasks;

            if (task != null)
            {
                await MyRestClient.UpdateTaskFavoritesAsync(task.Id, true);

                task.Favorite = true;
            }
        }

        private async void check_favorite_Unchecked(object sender, RoutedEventArgs e)
        {
            var check = sender as CheckBox;

            var task = check.DataContext as Tasks;

            if (task != null)
            {
                await MyRestClient.UpdateTaskFavoritesAsync(task.Id, false);

                task.Favorite = false;
            }
        }

        private async void btn_addTask_Click(object sender, RoutedEventArgs e)
        {
            var obj = listBox_groups.SelectedItem as TaskGroup;

            var date = date_leadTime.SelectedDate;

            if (string.IsNullOrWhiteSpace(txt_newTask.Text))
            {
                MessageBox.Show("task name empty");
                return;
            }

            if (date.HasValue && date.Value < DateTime.Now)
            {
                MessageBox.Show("The ultimate time should be more than the current");
                return;
            }

            var task = await MyRestClient.AddTask(new CreateTaskDto(txt_newTask.Text, string.Empty, obj.Id, date));

            tasksFalse.Add(task);

            obj.Tasks.Add(task);

            txt_newTask.Text = string.Empty;
        }

        private async void grid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var tasks = e.EditingElement.DataContext as Tasks;

            var textBox = e.EditingElement as TextBox;

            if (textBox == null)
                return;

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

        private async void grid_DeleteClick(object sender, RoutedEventArgs e)
        {
            var menu = sender as MenuItem;

            var contextmenu = menu.Parent as ContextMenu;

            var grid = contextmenu.PlacementTarget as DataGrid;

            var ids = new List<long>();

            foreach (Tasks task in grid.SelectedItems)
            {
                await MyRestClient.DeleteTask(task.Id);
                ids.Add(task.Id);
            }

            foreach (var id in ids)
            {
                var task = tasksFalse.FirstOrDefault(x => x.Id == id);
                if (task != null)
                    tasksFalse.Remove(task);
                task = tasksTrue.FirstOrDefault(x => x.Id == id);
                if (task != null)
                    tasksTrue.Remove(task);
            }
        }

        public void grid_UpdateClick(object sender, RoutedEventArgs e)
        {
            var menu = sender as MenuItem;

            var contextmenu = menu.Parent as ContextMenu;

            var grid = contextmenu.PlacementTarget as DataGrid;

            if (grid.SelectedItems.Count > 0)
            {
                var task = grid.SelectedItems[0] as Tasks;

                txt_taskName.Text = task.Name;
                txt_taskName.DataContext = task;
                check_taskStatus.IsChecked = task.Status;
                check_taskStatus.DataContext = task;
            }
        }

        private void window_Main_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            grid_TableFalse.Width = window_Main.Width - grid_Left.ActualWidth - columnDefinition_right.Width.Value - 40;
        }

        private void txt_taskName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txt_taskName_LostFocus(sender, e);
            }
        }

        private void txt_newTask_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btn_addTask_Click(sender, null);
            }
        }

        private async void txt_taskName_LostFocus(object sender, RoutedEventArgs e)
        {
            var task = txt_taskName.DataContext as Tasks;

            if (task != null && !string.IsNullOrWhiteSpace(txt_taskName.Text) && txt_taskName.Text != task.Name)
            {
                await MyRestClient.UpdateTaskNameAsync(task.Id, txt_taskName.Text);

                task.Name = txt_taskName.Text;

                UpdateTable();
            }
        }

        private void grid_tasksFalse_Sorting(object sender, DataGridSortingEventArgs e)
        {

        }

        private void listBox_groups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var obj = listBox_groups.SelectedItem as TaskGroup;

            tasksFalse = new ObservableCollection<Tasks>(obj.Tasks.Where(x => !x.Status));
            tasksTrue = new ObservableCollection<Tasks>(obj.Tasks.Where(x => x.Status));

            UpdateTable();
        }

        public void UpdateTable()
        {
            //grid_tasksFalse.ItemsSource = null;
            //grid_tasksTrue.ItemsSource = null;
            //grid_tasksFalse.ItemsSource = tasksFalse;
            //itemControl_tasksFalse.ItemsSource = tasksFalse;
            //grid_tasksTrue.ItemsSource = tasksTrue;
            //itemControl_tasksTrue.ItemsSource = tasksTrue;
        }
    }
}
