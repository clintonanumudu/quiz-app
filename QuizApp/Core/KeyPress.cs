using System;
using System.Windows;
using System.Windows.Input;
using QuizApp.MVVM.ViewModel;

namespace QuizApp.Core
{
    class KeyPress
    {
        public static void CheckKey(Key key)
        {
            if (key == Key.Left || key == Key.Right || key == Key.Up || key == Key.Down)
            {
                UpdateChoice(key);
            }
            if (key == Key.Enter || key == Key.Space)
            {
                SubmitChoice();
            }
        }

        public static void UpdateChoice(Key key)
        {
            char[] choices = { 'a', 'b', 'c', 'd' };

            Quiz page = ((MainWindow)System.Windows.Application.Current.MainWindow).frame.Content as Quiz;
            char previousChoice = (page.DataContext as MVVM.ViewModel.ViewModel).Question.CurrentChoice;
            char currentChoice = (page.DataContext as MVVM.ViewModel.ViewModel).Question.CurrentChoice;

            if (key == Key.Left)
            {
                if (currentChoice == 'b' || currentChoice == 'd')
                {
                    currentChoice = choices[Array.IndexOf(choices, previousChoice) - 1];
                }
            }
            else if (key == Key.Right)
            {
                if (currentChoice == 'a' || currentChoice == 'c')
                {
                    currentChoice = choices[Array.IndexOf(choices, previousChoice) + 1];
                }
            }
            else if (key == Key.Up)
            {
                if (currentChoice == 'c')
                {
                    currentChoice = 'a';
                }
                else if (currentChoice == 'd')
                {
                    currentChoice = 'b';
                }
            }
            else if (key == Key.Down)
            {
                if (currentChoice == 'a')
                {
                    currentChoice = 'c';
                }
                else if (currentChoice == 'b')
                {
                    currentChoice = 'd';
                }
            }

            (page.DataContext as ViewModel).Question.CurrentChoice = currentChoice;

            (page.DataContext as ViewModel).HighlightChoice(currentChoice, previousChoice);
        }

        public static void SubmitChoice()
        {
            Quiz page = ((MainWindow)System.Windows.Application.Current.MainWindow).frame.Content as Quiz;
            char choice = (page.DataContext as ViewModel).Question.CurrentChoice;

            char correctChoice = (page.DataContext as ViewModel).Question.CorrectChoice;

            if (choice == correctChoice)
            {
                Animations.CorrectAnswer(page, choice);
            }
            else
            {
                Animations.WrongAnswer(page, choice, correctChoice);
            }
        }
    }
}
