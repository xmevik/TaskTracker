using TaskTracker.Controllers;
using TaskTracker.Exceptions;
using TaskTracker.Models.Interfaces;

namespace TaskTracker.Commands
{
    public class UpdateTaskCommand : ICommand
    {
        private readonly TaskController TaskController;
        public string CommandName
        {
            get
            {
                return "update";
            }
        }

        public UpdateTaskCommand(TaskController taskController)
        {
            TaskController = taskController;
        }

        public void Execute(string[] args)
        {
            var description = args[1];

            if (!int.TryParse(args[0], out int taskId))
            {
                Console.WriteLine("Error: Please provide a valid task ID.");
            }
            if (string.IsNullOrWhiteSpace(description))
            {
                Console.WriteLine("Error: The description must not be empty.");
                return;
            }

            try
            {
                TaskController.UpdateTask(taskId, description);
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
