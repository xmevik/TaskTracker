using TaskTracker.Commands;
using TaskTracker.Controllers;
using TaskTracker.Services;

namespace TaskTracker
{
    internal class Program
    {
        private static readonly TaskController taskController = new(new JsonFilesController("./tasks.json"));

        static void Main(string[] args)
        {
            CommandService commandService = new();

            commandService.AddCommand(new AddTaskCommand(taskController));
            commandService.AddCommand(new ChangeStateTaskCommand(taskController));
            commandService.AddCommand(new DeleteTaskCommand(taskController));
            commandService.AddCommand(new GetListTasksCommand(taskController));
            commandService.AddCommand(new UpdateTaskCommand(taskController));

            commandService.ProcessCommand(args);
        }
    }
}
