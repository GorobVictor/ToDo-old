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
        User User { get; set; }

        public ItemsControlResource(User user)
        {
            InitializeComponent();

            tasksFalse = new ObservableCollection<Tasks>(user.TaskGroups.FirstOrDefault(x => x.Name == "ToDo").Tasks.Where(x => !x.Status));
            tasksTrue = new ObservableCollection<Tasks>(user.TaskGroups.FirstOrDefault(x => x.Name == "ToDo").Tasks.Where(x => x.Status));

            User = user;

            itemControl_tasksFalse.ItemsSource = tasksFalse;
            itemControl_tasksTrue.ItemsSource = tasksTrue;
        }
    }
}
