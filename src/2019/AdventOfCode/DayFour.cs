using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solutions.Days
{
    public class DayFour
    {
        public void Run()
        {
            var inputs = new int[]
            {
                264360, 746325
            };

            Console.WriteLine(ProblemOne(inputs));
            Console.WriteLine(ProblemTwo(inputs));
        }

        public int ProblemOne(int[] inputs)
        {
            return Enumerable.Range(inputs[0], inputs[1] - inputs[0] + 1)
                .Select(i => i.ToString().ToCharArray())
                .Where(ContainsDouble)
                .Count(HasNonDecreasingNumbers);
        }

        public int ProblemTwo(int[] inputs)
        {
            return Enumerable.Range(inputs[0], inputs[1] - inputs[0] + 1)
                .Select(i => i.ToString().ToCharArray())
                .Where(ContainsDoubleAdjusted)
                .Where(HasNonDecreasingNumbers)
                .Select(chars => string.Join("", chars))
                .Count();
        }

        private bool HasNonDecreasingNumbers(char[] s)
        {
            return Enumerable.Range(0, s.Length - 1)
                .All(i => s[i] <= s[i+1]);
        }

        private bool ContainsDouble(char[] s)
        {
            return s.Length != s.Distinct().Count();
        }

        private bool ContainsDoubleAdjusted(char[] s)
        {
            var dictionary = new Dictionary<char, int>();
            foreach (var c in s)
            {
                if (!dictionary.ContainsKey(c))
                {
                    dictionary.Add(c, 0);
                }

                dictionary[c]++;
            }

            return dictionary.Values.Any(i => i == 2);
        }
    }
}