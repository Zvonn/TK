using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class ExamResult
    {
        public int TotalPoints { get; set; }
        public int Grade { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }

    public interface IExamCalculatorService
    {
        ExamResult CalculateResults(string task1, string task2, string task3, string task4);
    }

    public class ExamCalculatorService : IExamCalculatorService
    {
        public ExamResult CalculateResults(string task1, string task2, string task3, string task4)
        {
            var result = new ExamResult();
            ValidateTask(task1, 10, "Задание 1", result);
            ValidateTask(task2, 50, "Задание 2", result);
            ValidateTask(task3, 30, "Задание 3", result);
            ValidateTask(task4, 10, "Задание 4", result);

            if (result.Errors.Count == 0)
            {
                result.TotalPoints = int.Parse(task1) + int.Parse(task2) + int.Parse(task3) + int.Parse(task4);
                result.Grade = CalculateGrade(result.TotalPoints);
            }

            return result;
        }

        private void ValidateTask(string input, int maxScore, string taskName, ExamResult result)
        {
            if (!int.TryParse(input, out int score))
            {
                result.Errors.Add($"• {taskName}: введите целое число");
                return;
            }

            if (score < 0 || score > maxScore)
            {
                result.Errors.Add($"• {taskName}: число должно быть от 0 до {maxScore}");
            }
        }

        private int CalculateGrade(int totalPoints)
        {
            if (totalPoints >= 90) return 5;
            if (totalPoints >= 70) return 4;
            if (totalPoints >= 50) return 3;
            return 2;
        }
    }
}
