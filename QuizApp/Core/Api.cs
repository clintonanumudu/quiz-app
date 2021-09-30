using System.IO;
using System.Net;
using System.Web;
using System;
using System.Linq;
using QuizApp.MVVM.Model;

namespace QuizApp.Core
{
    public class Api
    {
        public Question GetQuestion()
        {
            int[] categories = { 17, 18, 19, 22, 27, 30, 32 };

            int randomCategory = categories[new Random().Next(categories.Length)];

            System.Windows.MessageBox.Show(randomCategory.ToString());

            string apiURL = $"https://opentdb.com/api.php?amount=1&category={randomCategory.ToString()}&difficulty=easy&type=multiple";
            
            WebRequest request = WebRequest.Create(apiURL);

            WebResponse webResponse = request.GetResponse();

            Stream dataStream = webResponse.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);
            
            string response = reader.ReadToEnd();

            webResponse.Close();

            int start = response.IndexOf("\",\"question\":\"") + "\",\"question\":\"".Length;
            string theQuestion = response.Substring(start, response.IndexOf("\"", start) - start);

            string[] choices = new string[4];

            start = response.IndexOf("\",\"correct_answer\":\"") + "\",\"correct_answer\":\"".Length;
            choices[0] = response.Substring(start, response.IndexOf("\"", start) - start);
            string correctAnswer = choices[0];

            start = response.IndexOf("\",\"incorrect_answers\":[\"") + "\",\"incorrect_answers\":[\"".Length;
            choices[1] = response.Substring(start, response.IndexOf("\"", start) - start);

            start = response.IndexOf("\",\"incorrect_answers\":[\"" + choices[1] + "\",\"") + ("\",\"incorrect_answers\":[\"" + choices[1] + "\",\"").Length;
            choices[2] = response.Substring(start, response.IndexOf("\"", start) - start);

            start = response.IndexOf("\",\"incorrect_answers\":[\"" + choices[1] + "\",\"" + choices[2] + "\",\"") + ("\",\"incorrect_answers\":[\"" + choices[1] + "\",\"" + choices[2] + "\",\"").Length;
            choices[3] = response.Substring(start, response.IndexOf("\"", start) - start);

            Random random = new Random();

            choices = choices.OrderBy(x => random.Next()).ToArray();

            int correctPos = Array.IndexOf(choices, correctAnswer);

            char correctChoice = 'a';

            if (correctPos == 0)
            {
                correctChoice = 'a';
            }
            else if (correctPos == 1)
            {
                correctChoice = 'b';
            }
            else if (correctPos == 2)
            {
                correctChoice = 'c';
            }
            else if (correctPos == 3)
            {
                correctChoice = 'd';
            }

            Question question = new Question
            {
                TheQuestion = HttpUtility.HtmlDecode(theQuestion),
                ChoiceA = HttpUtility.HtmlDecode(choices[0]),
                ChoiceB = HttpUtility.HtmlDecode(choices[1]),
                ChoiceC = HttpUtility.HtmlDecode(choices[2]),
                ChoiceD = HttpUtility.HtmlDecode(choices[3]),
                TimeLeft = "02:00",
                CurrentChoice = 'a',
                CorrectChoice = correctChoice
            };

            return question;
        }
    }
}