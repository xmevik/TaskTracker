using Moq;
using TaskTracker.Models.Interfaces;
using TaskTracker.Services;

namespace TaskTracker.UnitTests
{
    public class CommandServiceTests
    {
        [Fact]
        public void ProcessCommand_WithNoCommandProvided_ShouldThrowArgumentNullException()
        {
            var commandService = new CommandService();
            Assert.Throws<ArgumentNullException>(() => commandService.ProcessCommand(new string[0]));
        }

        [Fact]
        public void ProcessCommand_WithUnknownCommand_ShouldThrowArgumentException()
        {
            var commandService = new CommandService();
            Assert.Throws<ArgumentException>(() => commandService.ProcessCommand(new[] { "unknown" }));
        }

        [Fact]
        public void ProcessCommand_WithValidCommand_ShouldExecuteCommand()
        {
            var mockCommand = new Mock<ICommand>();
            mockCommand.Setup(x => x.CommandName).Returns("add");
            mockCommand.Setup(x => x.Execute(It.IsAny<string[]>())).Verifiable();

            var commandService = new CommandService(new List<ICommand> { mockCommand.Object });
            commandService.ProcessCommand(new[] { "add", "description" });

            mockCommand.Verify(x => x.Execute(It.IsAny<string[]>()), Times.Once);
        }

        [Fact]
        public void AddCommand_ShouldAddCommandToList()
        {
            var mockCommand = new Mock<ICommand>();
            var commandService = new CommandService();

            commandService.AddCommand(mockCommand.Object);

            Assert.Contains(mockCommand.Object, commandService.Commands);
        }
    }
}
