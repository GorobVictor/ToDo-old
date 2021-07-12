using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileClient.Pages.MainPage
{
    public class MyMainPageFlyoutMenuItem
    {
        public MyMainPageFlyoutMenuItem()
        {
            TargetType = typeof(MyMainPageFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}