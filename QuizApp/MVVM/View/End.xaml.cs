using System.Windows;
using System.Windows.Controls;

namespace QuizApp
{
    /// <summary>
    /// Interaction logic for End.xaml
    /// </summary>
    public partial class End : Page
    {
        string name = "";
        public End(string message, string username)
        {
            InitializeComponent();
            messageTb.Text = message;
            name = username;
        }
        public void Try_Again(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Quiz(false, name));
        }
    }
}
