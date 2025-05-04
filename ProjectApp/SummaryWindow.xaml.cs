using System.Windows;

namespace ProjectApp
{
    public partial class SummaryWindow : Window
    {
        public SummaryWindow(string summary)
        {
            InitializeComponent(); // Must match your XAML file
            SummaryText.Text = summary; // Must match the x:Name in XAML
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
