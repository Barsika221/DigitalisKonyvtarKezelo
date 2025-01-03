using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalisKonyvtarKezelo
{
    public class ValidationHelper
    {
        public static bool ValidateEmail(string email)
        {
            return email.Contains("@") && email.Contains(".");
        }

        public static bool ValidatePassword(string password)
        {
            return password.Length >= 8;
        }

        public static bool ValidateUsername(string username)
        {
            return username.Length >= 4;
        }
        public static bool ValidatePublicationYear(int publicationYear)
        {
            int currentYear = DateTime.Now.Year;
            return publicationYear >= 1800 && publicationYear <= currentYear;
        }
    }
}
