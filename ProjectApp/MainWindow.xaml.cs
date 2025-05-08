using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using Newtonsoft.Json;
using ProjectApp.Models;
using ProjectApp.Data;
using System.IO;


namespace ProjectApp
{
    public partial class MainWindow : Window
    {
        private readonly FoodDatabase _foodDatabase;
        string PresetsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "presets.json");




        public MainWindow()
        {
            InitializeComponent();
            _foodDatabase = new FoodDatabase();
            Loaded += MainWindow_Loaded;

            LoadPresets();
        }

        private void LoadPresets()
        {
            // Use this to find the file path for presets
            // MessageBox.Show("Looking for: " + PresetsFilePath);

            if (File.Exists(PresetsFilePath))
            {
                string json = File.ReadAllText(PresetsFilePath);
                List<PresetFood> presets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PresetFood>>(json);
                PresetsListBox.ItemsSource = presets;
            }
            else
            {
                MessageBox.Show("Preset file not found!", "Error");
            }
        }

        private void SavePreset_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected preset from the ListBox
            PresetFood selectedPreset = (PresetFood)PresetsListBox.SelectedItem;

            if (selectedPreset != null)
            {
                // Convert the selected preset into a FoodEntry object
                FoodEntry newEntry = new FoodEntry
                {
                    Name = selectedPreset.Name,
                    Calories = selectedPreset.Calories,
                    Protein = selectedPreset.Protein,
                    Carbs = selectedPreset.Carbs,
                    Fat = selectedPreset.Fat
                };

                // Save the new FoodEntry to the database or add it to your collection
                var db = new FoodDatabase(); // Assuming you have a FoodDatabase class for saving
                db.AddFoodEntry(newEntry); // This is where you save the food entry (you may need to adjust this depending on how your database is structured)

                // Optionally, show a summary window like in the SubmitButton_Click method
                string summary = $"Food: {newEntry.Name}\n" +
                                 $"Calories: {newEntry.Calories}\n" +
                                 $"Protein: {newEntry.Protein}g\n" +
                                 $"Fat: {newEntry.Fat}g\n" +
                                 $"Carbs: {newEntry.Carbs}g";

                // Show a confirmation window
                SummaryWindow summaryWindow = new SummaryWindow(summary);
                summaryWindow.ShowDialog();
            }
            else
            {
                // If no preset is selected, show an error message
                MessageBox.Show("Please select a preset food to add.", "Error");
            }
        }

        private void UpdateCalorieProgress(double currentCalories, double goalCalories)
        {
            double percentage = currentCalories / goalCalories;
            double angle = 360 * percentage;
            double radius = 90;
            double centerX = 100;
            double centerY = 100;

            double radians = (Math.PI / 180) * angle;
            double x = centerX + radius * Math.Sin(radians);
            double y = centerY - radius * Math.Cos(radians);

            bool isLargeArc = angle > 180;

            var arcSegment = new ArcSegment
            {
                Point = new Point(x, y),
                Size = new Size(radius, radius),
                IsLargeArc = isLargeArc,
                SweepDirection = SweepDirection.Clockwise
            };

            var pathFigure = new PathFigure
            {
                StartPoint = new Point(centerX, centerY - radius),
                Segments = new PathSegmentCollection { arcSegment }
            };

            var pathGeometry = new PathGeometry
            {
                Figures = new PathFigureCollection { pathFigure }
            };

            ProgressArc.Data = pathGeometry;
            CalorieText.Text = $"{currentCalories}/{goalCalories} cal";
        }


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                UpdateCalorieProgress(950, 2000); // Example: 750 calories of 2000

                // Recent Food Entries
                var recentEntries = _foodDatabase.GetRecentEntries(3);
                while (recentEntries.Count < 3)
                {
                    recentEntries.Add(new FoodEntry { Name = "-", Calories = 0 });
                }

                RecentC_1.Text = $"{recentEntries[0].Name}     {recentEntries[0].Calories} cal";
                RecentC_2.Text = $"{recentEntries[1].Name}     {recentEntries[1].Calories} cal";
                RecentC_3.Text = $"{recentEntries[2].Name}     {recentEntries[2].Calories} cal";

                // Recent Activities
                var recentActivities = _foodDatabase.GetRecentActivities(3);
                while (recentActivities.Count < 3)
                {
                    recentActivities.Add(new ActivityEntry { Name = "-", CaloriesBurned = 0 });
                }

                RecentB_1.Text = $"{recentActivities[0].Name}     {recentActivities[0].CaloriesBurned} cal";
                RecentB_2.Text = $"{recentActivities[1].Name}     {recentActivities[1].CaloriesBurned} cal";
                RecentB_3.Text = $"{recentActivities[2].Name}     {recentActivities[2].CaloriesBurned} cal";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load data:\n{ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void Search_Focus(object sender, RoutedEventArgs e)
        {
            if (SearchBox.Text == "Search...")
            {
                SearchBox.Text = "";
            }
        }


        private void Search_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchBox.Text))
            {
                SearchBox.Text = "Search...";
            }
        }

        private void Entries_Button_Click(object sender, RoutedEventArgs e)
        {
            Entries_Button.ContextMenu.IsOpen = true;
        }

        private void AddFood_Click(object sender, RoutedEventArgs e)
        {
            var foodWindow = new FoodEntryWindow();
            foodWindow.ShowDialog();
        }

        private void AddActivity_Click(object sender, RoutedEventArgs e)
        {
            var activityWindow = new AddActivityWindow();
            activityWindow.ShowDialog();
        }


        private void ViewRecentEntries_Button_Click(object sender, RoutedEventArgs e)
        {
            RecentEntriesWindow recentWindow = new RecentEntriesWindow();
            recentWindow.ShowDialog();
        }
        private void Home_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
        }

        private void LoadEntryItems()
        {

            JsonConvert.DeserializeObject<List<string>>(SearchBox.Text);

        }



        private void Button_Hover(object sender, MouseEventArgs e)
        {
            Entries_Button.Background = Brushes.Blue;
            Entries_Button.VerticalAlignment = VerticalAlignment.Top;
        }

        private void Profile_Button_Click(object sender, RoutedEventArgs e)
        {
            Profile prof = new Profile();
            prof.Show();

        }
     
    }
}