using System.ComponentModel;
using System.Data;
using AOC.Solvers.Interfaces;

public class Day2Solver : ISolver
{
    public async Task<string> SolvePart1Async(StreamReader input)
    {
        int count = 0;

        while (!input.EndOfStream)
        {
            var line = await input.ReadLineAsync();
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var list = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            var diff = list[0] - list[1];

            if (Math.Abs(diff) > 3)
            {
                continue;
            }

            if (diff == 0)
            {
                continue;
            }

            bool isIncreasing = diff < 0;

            for (int i = 1; i < list.Count; i++)
            {
                if (i == list.Count - 1)
                {
                    count++;
                    break;
                }

                var difference = list[i] - list[i + 1];

                if (Math.Abs(difference) > 3)
                {
                    break;
                }

                if (difference == 0)
                {
                    break;
                }

                if (isIncreasing)
                {
                    if (difference > 0)
                    {
                        break;
                    }
                }
                else
                {
                    if (difference < 0)
                    {
                        break;
                    }
                }
            }
        }

        return count.ToString();
    }


    public async Task<string> SolvePart2Async(StreamReader input)
    {
        input.BaseStream.Seek(0, SeekOrigin.Begin);

        int count = 0;

        while (!input.EndOfStream)
        {
            var line = await input.ReadLineAsync();

            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var list = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            var diffs = new int[list.Count - 1];

            int countIncrease = 0;

            for (var i = 0; i < list.Count - 1; i++)
            {
                var temp = list[i] - list[i + 1];

                countIncrease += temp switch
                {
                    > 0 => 1,
                    < 0 => -1,
                    _ => 0
                };

                diffs[i] = temp;
            }

            int[] mistakes = new int[list.Count - 1];
            for (int i = 0; i < mistakes.Length; i++)
            {
                mistakes[i] = (diffs[i], countIncrease) switch
                {
                    (> 3 or < -3, _) => 9,
                    (0, _) => 1,
                    (< 0, > 0) => 1,
                    (> 0, < 0) => 1,
                    _ => 0
                };
            }

            var mistakesCount = mistakes.Sum();

            switch (mistakesCount)
            {
                case < 2:
                    count++;
                    continue;
                case 9 when (mistakes[0] == 9 || mistakes[1] == 9):
                    count++;
                    continue;
            }
        }

        return count.ToString();
    }
}