using System.Diagnostics;
using AOC.Solvers.Interfaces;

public class Day4Solver : ISolver
{
    public async Task<string> SolvePart1Async(StreamReader input)
    {
        int count = 0;

        var inputString = await input.ReadToEndAsync();
        var matrix = inputString.Split('\n', StringSplitOptions.TrimEntries)
            .Select(x => x.ToCharArray())
            .ToArray();

        var length = matrix.Length;
        var length2 = matrix[0].Length;

        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length2; j++)
            {
                var cellValue = matrix[i][j];

                if (cellValue != 'X') continue;
//i could probably skip some of the checks depening on being on the corners but whatever it is fast enough
                //check left
                if (j >= 3)
                {
                    if (matrix[i][j - 1] == 'M' && matrix[i][j - 2] == 'A' && matrix[i][j - 3] == 'S')
                    {
                        count++;
                    }
                }

                //check right
                if (j <= length2 - 4)
                {
                    if (matrix[i][j + 1] == 'M' && matrix[i][j + 2] == 'A' && matrix[i][j + 3] == 'S')
                    {
                        count++;
                    }
                }

                //check down
                if (i <= length - 4)
                {
                    if (matrix[i + 1][j] == 'M' && matrix[i + 2][j] == 'A' && matrix[i + 3][j] == 'S')
                    {
                        count++;
                    }
                }

                //check left down
                if (j >= 3 && i <= length - 4)
                {
                    if (matrix[i + 1][j - 1] == 'M' && matrix[i + 2][j - 2] == 'A' && matrix[i + 3][j - 3] == 'S')
                    {
                        count++;
                    }
                }

                //check right down
                if (j <= length2 - 4 && i <= length - 4)
                {
                    if (matrix[i + 1][j + 1] == 'M' && matrix[i + 2][j + 2] == 'A' && matrix[i + 3][j + 3] == 'S')
                    {
                        count++;
                    }
                }

                //check up
                if (i >= 3)
                {
                    if (matrix[i - 1][j] == 'M' && matrix[i - 2][j] == 'A' && matrix[i - 3][j] == 'S')
                    {
                        count++;
                    }
                }

                //check left up
                if (j >= 3 && i >= 3)
                {
                    if (matrix[i - 1][j - 1] == 'M' && matrix[i - 2][j - 2] == 'A' && matrix[i - 3][j - 3] == 'S')
                    {
                        count++;
                    }
                }

                //check right up
                if (j <= length2 - 4 && i >= 3)
                {
                    if (matrix[i - 1][j + 1] == 'M' && matrix[i - 2][j + 2] == 'A' && matrix[i - 3][j + 3] == 'S')
                    {
                        count++;
                    }
                }
            }
        }

        return count.ToString();
    }

    public async Task<string> SolvePart2Async(StreamReader input)
    {
        input.BaseStream.Seek(0, SeekOrigin.Begin);

        int count = 0;

        var inputString = await input.ReadToEndAsync();
        var matrix = inputString.Split('\n', StringSplitOptions.TrimEntries)
            .Select(x => x.ToCharArray())
            .ToArray();
        
        
        var length = matrix.Length;
        var length2 = matrix[0].Length;

        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length2; j++)
            {
                if (CheckPattern(matrix, i, j, length, length2))
                {
                    count++;
                }
            }
        }
        return count.ToString();
    }


    private bool CheckPattern(char[][] input, int x, int y, int length, int length2)
    {
        if (x == 0 || x == length - 1 || y == 0 || y == length2 - 1) return false;

        if (input[x][y] != 'A') return false;

        var topLeft = input[x - 1][y - 1];

        
        if (topLeft == 'M')
        {
            var topRight = input[x - 1][y + 1];

            if (topRight == 'M')
            {
                return input[x + 1][y + 1] == 'S' && input[x + 1][y - 1] == 'S';
            }

            if (topRight == 'S')
            {
                return input[x + 1][y + 1] == 'S' && input[x + 1][y - 1] == 'M';
            }
        }
        else if (topLeft == 'S')
        {
            var topRight = input[x - 1][y + 1];

            if (topRight == 'M')
            {
                return input[x + 1][y + 1] == 'M' && input[x + 1][y - 1] == 'S';
            }

            if (topRight == 'S')
            {
                return input[x + 1][y + 1] == 'M' && input[x + 1][y - 1] == 'M';
            }
        }


        return false;
    }
}