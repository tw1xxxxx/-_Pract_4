using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfApp1
{
    public partial class Result : Page
    {
        private int CorrectCount { get; }
        private int TotalCount { get; }

        public Result(int correctCount, int totalCount)
        {
            InitializeComponent();
            CorrectCount = correctCount;
            TotalCount = totalCount;
            UpdateResultText();
        }

        private void UpdateResultText()
        {
            txtResult.Text = $"Правильных ответов: {CorrectCount} из {TotalCount}";
        }

        private void btnShowFullAnswers_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new PolnPage());
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