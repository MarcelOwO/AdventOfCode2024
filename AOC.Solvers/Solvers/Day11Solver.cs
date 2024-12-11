using System.Collections.Concurrent;
using AOC.Solvers.Interfaces;

public class Day11Solver : ISolver
{
    ConcurrentDictionary<ulong, List<ulong>> dict = [];


    public async Task<string> SolvePart1Async(StreamReader input)
    {
        var data = await input.ReadToEndAsync();
        var list = data.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(ulong.Parse).ToList();
        var steps = 25;


        for (var i = 0; i < steps; i++)
        {
            Console.WriteLine(i);

            ConcurrentBag<ulong> bag = new();

            Parallel.ForEach(list, stone =>
            {
                if (dict.TryGetValue(stone, out var value))
                {
                    value.ForEach(bag.Add);
                }
                else
                {
                    if (stone == 0)
                    {
                        bag.Add(1);
                        dict[stone] = [1];
                    }
                    else if (stone.ToString().Length % 2 == 0)
                    {
                        var val = stone.ToString();
                        var length = val.Length / 2;
                        var first = ulong.Parse(val[..(length)]);
                        var last = ulong.Parse(val.Substring(length, length));
                        dict[stone] = [first, last];
                        bag.Add(first);
                        bag.Add(last);
                    }
                    else
                    {
                        dict[stone] = [stone * 2024];
                        bag.Add(stone * 2024);
                    }
                }
            });


            list = bag.ToList();
        }

        Console.WriteLine("/////////////////////////////////");
        return list.Count.ToString();
    }


    public async Task<string> SolvePart2Async(StreamReader input)
    {
        input.BaseStream.Seek(0, SeekOrigin.Begin);
        var data = await input.ReadToEndAsync();
        var list = data.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(ulong.Parse).ToList();
        var steps = 75;


        for (var i = 0; i < steps; i++)
        {
            Console.WriteLine(i);

            ConcurrentBag<ulong> bag = [];

            Parallel.ForEach(list, stone =>
            {
                if (dict.TryGetValue(stone, out var value))
                {
                    value.ForEach(bag.Add);
                }
                else
                {
                    if (stone == 0)
                    {
                        bag.Add(1);
                        dict[stone] = [1];
                    }
                    else if (stone.ToString().Length % 2 == 0)
                    {
                        var val = stone.ToString();
                        var length = val.Length / 2;
                        var first = ulong.Parse(val[..(length)]);
                        var last = ulong.Parse(val.Substring(length, length));
                        dict[stone] = [first, last];
                        bag.Add(first);
                        bag.Add(last);
                    }
                    else
                    {
                        dict[stone] = [stone * 2024];
                        bag.Add(stone * 2024);
                    }
                }
            });


            list = bag.ToList();
        }

        Console.WriteLine("/////////////////////////////////");
        return list.Count.ToString();
    }
}