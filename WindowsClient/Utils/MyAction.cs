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
                case "txtbox_RepeatPassword":
                    text = "Повторите пароль";
                    break;
                case "txtbox_Name":
                    text = "Имя";
                    break;
                case "txtbox_Surname":
                    text = "Фамилия";
                    break;
                case "txtbox_Email":
                    text = "Емейл";
                    break;
                case "txtbox_Phone":
                    text = "Номер телефона";
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
