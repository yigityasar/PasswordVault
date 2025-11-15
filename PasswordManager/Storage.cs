using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;


namespace PasswordManager
{
    public class PasswordItem
    {
        public string Platform { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Notes { get; set; }
    }
    public static class Storage
    {
        private static string filePath="passwords.json";
        public static List<PasswordItem> LoadPasswords()
        {
            if (!System.IO.File.Exists(filePath))
            {
                return new List<PasswordItem>();
            }
            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<PasswordItem>>(json);
        }
        public static void Save(List<PasswordItem> items)
        {
            var json = JsonConvert.SerializeObject(items, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }
}
