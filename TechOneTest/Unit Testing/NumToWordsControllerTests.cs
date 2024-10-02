using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechOneTest.Controllers;

namespace TechOneTest.Unit_Testing
{
    [TestClass]
    public class NumToWordsControllerTests
    {
        [TestMethod]
        public void ConvertToWords_ShouldReturnExpectedResult_ForWholeNumber()
        {
            decimal number = 123;
            string expected = "ONE HUNDRED AND TWENTY-THREE DOLLARS";
            string result = NumToWordsController.ConvertToWords(number);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ConvertToWords_ShouldReturnExpectedResult_ForNumberWithCents()
        {
            decimal number = 123.45M;
            string expected = "ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS";
            string result = NumToWordsController.ConvertToWords(number);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ConvertToWords_ShouldReturnExpectedResult_ForZero()
        {
            decimal number = 0;
            string expected = "ZERO DOLLARS";
            string result = NumToWordsController.ConvertToWords(number);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ConvertToWords_ShouldReturnExpectedResult_ForLargeNumber()
        {
            decimal number = 1000000;
            string expected = "ONE MILLION DOLLARS";
            string result = NumToWordsController.ConvertToWords(number);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ConvertToWords_ShouldReturnExpectedResult_ForNumberWithOneCent()
        {
            decimal number = 1.01M;
            string expected = "ONE DOLLAR AND ONE CENT";
            string result = NumToWordsController.ConvertToWords(number);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ConvertToWords_ShouldNotContainExtraSpaces()
        {
            decimal number = 1000000;
            string expected = "ONE MILLION DOLLARS";
            string result = NumToWordsController.ConvertToWords(number);

            Assert.AreEqual(expected, result);
            Assert.IsFalse(result.Contains("  "), "Result contains extra spaces");
        }
    }
}
