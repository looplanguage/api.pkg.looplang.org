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

        public static (Common.Models.Version, bool) GetSemVerFromString(string SemVer)
        {
            string[] splits = SemVer.Split(".");

            if (splits.Length != 3)
                return (new Common.Models.Version(0,0,0), false);

            int[] Version = new int[3];

            foreach (var o in splits.OfType<string>().Select((item, i) => new { item, i }))
            {
                bool succes = int.TryParse(o.item, out Version[o.i]);

                if (!succes)
                    return (new Common.Models.Version(0, 0, 0), false);
            }

            return (new Common.Models.Version(Version[0], Version[1], Version[2]), true);
        }

        public static bool IsVersionGreater(Common.Models.Version first, Common.Models.Version last)
        {
            if (first.Major < last.Major
                || (first.Major == last.Major && first.Minor < last.Minor)
                || (first.Major == last.Major && first.Minor == last.Minor && first.Patch <= last.Patch))
                return false;

            return true;
        }
    }

}
