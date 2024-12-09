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


        for (var i = diskArray.Count - 1; i >= 0; i -= 2)
        {
            var fileToMove = diskArray[i];
            var fileSize = fileToMove.Count;

            for (var j = 1; j < diskArray.Count; j += 2)
            {
                var freeSpace = diskArray[j];
                if (fileSize > freeSpace.Count(x => x == -1)) continue;

                var first = freeSpace.FindIndex(x => x == -1);

                for (var k = 0; k < fileSize; k++)
                {
                    freeSpace[first + k] = fileToMove[k];
                    fileToMove[k] = -1;
                }

                break;

            }
        }

        var newComplete = diskArray.SelectMany(x => x).ToList();

        var c = newComplete.Select((x, i) => x == -1 ? 0 : (long)x * i).Sum();

        return c.ToString();
    }
}