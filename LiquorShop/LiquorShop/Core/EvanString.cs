using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvanDangol.Core
{
  public static  class EvanString
    {
      public static string ToTitleCase(this string inputString)
      {
          inputString = inputString.Trim();
          inputString = inputString.ToLower();

          string[] inputStringAsArray = inputString.Split(' ');
          StringBuilder sb = new StringBuilder();
          for (int i = 0; i < inputStringAsArray.Length; i++)
          {
              if (inputStringAsArray[i].Length > 0)
              {
                  sb.AppendFormat("{0}{1} ",
                  inputStringAsArray[i].Substring(0, 1).ToUpper(),
                  inputStringAsArray[i].Substring(1));
              }
          }

          return sb.ToString(0, sb.Length - 1);
      }
    }
}
