using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoDemoLauncher.Helper
{
    class ConfigurationFile : IniFile
    {
        private static ConfigurationFile instance = null;
        private string appDataPath;
        private string iniPath; 

        private ConfigurationFile()
        {
            appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/StoDemoLauncher";
            iniPath = appDataPath + "/config.ini";

            if (!System.IO.Directory.Exists(this.appDataPath)) System.IO.Directory.CreateDirectory(this.appDataPath);
            if (System.IO.File.Exists(this.iniPath)) this.LoadIniFile(this.iniPath);
            else this.groups = new Dictionary<string, Dictionary<string, string>>();
        }

        public static ConfigurationFile GetInstance()
        {
            if(ConfigurationFile.instance == null)
            {
                ConfigurationFile.instance = new ConfigurationFile();
            }
            return ConfigurationFile.instance;
        }

        public void Save()
        {
            System.IO.File.WriteAllLines(this.iniPath, this.GetFileContents().ToArray());
        }
    }
}
