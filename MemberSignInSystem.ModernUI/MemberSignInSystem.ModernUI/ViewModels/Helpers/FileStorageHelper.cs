using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Xml.Serialization;
using System.IO;
using MemberSignInSystem.ModernUI.ViewModels.Models;

namespace MemberSignInSystem.ModernUI.ViewModels.Helpers
{
    class FileStorageHelper
    {
        public static void CopyStream(Stream input, Stream output)
        {
            int chunk = 1; //1 byte , you can set it more than 1 byte , however , when do that , we will failed to open an zip file
            byte[] buffer = new byte[chunk];
            int count = 0; //If there are nothing to read, we will return.
            do
            {
                count = input.Read(buffer, 0, chunk);
                if (count <= 0) break;
                output.Write(buffer, 0, chunk);
            } while (input.CanRead);

            output.Flush();
            output.Close();
            input.Close();
        }

        public static void Save(object saveMe, Type saveType, string filePath)
        {
            string directory = filePath.Substring(0, filePath.LastIndexOf('/') + 1);
            string directoryUri = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, directory);
            string targetUri = filePath.Substring(filePath.LastIndexOf('/') + 1);

            // Create directory folder if it doesn't already exist
            bool folderExists = Directory.Exists(directoryUri);
            if (!folderExists) Directory.CreateDirectory(directoryUri);

            XmlSerializer writer = new XmlSerializer(saveType);

            StreamWriter file = new StreamWriter(directoryUri + targetUri);
            writer.Serialize(file, saveMe);
            file.Close();
        }

        public static Object Retrieve(string filePath, Type fileType)
        {
            string targetUri = String.Format("{0}{1}",
                AppDomain.CurrentDomain.BaseDirectory, filePath);

            if (File.Exists(targetUri))
            {
                XmlSerializer reader = new XmlSerializer(fileType);
                StreamReader file = new StreamReader(targetUri);

                return reader.Deserialize(file);
            }
            // File not found
            return null;
        }
        public static List<Family> RetrieveFamilyList(string filePath)
        {
            return (List<Family>)Retrieve(filePath, typeof(List<Family>));
        }
        public static List<Pool> RetrievePoolList(string filePath)
        {
            return (List<Pool>)Retrieve(filePath, typeof(List<Pool>));
        }
        public static List<String> RetrieveStringList(string filePath)
        {
            return (List<String>)Retrieve(filePath, typeof(List<String>));
        }
        public static String RetrieveString(string filePath)
        {
            return (String)Retrieve(filePath, typeof(String));
        }
        public static int RetrieveInteger(string filePath)
        {
            return (int)Retrieve(filePath, typeof(int));
        }
        public static Boolean? RetrieveNullableBoolean(string filePath)
        {
            return (Boolean?)Retrieve(filePath, typeof(Boolean?));
        }
    }
}
