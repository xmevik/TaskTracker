using TaskTracker.Exceptions;
using TaskTracker.Models;
using TaskTracker.Models.Interfaces;

namespace TaskTracker.Controllers
{
    /// <summary>
    /// Represents a controller for handling tasks.
    /// </summary>
    public class TaskController
    {
        private IRWController FilesController;
        private List<Tasks> LoadedTasks = null!;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskController"/> class.
        /// </summary>
        /// <param name="filesController">The files controller.</param>
        public TaskController(IRWController filesController)
        {
            FilesController = filesController;
            LoadTasks();
        }

        /// <summary>
        /// Adds a new task.
        /// </summary>
        /// <param name="description">The description of the task.</param>
        public void AddTask(string description)
        {
            int? lastId = LoadedTasks.OrderByDescending(x => x.Id).FirstOrDefault()?.Id;

            LoadedTasks.Add(new()
            {
                Id = lastId != null ? lastId.Value + 1 : 1,
                Description = description
            });

            ValidateTasks();
        }

        /// <summary>
        /// Updates the task with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the task.</param>
        /// <param name="description">The new description of the task.</param>
        public void UpdateTask(int id, string description)
        {
            Tasks? newTask = FindTaskById(id);

            if (newTask is not null)
            {
                newTask.Description = description;
                newTask.UpdatedAt = DateTime.Now;
                ValidateTasks();
            }
            else
                throw new TaskNotFoundException(id);
        }

        /// <summary>
        /// Deletes the task with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the task.</param>
        public void DeleteTask(int id)
        {
            Tasks? taskToRemove = FindTaskById(id);

            if (taskToRemove is not null)
            {
                LoadedTasks.Remove(taskToRemove);
                ValidateTasks();
            }
            else
                throw new TaskNotFoundException(id);
        }

        /// <summary>
        /// Changes the state of the task with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the task.</param>
        /// <param name="status">The new status of the task.</param>
        public void ChangeState(int id, Status status)
        {
            Tasks? taskToChange = FindTaskById(id);

            if (taskToChange is not null)
            {
                taskToChange.Status = status;
                taskToChange.UpdatedAt = DateTime.Now;

                ValidateTasks();
            }
            else
                throw new TaskNotFoundException(id);
        }

        /// <summary>
        /// Gets the tasks with the specified status.
        /// </summary>
        /// <param name="status">The status of the tasks.</param>
        /// <returns>The list of tasks.</returns>
        public IList<Tasks> GetTasks(Status? status)
        {
            if (status != null)
                return LoadedTasks.FindAll(x => x.Status == status);
            return LoadedTasks;
        }

        private Tasks? FindTaskById(int id)
        {
            return LoadedTasks.FirstOrDefault(x => x.Id == id);
        }

        private void LoadTasks()
        {
            LoadedTasks = FilesController.ReadTasks().ToList();
        }

        private void ValidateTasks()
        {
            FilesController.WriteTasks(LoadedTasks);
        }
    }

}
