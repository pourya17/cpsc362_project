using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

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
            // Define the path to the presets.json file within the Data folder
            string presetsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "presets.json");

            // Ensure the Data folder exists
            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Data")))
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Data"));
            }

            List<PresetFood> foodPresets;

            if (File.Exists(presetsFilePath))
            {
                // Read the current presets from the file
                var jsonData = File.ReadAllText(presetsFilePath);
                foodPresets = JsonSerializer.Deserialize<List<PresetFood>>(jsonData);
            }
            else
            {
                // If the file doesn't exist, start with a new list
                foodPresets = new List<PresetFood>();
            }

            // Add the new preset to the list
            foodPresets.Add(preset);

            // Serialize the list of presets and save it back to the file
            var updatedJsonData = JsonSerializer.Serialize(foodPresets, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(presetsFilePath, updatedJsonData);
        }

    }
}
