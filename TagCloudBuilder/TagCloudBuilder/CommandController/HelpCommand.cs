using System;
using System.Linq;

namespace TagCloudBuilder.CommandController
{
	public class HelpCommand : ICommand
	{
		private readonly Lazy<ICommand[]> _commands;

		public HelpCommand(Lazy<ICommand[]> commands)
		{
			_commands = commands;
		}

		public Result<None> Execute(string[] args)
		{
			return Result.Ok();
		}

		public string GetCommandName()
		{
			return "help";
		}

		public string GetCommandSyntax()
		{
			return "help";
		}

		public string GetSuccessMessage()
		{
			return _commands.Value.Aggregate("", (current, command) => current + (command.GetCommandName() + "\n"));
		}
	}
}