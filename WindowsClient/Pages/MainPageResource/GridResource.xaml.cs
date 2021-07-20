using Core.Entities;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WindowsClient.Pages.MainPageResource
{
    /// <summary>
    /// Логика взаимодействия для GridResource.xaml
    /// </summary>
    public partial class GridResource : UserControl
    {
        public ObservableCollection<Tasks> tasksFalse { get; set; }
        public ObservableCollection<Tasks> tasksTrue { get; set; }

        public event EventHandler<RoutedEventArgs> event_grid_UpdateClick;

        public void grid_UpdateClick(object sender, RoutedEventArgs e)
        {
            event_grid_UpdateClick?.Invoke(sender, e);
        }

        public event EventHandler<DataGridCellEditEndingEventArgs> event_grid_CellEditEnding;

        public void grid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            event_grid_CellEditEnding?.Invoke(sender, e);
        }

        public event EventHandler<KeyEventArgs> event_grid_PreviewKeyDown;

        public void grid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            event_grid_PreviewKeyDown?.Invoke(sender, e);
        }

        public event EventHandler<RoutedEventArgs> event_grid_DeleteClick;

        public void grid_DeleteClick(object sender, RoutedEventArgs e)
        {
            event_grid_DeleteClick?.Invoke(sender, e);
        }

        public event EventHandler<RoutedEventArgs> event_check_status_Checked;

        public void check_status_Checked(object sender, RoutedEventArgs e)
        {
            event_check_status_Checked?.Invoke(sender, e);
        }

        public event EventHandler<RoutedEventArgs> event_check_status_Unchecked;

        public void check_status_Unchecked(object sender, RoutedEventArgs e)
        {
            event_check_status_Unchecked?.Invoke(sender, e);
        }

        public event EventHandler<RoutedEventArgs> event_check_favorite_Checked;

        public void check_favorite_Checked(object sender, RoutedEventArgs e)
        {
            event_check_favorite_Checked?.Invoke(sender, e);
        }

        public event EventHandler<RoutedEventArgs> event_check_favorite_Unchecked;

        public void check_favorite_Unchecked(object sender, RoutedEventArgs e)
        {
            event_check_favorite_Unchecked?.Invoke(sender, e);
        }

        public GridResource(ObservableCollection<Tasks> tasksFalse, ObservableCollection<Tasks> tasksTrue)
        {
            InitializeComponent();
            this.DataContext = this;
            this.tasksFalse = tasksFalse;
            this.tasksTrue = tasksTrue;

            grid_tasksFalse.ItemsSource = tasksFalse;
            grid_tasksTrue.ItemsSource = tasksTrue;
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
    }
}
