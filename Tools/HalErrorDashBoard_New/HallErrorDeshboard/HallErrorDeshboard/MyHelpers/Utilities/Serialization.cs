using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace MyHelpers
{
    class Serialization
    {

        public static void Serialize(Object obj, string path)
        {

            BinaryFormatter formatter = new BinaryFormatter();

            if (!Directory.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path));

            if (!File.Exists(path))
                File.Create(path).Close();

            Stream stream = File.OpenWrite(path);

            formatter.Serialize(stream, obj);
            stream.Close();
        }

        public static Object Deserialize(string path)
        {
            if (!File.Exists(path))
                return null;

            Stream stream = File.OpenRead(path);
            BinaryFormatter formatter = new BinaryFormatter();
            Object obj;
            
            try
            {
                obj = formatter.Deserialize(stream);
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                stream.Close();
            }

            return obj;
        }


    }


}
