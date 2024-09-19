using App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class RomanNumberValidatorTest
    {
        [TestMethod]
        public void DigitValueTest()
        {
            //Dictionary<char, int> testCases = new()
            //{
            //    {'N', 0},
            //    {'I', 1},
            //    {'V', 5},
            //    {'X', 10},
            //    {'L', 50},
            //    {'C', 100},
            //    {'D', 500},
            //    {'M', 1000},
            //};
            //foreach (var testCase in testCases)
            //{
            //    Assert.AreEqual(testCase.Value,
            //        RomanNumber.DigitValue(testCase.Key),
            //        $"{testCase.Key} => {testCase.Value}"
            //        );
            //}

            //char[] excCases = { '1', 'x', 'i', '&' };

            //foreach (var testCase in excCases)
            //{
            //    var ex = Assert.ThrowsException<ArgumentException>(
            //    () => RomanNumberValidator.DigitValue(testCase),
            //    $"DigitValue({testCase}) must throw ArgumentException"
            //    );
            //    Assert.IsTrue(
            //    ex.Message.Contains($"'{testCase}'"),
            //        "DigitValue ex.Message should contain a symbol which cause exception:" +
            //        $" symbol: '{testCase}', ex.Message: '{ex.Message}'"
            //        );
            //    Assert.IsTrue(
            //        ex.Message.Contains($"{nameof(RomanNumber)}") &&
            //        ex.Message.Contains($"{nameof(RomanNumber.DigitValue)}"),
            //        "DigitValue ex.Message should contain a symbol which cause exception:" +
            //        $" symbol: '{testCase}', ex.Message: '{ex.Message}'"
            //        );
            //}

            #region HW2

            char[] excCases = { '0', '1', 'x', 'i', '&' };

            foreach (var digit in excCases)
            {
                var ex = Assert.ThrowsException<ArgumentException>(
                () => RomanNumberValidator.DigitValue(digit),
                $"DigitValue({digit}) must throw ArgumentException"
                );
                Assert.IsTrue(
                    ex.Message.Contains($"'{digit}'"),
                    "Not valid Roman digit:" +
                    $" argument: ('{digit}'), ex.Message: {ex.Message}"
                    );
                Assert.IsTrue(
                    ex.Message.Contains($"{nameof(RomanNumber)}") &&
                    ex.Message.Contains($"DigitValue"),
                    "Not valid Roman digit:" +
                    $" argument: ('{digit}'), ex.Message: {ex.Message}"
                    );
            }
            #endregion
        }

        [TestMethod]
        public void DigitRatiosTest()
        {
            Object[][] exCases2 = [
                ["VX", "V", "X", 0],  // ---
                ["LC", "L", "C", 0],  // "відстань" між цифрами при відніманні:
                ["DM", "D", "M", 0],  // відніматись можуть I, X, СI причому від
                ["IC", "I", "C", 0],  // двох сусідніх цифр (I - від V та X, ... )
                ["MIM", "I", "M", 1],
                ["MVM", "V", "M", 1],
                ["MXM", "X", "M", 1],
                ["CVC", "V", "C", 1],
                ["MCVC", "V", "C", 2],
                ["DCIC", "I", "C", 2],
                ["IM", "I", "M", 0],
            ];
            foreach (var exCase in exCases2)
            {
                var ex = Assert.ThrowsException<FormatException>(
                    () => RomanNumber.Parse(exCase[0].ToString()!),
                    $"RomanNumber.Parse(\"{exCase[0]}\") must throw FormatException"
                    );
                Assert.IsTrue(
                    ex.Message.Contains($"illegal sequence: '{exCase[1]}' before '{exCase[2]}'"),
                    $"ex.Message must contain symbols which cause error: '{exCase[1]}' and '{exCase[2]}', testCase: '{exCase[0]}', ex.Message: {ex.Message}"
                    );
                Assert.IsTrue(
                    ex.Message.Contains($"in position {exCase[3]}"),
                    $"ex.Message must contain error symbol position, testCase: '{exCase[0]}'"
                    );
                Assert.IsTrue(
                    ex.Message.Contains(nameof(RomanNumber)) &&
                    ex.Message.Contains(nameof(RomanNumber)),
                    $"ex.Message must contain names of class and method, testCase: '{exCase[0]}', ex.Message: {ex.Message}"
                    );
            }
        }

        [TestMethod]
        public void InvalidLessCounterTest()
        {
            Object[][] exCases3 = [
                [ "IIX", 'X', 2 ],   // Перед цифрою є декілька цифр, менших за неї
                [ "VIX", 'X', 2 ],   // !! кожна пара цифр - правильна комбінація,
                [ "XXC", 'C', 2 ],   //    проблема створюється щонайменше трьома цифрами
                [ "IXC", 'C', 2 ],
                [ "IIIV", 'V', 3]
            ];
            foreach (var exCase in exCases3)
            {
                var ex = Assert.ThrowsException<FormatException>(
                    () => RomanNumber.Parse(exCase[0].ToString()!),
                    $"RomanNumber.Parse(\"{exCase[0]}\") must throw FormatException"
                    );
                Assert.IsTrue(
                    ex.Message.Contains($"illegal sequence: more than one smaller digits before '{exCase[1]}'"),
                    $"ex.Message must contain symbol before error: '{exCase[1]}', testCase: '{exCase[0]}', ex.Message: {ex.Message}"
                    );
                Assert.IsTrue(
                    ex.Message.Contains($"in position {exCase[2]}"),
                    $"ex.Message must contain error symbol position, testCase: '{exCase[0]}'"
                    );
                Assert.IsTrue(
                    ex.Message.Contains(nameof(RomanNumber)) &&
                    ex.Message.Contains(nameof(RomanNumber)),
                    $"ex.Message must contain names of class and method, testCase: '{exCase[0]}', ex.Message: {ex.Message}"
                    );
            }
        }

        [TestMethod]
        public void MaxAndLessCountersTest()
        {
            Object[][] exCases4 = [
                [ "IXX", 'I', 0 ],
                [ "IXXX", 'I', 0],
                [ "XCC", 'X', 0 ],
                [ "XCCC", 'X', 0],
                [ "CXCC", 'X', 1],
                [ "CMM", 'C', 0 ],
                [ "CMMM", 'C', 0],
                [ "MCMM", 'C', 1],
                [ "LCC", 'L', 0 ],
                [ "ICCC", 'I', 0]
            ];
            foreach (var exCase in exCases4)
            {
                var ex = Assert.ThrowsException<FormatException>(
                    () => RomanNumber.Parse(exCase[0].ToString()!),
                    $"RomanNumber.Parse(\"{exCase[0]}\") must throw FormatException"
                    );
            }
        }

        [TestMethod]
        public void CheckZeroDigitTest()
        {
            Object[][] exCases5 = [
                [ "NN",   '0', 1 ],   // Цифра N не може бути у числі, тільки
                [ "IN",   '1', 1 ],   // сама по собі
                [ "NX",   '0', 0 ],
                [ "NC",   '1', 1 ],
                [ "XNC",  '1', 1 ],
                [ "XVIN", '3', 3 ],
                [ "XNNC", '1', 1 ],
                [ "NMC",  '1', 1 ],
                [ "NIX",  '1', 1 ]
            ];
            foreach (var exCase in exCases5)
            {
                var ex = Assert.ThrowsException<FormatException>(
                    () => RomanNumberValidator.CheckZeroDigit(exCase[0].ToString()!),
                    $"RomanNumber.CheckZeroDigit(\"{exCase[0]}\") must throw FormatException"
                    );
            }
        }
    }
}
