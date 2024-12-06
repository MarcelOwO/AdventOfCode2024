using System.Collections.Concurrent;
using AOC.Solvers.Interfaces;

public class Day6Solver : ISolver
{
    private byte[,] _matrix;
    private (byte x, byte y) _originalGuardPos;
    private (byte x, byte y) _guardPos;
    private HashSet<(byte x, byte y)> _visited = [];

    public async Task<string> SolvePart1Async(StreamReader input)
    {
        var jaggedMatrix = (await input.ReadToEndAsync()).Split('\n', StringSplitOptions.TrimEntries);

        _matrix = new byte[jaggedMatrix[0].Length, jaggedMatrix.Length];

        for (var x = 0; x < jaggedMatrix.Length; x++)
        {
            var row = jaggedMatrix[x].AsSpan(); // cant tell if this helps or not??

            for (var y = 0; y < row.Length; y++)
            {
                byte val = (row[y])switch
                {
                    '.' => 0,
                    '#' => 1,
                    '^' => 2,
                    _ => throw new ArgumentOutOfRangeException()
                };


                if (val == 2)
                {
                    _guardPos = ((byte)y, (byte)x);
                    _originalGuardPos = ((byte)y, (byte)x);
                }

                _matrix[y, x] = val;
            }
        }

        _visited.Add(_guardPos);

        RunNorth();

        return _visited.Count.ToString();
    }

    private void RunEast()
    {
        while (true)
        {
            if (_guardPos.x + 1 == _matrix.GetLength(1)) break;
            if (_matrix[_guardPos.x + 1, _guardPos.y] == 1)
            {
                RunSouth();
                break;
            }

            _guardPos.x++;
            _visited.Add(_guardPos);
        }
    }


    private void RunSouth()
    {
        while (true)
        {
            if (_guardPos.y + 1 == _matrix.GetLength(0)) break;
            if (_matrix[_guardPos.x, _guardPos.y + 1] == 1)
            {
                RunWest();
                break;
            }

            _guardPos.y++;
            _visited.Add(_guardPos);
        }
    }


    private void RunWest()
    {
        while (true)
        {
            if (_guardPos.x - 1 == -1) break;
            if (_matrix[_guardPos.x - 1, _guardPos.y] == 1)
            {
                RunNorth();
                break;
            }

            _guardPos.x--;
            _visited.Add(_guardPos);
        }
    }


    private void RunNorth()
    {
        while (true)
        {
            if (_guardPos.y - 1 == -1) break;
            if (_matrix[_guardPos.x, _guardPos.y - 1] == 1)
            {
                RunEast();
                break;
            }

            _guardPos.y--;
            _visited.Add(_guardPos);
        }
    }

    public async Task<string> SolvePart2Async(StreamReader input)
    {
        var possiblePositions = _visited.ToList();
        _visited.Remove(_originalGuardPos);

        var count = 0;
        byte sizeCol = (byte)_matrix.GetLength(0);
        byte sizeRow = (byte)_matrix.GetLength(1);

        var partitioner = Partitioner.Create(possiblePositions);

        Parallel.ForEach(partitioner, pos =>
        {
            var guardPost = _originalGuardPos;

            var list = new HashSet<(byte x, byte y, byte d)>();

            var copy_matrix = new byte[sizeCol, sizeRow];
            
            for(var row=0; row < sizeRow; row++)
            {
                Buffer.BlockCopy(_matrix, row * sizeCol, copy_matrix, row * sizeCol, sizeCol);
            }

            copy_matrix[pos.x, pos.y] = 1;

            if (RunNorth2(copy_matrix, list, ref guardPost, sizeCol))
            {
                Interlocked.Increment(ref count);
            }
        });

        return count.ToString();
    }

    private bool RunEast2(byte[,] matrix, HashSet<(byte x, byte y, byte d)> visited, ref (byte x, byte y) guardPos,
        byte size)
    {
        while (true)
        {
            if (guardPos.x + 1 == size)
            {
                return false;
            }

            if (matrix[guardPos.x + 1, guardPos.y] == 1)
            {
                return RunSouth2(matrix, visited, ref guardPos, size);
            }

            guardPos.x++;

            var newVisited = (guardPos.x, guardPos.y, (byte)0);

            if (visited.Contains(newVisited)) return true;

            visited.Add(newVisited);
        }

        return false;
    }


    private bool RunSouth2(byte[,] matrix, HashSet<(byte x, byte y, byte d)> visited, ref (byte x, byte y) guardPos,
        byte size)
    {
        while (true)
        {
            if (guardPos.y + 1 == size) return false;
            if (matrix[guardPos.x, guardPos.y + 1] == 1)
            {
                return RunWest2(matrix, visited, ref guardPos, size);
            }

            guardPos.y++;
            var newVisited = (guardPos.x, guardPos.y, (byte)1);

            if (visited.Contains(newVisited)) return true;

            visited.Add(newVisited);
        }

        return false;
    }


    private bool RunWest2(byte[,] matrix, HashSet<(byte x, byte y, byte d)> visited, ref (byte x, byte y) guardPos,
        byte size)
    {
        while (true)
        {
            if (guardPos.x - 1 == -1) return false;
            if (matrix[guardPos.x - 1, guardPos.y] == 1)
            {
                return RunNorth2(matrix, visited, ref guardPos, size);
            }

            guardPos.x--;
            var newVisited = (guardPos.x, guardPos.y, (byte)2);

            if (visited.Contains(newVisited)) return true;

            visited.Add(newVisited);
        }

        return false;
    }


    private bool RunNorth2(byte[,] matrix, HashSet<(byte x, byte y, byte d)> visited, ref (byte x, byte y) guardPos,
        byte size)
    {
        while (true)
        {
            if (guardPos.y - 1 == -1) return false;

            if (matrix[guardPos.x, guardPos.y - 1] == 1)
            {
                return RunEast2(matrix, visited, ref guardPos, size);
            }

            guardPos.y--;

            var newVisited = (guardPos.x, guardPos.y, (byte)3);

            if (visited.Contains(newVisited)) return true;

            visited.Add(newVisited);
        }

        return false;
    }
}