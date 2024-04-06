using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;

namespace WpfApp1
{
    public partial class TestWindow : Page
    {
        private ObservableCollection<QuestionAnswer> questionAnswers;

        public TestWindow()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            questionAnswers = new ObservableCollection<QuestionAnswer>();
            TestGrid.ItemsSource = questionAnswers;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                string json = File.ReadAllText("data.json");
                questionAnswers = JsonConvert.DeserializeObject<ObservableCollection<QuestionAnswer>>(json);
                TestGrid.ItemsSource = questionAnswers;
            }
            catch (Exception)
            {
         
            }
        }

        private void SaveData()
        {
            string json = JsonConvert.SerializeObject(questionAnswers);
            File.WriteAllText("data.json", json);
        }
        private void TestGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtQuestion.Text) && !string.IsNullOrWhiteSpace(txtAnswer.Text))
            {
                var qa = new QuestionAnswer
                {
                    Question = txtQuestion.Text,
                    Answer = txtAnswer.Text
                };

                questionAnswers.Add(qa);

                txtQuestion.Clear();
                txtAnswer.Clear();

                SaveData();
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните пустые поля", "Пустые поля", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveData();
            MessageBox.Show("Тест сохранен", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            Window window = Window.GetWindow(this);
            window.Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var qa = button?.DataContext as QuestionAnswer;
            if (qa != null)
            {
                questionAnswers.Remove(qa);
                SaveData();
            }
        }
    }

    public class QuestionAnswer : System.ComponentModel.INotifyPropertyChanged
    {
        private string question;
        private string answer;

        public string Question
        {
            get { return question; }
            set
            {
                if (value != question)
                {
                    question = value;
                    OnPropertyChanged("Question");
                }
            }
        }

        public string Answer
        {
            get { return answer; }
            set
            {
                if (value != answer)
                {
                    answer = value;
                    OnPropertyChanged("Answer");
                }
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
    }
}