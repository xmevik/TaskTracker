namespace TaskTracker.Exceptions
{
   /// <summary>
   /// Represents an exception that is thrown when a task is not found.
   /// </summary>
   public class TaskNotFoundException : Exception
   {
       /// <summary>
       /// Gets or sets the ID of the task.
       /// </summary>
       public int TaskId { get; set; }

       /// <summary>
       /// Initializes a new instance of the <see cref="TaskNotFoundException"/> class.
       /// </summary>
       /// <param name="taskId">The ID of the task.</param>
       public TaskNotFoundException(int taskId)
           : base($"Task with id {taskId} was not found")
       {
           TaskId = taskId;
       }

       /// <summary>
       /// Initializes a new instance of the <see cref="TaskNotFoundException"/> class.
       /// </summary>
       /// <param name="taskId">The ID of the task.</param>
       /// <param name="message">The message that describes the error.</param>
       public TaskNotFoundException(int taskId, string message)
           : base($"Task with id {taskId} was not found. more info: {message}")
       {
           TaskId = taskId;
       }
   }

}
