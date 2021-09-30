using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using QuizApp.Core;

namespace QuizApp
{
    /// <summary>
    /// Interaction logic for Name.xaml
    /// </summary>
    public partial class Name : Page
    {
        public Name()
        {
            InitializeComponent();
        }
        private void Textbox_Loaded(object sender, RoutedEventArgs e)
        {
            nameField.Focus();
        }
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && nameField.Text.Length > 0)
            {
                Animations.Welcome(this);
            }
        }
    }
}
