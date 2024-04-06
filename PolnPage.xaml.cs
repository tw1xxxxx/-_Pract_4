using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace WpfApp1
{
    public partial class PolnPage : Page
    {
        public PolnPage(string question, string correctAnswer, string userAnswer)
        {
            InitializeComponent();

            DisplayQuestionAndAnswers(question, correctAnswer, userAnswer);
        }

        public PolnPage()
        {
        }

        private void DisplayQuestionAndAnswers(string question, string correctAnswer, string userAnswer)
        {

            var questionTextBlock = new TextBlock
            {
                Text = $"Вопрос: {question}",
                Margin = new Thickness(5),
                Foreground = Brushes.White,
                FontWeight = FontWeights.Bold
            };
            var correctAnswerTextBlock = new TextBlock
            {
                Text = $"Правильный ответ: {correctAnswer}",
                Margin = new Thickness(5),
                Foreground = Brushes.White
            };
            var userAnswerTextBlock = new TextBlock
            {
                Text = $"Ваш ответ: {userAnswer}",
                Margin = new Thickness(5),
                Foreground = Brushes.White
            };

            MainStackPanel.Children.Add(questionTextBlock);
            MainStackPanel.Children.Add(correctAnswerTextBlock);
            MainStackPanel.Children.Add(userAnswerTextBlock);
        }

      
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            ReturnToMainWindow();
        }


        private void ReturnToMainWindow()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            CloseCurrentWindow();
        }

        private void CloseCurrentWindow()
        {
            Window window = Window.GetWindow(this);
            window.Close();
        }
    }
}