using System;
using TagCloudBuilder.TagCloudBuilder;

namespace TagCloudBuilder.CommandController
{
	public class PathToWordsChangeCommand : ICommand
	{
		private readonly CloudSettings _settings;

		public PathToWordsChangeCommand(CloudSettings settings)
		{
			_settings = settings;
		}

		public Result<None> Execute(string[] args)
		{
			return Result.OfAction(() =>
			{
				if (args.Length != 1)
					throw new ArgumentException();
				_settings.PathToWords = args[0];
			});
		}

		public string GetCommandName()
		{
			return "set pathwords";
		}

		public string GetCommandSyntax()
		{
			return "set pathwords <string>Path";
		}

		public string GetSuccessMessage()
		{
			return $"Path to words was changed: {_settings.PathToWords}";
		}
	}
}
