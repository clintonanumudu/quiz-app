using System.Windows.Controls;
using System.Windows;

namespace QuizApp.Core
{
    class FontSize
    {
        public static void resize(Quiz window, double width, double height)
        {
            UIElement[] group1 = { window.question };
            int minSize = 24;
            double newSize = width / 50;
            resizeTo(group1, newSize, minSize);

            UIElement[] group2 = { window.username, window.timeLeft, window.points };
            minSize = 0;
            resizeTo(group2, newSize, minSize);

            UIElement[] group3 = { window.choiceA, window.choiceB, window.choiceC, window.choiceD };
            minSize = 20;
            resizeTo(group3, newSize, minSize);
        }
        public static void resizeTo(UIElement[] group, double newSize, int minSize)
        {
            foreach (UIElement element in group)
            {
                if (newSize < minSize)
                {
                    newSize = minSize;
                }
                if (element.GetType() == typeof(TextBlock))
                {
                    (element as TextBlock).FontSize = newSize;
                }
                else if (element.GetType() == typeof(Label))
                {
                    (element as Label).FontSize = newSize;
                }
            }
        }
    }
}
