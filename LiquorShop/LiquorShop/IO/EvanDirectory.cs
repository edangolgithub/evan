using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;

namespace EvanDangol.IO
{
    public static class EvanDirectory
    {
        public static string DirectoryName { get; set; }

        public static void MakeDirectoryInDesktopCurrent(this string DirectoryName)
        {
           
            DirectoryInfo dir = Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + DirectoryName);
            string path = dir.FullName;
            Directory.SetCurrentDirectory(path);
        }
        public static void SetWorkingDirectory(this string path)
        {
            Directory.SetCurrentDirectory(path);
        }

        public static void MakeDirectoryInSpecialPlaceCurrent(this string DirectoryName, Environment.SpecialFolder specialfolder)
        {
           
            DirectoryInfo dir = Directory.CreateDirectory(Environment.GetFolderPath(specialfolder) + "\\" + DirectoryName);
            string path = dir.FullName;
            Directory.SetCurrentDirectory(path);
        }
        public static string MakeDirectoryInSpecialPlace(this string DirectoryName, Environment.SpecialFolder specialfolder)
        {
            var path = Environment.GetFolderPath(specialfolder);
            var mainpath = Path.Combine(path, DirectoryName);
            if (!Directory.Exists(mainpath))
            {
                Directory.CreateDirectory(mainpath);
            }
            return mainpath;
        }
        public static string MakeDirectoryInSpecialPlaceWithPermission(this string DirectoryName, Environment.SpecialFolder specialfolder)
        {
            DirectorySecurity ds = new DirectorySecurity();
           
            var path = Environment.GetFolderPath(specialfolder);
            var mainpath = Path.Combine(path, DirectoryName);
            var rights = FileSystemRights.FullControl;
            var user =new SecurityIdentifier(WellKnownSidType.AuthenticatedUserSid,null);
            FileSystemAccessRule rule = new FileSystemAccessRule(user,rights,AccessControlType.Allow);
            ds.SetAccessRule(rule);

           
            if (!Directory.Exists(mainpath))
            {
                DirectoryInfo d = new DirectoryInfo(mainpath);
                d.Create(ds);
               // Directory.CreateDirectory(mainpath, ds);
                  
             
            }
            return mainpath;
        }
        public static void SetFullControlPermissionsToEveryone(this string FolderPath)
        {
            const FileSystemRights rights = FileSystemRights.FullControl;

            var allUsers = new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null);

            // Add Access Rule to the actual directory itself
            var accessRule = new FileSystemAccessRule(
                allUsers,
                rights,
                InheritanceFlags.None,
                PropagationFlags.NoPropagateInherit,
                AccessControlType.Allow);

            var info = new DirectoryInfo(FolderPath);
            var security = info.GetAccessControl(AccessControlSections.Access);

            bool result;
            security.ModifyAccessRule(AccessControlModification.Set, accessRule, out result);

            if (!result)
            {
                throw new InvalidOperationException("Failed to give full-control permission to all users for path " + FolderPath);
            }

            // add inheritance
            var inheritedAccessRule = new FileSystemAccessRule(
                allUsers,
                rights,
                InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                PropagationFlags.InheritOnly,
                AccessControlType.Allow);

            bool inheritedResult;
            security.ModifyAccessRule(AccessControlModification.Add, inheritedAccessRule, out inheritedResult);

            if (!inheritedResult)
            {
                throw new InvalidOperationException("Failed to give full-control permission inheritance to all users for " + FolderPath);
            }

            info.SetAccessControl(security);
        }
        public static void OpenFile(string path)
        {
            Process process = new Process();
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.FileName = path;
            process.Start();
        }
    }
}
