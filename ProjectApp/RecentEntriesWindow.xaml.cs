using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ProjectApp.Data;
using ProjectApp.Models;

namespace ProjectApp
{
    public partial class RecentEntriesWindow : Window
    {
        private FoodDatabase _db;

        public RecentEntriesWindow()
        {
            InitializeComponent();
            _db = new FoodDatabase();
            LoadRecentEntries();
        }

        private void LoadRecentEntries()
        {
            var db = new FoodDatabase();

            // Get the last 10 food entries
            List<FoodEntry> allFoodEntries = db.GetAllEntries();
            var last10FoodEntries = allFoodEntries.OrderByDescending(e => e.Date).Take(10).ToList();

            // Get the last 10 activity entries
            List<ActivityEntry> allActivityEntries = db.GetRecentActivities(10);
            var last10ActivityEntries = allActivityEntries.OrderByDescending(e => e.Date).Take(10).ToList();

            // Combine both food and activity entries into a single list with the Date property for both types
            var combinedEntries = last10FoodEntries
                .Select(e => new
                {
                    Type = "Food",
                    e.Name,
                    e.Date,
                    Summary = $"Calories: {e.Calories} | Protein: {e.Protein}g | Fat: {e.Fat}g | Carbs: {e.Carbs}g | Date: {e.Date:g}",
                    e.Id,
                    IsFoodEntry = true  // Track whether it's a FoodEntry
                })
                .Concat(last10ActivityEntries
                    .Select(e => new
                    {
                        Type = "Activity",
                        e.Name,
                        e.Date,
                        Summary = $"Calories Burned: {e.CaloriesBurned} | Date: {e.Date:g}",
                        e.Id,
                        IsFoodEntry = false  // Track whether it's an ActivityEntry
                    }))
                .OrderByDescending(entry => entry.Date)  // Now you can sort by Date
                .Take(10)  // Limit to 10 entries
                .ToList();

            // Bind to the UI
            EntriesList.ItemsSource = combinedEntries;
        }




        // Handle the deletion of an entry
        // Handle the deletion of an entry
        private void DeleteEntry_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            dynamic entry = button?.DataContext;

            if (entry != null)
            {
                var result = MessageBox.Show("Are you sure you want to delete this entry?", "Confirm Deletion", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    if (entry.IsFoodEntry)
                    {
                        _db.DeleteFoodEntry((int)entry.Id);
                    }
                    else
                    {
                        _db.DeleteActivityEntry((int)entry.Id);
                    }

                    LoadRecentEntries();
                }
            }
        }


    }
}
