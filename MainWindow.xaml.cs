using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private const string CorrectPassword = "123456789";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void GoToTestButton_Click(object sender, RoutedEventArgs e)
        {
            TestResultPage testResultPage = new TestResultPage();
            this.Content = testResultPage;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            string enteredPassword = PasswordBox.Password;


            if (enteredPassword == CorrectPassword)
            {

                TestWindow editWindow = new TestWindow();
                this.Content = editWindow;

            }
          
            else
            {
                MessageBox.Show("Неправильный пароль! Попробуйте снова.");
                PasswordBox.Clear(); 
            }
        }

      
    }
}