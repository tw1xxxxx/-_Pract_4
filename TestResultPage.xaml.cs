using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Newtonsoft.Json;

namespace WpfApp1
{
    public partial class TestResultPage : Page
    {
        private List<QuestionAnswer> questionAnswers;

        public TestResultPage()
        {
            InitializeComponent();
            LoadQuestions();
            DisplayQuestions();
        }

        private void LoadQuestions()
        {
            try
            {
                string json = File.ReadAllText("data.json");
                questionAnswers = JsonConvert.DeserializeObject<List<QuestionAnswer>>(json);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Файл с вопросами не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                questionAnswers = new List<QuestionAnswer>();
            }
            catch (JsonException)
            {
                MessageBox.Show("Ошибка при чтении файла с вопросами.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                questionAnswers = new List<QuestionAnswer>();
            }
        }

        private void DisplayQuestions()
        {
            foreach (var question in questionAnswers)
            {
                var questionPanel = CreateQuestionPanel(question);
                MainStackPanel.Children.Add(questionPanel);
            }
        }

        private StackPanel CreateQuestionPanel(QuestionAnswer question)
        {
            var questionPanel = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(0, 5, 0, 5)
            };

            var questionTextBlock = new TextBlock
            {
                Text = question.Question,
                Margin = new Thickness(5),
                TextWrapping = TextWrapping.Wrap,
                Foreground = Brushes.White,

                FontWeight = FontWeights.Bold
            };
            questionPanel.Children.Add(questionTextBlock);

            var answerTextBox = new TextBox
            {
                Margin = new Thickness(5),
                VerticalAlignment = VerticalAlignment.Center,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1)
            };
            questionPanel.Children.Add(answerTextBox);

            return questionPanel;
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            int correctCount = CalculateCorrectAnswers();
            ShowResult(correctCount);
        }

        private int CalculateCorrectAnswers()
        {
            int correctCount = 0;
            for (int i = 0; i < questionAnswers.Count; i++)
            {
                var answerTextBox = (TextBox)((StackPanel)MainStackPanel.Children[i]).Children[1];
                if (answerTextBox.Text.Trim() == questionAnswers[i].Answer)
                {
                    correctCount++;
                }
                else
                {
                    answerTextBox.Background = Brushes.Red;
                }
            }
            return correctCount;
        }

        private void ShowResult(int correctCount)
        {
            foreach (var question in questionAnswers)
            {
                var answerTextBox = (TextBox)((StackPanel)MainStackPanel.Children[questionAnswers.IndexOf(question)]).Children[1];
                PolnPage polnPage = new PolnPage(question.Question, question.Answer, answerTextBox.Text);
                mainResult.NavigationService.Navigate(polnPage);
            }

            Result resultPage = new Result(correctCount, questionAnswers.Count);
            mainResult.NavigationService.Navigate(resultPage);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            Window window = Window.GetWindow(this);
            window.Close();
        }
    }
}