using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace ProjectApp
{
    public class PresetFood
    {
        public string Name { get; set; }
        public int Calories { get; set; }
        public int Protein { get; set; }
        public int Carbs { get; set; }
        public int Fat { get; set; }
    }

    public static class PresetManager
    {
        private const string PresetsFilePath = "Data\\presets.json";


        public static void SavePreset(PresetFood preset)
        {
            string folder = Path.Combine(Directory.GetCurrentDirectory(), "Data");
            string path = Path.Combine(folder, "presets.json");

            try
            {
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                List<PresetFood> presets;
                if (File.Exists(path))
                {
                    string json = File.ReadAllText(path);
                    presets = JsonSerializer.Deserialize<List<PresetFood>>(json) ?? new List<PresetFood>();
                }
                else
                {
                    presets = new List<PresetFood>();
                }

                presets.Add(preset);
                string updatedJson = JsonSerializer.Serialize(presets, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(path, updatedJson);

                MessageBox.Show("Preset saved to:\n" + path); // Optional debug
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save preset: " + ex.Message, "Error");
            }
        }


    }
}
