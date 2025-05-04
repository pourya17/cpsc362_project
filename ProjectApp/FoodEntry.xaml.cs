using ProjectApp.Data;
using ProjectApp.Models;
using System;
using System.Windows;

namespace ProjectApp
{
    public partial class FoodEntryWindow : Window
    {
        public FoodEntryWindow()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {

            var FoodEntry = new ProjectApp.Models.FoodEntry
            {
                Name = FoodNameTextBox.Text,
                Calories = int.TryParse(CaloriesTextBox.Text, out var cal) ? cal : 0,
                Protein = int.TryParse(ProteinTextBox.Text, out var protein) ? protein : 0,
                Fat = int.TryParse(FatTextBox.Text, out var fat) ? fat : 0,
                Carbs = int.TryParse(CarbsTextBox.Text, out var carbs) ? carbs : 0,
            };

            var db = new FoodDatabase();
            db.AddFoodEntry(FoodEntry);
            string summary = $"Food: {FoodNameTextBox.Text}\n" +
                             $"Calories: {CaloriesTextBox.Text}\n" +
                             $"Protein: {ProteinTextBox.Text}g\n" +
                             $"Fat: {FatTextBox.Text}g\n" +
                             $"Carbs: {CarbsTextBox.Text}g";

            SummaryWindow summaryWindow = new SummaryWindow(summary);
            summaryWindow.ShowDialog();
        }

    }
}
