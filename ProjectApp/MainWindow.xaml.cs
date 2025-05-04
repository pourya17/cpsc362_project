using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using Newtonsoft.Json;

namespace ProjectApp
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
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
            FoodEntry foodEntryWindow = new FoodEntry();
            foodEntryWindow.Show();
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