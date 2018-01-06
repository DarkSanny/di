using System;
using System.Drawing;
using System.Linq;
using TagCloudBuilder.TagCloudBuilder;

namespace TagCloudBuilder.CommandController
{
	public class ImageSizeChangeCommand	 : ICommand
	{
		private readonly CloudSettings _settings;

		public ImageSizeChangeCommand(CloudSettings settings)
		{
			_settings = settings;
		}

		public Result<None> Execute(string[] args)
		{
			return Result.OfAction(() =>
			{
				if (!IsCorrectArgs(args))
					throw new Exception("Incorrect syntax \n" + GetCommandSyntax());
				var size = args.Select(int.Parse).ToList();
				_settings.ImageSize = new Size(size[0], size[1]);
			});
		}

		private bool IsCorrectArgs(string[] args)
		{
			if (args.Length != 2) return false;
			if (args.Select(arg => int.TryParse(arg, out _)).Any(arg => arg == false)) return false;
			if (args.Select(int.Parse).Any(arg => arg < 0)) return false;
			return true;
		}

		public string GetCommandName()
		{
			return "set size";
		}

		public string GetCommandSyntax()
		{
			return "set size <int: MinValue 0>Width <int: MinValue 0>Height";
		}

		public string GetSuccessMessage()
		{
			return $"Size changed: Width = {_settings.ImageSize.Width} Height = {_settings.ImageSize.Height}";
		}
	}
}
