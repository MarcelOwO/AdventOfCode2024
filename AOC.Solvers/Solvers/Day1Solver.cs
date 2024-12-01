using AOC.Solvers.Interfaces;

namespace AOC.Solvers.Solvers;

public class Day1Solver : ISolver
{
    private readonly List<int> _leftList = new List<int>();
    private readonly List<int> _rightList = new List<int>();

    public async Task<string> SolveAsync(StreamReader input)
    {

        while (!input.EndOfStream)
        {
            var line = await input.ReadLineAsync();

            if (string.IsNullOrWhiteSpace(line)) continue;

            var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            _leftList.Add(numbers[0]);
            _rightList.Add(numbers[1]);
        }

        _leftList.Sort();
        _rightList.Sort();

        if (_leftList.Count != _rightList.Count) throw new Exception("Lists are not equal");

        var sum = _leftList.Select((t, i) => Math.Abs(t - _rightList[i])).Sum();

        return sum.ToString();
    }
}