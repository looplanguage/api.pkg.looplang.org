using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace lpr.Logic
{
    public static class Utilities
    {
        public static bool ValidateName(string name)
        {
            Regex regex = new Regex(@"^[a-zA-Z]+-?[a-zA-Z]+$");
            Match match = regex.Match(name);

            if (match.Success)
                return true;
            return false;
        }


        public static string GenerateRandomValidString(int min, int max)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            Random random = new Random();
            int length = random.Next(min, max);
            
            string str = "";
            for (int i = 0; i < length; i++)
            {
                str += chars[random.Next(chars.Length)];
            }
            return str;
        }
    }

}
