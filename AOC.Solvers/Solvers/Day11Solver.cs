using System.Collections.Concurrent;
using AOC.Solvers.Interfaces;

public class Day11Solver : ISolver
{
    Dictionary<uint, List<uint>> _cache = new();

    public async Task<string> SolvePart1Async(StreamReader input)
    {
        var data = await input.ReadToEndAsync();
        var list = data.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(uint.Parse).ToList();
        const int steps = 25;

        return (await GetCount(steps, list)).ToString();
    }

    public async Task<string> SolvePart2Async(StreamReader input)
    {
        input.BaseStream.Seek(0, SeekOrigin.Begin);

        var data = await input.ReadToEndAsync();
        var list = data.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(uint.Parse).ToList();
        const int steps = 75;

        return (await GetCount(steps, list)).ToString();
    }


    private async Task<int> GetCount(int steps, List<uint> list)
    {
        for (var i = 0; i < steps; i++)
        {
            Console.WriteLine($"Step {i}");

            for (int j = 0; j < list.Count; j++)
            {
                var stone = list[j];

                if (_cache.TryGetValue(stone, out var value))
                {
                    if (value.Count > 1)
                    {
                        list[j] = value[0];
                        list.Add(value[1]);
                    }
                    else
                    {
                        list[j] = value[0];
                    }
                }

                if (stone == 0)
                {
                    _cache[stone] = [0];
                    list[j] = 1;
                }
                else if (stone.ToString().Length % 2 == 0)
                {
                    var str = stone.ToString();
                    var length = str.Length / 2;

                    var first = uint.Parse(str.Substring(0, length));
                    var second = uint.Parse(str.Substring(length, length));
                    list[j] = first;
                    list.Add(second);
                }
                else
                {
                    list[j] *= 2024;
                }
            }
        }


        return list.Count;
    }
}