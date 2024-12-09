using System.Text;
using System.Text.Json;
using TaskTracker.Models;
using TaskTracker.Models.Interfaces;

namespace TaskTracker.Controllers
{
   /// <summary>
   /// Represents a controller for handling JSON files.
   /// </summary>
   internal class JsonFilesController : IRWController
   {
       private readonly string filePath;
       private FileStream? fstream;

       /// <summary>
       /// Initializes a new instance of the <see cref="JsonFilesController"/> class.
       /// </summary>
       /// <param name="filePath">The file path.</param>
       public JsonFilesController(string filePath)
       {
           this.filePath = filePath;
       }

       /// <summary>
       /// Reads the tasks from the JSON file.
       /// </summary>
       /// <returns>The list of tasks.</returns>
       public IList<Tasks> ReadTasks()
       {
           fstream = BuildStream(FileMode.OpenOrCreate, FileAccess.Read);

           byte[] buffer = new byte[fstream.Length];
           fstream.Read(buffer, 0, buffer.Length);
           string tasksJson = Encoding.Default.GetString(buffer);

           List<Tasks> tasks;

           if (tasksJson.Length > 0)
               tasks = JsonSerializer.Deserialize<List<Tasks>>(tasksJson) ?? new();
           else
               tasks = new();

           fstream.Dispose();

           return tasks;
       }

       /// <summary>
       /// Writes the tasks to the JSON file.
       /// </summary>
       /// <param name="tasks">The list of tasks.</param>
       public void WriteTasks(IList<Tasks> tasks)
       {
           fstream = BuildStream(FileMode.Truncate, FileAccess.Write);

           string tasksJson = JsonSerializer.Serialize(tasks);
           byte[] buffer = Encoding.Default.GetBytes(tasksJson);

           fstream.Write(buffer, 0, buffer.Length);

           fstream.Dispose();
       }

       private FileStream BuildStream(FileMode fileMode, FileAccess fileAccess)
       {
           return new(filePath, fileMode, fileAccess);
       }
   }

}
