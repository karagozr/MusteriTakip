using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.WebPages;

namespace MusteriTakip.Helpers
{
    public static class FileHelper<T> where T:class
    {
        public static List<T> DosyaOku()
        {
            List<T> list = new List<T>();
            string dir = HttpRuntime.AppDomainAppPath;
            string serializationFile = Path.Combine(dir, typeof(T).Name+".bin");

            if (!File.Exists(serializationFile)) return null;
            using (Stream stream = File.Open(serializationFile, FileMode.Open))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                list = (List<T>)bformatter.Deserialize(stream);
            }

            return list;
        }

        public static void DosyaYaz(List<T> list)
        {
            string dir = HttpRuntime.AppDomainAppPath;
            string serializationFile = Path.Combine(dir, typeof(T).Name + ".bin");

            using (Stream stream = File.Open(serializationFile, FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                bformatter.Serialize(stream, list);
            }
        }
    }
}