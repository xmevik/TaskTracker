using TaskTracker.Models.Interfaces;

namespace TaskTracker.Services
{
   /// <summary>
   /// Represents a service for processing commands.
   /// </summary>
   public class CommandService
   {
       /// <summary>
       /// Gets the list of commands.
       /// </summary>
       public readonly List<ICommand> Commands;

       /// <summary>
       /// Initializes a new instance of the <see cref="CommandService"/> class.
       /// </summary>
       public CommandService() : this(new List<ICommand>()) { }

       /// <summary>
       /// Initializes a new instance of the <see cref="CommandService"/> class.
       /// </summary>
       /// <param name="commands">The list of commands.</param>
       public CommandService(List<ICommand> commands)
       {
           Commands = commands;
       }

       /// <summary>
       /// Processes the specified command.
       /// </summary>
       /// <param name="args">The arguments.</param>
       public void ProcessCommand(string[] args)
       {
           if (args.Length == 0)
           {
               throw new ArgumentNullException("Error: No command provided.");
           }

           var commandType = args[0].ToLower();
           var command = Commands.FindLast(x => x.CommandName == commandType);

           if (command is not null)
               command.Execute(args.Skip(1).ToArray());
           else
           {
               throw new ArgumentException($"Unknown command: {commandType}");
           }
       }

       /// <summary>
       /// Adds the specified command to the list of commands.
       /// </summary>
       /// <param name="command">The command.</param>
       public void AddCommand(ICommand command)
       {
           Commands.Add(command);
       }
   }

}
