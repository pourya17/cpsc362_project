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
            string foodName = FoodNameTextBox.Text;
            bool caloriesParsed = int.TryParse(CaloriesTextBox.Text, out int calories);
            bool proteinParsed = float.TryParse(ProteinTextBox.Text, out float protein);
            bool fatParsed = float.TryParse(FatTextBox.Text, out float fat);
            bool carbsParsed = float.TryParse(CarbsTextBox.Text, out float carbs);

            if (!caloriesParsed || !proteinParsed || !fatParsed || !carbsParsed)
            {
                MessageBox.Show("Please enter valid numeric values.");
                return;
            }

            // For now, just display the entered values
            string summary = $"Food: {foodName}\nCalories: {calories}\nProtein: {protein}g\nFat: {fat}g\nCarbs: {carbs}g";
            MessageBox.Show(summary, "Entry Saved");

            // TODO: Save to file or database
        }
    }
}
