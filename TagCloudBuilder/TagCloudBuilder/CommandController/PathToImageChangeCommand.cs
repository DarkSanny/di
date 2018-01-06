using System;
using TagCloudBuilder.TagCloudBuilder;

namespace TagCloudBuilder.CommandController
{
	public class PathToImageChangeCommand  : ICommand
	{
		private readonly CloudSettings _settings;

		public PathToImageChangeCommand(CloudSettings settings)
		{
			_settings = settings;
		}
		public Result<None> Execute(string[] args)
		{
			return Result.OfAction(() =>
			{
			   if (args.Length != 2)
				   throw new Exception("Incorrect syntax \n" + GetCommandSyntax());
				_settings.PathToImage = args[0];
				_settings.ImageName = args[1];
			});
		}

		public string GetCommandName()
		{
			return "set pathimage";
		}

		public string GetCommandSyntax()
		{
			return "set pathimage <string>Path <string>ImageName";
		}

		public string GetSuccessMessage()
		{
			return $"Path to Image changed: {_settings.PathToImage}\\{_settings.ImageName}";
		}
	}
}
