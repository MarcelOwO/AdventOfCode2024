using AOC.Solvers;

Console.WriteLine("Starting");

var solver = new SolverController();
List<Task> tasks = new();

Enumerable.Range(1, 4).ToList().ForEach(x =>
{
    var task =  solver.SolveAsync(x); 
    tasks.Add(task);
});

Task.WaitAll(tasks.ToArray());

Console.WriteLine("Finished");