namespace AOC.Solvers.Interfaces;

public interface ISolver
{
    Task<string> SolveAsync(StreamReader input);
}