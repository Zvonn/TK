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
        /// <summary>
        /// Вычисляет результаты экзамена на основе полученных баллов за задания
        /// </summary>
        /// <param name="task1">Баллы за задание 1 в строковом формате</param>
        /// <param name="task2">Баллы за задание 2 в строковом формате</param>
        /// <param name="task3">Баллы за задание 3 в строковом формате</param>
        /// <param name="task4">Баллы за задание 4 в строковом формате</param>
        /// <returns>Объект ExamResult с результатами проверки, суммой баллов и итоговой оценкой</returns>
        ExamResult CalculateResults(string task1, string task2, string task3, string task4);
    }

    public class ExamCalculatorService : IExamCalculatorService
    {
        /// <summary>
        /// Основной метод расчета экзаменационных результатов
        /// Проверяет валидность входных данных для каждого задания,
        /// вычисляет общую сумму баллов и определяет итоговую оценку.
        /// При обнаружении ошибок валидации добавляет их в результат.
        /// </summary>
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

        /// <summary>
        /// Валидирует баллы за задание
        /// </summary>
        /// <param name="input">Входное значение баллов</param>
        /// <param name="maxScore">Максимально допустимый балл для задания</param>
        /// <param name="taskName">Название задания для сообщений об ошибках</param>
        /// <param name="result">Объект результатов для записи ошибок</param>
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

        /// <summary>
        /// Определяет итоговую оценку по пятибалльной шкале на основе суммы баллов
        /// </summary>
        /// <param name="totalPoints">Общая сумма набранных баллов</param>
        /// <returns>
        /// 5 - при 90+ баллов,
        /// 4 - при 70-89 баллов,
        /// 3 - при 50-69 баллов,
        /// 2 - при менее 50 баллов
        /// </returns>
        private int CalculateGrade(int totalPoints)
        {
            if (totalPoints >= 90) return 5;
            if (totalPoints >= 70) return 4;
            if (totalPoints >= 50) return 3;
            return 2;
        }
    }
}