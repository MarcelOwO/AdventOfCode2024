namespace AOC.Inputs;

public class InputController
{
    private readonly Dictionary<int, StreamReader> _inputs;

    public InputController()
    {
        _inputs = new Dictionary<int, StreamReader>();

        for (var day = 1; day < 25; day++)
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, $"InputFiles/Day{day}.txt");

            try
            {
                _inputs[day] = new(filePath);
            }
            catch (Exception e) when (e is FileNotFoundException)
            {
                Console.WriteLine($"Day {day} file not found");
            }
        }
    }

    public StreamReader this[int day]
    {
        get
        {
            try
            {
                
                return _inputs[day];
            }
            catch (Exception e) when (e is KeyNotFoundException)
            {
                Console.WriteLine($"Day {day} file not found");
                throw;
            }
        }
    }
}