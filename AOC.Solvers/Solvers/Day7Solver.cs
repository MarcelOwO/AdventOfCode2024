using AOC.Solvers.Interfaces;

public class Day7Solver : ISolver
{
    public async Task<string> SolvePart1Async(StreamReader input)
    {
        ulong count = 0;
        while (!input.EndOfStream)
        {
            var line = await input.ReadLineAsync();
            if (string.IsNullOrWhiteSpace(line)) continue;

            var parts = line.Split(":", StringSplitOptions.TrimEntries);
            var result = ulong.Parse(parts[0]);
            var nums = parts[1].Split(" ", StringSplitOptions.TrimEntries).Select(ulong.Parse).ToList();

            List<ulong> possible = [nums[0]];

            for (int i = 0; i < nums.Count - 1; i++)
            {
                var newPossible = new List<ulong>();

                foreach (var last in possible)
                {
                    newPossible.Add(last + nums[i + 1]);
                    newPossible.Add(last * nums[i + 1]);
                }
                possible = newPossible;
            }

            if (possible.Contains(result))
            {
                count += result;
            }
        }
        return count.ToString();
    }

    public async Task<string> SolvePart2Async(StreamReader input)
    {
        input.BaseStream.Seek(0, SeekOrigin.Begin);
        ulong count = 0;
        while (!input.EndOfStream)
        {
            var line = await input.ReadLineAsync();
            if (string.IsNullOrWhiteSpace(line)) continue;

            var parts = line.Split(":", StringSplitOptions.TrimEntries);
            var result = ulong.Parse(parts[0]);
            var nums = parts[1].Split(" ", StringSplitOptions.TrimEntries).Select(ulong.Parse).ToList();

            List<ulong> possible = [nums[0]];

            for (int i = 0; i < nums.Count - 1; i++)
            {
                var newPossible = new List<ulong>();

                foreach (var last in possible)
                {
                    newPossible.Add(last + nums[i + 1]);
                    newPossible.Add(last * nums[i + 1]);
                    newPossible.Add(ulong.Parse(last.ToString()+nums[i + 1].ToString()));
                }

                possible = newPossible;
            }

            if (possible.Contains(result))
            {
                count += result;
            }
        }

        return count.ToString();
    }
}