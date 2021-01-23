using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Text;
using System.Xml;
using System.Security.AccessControl;
using System.Security.Principal;

namespace EvanDangol.IO
{
    /// <summary>
    /// 
    /// </summary>
    public enum EvanSerializationFormat : byte { Binary, Xml };
    public static class EvanSerialization
    {
       


        static void SetFullControlPermissionsToEveryone(string path)
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

            var info = new DirectoryInfo(path);
            var security = info.GetAccessControl(AccessControlSections.Access);

            bool result;
            security.ModifyAccessRule(AccessControlModification.Set, accessRule, out result);

            if (!result)
            {
                throw new InvalidOperationException("Failed to give full-control permission to all users for path " + path);
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
                throw new InvalidOperationException("Failed to give full-control permission inheritance to all users for " + path);
            }

            info.SetAccessControl(security);
        }
        /// <summary>
        /// Serialize Objects 
        /// </summary>
        /// <param name="ObjectToSerialize">Object To Serialize</param>
        /// <param name="filename">Format Binary or Xml </param>
        /// 
        public static void SerializeMeAsXml(this object ObjectToSerialize,string filename=null)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite))
            {
                XmlSerializer xser = new XmlSerializer(ObjectToSerialize.GetType());
                xser.Serialize(fs, ObjectToSerialize);
                fs.Close();
            }
        }
         public static object DeSerializeMeAsXml(this object ObjectToDeSerialize, string filename = null)
         {
             XmlSerializer serializer = new XmlSerializer(ObjectToDeSerialize.GetType());
             using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
             {
                 XmlReader reader = XmlReader.Create(fs);
                 ObjectToDeSerialize = serializer.Deserialize(reader);
                 return ObjectToDeSerialize;
             }
         }
        public static void Serialize(this object ObjectToSerialize, string filename)
        {
            BinaryFormatter Bm = new BinaryFormatter();
            using (FileStream fs = new FileStream(filename, FileMode.Create,FileAccess.Write))
            {
                Bm.Serialize(fs, ObjectToSerialize);
                fs.Close();
            }
        }
        public static void SerializeMeAs(this object ObjectToSerialize, EvanSerializationFormat Format,string filename=null)
        {
            FileStream fs;
            DirectoryInfo dir = null;
            string s = "";
            if (filename == null)
            {
                 s = ObjectToSerialize.GetType().Name + ".Evan";
            }
            else
            {
                s = filename;
            }
            string specialdir = "";
            if (filename == null)
            {
                specialdir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\SerializedData";


                if (!Directory.Exists(specialdir))
                {
                    Directory.CreateDirectory(specialdir);
                    SetFullControlPermissionsToEveryone(specialdir);
                }
                switch (Format)
                {
                    case EvanSerializationFormat.Binary:
                        BinaryFormatter Bm = new BinaryFormatter();
                        using (fs = new FileStream(dir + @"\" + s, FileMode.Create))
                        {
                            Bm.Serialize(fs, ObjectToSerialize);
                            fs.Close();
                        }
                        break;
                    case EvanSerializationFormat.Xml:

                        s = s.Replace("Evan", "Xml");
                        using (fs = new FileStream(specialdir + "\\" + s, FileMode.Create, FileAccess.ReadWrite))
                        {
                            XmlSerializer xser = new XmlSerializer(ObjectToSerialize.GetType());
                            xser.Serialize(fs, ObjectToSerialize);
                            fs.Close();
                        }
                        break;
                }
            }
            else
            {
               
                switch (Format)
                {
                    case EvanSerializationFormat.Binary:
                        BinaryFormatter Bm = new BinaryFormatter();
                        using (fs = new FileStream(s, FileMode.Create))
                        {
                            Bm.Serialize(fs, ObjectToSerialize);
                            fs.Close();
                        }
                        break;
                    case EvanSerializationFormat.Xml:

                        s = s.Replace("Evan", "Xml");
                        using (fs = new FileStream(s, FileMode.Create, FileAccess.ReadWrite))
                        {
                            XmlSerializer xser = new XmlSerializer(ObjectToSerialize.GetType());
                            xser.Serialize(fs, ObjectToSerialize);
                            fs.Close();
                        }
                        break;
                }
            }
           

        }
        public static object DeserializeObject(string FileToDeserialize)
        {
            BinaryFormatter bm = new BinaryFormatter();
            using (FileStream fs = new FileStream(FileToDeserialize, FileMode.Open))
            {
                return bm.Deserialize(fs);
            }

        }
        public static object DeserializeObjectOld(string FileToDeserialize)
        {
            BinaryFormatter bm = new BinaryFormatter();
            using (FileStream fs = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"SerializedData\" + FileToDeserialize, FileMode.Open))
            {
                return bm.Deserialize(fs);
            }

        }
        /// <summary>
        ///  Deserialze the xml file and assign to object variable.\n
        /// use following pattern::  personArrayobject= personArrayobject.DeserializeObject("person[].Xml") as person[];  ::
        /// </summary>
        /// <param name="ObjectType">Any objects /any object array</param>
        /// <param name="filename">xml File location in the harddrive</param>
        /// <returns></returns>
        public static object DeserializeXml(this Object ObjectType, string filename)
        {
            try
            {
                string specialdir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\SerializedData";
                if (!Directory.Exists(specialdir))
                {
                    Directory.CreateDirectory(specialdir);
                    SetFullControlPermissionsToEveryone(specialdir);
                }
                if(!File.Exists(specialdir+"\\"+filename))
                {
                    ObjectType.SerializeMeAs(EvanSerializationFormat.Xml,filename);
                }
                XmlSerializer serializer = new XmlSerializer(ObjectType.GetType());
                using (FileStream fs = new FileStream(specialdir+"\\" + filename, FileMode.Open, FileAccess.Read))
                {
                    XmlReader reader = XmlReader.Create(fs);
                    ObjectType = serializer.Deserialize(reader);
                    return ObjectType;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
