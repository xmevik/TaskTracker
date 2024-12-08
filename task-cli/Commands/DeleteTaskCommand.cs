using TaskTracker.Controllers;
using TaskTracker.Exceptions;
using TaskTracker.Models.Interfaces;

namespace TaskTracker.Commands
{
    public class DeleteTaskCommand : ICommand
    {
        private readonly TaskController TaskController;
        public string CommandName
        {
            get
            {
                return "delete";
            }
        }

        public DeleteTaskCommand(TaskController taskController)
        {
            TaskController = taskController;
        }

        public void Execute(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Error: Please provide a task ID.");
                return;
            }

            if (!int.TryParse(args[0], out int taskId))
            {
                Console.WriteLine("Error: Please provide a valid task ID.");
                return;
            }

            try
            {
                TaskController.DeleteTask(taskId);
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
