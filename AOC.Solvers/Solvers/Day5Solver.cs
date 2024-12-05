using System.Collections.Concurrent;
using AOC.Solvers.Interfaces;

public class Day5Solver : ISolver
{
    private List<List<int>> Lists { get; set; } = [];
    private ConcurrentDictionary<int, List<int>> RuleDict { get; set; } = [];

    public async Task<string> SolvePart1Async(StreamReader input)
    {
        bool switchFlag = false;

        while (!input.EndOfStream)
        {
            var line = await input.ReadLineAsync();

            if (string.IsNullOrWhiteSpace(line) && !switchFlag)
            {
                switchFlag = true;
                continue;
            }

            if (switchFlag)
            {
                Lists.Add(line.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList());
            }
            else
            {
                var parts = line.Split('|').Select(int.Parse).ToList();

                if (!RuleDict.TryAdd(parts[0], [parts[1]]))
                {
                    RuleDict[parts[0]].Add(parts[1]);
                }
            }
        }

        var values = new ConcurrentBag<int>();

        Parallel.ForEach(Lists, async list =>
        {
            var check = list.All(element =>
            {
                var list2 = Enumerable.Range(0, list.IndexOf(element)).Select(x => list[x]).ToList();
                return RuleDict[element].ToList().All(x => !list2.Contains(x));
            });

            if (check)
            {
                values.Add(list[list.Count / 2]);
            }
        });

        return values.Sum().ToString();
    }

    public async Task<string> SolvePart2Async(StreamReader input)
    {
        var values = new ConcurrentBag<int>();

        Parallel.ForEach(Lists, list =>
        {
            var check = list.All(element =>
            {
                var list2 = Enumerable.Range(0, list.IndexOf(element)).Select(x => list[x]).ToList();
                return RuleDict[element].ToList().All(x => !list2.Contains(x));
            });

            if (check)
            {
                return;
            }

            for (int i = 0; i < list.Count; i++)
            {
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    if (RuleDict[list[j]].Contains(list[i]))
                    {
                        list[i] = list[j] ^ list[i];
                        list[j] = list[i] ^ list[j];
                        list[i] = list[j] ^ list[i];

                        i = 0;
                        j = -1;
                    }
                }
            }

            values.Add(list[list.Count / 2]);
        });

        return values.Sum().ToString();
    }
}