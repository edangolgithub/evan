// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using System.Security.AccessControl;
// using System.Security.Principal;
// using System.Text;
// using System.Threading.Tasks;

// namespace LiquorShop.Classes
// {
//   public static  class AdminstrationHelper
//     {
//         public static string MakeDirectory(string path)
//         {
//             DirectorySecurity ds = new DirectorySecurity();

//             //var path = Environment.GetFolderPath(specialfolder);
//             //var mainpath = Path.Combine(path, DirectoryName);
//             var rights = FileSystemRights.FullControl;
//             var user = new SecurityIdentifier(WellKnownSidType.AuthenticatedUserSid, null);
//             FileSystemAccessRule rule = new FileSystemAccessRule(user, rights, AccessControlType.Allow);
//             ds.SetAccessRule(rule);


//             if (!Directory.Exists(path))
//             {
//                 DirectoryInfo d = new DirectoryInfo(path);
//                 d.Create(ds);
//                 // Directory.CreateDirectory(mainpath, ds);


//             }
//             return path;
//         }
//     }
// }
