 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApp.Models
{
    public class FoodEntry
    {
        public int Id { get; set; } // Required for SQLite primary key
        public string Name { get; set; }
        public int Calories { get; set; }
        public int Protein { get; set; }
        public int Fat { get; set; }
        public int Carbs { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
    public class ActivityEntry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CaloriesBurned { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
    public class PresetFood
    {
        public string Name { get; set; }
        public int Calories { get; set; }
        public int Protein { get; set; }
        public int Carbs { get; set; }
        public int Fat { get; set; }
    }


}
