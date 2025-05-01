using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordCalc.ClassLib
{
    public static class GlobalSettings
    {

        private static string _settingsFilePath = Path.Combine(
            Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName,
            "settings.txt");
        public static string SettingsFilePath 
        { 
            get { return _settingsFilePath; }
            set { _settingsFilePath = value; }
        }

        public static int FloatPrecisionDefault { get; set; } = 1;

        public static int FloatPrecisionTranslationVector { get; set; } = 2;

        public static int FloatPrecisionScaleVector { get; set; } = 3;

        public static int FloatPrecisionEulerAnglesDeg { get; set; } = 4;

        public static int FloatPrecisionEulerAnglesRad { get; set; } = 5;

        public static int FloatPrecisionQuaternion { get; set; } = 6;

        public static int FloatPrecisionMatrix { get; set; } = 7;

        public static  void LoadSettings()
        {
            LoadSettings(SettingsFilePath);
        }
        public static void LoadSettings(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(path))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] parts = line.Split('=');
                            if (parts.Length == 2)
                            {
                                string key = parts[0].Trim();
                                string value = parts[1].Trim();
                                switch (key)
                                {
                                    case "FloatPrecisionDefault":
                                        FloatPrecisionDefault = int.Parse(value);
                                        break;
                                    case "FloatPrecisionTranslationVector":
                                        FloatPrecisionTranslationVector = int.Parse(value);
                                        break;
                                    case "FloatPrecisionScaleVector":
                                        FloatPrecisionScaleVector = int.Parse(value);
                                        break;
                                    case "FloatPrecisionEulerAnglesDeg":
                                        FloatPrecisionEulerAnglesDeg = int.Parse(value);
                                        break;
                                    case "FloatPrecisionEulerAnglesRad":
                                        FloatPrecisionEulerAnglesRad = int.Parse(value);
                                        break;
                                    case "FloatPrecisionQuaternion":
                                        FloatPrecisionQuaternion = int.Parse(value);
                                        break;
                                    case "FloatPrecisionMatrix":
                                        FloatPrecisionMatrix = int.Parse(value);
                                        break;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading settings, default settings will be used: {ex.Message}");
                }
            }
        }

        public static void SaveSettings()
        {
            SaveSettings(SettingsFilePath);
        }
        public static void SaveSettings(string filePath) 
        { 
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine($"FloatPrecisionDefault = {FloatPrecisionDefault}");
                    writer.WriteLine($"FloatPrecisionTranslationVector = {FloatPrecisionTranslationVector}");
                    writer.WriteLine($"FloatPrecisionScaleVector = {FloatPrecisionScaleVector}");
                    writer.WriteLine($"FloatPrecisionEulerAnglesDeg = {FloatPrecisionEulerAnglesDeg}");
                    writer.WriteLine($"FloatPrecisionEulerAnglesRad = {FloatPrecisionEulerAnglesRad}");
                    writer.WriteLine($"FloatPrecisionQuaternion = {FloatPrecisionQuaternion}");
                    writer.WriteLine($"FloatPrecisionMatrix = {FloatPrecisionMatrix}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving settings: {ex.Message}");
            }
        }
    }
}
