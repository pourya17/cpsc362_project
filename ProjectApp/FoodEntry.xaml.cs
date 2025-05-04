using System;
using System.Windows;

namespace ProjectApp
{
    public partial class FoodEntry : Window
    {
        public FoodEntry()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
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
