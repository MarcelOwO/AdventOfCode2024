
using AOC.Solvers;

Console.WriteLine("Starting");

var solver = new SolverController();

var result = await solver.SolveAsync(1);

Console.WriteLine(result);