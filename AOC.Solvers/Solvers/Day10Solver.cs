using AOC.Solvers.Interfaces;

public class Day10Solver : ISolver
{
    public async Task<string> SolvePart1Async(StreamReader input)
    {
        var data = await input.ReadToEndAsync();

        var grid = data.Split("\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Select(x => (int)x - '0').ToArray())
            .ToArray();

        var score = 0;

        for (var y = 0; y < grid[0].Length; y++)
        {
            for (var x = 0; x < grid.Length; x++)
            {
                var val = grid[y][x];

                Console.Write(val);

                if (val != 0) continue;

                var heads = GetTrailHeads((x, y), grid);

                score += heads;
            }

            Console.WriteLine();
        }

        return score.ToString();
    }

    private int GetTrailHeads((int x, int y) pos, int[][] grid)
    {
        var counter = 0;
        var current = 1;

        HashSet<(int x, int y)> surrounding = [pos];

        while (current <= 9)
        {
            surrounding = GetAllSurrounding(ref current, grid, surrounding);

            if (surrounding.Count == 0)
            {
                return 0;
            }
        }

        counter = surrounding.Count;

        return counter;
    }

    private HashSet<(int x, int y)> GetAllSurrounding(ref int current, int[][] grid,
        HashSet<(int x, int y)> surrounding)
    {
        var newSurrounding = new HashSet<(int x, int y)>();

        var maxX = grid[0].Length;
        var maxY = grid.Length;

        foreach (var pos in surrounding)
        {
            try
            {
                if (pos.x != 0 && grid[pos.y][pos.x - 1] == current)
                {
                    newSurrounding.Add((pos.x - 1, pos.y));
                }
            }
            catch (IndexOutOfRangeException e)
            {
            }

            try
            {
                if (pos.y != 0 && grid[pos.y - 1][pos.x] == current)
                {
                    newSurrounding.Add((pos.x, pos.y - 1));
                }
            }
            catch (IndexOutOfRangeException e)
            {
            }

            try
            {
                if (pos.x < grid[0].Length && grid[pos.y][pos.x + 1] == current)
                {
                    newSurrounding.Add((pos.x + 1, pos.y));
                }
            }
            catch (IndexOutOfRangeException e)
            {
            }

            try
            {
                if (pos.y < maxY && grid[pos.y + 1][pos.x] == current)
                {
                    newSurrounding.Add((pos.x, pos.y + 1));
                }
            }
            catch (IndexOutOfRangeException e)
            {
            }
        }

        current++;

        return newSurrounding;
    }

    public async Task<string> SolvePart2Async(StreamReader input)
    {
        input.BaseStream.Seek(0, SeekOrigin.Begin);
        var data = await input.ReadToEndAsync();

        var grid = data.Split("\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Select(x => (int)x - '0').ToArray())
            .ToArray();

        var score = 0;

        for (var y = 0; y < grid[0].Length; y++)
        {
            for (var x = 0; x < grid.Length; x++)
            {
                var val = grid[y][x];

                Console.Write(val);

                if (val != 0) continue;

                var heads = GetDistinctTrails((x, y), grid);

                score += heads;
            }

            Console.WriteLine();
        }

        return score.ToString();
    }

    private int GetDistinctTrails((int x, int y) pos, int[][] grid)
    {
        var counter = 0;
        var current = 1;

        List<(int x, int y)> surrounding = [pos];

        while (current <= 9)
        {
            surrounding = GetAllSurrounding2(ref current, grid, surrounding);

            if (surrounding.Count == 0)
            {
                return 0;
            }
        }

        counter = surrounding.Count;

        return counter;
    }

    private List<(int x, int y)> GetAllSurrounding2(ref int current, int[][] grid, List<(int x, int y)> surrounding)
    {
        {
            var newSurrounding = new List<(int x, int y)>();

            var maxX = grid[0].Length;
            var maxY = grid.Length;

            foreach (var pos in surrounding)
            {
                try
                {
                    if (pos.x != 0 && grid[pos.y][pos.x - 1] == current)
                    {
                        newSurrounding.Add((pos.x - 1, pos.y));
                    }
                }
                catch (IndexOutOfRangeException e)
                {
                }

                try
                {
                    if (pos.y != 0 && grid[pos.y - 1][pos.x] == current)
                    {
                        newSurrounding.Add((pos.x, pos.y - 1));
                    }
                }
                catch (IndexOutOfRangeException e)
                {
                }

                try
                {
                    if (pos.x < grid[0].Length && grid[pos.y][pos.x + 1] == current)
                    {
                        newSurrounding.Add((pos.x + 1, pos.y));
                    }
                }
                catch (IndexOutOfRangeException e)
                {
                }

                try
                {
                    if (pos.y < maxY && grid[pos.y + 1][pos.x] == current)
                    {
                        newSurrounding.Add((pos.x, pos.y + 1));
                    }
                }
                catch (IndexOutOfRangeException e)
                {
                }
            }

            current++;

            return newSurrounding;
        }
    }
}