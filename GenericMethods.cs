using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ejednevnichek__
{
    internal class GenericMethod
    {
        public static void Serialize<T>(T letter, string filename)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string json = JsonConvert.SerializeObject(letter);
            if (File.Exists(desktop+"\\"+filename))
            {
                File.WriteAllText(desktop + "\\" + filename, json);

            }
            else
            {
                File.Create(desktop + "\\" + filename).Close();
                File.WriteAllText(desktop + "\\" + filename, json);
            }
        }
        public static T Deserialize<T>(string fileName)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string json = "";
            if (File.Exists(desktop + "\\" + fileName))
            {
                
                 json = File.ReadAllText(desktop + "\\" + fileName);
            }
            else
            {
                File.Create(desktop + "\\" + fileName);
                json = File.ReadAllText(desktop + "\\" + fileName);
            }
                T Notes = JsonConvert.DeserializeObject<T>(json);
            return Notes;
        }
    }
}
