using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using QuizApp.Core;
using QuizApp.MVVM.ViewModel;

namespace QuizApp
{
    /// <summary>
    /// Interaction logic for Quiz.xaml
    /// </summary>
    public partial class Quiz : Page
    {
        bool fadeIn = true;
        string name = "";
        public Quiz(bool fade, string username)
        {
            InitializeComponent();
            System.Windows.Application.Current.MainWindow.KeyDown += KeyPress;
            fadeIn = fade;
            name = username;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel viewModel = new ViewModel();
            this.DataContext = viewModel;

            viewModel.Username = name;
            if (fadeIn)
            {
                Animations.FadeInQuiz(this);
            }
            else
            {
                (this.DataContext as ViewModel).StartTimer();
            }
        }
        private void Resize(object sender, SizeChangedEventArgs e)
        {
            Core.FontSize.resize(this, e.NewSize.Width, e.NewSize.Height);
        }
        public void KeyPress(object sender, KeyEventArgs e)
        {
            Core.KeyPress.CheckKey(e.Key);
        }
    }
}
