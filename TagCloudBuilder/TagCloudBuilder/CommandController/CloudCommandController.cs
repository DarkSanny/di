using System;
using System.IO;
using System.Linq;

namespace TagCloudBuilder.CommandController
{
	public class CloudCommandController : ICommandController
	{
		private readonly TextWriter _writer;
		private readonly ICommand[] _commands;

		public CloudCommandController(TextWriter writer, params ICommand[] commands)
		{
			_writer = writer;
			_commands = commands;
		}

		public Result<None> Execute(string commandLine)
		{
			return Result.OfAction(() =>
			{
				commandLine = string.Join(" ", commandLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
				var command = _commands
					.Where(c => commandLine.StartsWith(c.GetCommandName()))
					.OrderByDescending(c => c.GetCommandName().Length)
					.FirstOrDefault();
				if (command != null)
				{
					var result = command
						.Execute(commandLine.Substring(command.GetCommandName().Length)
						.Split(new [] {' '}, StringSplitOptions.RemoveEmptyEntries));
					if (result.IsSuccess)
						_writer.WriteLine(command.GetSuccessMessage());
					else _writer.WriteLine("Incorrect syntax \n" + command.GetCommandSyntax());
				}	
				else _writer.WriteLine("Incorrect command!");
			});
		}
	}
}
