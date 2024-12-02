namespace AOC.Solvers.Interfaces;

public interface ISolver
{
    Task<string> SolvePart1Async(StreamReader input);
    Task<string> SolvePart2Async(StreamReader input);
}