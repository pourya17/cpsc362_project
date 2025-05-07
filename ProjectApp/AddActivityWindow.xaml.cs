using ProjectApp.Data;
using ProjectApp.Models;
using System;
using System.Windows;

namespace ProjectApp
{
    public partial class AddActivityWindow : Window
    {
        private FoodDatabase _database;

        public AddActivityWindow()
        {
            InitializeComponent();
            _database = new FoodDatabase();
        }

        // Event handler for Submit Button click
       private void SubmitButton_Click(object sender, RoutedEventArgs e)
{
    // Get the values from the textboxes
    string activityName = ActivityNameTextBox.Text.Trim();
    if (string.IsNullOrEmpty(activityName))
    {
        MessageBox.Show("Please enter an activity name.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
        return;
    }

    if (!int.TryParse(CaloriesBurnedTextBox.Text, out int caloriesBurned))
    {
        MessageBox.Show("Please enter a valid number for calories burned.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
        return;
    }

    // Create the ActivityEntry object
    var activity = new ActivityEntry
    {
        Name = activityName,
        CaloriesBurned = caloriesBurned
    };

    // Call the database method to add the activity entry
    try
    {
        _database.AddActivityEntry(activity);
        MessageBox.Show("Activity successfully added!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

        // Clear the form fields
        ActivityNameTextBox.Clear();
        CaloriesBurnedTextBox.Clear();
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Error adding activity: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}
    }
}
