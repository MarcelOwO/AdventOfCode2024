using AOC.Solvers.Interfaces;

public class Day9Solver : ISolver
{
    public async Task<string> SolvePart1Async(StreamReader input)
    {
        var disk = await input.ReadToEndAsync();

        var diskA = disk.Trim().Select(x => (int)(x - '0')).ToList();

        List<List<int>> diskArray = [];

        var fileCount = 0;

        for (var i = 0; i < diskA.Count; i++)
        {
            var alt = i % 2 == 0;
            var val = diskA[i];
            List<int> newList = [];

            if (alt)
            {
                for (int j = 0; j < val; j++)
                {
                    newList.Add(fileCount);
                }

                fileCount++;
            }
            else
            {
                for (int j = 0; j < val; j++)
                {
                    newList.Add(-1);
                }
            }

            diskArray.Add(newList);
        }

        var complete = diskArray.SelectMany(x => x).ToList();

        int pointerBegin = 0;

        int pointerEnd = complete.Count - 1;

        while (pointerBegin < pointerEnd)
        {
            if (complete[pointerBegin] != -1)
            {
                pointerBegin++;
            }
            else
            {
                while (complete[pointerEnd] == -1)
                {
                    pointerEnd--;
                }

                complete[pointerBegin] = complete[pointerEnd];
                complete[pointerEnd] = -1;
            }
        }

        var newComplete = complete.GetRange(0, pointerBegin);

        long count = 0;

        for (var i = 0; i < newComplete.Count; i++)
        {
            count += newComplete[i] * i;
        }

        return count.ToString();
    }

    public async Task<string> SolvePart2Async(StreamReader input)
    {
        input.BaseStream.Seek(0, SeekOrigin.Begin);

        var disk = (await input.ReadToEndAsync()).Trim().Select(x => (int)(x - '0')).ToList();

        List<List<int>> diskArray = [];

        var fileCount = 0;

        for (var i = 0; i < disk.Count; i++)
        {
            var val = disk[i];
            List<int> newList = [];

            if (i % 2 == 0)
            {
                for (var j = 0; j < val; j++)
                {
                    newList.Add(fileCount);
                }

                fileCount++;
            }
            else
            {
                for (var j = 0; j < val; j++)
                {
                    newList.Add(-1);
                }
            }

            diskArray.Add(newList);
        }

        var complete = diskArray.SelectMany(x => x).ToList();
        var fileIndex = complete.Max();
        
        while (fileIndex >= 0)
        {
            var (lastFileBegin, lastFileSize) = GetLastFile(ref fileIndex, complete);

            var freeSpaceBegin = FindFreeSpace(lastFileSize, complete, lastFileBegin);
            if (freeSpaceBegin == -1)
            {
                continue;
            }

            MoveFile(freeSpaceBegin, lastFileBegin, lastFileSize, complete);
        }

        var c = complete.Select((x, i) => x == -1 ? 0 : (long)x * i).Sum();

        return c.ToString();
    }

    private void MoveFile(int freeSpaceBegin, int lastFileBegin, int lastFileSize, List<int> complete)
    {
        for (int i = 0; i < lastFileSize; i++)
        {
            complete[freeSpaceBegin + i] = complete[lastFileBegin + i];
            complete[lastFileBegin + i] = -1;
        }
    }

    private int FindFreeSpace(int lastFileSize, List<int> complete, int bound = 0)
    {
        var current = 0;
        for (int i = 0; i < bound; i++)
        {
            if (complete[i] != -1)
            {
                current = 0;
            }
            else
            {
                current++;
            }

            if (current == lastFileSize)
            {
                return i - lastFileSize + 1;
            }
        }

        return -1;
    }

    private (int lastFileBegin, int lastFileSize) GetLastFile(ref int fileIndex, List<int> complete)
    {
        var copy = fileIndex;
        var first = complete.FindIndex(x => x == copy);
        var last = complete.FindLastIndex(x => x == copy);
        fileIndex--;
        return (first, last - first + 1);
    }
}