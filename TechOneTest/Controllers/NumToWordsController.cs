using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using System.Numerics;

namespace TechOneTest.Controllers
{
    public class NumToWordsController : Controller
    {
        public class ConversionRequest
        {
            public string? number { get; set; }
        }

        private static string[] ones = {
                "ZERO", "ONE", "TWO", "THREE", "FOUR",
                "FIVE", "SIX", "SEVEN", "EIGHT", "NINE"
            };

        private static string[] teens = {
                "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN",
                "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN"
            };

        private static string[] tens = {
                "TWENTY", "THIRTY", "FORTY", "FIFTY",
                "SIXTY", "SEVENTY", "EIGHTY", "NINETY"
            };

        private static string[] groups = {
                "", "THOUSAND", "MILLION", "BILLION", "TRILLION",
                "QUADRILLION", "QUINTILLION", "SEXTILLION", "SEPTILLION",
                "OCTILLION", "NONILLION", "DECILLION"
            };

        public static string ConvertToWords(decimal number)
        {
            string result = "";

            BigInteger dollars = (BigInteger)number;
            int cents = (int)((number - (decimal)dollars) * 100);

            if (dollars > 0)
            {
                for (int i = groups.Length - 1; i >= 0; i--)
                {
                    BigInteger groupValue = BigInteger.Pow(1000, i);
                    int groupNumber = (int)(dollars / groupValue % 1000);
                    if (groupNumber > 0)
                    {
                        result += ConvertThreeDigitGroup(groupNumber, groups[i]) + " ";
                    }
                }
                result += (dollars == 1 ? " DOLLAR" : " DOLLARS");
            }
            else
            {
                result += "ZERO DOLLARS";
            }

            if (cents > 0)
            {
                result += " AND ";
                result += ConvertTwoDigitGroup(cents);
                result += (cents == 1 ? " CENT" : " CENTS");
            }

            // Trim and replace multiple spaces with a single space
            return System.Text.RegularExpressions.Regex.Replace(result.Trim(), @"\s+", " ");
        }

        private static string ConvertThreeDigitGroup(int number, string groupLabel)
        {
            if (number == 0)
            {
                return "";
            }

            string groupWords = "";

            int hundreds = number / 100;
            int remainder = number % 100;

            if (hundreds > 0)
            {
                groupWords += ones[hundreds] + " HUNDRED";
            }

            if (remainder > 0)
            {
                if (hundreds > 0)
                {
                    groupWords += " AND ";
                }

                if (remainder < 10)
                {
                    groupWords += ones[remainder];
                }
                else if (remainder < 20)
                {
                    groupWords += teens[remainder - 10];
                }
                else
                {
                    groupWords += tens[remainder / 10 - 2];
                    if (remainder % 10 > 0)
                    {
                        groupWords += "-" + ones[remainder % 10];
                    }
                }
            }

            if (!string.IsNullOrEmpty(groupLabel))
            {
                groupWords += " " + groupLabel;
            }

            return groupWords.Trim();
        }

        private static string ConvertTwoDigitGroup(int number)
        {
            if (number < 10)
            {
                return ones[number];
            }
            else if (number < 20)
            {
                return teens[number - 10];
            }
            else
            {
                string result = tens[number / 10 - 2];
                if (number % 10 > 0)
                {
                    result += "-" + ones[number % 10];
                }
                return result;
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("convert")]
        public async Task<IActionResult> Convert()
        {
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                string requestBody = await reader.ReadToEndAsync();

                try
                {
                    ConversionRequest? body = JsonSerializer.Deserialize<ConversionRequest>(requestBody);

                    if (body?.number == null)
                    {
                        return BadRequest("Invalid input.");
                    }

                    string words = ConvertToWords(decimal.Parse(body.number));
                    return Json(new { words });
                }
                catch (JsonException)
                {
                    return BadRequest("Invalid input.");
                }
                catch (OverflowException)
                {
                    return BadRequest("Number too large.");
                }
            }
        }
    }
}
