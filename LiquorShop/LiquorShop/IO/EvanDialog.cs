using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EvanDangol.IO
{
   public static class EvanDialog
    {
       public static FolderBrowserDialog FolderBrowseDialog()
       {
           FolderBrowserDialog dg = new FolderBrowserDialog();
           dg.ShowDialog();
           return dg;
       }
       public static OpenFileDialog OpenFileDialog() 
       {
           OpenFileDialog dg = new OpenFileDialog();
           dg.ShowDialog();
           return dg;
       }
       public static SaveFileDialog SaveFileDialog()
       {
           SaveFileDialog dg = new SaveFileDialog();
           dg.ShowDialog();
           return dg;
       }
    }
}
