using AOC.Solvers;

Console.WriteLine("Starting");

var solver = new SolverController();

await solver.SolveAsync(12);


/*
    List<Task> tasks = new();
Enumerable.Range(1, 24).ToList().ForEach(x =>
{
    var task =  solver.SolveAsync(x);
    tasks.Add(task);
});

Task.WaitAll(tasks.ToArray());
*/

Console.WriteLine("Finished");