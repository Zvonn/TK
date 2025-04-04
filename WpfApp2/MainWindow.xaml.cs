using System.Text;
using System.Windows;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        private readonly IExamCalculatorService _calculator;

        public MainWindow()
        {
            InitializeComponent();
            _calculator = new ExamCalculatorService();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            var result = _calculator.CalculateResults(
                task1Box.Text,
                task2Box.Text,
                task3Box.Text,
                task4Box.Text
            );

            if (result.Errors.Count > 0)
            {
                resultText.Text = "Ошибки ввода:\n" + string.Join("\n", result.Errors);
            }
            else
            {
                resultText.Text = $"Сумма баллов: {result.TotalPoints}\nОценка: {result.Grade}";
            }
        }
    }
}