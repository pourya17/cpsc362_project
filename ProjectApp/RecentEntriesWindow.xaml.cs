using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ProjectApp.Data;
using ProjectApp.Models;

namespace ProjectApp
{
    public partial class RecentEntriesWindow : Window
    {
        public RecentEntriesWindow()
        {
            InitializeComponent();
            LoadRecentEntries();
        }

        private void LoadRecentEntries()
        {
            var db = new FoodDatabase();
            List<FoodEntry> allEntries = db.GetAllEntries();
            var last10 = allEntries.OrderByDescending(e => e.Date).Take(10).ToList();

            var displayList = last10.Select(e => new
            {
                e.Name,
                Summary = $"Calories: {e.Calories} | Protein: {e.Protein}g | Fat: {e.Fat}g | Carbs: {e.Carbs}g | Date: {e.Date:g}"
            });

            EntriesList.ItemsSource = displayList;
        }
    }
}
