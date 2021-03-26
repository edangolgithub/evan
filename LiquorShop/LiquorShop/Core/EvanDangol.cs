using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace EvanDangol.Core
{
    public delegate void CallMe(string text);

    public enum EvanDataProvider { SqlExpress,SqlServer,SqlClient, OleDb,Access12, Odbc, SqlServerCe,MySql, None }
     
    public enum EvanMonth : byte { January = 1, February, March, April, May, June, July, August, September, October, November, December };
   #pragma warning disable 168



    [Serializable]
    public class EvanException : Exception
    {
        public EvanException() { }
        public EvanException(string message) : base(message) { }
        public EvanException(string message, Exception inner) : base(message, inner) { }
        protected EvanException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

  
    #region MouseMove
    public static class LocationMove
    {
        public static void Randomize(this Control c)
        {
            Random random = new Random();
            int p = random.Next(0, c.Parent.ClientRectangle.Width);
            int p1 = random.Next(0, c.Parent.ClientRectangle.Height);
            c.Location = new Point(p, p1);
        }
    }
    #endregion
    #region Reflection

    public static class EvanReflection
    {
        public delegate void delmethod(TextBox c, Assembly a);
        public static void ShowAssembly(this TextBox c)
        {
            Assembly asembly;
            asembly = Assembly.GetExecutingAssembly();
            ShowAssembly(c, asembly);
        }
        public static void ShowAssembly(this TextBox c, string AssemblyFileName)
        {

            try
            {
                delmethod dd = ShowAssembly;
                Assembly asembly = Assembly.LoadFrom(Path.GetFullPath(AssemblyFileName));
                c.BeginInvoke(dd, c, asembly);
            }
            catch (Exception)
            {
                MessageBox.Show("Only .Net Assembly can be vewied");
            }

        }
        public static void ShowAssembly(TextBox c, Assembly asembly)
        {
            StringBuilder sb = new StringBuilder();
            Type[] t = asembly.GetTypes().OrderBy(a => a.FullName).ToArray();
            foreach (Type tt in t)
            {

                sb.Append("class Name   : " + tt.Name);
                foreach (var mi in tt.GetMethods(
                    BindingFlags.DeclaredOnly |
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.Static)
                    .OrderByDescending(a => a.Name))
                {
                    var a = mi.GetParameters().Select(aa => aa.ParameterType);

                    sb.Append(Environment.NewLine + "Function name :" + mi.Name);

                    sb.Append(Environment.NewLine + "Return Type   : " + mi.ReturnType.Name);

                    sb.Append(Environment.NewLine + "method signature :-");
                    ParameterInfo[] pi = mi.GetParameters();

                    sb.Append(" " + mi.ReturnType.Name + " " + mi.Name + "(");
                    for (int i = 0; i < pi.Length; i++)
                    {
                        sb.Append(pi[i].ParameterType.Name + " " + pi[i].Name);
                        if (i + 1 < pi.Length) sb.Append(", ");
                    }

                    sb.Append(")");

                    sb.Append(Environment.NewLine);
                }
                sb.Append(Environment.NewLine);
            }
            c.Text = sb.ToString();

        }

    }
    #endregion
}