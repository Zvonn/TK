using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfApp2;
using System.Linq;

namespace ExamCalculatorTests
{
    [TestClass]
    public class ExamCalculatorTestss
    {
        private readonly ExamCalculatorService _calculator = new ExamCalculatorService();

        [TestMethod]
        public void CalculateGrade_90Points_Returns5()
        {
            var result = _calculator.CalculateResults("10", "50", "30", "10");
            Assert.AreEqual(5, result.Grade);
            Assert.AreEqual(100, result.TotalPoints);
        }

        [TestMethod]
        public void CalculateGrade_89Points_Returns4()
        {
            var result = _calculator.CalculateResults("9", "50", "30", "0");
            Assert.AreEqual(4, result.Grade);
            Assert.AreEqual(89, result.TotalPoints);
        }

        [TestMethod]
        public void CalculateGrade_50Points_Returns3()
        {
            var result = _calculator.CalculateResults("0", "50", "0", "0");
            Assert.AreEqual(3, result.Grade);
            Assert.AreEqual(50, result.TotalPoints);
        }

        [TestMethod]
        public void CalculateGrade_49Points_Returns2()
        {
            var result = _calculator.CalculateResults("0", "49", "0", "0");
            Assert.AreEqual(2, result.Grade);
            Assert.AreEqual(49, result.TotalPoints);
        }

        [TestMethod]
        public void ValidateInput_AllInvalid_ReturnsAllErrors()
        {
            var result = _calculator.CalculateResults("-1", "abc", "31", "11");

            Assert.AreEqual(4, result.Errors.Count);
            Assert.IsTrue(result.Errors.Contains("• Задание 1: число должно быть от 0 до 10"));
            Assert.IsTrue(result.Errors.Contains("• Задание 2: введите целое число"));
            Assert.IsTrue(result.Errors.Contains("• Задание 3: число должно быть от 0 до 30"));
            Assert.IsTrue(result.Errors.Contains("• Задание 4: число должно быть от 0 до 10"));
        }

        [TestMethod]
        public void ValidateInput_EmptyValues_ReturnsErrors()
        {
            var result = _calculator.CalculateResults("", "", "", "");

            Assert.AreEqual(4, result.Errors.Count);
            Assert.IsTrue(result.Errors.All(e => e.EndsWith("введите целое число")));
        }

        [TestMethod]
        public void ValidateInput_DecimalValues_ReturnsErrors()
        {
            var result = _calculator.CalculateResults("5.5", "30.1", "15.9", "9.9");
            Assert.AreEqual(4, result.Errors.Count);
            Assert.IsTrue(result.Errors.All(e => e.Contains("целое число")));
        }

        [TestMethod]
        public void ValidateInput_CombinationValidInvalid_ReturnsPartialErrors()
        {
            var result = _calculator.CalculateResults("10", "60", "30", "10");

            Assert.AreEqual(1, result.Errors.Count);
            Assert.IsTrue(result.Errors.Contains("• Задание 2: число должно быть от 0 до 50"));
        }
    }
}