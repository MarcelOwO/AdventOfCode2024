using AOC.Solvers.Interfaces;

namespace AOC.Solvers.Solvers;

public class Day1Solver : ISolver
{
    public async Task<string> SolvePart1Async(StreamReader input)
    {
        List<int> leftList = [];
        List<int> rightList = [];

        while (!input.EndOfStream)
        {
            var line = await input.ReadLineAsync();

            if (string.IsNullOrWhiteSpace(line)) continue;

            var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            leftList.Add(numbers[0]);
            rightList.Add(numbers[1]);
        }

        leftList.Sort();
        rightList.Sort();

        if (leftList.Count != rightList.Count) throw new Exception("Lists are not equal");

        var sum = leftList.Select((t, i) => Math.Abs(t - rightList[i])).Sum();

        return sum.ToString();
    }

//could refactor to not read the same input again but reuse it from the first part, but whatever it is already instant anyway
    public async Task<string> SolvePart2Async(StreamReader input)
    {
        input.BaseStream.Seek(0, SeekOrigin.Begin);

        List<int> leftList = [];
        Dictionary<int, int> rightList = [];

        while (!input.EndOfStream)
        {
            var line = await input.ReadLineAsync();

            if (string.IsNullOrWhiteSpace(line)) continue;

            var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            leftList.Add(numbers[0]);

            var number = numbers[1];

            if (!rightList.TryAdd(number, 1))
            {
                rightList[number] += 1;
            }
        }

        var sum = 0;

        foreach (var leftListNumber in leftList)
        {
            rightList.TryGetValue(leftListNumber, out var count);

            sum += leftListNumber * count;
        }

        return sum.ToString();
    }
}