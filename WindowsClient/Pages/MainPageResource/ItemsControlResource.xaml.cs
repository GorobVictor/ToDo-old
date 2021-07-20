using Core.Entities;
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
    /// Логика взаимодействия для ItemsControlResource.xaml
    /// </summary>
    public partial class ItemsControlResource : UserControl
    {
        ObservableCollection<Tasks> tasksFalse { get; set; }
        ObservableCollection<Tasks> tasksTrue { get; set; }

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

        public ItemsControlResource(ObservableCollection<Tasks> tasksFalse, ObservableCollection<Tasks> tasksTrue)
        {
            InitializeComponent();

            itemControl_tasksFalse.ItemsSource = tasksFalse;
            itemControl_tasksTrue.ItemsSource = tasksTrue;
        }
    }
}
