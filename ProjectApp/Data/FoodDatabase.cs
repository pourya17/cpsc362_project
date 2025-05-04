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
    }
}
