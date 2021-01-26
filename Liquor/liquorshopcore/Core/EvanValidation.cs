using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EvanDangol.Core
{
    public static class EvanValidation
    {
        public static bool IsNameValid(this string Name)
        {
            try
            {
                bool a = Regex.IsMatch(Name, @"^[a-zA-Z]+$");
                bool b = Name.IsNullOrEmpty();
                return a && b;
            }
            catch
            {
                return false;
            }

        }
        public static bool IsNullOrEmpty(this string s)
        {
            return !string.IsNullOrEmpty(s);
        }

        public static bool IsNumberOnly(this string s)
        {
            foreach (char c in s)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }

        public static bool IsAlpha(this string input)
        {
            return Regex.IsMatch(input, "^[a-zA-Z]+$");
        }

        public static bool IsAlphaNumeric(this string input)
        {
            return Regex.IsMatch(input, "^[a-zA-Z0-9]+$");
        }

        public static bool IsAlphaNumericWithUnderscore(this string input)
        {
            return Regex.IsMatch(input, "^[a-zA-Z0-9_]+$");
        }
        public static bool IsISBN(this string isbn)
        {
            return Regex.IsMatch(isbn, @"ISBN\x20(?=.{13}$)\d{1,5}([- ])\d{1,7}\1\d{1,6}\1(\d|X)$");
        }
        public static bool IsIsbnNumber(this string isbn)
        {
           if( Regex.IsMatch(isbn,@"^(\d{13}|\s*)?$"))
           {
               return true;
           }
           else if(Regex.IsMatch(isbn,@"^(\d{10}|\s*)?$"))
           {
               return true;
           }
           return false;
        }
    }
}
