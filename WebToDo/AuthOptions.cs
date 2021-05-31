using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebToDo
{
    public class AuthOptions
    {
        public const string ISSUER = "MoneyWallet";
        public const string AUDIENCE = "MoneyWalletClient";
        const string KEY = "secretkey.123456789123456789";
        public const int LIFETIME = 1;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
