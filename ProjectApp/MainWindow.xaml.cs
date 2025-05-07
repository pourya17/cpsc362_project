using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using Newtonsoft.Json;
using ProjectApp.Models;
using ProjectApp.Data;

namespace ProjectApp
{
    public partial class MainWindow : Window
    {
        private readonly FoodDatabase _foodDatabase;
        public MainWindow()
        {
            InitializeComponent();
            _foodDatabase = new FoodDatabase();
            Loaded += MainWindow_Loaded;

        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
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