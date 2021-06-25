using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class Settings
    {
        public Settings()
        {
        }

        public Settings(string token)
        {
            Token = token;
        }

        public string Token { get; set; }
    }
}
