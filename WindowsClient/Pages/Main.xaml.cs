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

            tasksFalse = new ObservableCollection<Tasks>(user/*.TaskGroups.FirstOrDefault(x => x.Name == "ToDo")*/.Tasks.Where(x => !x.Status));
            tasksTrue = new ObservableCollection<Tasks>(user/*.TaskGroups.FirstOrDefault(x => x.Name == "ToDo")*/.Tasks.Where(x => x.Status));

            grid_tasksFalse.ItemsSource = tasksFalse;
            grid_tasksTrue.ItemsSource = tasksTrue;

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
            tasksFalse.Add(await MyRestClient.AddTask(new CreateTaskDto(txt_newTask.Text, string.Empty)));

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

        private async void grid_UpdateClick(object sender, RoutedEventArgs e)
        {
            columnDefinition_right.Width = new GridLength(200);

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

                window_Main_SizeChanged(sender, null);
            }
        }

        private void openGrid_Click(object sender, RoutedEventArgs e)
        {
            if (grid_tasksTrue.Visibility == Visibility.Hidden)
            {
                var packIcon = new PackIcon();
                packIcon.Kind = PackIconKind.ArrowDown;

                openGrid.Content = packIcon;
                grid_tasksTrue.Visibility = Visibility.Visible;
            }
            else if (grid_tasksTrue.Visibility == Visibility.Visible)
            {
                var packIcon = new PackIcon();
                packIcon.Kind = PackIconKind.ArrowRight;

                openGrid.Content = packIcon;
                grid_tasksTrue.Visibility = Visibility.Hidden;
            }
        }

        private void window_Main_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            grid_TableFalse.Width = window_Main.Width - grid_Left.ActualWidth - columnDefinition_right.Width.Value - 40;
        }

        private async void txt_taskName_KeyDown(object sender, KeyEventArgs e)
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

                grid_tasksFalse.ItemsSource = null;
                grid_tasksTrue.ItemsSource = null;
                grid_tasksFalse.ItemsSource = tasksFalse;
                grid_tasksTrue.ItemsSource = tasksTrue;
            }
        }

        private void grid_tasksFalse_Sorting(object sender, DataGridSortingEventArgs e)
        {

        }
    }
}
