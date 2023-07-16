using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solutions.Days
{
    public class DayTwo
    {
        public void Run()
        {
            var inputs = new int[]
            {
                1, 0, 0, 3, 1, 1, 2, 3, 1, 3, 4, 3, 1, 5, 0, 3, 2, 1, 6, 19, 1, 19, 6, 23, 2, 23, 6, 27, 2, 6, 27, 31,
                2, 13, 31, 35, 1, 9, 35, 39, 2, 10, 39, 43, 1, 6, 43, 47, 1, 13, 47, 51, 2, 6, 51, 55, 2, 55, 6, 59, 1,
                59, 5, 63, 2, 9, 63, 67, 1, 5, 67, 71, 2, 10, 71, 75, 1, 6, 75, 79, 1, 79, 5, 83, 2, 83, 10, 87, 1, 9,
                87, 91, 1, 5, 91, 95, 1, 95, 6, 99, 2, 10, 99, 103, 1, 5, 103, 107, 1, 107, 6, 111, 1, 5, 111, 115, 2,
                115, 6, 119, 1, 119, 6, 123, 1, 123, 10, 127, 1, 127, 13, 131, 1, 131, 2, 135, 1, 135, 5, 0, 99, 2, 14,
                0, 0
            };

            Console.WriteLine(ProblemOne(inputs, 12, 2));
            Console.WriteLine(ProblemTwo(inputs));
        }

        public int ProblemOne(int[] originalInput, int noun, int verb)
        {
            var inputs = new int[originalInput.Length];
            originalInput.CopyTo(inputs, 0);

            inputs[1] = noun;
            inputs[2] = verb;

            for (var i = 0; i < inputs.Length; i += 4)
            {
                var op = inputs[i];
                if (inputs[i] == 99)
                    break;

                var a = inputs[i + 1];
                var b = inputs[i + 2];
                var c = inputs[i + 3];

                if (a >= inputs.Length || b >= inputs.Length || c >= inputs.Length)
                    return -1;

                inputs[c] = (op == 1) ? inputs[a] + inputs[b] : inputs[a] * inputs[b];
            }

            return inputs[0];
        }

        public int ProblemTwo(int[] inputs)
        {
            var range = Enumerable.Range(0, 100);
            var result = range.SelectMany(noun => range, (k, j) => new
                {
                    k, 
                    j, 
                    computed = ProblemOne(inputs, k, j)
                })
                .Where(i => i.computed == 19690720)
                .Select(i => 100 * i.k + i.j)
                .FirstOrDefault();

            return result;
        }
    }
}