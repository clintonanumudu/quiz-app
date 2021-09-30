using System.Windows.Controls;
using System;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Windows;
using System.Collections.Generic;
using QuizApp.MVVM.ViewModel;

namespace QuizApp.Core
{
    class Animations
    {
        public static void Welcome(Name page)
        {
            string name = page.nameField.Text;

            FadeOut();

            void FadeOut()
            {
                DoubleAnimation animation = new DoubleAnimation(0, TimeSpan.FromSeconds(1));
                page.message.BeginAnimation(TextBlock.OpacityProperty, animation);
                page.nameField.BeginAnimation(TextBlock.OpacityProperty, animation);
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(3);
                timer.IsEnabled = true;
                timer.Tick += FadeIn;
            }

            void FadeIn(object sender, EventArgs e)
            {
                (sender as DispatcherTimer).Stop();
                page.stackpanel.Children.Remove(page.nameField);
                page.message.Text = $"Welcome to the show, {name}!";
                DoubleAnimation animation = new DoubleAnimation(1, TimeSpan.FromSeconds(2));
                page.message.BeginAnimation(TextBlock.OpacityProperty, animation);
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(6);
                timer.IsEnabled = true;
                timer.Tick += FadeOut2;
            }
            void FadeOut2(object sender, EventArgs e)
            {
                (sender as DispatcherTimer).Stop();
                DoubleAnimation animation = new DoubleAnimation(0, TimeSpan.FromSeconds(2));
                page.message.BeginAnimation(TextBlock.OpacityProperty, animation);
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(3);
                timer.IsEnabled = true;
                timer.Tick += GotoQuiz;
            }
            void GotoQuiz(object sender, EventArgs e)
            {
                (sender as DispatcherTimer).Stop();
                page.NavigationService.Navigate(new Quiz(true, name));
            }
        }

        public static void FadeInQuiz(Quiz page)
        {
            page.container.Opacity = 0;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.IsEnabled = true;
            timer.Tick += FadeIn;

            void FadeIn(object sender, EventArgs e)
            {
                (sender as DispatcherTimer).Stop();
                DoubleAnimation animation = new DoubleAnimation(1, TimeSpan.FromSeconds(2));
                page.container.BeginAnimation(TextBlock.OpacityProperty, animation);
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(4);
                timer.IsEnabled = true;
                timer.Tick += StartTimer;
            }
            void StartTimer(object sender, EventArgs e)
            {
                (sender as DispatcherTimer).Stop();
                ViewModel viewModel = page.DataContext as ViewModel;
                viewModel.StartTimer();
            }
        }

        public static void WrongAnswer(Quiz page, char choice, char correctChoice)
        {
            MainWindow window = System.Windows.Application.Current.MainWindow as MainWindow;
            ViewModel viewModel = page.DataContext as ViewModel;

            viewModel.FreezeTimer();

            window.KeyDown -= page.KeyPress;

            int toggle = 0;

            int blinks = 0;
            int blinkAmount = 12;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.IsEnabled = true;
            timer.Tick += ShowCorrect;

            void ShowCorrect(object sender, EventArgs e)
            {
                (sender as DispatcherTimer).Stop();

                viewModel.FreezeTimer();

                IDictionary<char, Label> choiceElements = new Dictionary<char, Label>();
                choiceElements.Add('a', page.choiceA);
                choiceElements.Add('b', page.choiceB);
                choiceElements.Add('c', page.choiceC);
                choiceElements.Add('d', page.choiceD);

                Label wrongElement = choiceElements[choice];
                Label correctElement = choiceElements[correctChoice];

                DispatcherTimer timer = new DispatcherTimer();

                var bc = new BrushConverter();
                Brush[] colors = { (Brush)bc.ConvertFrom("#a80000"), (Brush)bc.ConvertFrom("#008f0e") };
                if (toggle == 0)
                {
                    timer.Interval = TimeSpan.FromSeconds(0.5);
                    toggle = 1;
                    blinks++;
                    wrongElement.Background = colors[0];
                    wrongElement.Foreground = Brushes.White;
                    correctElement.Background = colors[1];
                    correctElement.Foreground = Brushes.White;
                    correctElement.BorderBrush = Brushes.White;
                    correctElement.BorderThickness = new Thickness(2);
                }
                else
                {
                    timer.Interval = TimeSpan.FromSeconds(0.25);
                    toggle = 0;
                    wrongElement.Background = Brushes.White;
                    wrongElement.Foreground = Brushes.Black;
                    correctElement.Background = Brushes.White;
                    correctElement.Foreground = Brushes.Black;
                }
                timer.IsEnabled = true;
                timer.Tick += ShowCorrect;
                if (blinks == blinkAmount + 1)
                {
                    timer.Stop();
                    ViewModel viewModel = page.DataContext as ViewModel;
                    viewModel.Eliminate();
                }
            }
        }
        public static void CorrectAnswer(Quiz page, char choice)
        {
            MainWindow window = System.Windows.Application.Current.MainWindow as MainWindow;
            ViewModel viewModel = page.DataContext as ViewModel;

            window.KeyDown -= page.KeyPress;

            viewModel.FreezeTimer();

            int toggle = 0;

            int blinks = 0;
            int blinkAmount = 12;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.IsEnabled = true;
            timer.Tick += ShowCorrect;

            void ShowCorrect(object sender, EventArgs e)
            {
                (sender as DispatcherTimer).Stop();
                Label element = page.choiceA;
                if (choice == 'a')
                {
                    element = page.choiceA;
                }
                else if (choice == 'b')
                {
                    element = page.choiceB;
                }
                else if (choice == 'c')
                {
                    element = page.choiceC;
                }
                else if (choice == 'd')
                {
                    element = page.choiceD;
                }

                DispatcherTimer timer = new DispatcherTimer();

                var bc = new BrushConverter();
                Brush[] colors = { (Brush)bc.ConvertFrom("#008f0e"), (Brush)bc.ConvertFrom("#1c50c9") };
                if (toggle == 0)
                {
                    timer.Interval = TimeSpan.FromSeconds(0.5);
                    toggle = 1;
                }
                else
                {
                    timer.Interval = TimeSpan.FromSeconds(0.25);
                    toggle = 0;
                    blinks++;
                }
                timer.IsEnabled = true;
                timer.Tick += ShowCorrect;
                element.Background = colors[toggle];
                if (blinks == blinkAmount + 1)
                {
                    timer.Stop();
                    viewModel.AddPoints(choice);
                }
            }
        }
    }
}
