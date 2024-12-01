using AOC.Inputs;
using AOC.Solvers.Interfaces;
using AOC.Solvers.Solvers;

namespace AOC.Solvers;

public class SolverController
{
    private readonly Lazy<InputController> _inputController = new(() => new InputController());

    private readonly Dictionary<int, Lazy<ISolver>> _solvers = new()
    {
        { 1, new Lazy<ISolver>(() => new Day1Solver()) },
        { 2, new Lazy<ISolver>(() => new Day1Solver()) },
        { 3, new Lazy<ISolver>(() => new Day3Solver()) },
        { 4, new Lazy<ISolver>(() => new Day4Solver()) },
        { 5, new Lazy<ISolver>(() => new Day5Solver()) },
        { 6, new Lazy<ISolver>(() => new Day6Solver()) },
        { 7, new Lazy<ISolver>(() => new Day7Solver()) },
        { 8, new Lazy<ISolver>(() => new Day8Solver()) },
        { 9, new Lazy<ISolver>(() => new Day9Solver()) },
        { 10, new Lazy<ISolver>(() => new Day10Solver()) },
        { 11, new Lazy<ISolver>(() => new Day11Solver()) },
        { 12, new Lazy<ISolver>(() => new Day12Solver()) },
        { 13, new Lazy<ISolver>(() => new Day13Solver()) },
        { 14, new Lazy<ISolver>(() => new Day14Solver()) },
        { 15, new Lazy<ISolver>(() => new Day15Solver()) },
        { 16, new Lazy<ISolver>(() => new Day16Solver()) },
        { 17, new Lazy<ISolver>(() => new Day17Solver()) },
        { 18, new Lazy<ISolver>(() => new Day18Solver()) },
        { 19, new Lazy<ISolver>(() => new Day19Solver()) },
        { 20, new Lazy<ISolver>(() => new Day20Solver()) },
        { 21, new Lazy<ISolver>(() => new Day21Solver()) },
        { 22, new Lazy<ISolver>(() => new Day22Solver()) },
        { 23, new Lazy<ISolver>(() => new Day23Solver()) },
        { 24, new Lazy<ISolver>(() => new Day24Solver()) },
        { 25, new Lazy<ISolver>(() => new Day25Solver()) }
    };

    public async Task<string> SolveAsync(int day)
    {
        var input = _inputController.Value[day];
        return await _solvers[day].Value.SolveAsync(input);
    }
}