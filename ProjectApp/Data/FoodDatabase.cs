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
                string sql = "SELECT Id, Name, Calories, Protein, Fat, Carbs, Date FROM FoodEntry";
                using var cmd = new SQLiteCommand(sql, conn);
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

        private void InitializeDatabase()
        {
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();

                // Create FoodEntry table if it doesn't exist
                string foodEntryTableSql = @"CREATE TABLE IF NOT EXISTS FoodEntry (
                                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                        Name TEXT NOT NULL,
                                        Calories INTEGER,
                                        Protein INTEGER,
                                        Fat INTEGER,
                                        Carbs INTEGER,
                                        Date TEXT)";
                using var cmd = new SQLiteCommand(foodEntryTableSql, conn);
                cmd.ExecuteNonQuery();

                // Drop and recreate ActivityEntry table to ensure it's fresh
                string dropActivityTableSql = "DROP TABLE IF EXISTS ActivityEntry";
                using var dropCmd = new SQLiteCommand(dropActivityTableSql, conn);
                dropCmd.ExecuteNonQuery();

                string activityTableSql = @"CREATE TABLE IF NOT EXISTS ActivityEntry (
                                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                        Name TEXT NOT NULL,
                                        CaloriesBurned INTEGER,
                                        Date TEXT)";
                using var cmd2 = new SQLiteCommand(activityTableSql, conn);
                cmd2.ExecuteNonQuery();
            }
        }

        //ADD FOOD
        public void AddFoodEntry(FoodEntry entry)
        {
            try
            {
                using (var conn = new SQLiteConnection(ConnectionString))
                {
                    conn.Open();

                    string sql = "INSERT INTO FoodEntry (Name, Calories, Protein, Fat, Carbs, Date) VALUES (@Name, @Calories, @Protein, @Fat, @Carbs, @Date)";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", entry.Name);
                        cmd.Parameters.AddWithValue("@Calories", entry.Calories);
                        cmd.Parameters.AddWithValue("@Protein", entry.Protein);
                        cmd.Parameters.AddWithValue("@Fat", entry.Fat);
                        cmd.Parameters.AddWithValue("@Carbs", entry.Carbs);
                        cmd.Parameters.AddWithValue("@Date", entry.Date.ToString("s"));

                        cmd.ExecuteNonQuery();  // Execute the query to insert the food entry

                        // Verify the data insertion by checking if the food entry exists in the database
                        string verifySql = "SELECT COUNT(*) FROM FoodEntry WHERE Name = @Name AND Calories = @Calories";
                        using (var verifyCmd = new SQLiteCommand(verifySql, conn))
                        {
                            verifyCmd.Parameters.AddWithValue("@Name", entry.Name);
                            verifyCmd.Parameters.AddWithValue("@Calories", entry.Calories);

                            int count = Convert.ToInt32(verifyCmd.ExecuteScalar());
                            if (count > 0)
                            {
                                Console.WriteLine("Food entry inserted successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Error: Food entry was not inserted.");
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite Error: {ex.Message}");
                throw new Exception("An error occurred while inserting the food entry.", ex);
            }
        }

        //Gets the last 3 food entries for the home page
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
        //ADD ACTIVITY
        public void AddActivityEntry(ActivityEntry activity)
        {
            try
            {
                using (var conn = new SQLiteConnection(ConnectionString))
                {
                    conn.Open();

                    string sql = "INSERT INTO ActivityEntry (Name, CaloriesBurned, Date) VALUES (@Name, @CaloriesBurned, @Date)";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", activity.Name);
                        cmd.Parameters.AddWithValue("@CaloriesBurned", activity.CaloriesBurned);
                        cmd.Parameters.AddWithValue("@Date", activity.Date.ToString("s"));

                        cmd.ExecuteNonQuery();  // Execute the query to insert the activity

                        // Verify the data insertion by checking if the activity exists in the database
                        string verifySql = "SELECT COUNT(*) FROM ActivityEntry WHERE Name = @Name AND CaloriesBurned = @CaloriesBurned";
                        using (var verifyCmd = new SQLiteCommand(verifySql, conn))
                        {
                            verifyCmd.Parameters.AddWithValue("@Name", activity.Name);
                            verifyCmd.Parameters.AddWithValue("@CaloriesBurned", activity.CaloriesBurned);

                            int count = Convert.ToInt32(verifyCmd.ExecuteScalar());
                            if (count > 0)
                            {
                                Console.WriteLine("Activity inserted successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Error: Activity was not inserted.");
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite Error: {ex.Message}");
                throw new Exception("An error occurred while inserting the activity entry.", ex);
            }
        }
        //Gets the last 3 activity entries for the home page
        public List<ActivityEntry> GetRecentActivities(int count = 3)
        {
            var entries = new List<ActivityEntry>();

            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                string sql = "SELECT Id, Name, CaloriesBurned, Date FROM ActivityEntry ORDER BY Date DESC LIMIT @Count";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Count", count);

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    entries.Add(new ActivityEntry
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        CaloriesBurned = reader.GetInt32(2),
                        Date = DateTime.Parse(reader.GetString(3))
                    });
                }
            }

            return entries;   
        }
        //DELETE AN ENTRY
        public void DeleteFoodEntry(int id)
        {
            try
            {
                using var conn = new SQLiteConnection(ConnectionString);
    conn.Open();

    string sql = "DELETE FROM FoodEntry WHERE Id = @Id";
    using var cmd = new SQLiteCommand(sql, conn);
    cmd.Parameters.AddWithValue("@Id", id);
    cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite Error: {ex.Message}");
                throw new Exception("An error occurred while deleting the food entry.", ex);
            }
        }

        public void DeleteActivityEntry(int id)
        {
            try
            {
                using (var conn = new SQLiteConnection(ConnectionString))
                {
                    conn.Open();

                    string sql = "DELETE FROM ActivityEntry WHERE Id = @Id";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite Error: {ex.Message}");
                throw new Exception("An error occurred while deleting the activity entry.", ex);
            }
        }

    }
}
