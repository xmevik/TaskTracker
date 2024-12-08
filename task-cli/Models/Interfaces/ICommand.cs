namespace TaskTracker.Models.Interfaces
{
    public interface ICommand
    {
        public string CommandName { get; }
        void Execute(string[] args);
    }
}
