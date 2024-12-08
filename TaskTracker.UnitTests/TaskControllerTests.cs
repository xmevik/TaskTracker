using Moq;
using TaskTracker.Models;
using TaskTracker.Controllers;
using TaskTracker.Models.Interfaces;

namespace TaskTracker.UnitTests
{
    public class TaskControllerTests
    {
        public Mock<IRWController> mockFilesController;
        public IList<Tasks> result;
        public TaskControllerTests()
        {
            mockFilesController = new();
            result = [];
            Setup();
        }

        void Setup()
        {
            mockFilesController.Setup(x => x.WriteTasks(It.IsAny<IList<Tasks>>()))
                               .Callback<IList<Tasks>>(x => result = x);
        }

        [Fact]
        public void AddTask_ShouldAddTask()
        {
            mockFilesController.Setup(x => x.ReadTasks()).Returns([]);

            var taskController = new TaskController(mockFilesController.Object);

            taskController.AddTask("Test task");

            Assert.Single(result);
            Assert.Equal("Test task", result[0].Description);
        }

        [Fact]
        public void UpdateTask_ShouldUpdateTask()
        {
            mockFilesController.Setup(x => x.ReadTasks()).Returns([new Tasks { Id = 1, Description = "Old description" }]);
            var taskController = new TaskController(mockFilesController.Object);

            taskController.UpdateTask(1, "New description");

            Assert.Equal("New description", result[0].Description);
        }

        [Fact]
        public void DeleteTask_ShouldDeleteTask()
        {
            mockFilesController.Setup(x => x.ReadTasks()).Returns([new Tasks { Id = 1, Description = "Test task" }]);
            var taskController = new TaskController(mockFilesController.Object);

            taskController.DeleteTask(1);

            Assert.Empty(result);
        }

        [Fact]
        public void ChangeState_ShouldChangeTaskState()
        {
            mockFilesController.Setup(x => x.ReadTasks()).Returns([new Tasks { Id = 1, Description = "", Status = Status.todo }]);
            var taskController = new TaskController(mockFilesController.Object);

            taskController.ChangeState(1, Status.inProgress);

            Assert.Equal(Status.inProgress, result[0].Status);
        }

        [Fact]
        public void GetTasks_ShouldReturnTasksByStatus()
        {
            mockFilesController.Setup(x => x.ReadTasks()).Returns([new Tasks { Id = 1, Description = "", Status = Status.todo }, new Tasks { Id = 2, Description = "", Status = Status.inProgress }]);
            var taskController = new TaskController(mockFilesController.Object);

            var result = taskController.GetTasks(Status.todo);

            Assert.Single(result);
            Assert.Equal(Status.todo, result[0].Status);
        }
    }
}