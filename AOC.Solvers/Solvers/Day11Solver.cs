using System.Numerics;
using System.Runtime.CompilerServices;
using AOC.Solvers.Interfaces;

namespace AOC.Solvers.Solvers;

public class Day11Solver : ISolver
{
    private readonly Dictionary<(BigInteger stone, BigInteger steps), BigInteger> _cache = [];

    public async Task<string> SolvePart1Async(StreamReader input)
    {
        var data = await input.ReadToEndAsync();
        var list = data.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(BigInteger.Parse).ToList();
        const int steps = 25;

        return GetCount(steps, list).ToString();
    }

    public async Task<string> SolvePart2Async(StreamReader input)
    {
        input.BaseStream.Seek(0, SeekOrigin.Begin);

        var data = await input.ReadToEndAsync();
        var list = data.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(BigInteger.Parse).ToList();
        const int steps = 75;

        return GetCount(steps, list).ToString();
    }

    private BigInteger GetCount(int steps, List<BigInteger> stones)
    {
        if (steps == 0)
        {
            return stones.Count;
        }

        BigInteger count = 0;

        foreach (var stone in stones)
        {
            if (_cache.TryGetValue((stone, steps), out var cacheCount))
            {
                count += cacheCount;
            }
            else
            {
                var result = GetCount(steps - 1, Blink(stone));
                count += result;
                _cache.Add((stone, steps), result);
            }
        }

        return count;
    }


    private List<BigInteger> Blink(BigInteger stone)
    {
        if (stone == 0)
        {
            return [1];
        }

        var length = stone.ToString().Length;

        if (length % 2 != 0) return [stone * 2024];

        var divider = BigInteger.Pow(10, length / 2);
        return [stone / divider, stone % divider];
    }
}