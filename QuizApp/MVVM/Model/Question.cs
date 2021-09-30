using System.ComponentModel;

namespace QuizApp.MVVM.Model
{
    public class Question : INotifyPropertyChanged
    {

        private string _theQuestion;
        private string _choiceA;
        private string _choiceB;
        private string _choiceC;
        private string _choiceD;
        private string _timeLeft;
        private char _currentChoice;
        private char _correctChoice;

        public string TheQuestion
        {
            get { return _theQuestion; }
            set { _theQuestion = value; OnPropertyChanged("TheQuestion"); }
        }

        public string ChoiceA
        {
            get { return _choiceA; }
            set
            {
                _choiceA = value; OnPropertyChanged("ChoiceA");

            }
        }

        public string ChoiceB
        {
            get { return _choiceB; }
            set
            {
                _choiceB = value; OnPropertyChanged("ChoiceB");

            }
        }

        public string ChoiceC
        {
            get { return _choiceC; }
            set
            {
                _choiceC = value; OnPropertyChanged("ChoiceC");

            }
        }

        public string ChoiceD
        {
            get { return _choiceD; }
            set
            {
                _choiceD = value; OnPropertyChanged("ChoiceD");

            }
        }

        public string TimeLeft
        {
            get { return _timeLeft; }
            set
            {
                _timeLeft = value; OnPropertyChanged("TimeLeft");

            }
        }

        public char CurrentChoice
        {
            get { return _currentChoice; }
            set { _currentChoice = value; OnPropertyChanged("CurrentChoice"); }
        }

        public char CorrectChoice
        {
            get { return _correctChoice; }
            set { _correctChoice = value; OnPropertyChanged("CorrectChoice"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}
