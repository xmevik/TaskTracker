using TaskTracker.Controllers;
using TaskTracker.Exceptions;
using TaskTracker.Models;
using TaskTracker.Models.Interfaces;

namespace TaskTracker.Commands
{
    public class ChangeStateTaskCommand : ICommand
    {
        private readonly TaskController TaskController;
        public string CommandName
        {
            get
            {
                return "mark";
            }
        }

        public ChangeStateTaskCommand(TaskController repository)
        {
            TaskController = repository;
        }

        public void Execute(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Error: Both ID and Status must be provided.");
                return;
            }

            if (string.IsNullOrWhiteSpace(args[0]))
            {
                Console.WriteLine("Error: The status must not be empty.");
                return;
            }

            if (!int.TryParse(args[1], out var taskId))
            {
                Console.WriteLine("Error: The first argument must be a valid numeric ID.");
                return;
            }

            try
            {
                Status status;
                var value = args[0];

                status = value switch
                {
                    nameof(Status.done) => Status.done,
                    nameof(Status.todo) => Status.todo,
                    "in-progress" => Status.inProgress,
                    _ => throw new FormatException($"Unknown status, got: {value}, should be one of three predefined: [todo, in-progress, done]."),
                };

                TaskController.ChangeState(taskId, status);
            }
            catch (TaskNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting task: {ex.Message}");
            }
        }
    }
}
