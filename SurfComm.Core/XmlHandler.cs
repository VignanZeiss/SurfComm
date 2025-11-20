using System.Xml.Serialization;
using System;
using System.IO;
using System.Windows;


namespace SurfComm.Core
{

    public class XmlHandler
    {
        


        public static void SaveToXml<T>(T Data , string fileName)
        {
            try
            {
                string basePath = FilePath.DocumentsFolder; 
                string folder = Path.Combine(basePath, "Zeiss", "SurfComConnect");
                string path = Path.Combine(folder, fileName);

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                if (File.Exists(path)) File.Delete(path);

                using FileStream fs = new(path, FileMode.Create, FileAccess.Write, FileShare.None);
                XmlSerializer serializer = new(typeof(T));
                serializer.Serialize(fs, Data);

            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving XML: {ex.Message}", ex);
            }

        }

        public static T? LoadFromXml<T>(string fileName) where T : class
        {
            string basePath = FilePath.DocumentsFolder;
            string folder = Path.Combine(basePath, "Zeiss", "SurfComConnect");
            string path = Path.Combine(folder, fileName);
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                using var reader = new StreamReader(path);
                var data = serializer.Deserialize(reader);
                if (data is T typed)
                    return typed;
            }
            catch (Exception ex)
            {
                //  Log if a specific file is corrupt so it doesn't crash the loop
                System.Diagnostics.Debug.WriteLine($"Failed to load {fileName}: {ex.Message}");
            }


            return null;
        }

       



    }
}
