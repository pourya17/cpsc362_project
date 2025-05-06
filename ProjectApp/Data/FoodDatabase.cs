using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using ProjectApp.Models;

namespace ProjectApp.Data
{
    public class FoodDatabase
    {
        private const string DbName = "food_entries.db";
        private const string ConnectionString = "Data Source=" + DbName;

        public FoodDatabase()
        {
            if (!File.Exists(DbName))
                InitializeDatabase();
        }

        public List<FoodEntry> GetAllEntries()
        {
            var entries = new List<FoodEntry>();

            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                string sql = "SELECT Name, Calories, Protein, Fat, Carbs, Date FROM FoodEntry";
                using var cmd = new SQLiteCommand(sql, conn);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    entries.Add(new FoodEntry
                    {
                        Name = reader.GetString(0),
                        Calories = reader.GetInt32(1),
                        Protein = reader.GetInt32(2),
                        Fat = reader.GetInt32(3),
                        Carbs = reader.GetInt32(4),
                        Date = DateTime.Parse(reader.GetString(5))
                    });
                }
            }

            return entries;
        }

        private void InitializeDatabase()
        {
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                string sql = @"CREATE TABLE IF NOT EXISTS FoodEntry (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                Name TEXT NOT NULL,
                                Calories INTEGER,
                                Protein INTEGER,
                                Fat INTEGER,
                                Carbs INTEGER,
                                Date TEXT)";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
        }

        public void AddFoodEntry(FoodEntry entry)
        {
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                string sql = "INSERT INTO FoodEntry (Name, Calories, Protein, Fat, Carbs, Date) VALUES (@Name, @Calories, @Protein, @Fat, @Carbs, @Date)";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Name", entry.Name);
                cmd.Parameters.AddWithValue("@Calories", entry.Calories);
                cmd.Parameters.AddWithValue("@Protein", entry.Protein);
                cmd.Parameters.AddWithValue("@Fat", entry.Fat);
                cmd.Parameters.AddWithValue("@Carbs", entry.Carbs);
                cmd.Parameters.AddWithValue("@Date", entry.Date.ToString("s"));
                cmd.ExecuteNonQuery();
            }
        }
        //Gets the last 3 entries for the home page
        public List<FoodEntry> GetRecentEntries(int count = 3)
        {
            var entries = new List<FoodEntry>();

            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                string sql = "SELECT Id, Name, Calories, Protein, Fat, Carbs, Date FROM FoodEntry ORDER BY Date DESC LIMIT @Count";

                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Count", count);

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    entries.Add(new FoodEntry
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Calories = reader.GetInt32(2),
                        Protein = reader.GetInt32(3),
                        Fat = reader.GetInt32(4),
                        Carbs = reader.GetInt32(5),
                        Date = DateTime.Parse(reader.GetString(6))
                    });
                }
            }

            return entries;
        }
    }
}
