using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Media;
using QuizApp.MVVM.Model;

namespace QuizApp.MVVM.ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        private string _username;

        private Question _question;

        private string _points;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                NotifyPropertyChanged("Username");
            }
        }

        public Question Question
        {
            get { return _question; }
            set
            {
                _question = value;
                NotifyPropertyChanged("Question");
            }
        }

        public string Points
        {
            get { return _points; }
            set
            {
                _points = value;
                NotifyPropertyChanged("Points");
            }
        }

        private static DispatcherTimer aTimer;

        public ViewModel()
        {
            GenerateQuestion('a');
            FreezeTimer();
            Question.TimeLeft = "02:00";
            HighlightChoice(Question.CurrentChoice, 'a');
            Points = "★0";
        }

        private void ATimer_Elapsed(object sender, EventArgs e)
        {
            string previous = Question.TimeLeft;
            int m = Int32.Parse(previous.Substring(0, previous.IndexOf(":")));
            int s = Int32.Parse(previous.Substring(previous.IndexOf(":")+1));
            s--;
            if (s == -1)
            {
                s = 59;
                m--;
            }
            if (s == 59 && m == -1)
            {
                Eliminate();
            }
            Question.TimeLeft = string.Format("{0}:{1}", m.ToString().PadLeft(2, '0'), s.ToString().PadLeft(2, '0'));
        }

        public void GenerateQuestion(char previousChoice)
        {
            Question = new Core.Api().GetQuestion();
            HighlightChoice('a', previousChoice);
            StartTimer();
        }

        public void HighlightChoice(char currentChoice, char previousChoice)
        {
            Quiz window = ((MainWindow)System.Windows.Application.Current.MainWindow).frame.Content as Quiz;
            var element = window.choiceA;

            if (previousChoice == 'a')
            {
                element = window.choiceA;
            }
            else if (previousChoice == 'b')
            {
                element = window.choiceB;
            }
            else if (previousChoice == 'c')
            {
                element = window.choiceC;
            }
            else if (previousChoice == 'd')
            {
                element = window.choiceD;
            }

            element.BorderThickness = new Thickness(0);
            element.Background = Brushes.White;
            element.Foreground = Brushes.Black;

            if (currentChoice == 'a')
            {
                element = window.choiceA;
            }
            else if (currentChoice == 'b')
            {
                element = window.choiceB;
            }
            else if (currentChoice == 'c')
            {
                element = window.choiceC;
            }
            else if (currentChoice == 'd')
            {
                element = window.choiceD;
            }

            var bc = new BrushConverter();
            element.Background = (Brush)bc.ConvertFrom("#1c50c9");
            element.Foreground = Brushes.White;
            element.BorderBrush = Brushes.White;
            element.BorderThickness = new Thickness(1);
        }

        public void StartTimer()
        {
            aTimer = new DispatcherTimer();
            aTimer.Interval = TimeSpan.FromSeconds(1);
            aTimer.IsEnabled = true;
            aTimer.Tick += ATimer_Elapsed;
        }

        public void FreezeTimer()
        {
            aTimer.Stop();
        }

        public void AddPoints(char previousChoice)
        {
            MainWindow window = (MainWindow)System.Windows.Application.Current.MainWindow;
            Quiz page = window.frame.Content as Quiz;
            int points = Convert.ToInt32(Points.Substring(1));
            Points = "★" + (points + 100).ToString();
            GenerateQuestion(previousChoice);
            window.KeyDown += page.KeyPress;
        }

        public void Eliminate()
        {
            Quiz page = (System.Windows.Application.Current.MainWindow as MainWindow).frame.Content as Quiz;
            ViewModel viewModel = page.DataContext as ViewModel;
            string points = viewModel.Points.Substring(1);
            page.NavigationService.Navigate(new End($"You were eliminated with {points} points!", Username));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
    }
}
