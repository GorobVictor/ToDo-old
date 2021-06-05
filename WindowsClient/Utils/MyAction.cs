using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WindowsClient.Utils
{
    public static class MyAction
    {
        public static void MyGotFocus(TextBox box)
        {
            string text = GetText(box);

            if (box.Text == text)
            {
                box.Text = string.Empty;
            }
        }

        public static void MyLostFocus(TextBox box)
        {
            string text = GetText(box);

            if (string.IsNullOrWhiteSpace(box.Text))
            {
                box.Text = text;
            }
        }

        private static string GetText(TextBox box)
        {
            string text = "";

            switch (box.Name)
            {
                case "txt_email":
                    text = "Email";
                    break;
                case "txt_password":
                    text = "Password";
                    break;
                case "txt_newTask":
                    text = "Add to task";
                    break;
                case "txt_firstName":
                    text = "First Name";
                    break;
                case "txt_lastName":
                    text = "Last Name";
                    break;
                case "txt_phone":
                    text = "Phone";
                    break;
            }

            return text;
        }

        public static bool CheckBox(TextBox box)
        {
            if (box.Text == GetText(box))
                return false;
            return true;
        }

        public static bool CheckBox(List<TextBox> boxes)
        {
            foreach (var box in boxes)
            {
                if (box.Text == GetText(box))
                    return false;
            }
            return true;
        }
    }
}
