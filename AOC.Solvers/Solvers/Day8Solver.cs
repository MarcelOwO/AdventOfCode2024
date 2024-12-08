using System.Collections.Concurrent;
using AOC.Solvers.Interfaces;

public class Day8Solver : ISolver
{
    public async Task<string> SolvePart1Async(StreamReader input)
    {
        var data = await input.ReadToEndAsync();
        var lines = data.Split('\n', StringSplitOptions.TrimEntries);

        Dictionary<char, List<(int x, int y)>> dict = new();

        List<(int x, int y )> count = [];

        var maxX = lines[0].Length;
        var maxY = lines.Length;

        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                var character = lines[y][x];

                if (character == '.') continue;

                if (dict.ContainsKey(character))
                {
                    dict[character].Add((x, y));
                }
                else
                {
                    dict[character] = [(x, y)];
                }
            }
        }

        dict.ToList().ForEach(x =>
        {
            var valueCount = x.Value.Count;


            for (int i = 0; i < valueCount - 1; i++)
            {
                for (int j = i + 1; j < valueCount; j++)
                {
                    var point1 = x.Value[i];
                    var point2 = x.Value[j];

                    (int x, int y) vector1 = (point2.x - point1.x, point2.y - point1.y);
                    (int x, int y) vector2 = (point1.x - point2.x, point1.y - point2.y);

                    (int x, int y) newPoint1 = (point1.x + vector2.x, point1.y + vector2.y);
                    (int x, int y) newPoint2 = (point2.x + vector1.x, point2.y + vector1.y);

                    if (newPoint1.x >= 0 && newPoint1.x < maxX && newPoint1.y >= 0 && newPoint1.y < maxY)
                    {
                        count.Add(newPoint1);
                    }

                    if (newPoint2.x >= 0 && newPoint2.x < maxX && newPoint2.y >= 0 && newPoint2.y < maxY)
                    {
                        count.Add(newPoint2);
                    }
                }
            }
        });

        var points = count.Distinct().ToList();

        return points.Count().ToString();
    }

    public async Task<string> SolvePart2Async(StreamReader input)
    {
        input.BaseStream.Seek(0, SeekOrigin.Begin);

        var data = await input.ReadToEndAsync();
        var lines = data.Split('\n', StringSplitOptions.TrimEntries);

        Dictionary<char, List<(int x, int y)>> dict = new();

        ConcurrentBag<(int x, int y )> count = [];

        var maxX = lines[0].Length;
        var maxY = lines.Length;

        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                var character = lines[y][x];
                if (character == '.') continue;

                if (dict.ContainsKey(character))
                {
                    dict[character].Add((x, y));
                }
                else
                {
                    dict[character] = [(x, y)];
                }
            }
        }

        dict.ToList().ForEach(x =>
        {
            var valueCount = x.Value.Count;

            for (int i = 0; i < valueCount - 1; i++)
            {
                for (int j = i + 1; j < valueCount; j++)
                {
                    var point1 = x.Value[i];
                    var point2 = x.Value[j];
                    count.Add(point1);
                    count.Add(point2);

                    (int x, int y) vector1 = (point2.x - point1.x, point2.y - point1.y);
                    (int x, int y) vector2 = (point1.x - point2.x, point1.y - point2.y);

                    (int x, int y) newPoint1 = (point1.x + vector2.x, point1.y + vector2.y);
                    (int x, int y) newPoint2 = (point2.x + vector1.x, point2.y + vector1.y);


                    while (newPoint1.x >= 0 && newPoint1.x < maxX && newPoint1.y >= 0 && newPoint1.y < maxY)
                    {
                        count.Add(newPoint1);
                        newPoint1 = (newPoint1.x + vector2.x, newPoint1.y + vector2.y);
                    }

                    while (newPoint2.x >= 0 && newPoint2.x < maxX && newPoint2.y >= 0 && newPoint2.y < maxY)
                    {
                        count.Add(newPoint2);
                        newPoint2 = (newPoint2.x + vector1.x, newPoint2.y + vector1.y);
                    }
                }
            }
        });


        return count.Distinct().ToList().Count.ToString();
    }
}