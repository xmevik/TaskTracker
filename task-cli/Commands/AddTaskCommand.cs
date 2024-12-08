using TaskTracker.Controllers;
using TaskTracker.Models.Interfaces;

namespace TaskTracker.Commands
{
    public class AddTaskCommand : ICommand
    {
        private readonly TaskController TaskController;
        public string CommandName
        {
            get
            {
                return "add";
            }
        }

        public AddTaskCommand(TaskController taskController)
        {
            TaskController = taskController;
        }

        public void Execute(string[] args)
        {
            var description = string.Join(" ", args);
            if (string.IsNullOrWhiteSpace(description))
            {
                Console.WriteLine("Error: No Description were provided");
                return;
            }

            try
            {
                TaskController.AddTask(description);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding task: {ex.Message}");
            }

        }
    }
}
