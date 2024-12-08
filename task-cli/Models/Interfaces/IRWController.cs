namespace TaskTracker.Models.Interfaces
{
    public interface IRWController
    {
        public IList<Tasks> ReadTasks();
        public void WriteTasks(IList<Tasks> tasks);
    }
}
