using TaskTracker.Controllers;
using TaskTracker.Exceptions;
using TaskTracker.Models;
using TaskTracker.Models.Interfaces;

namespace TaskTracker.Commands
{
    public class GetListTasksCommand : ICommand
    {
        private readonly TaskController TaskController;
        public string CommandName
        {
            get
            {
                return "list";
            }
        }

        public GetListTasksCommand(TaskController taskController)
        {
            TaskController = taskController;
        }

        public void Execute(string[] args)
        {
            try
            {
                Status? status = null;
                var value = args.Length >= 1 ? args[0] : "string.Empty";

                status = value switch
                {
                    nameof(Status.done) => Status.done,
                    nameof(Status.todo) => Status.todo,
                    nameof(Status.inProgress) => Status.inProgress,
                    "string.Empty" => null,
                    _ => throw new FormatException($"Unknown status, got: {value}, should be one of three predefined: [todo, in-progress, done]."),
                };

                ShowTasks(TaskController.GetTasks(status));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting task: {ex.Message}");
            }
        }

        private static void ShowTasks(IList<Tasks> tasks)
        {
            if (tasks.Count == 0) Console.WriteLine("There are no tasks, you can add new one :)");

            var orderedTasks = tasks.OrderBy(t => t.Id);

            foreach (var task in orderedTasks)
            {
                Console.WriteLine($"|{task.Id,-5}|{task.Description,-20}|{task.Status,-10}|{task.CreatedAt,-14}|{task.UpdatedAt,-14}|");
            }
        }
    }
}
