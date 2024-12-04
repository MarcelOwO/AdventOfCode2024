using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using AOC.Solvers.Interfaces;

public class Day3Solver : ISolver
{
    public async Task<string> SolvePart1Async(StreamReader input)
    {
        var someInput = await input.ReadToEndAsync();

        var matches = Regex.Matches(someInput, @"mul\((\d+),(\d+)\)")
            .Select(x => (int.Parse(x.Groups[1].Value) * int.Parse(x.Groups[2].Value)))
            .ToList().Sum();
        return matches.ToString();
    }


    public async Task<string> SolvePart2Async(StreamReader input)
    {
        input.BaseStream.Seek(0, SeekOrigin.Begin);

        var someInput = await input.ReadToEndAsync();

        var result = 0;

        var matches = Regex.Matches(someInput, @"mul\((\d+),(\d+)\)|do\(\)|don't\(\)")
            .ToList();

        bool isEnabled = true;

        foreach (var match in matches)
        {
            if (match.Value[0] == 'd')
            {
                isEnabled = match.Value == "do()";
            }
            else
            {
                if (!isEnabled) continue;

                result += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
            }
        }

        return result.ToString();
    }
}