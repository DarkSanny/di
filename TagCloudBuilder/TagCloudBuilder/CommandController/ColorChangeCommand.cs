using System;
using System.Drawing;
using System.Linq;
using TagCloudBuilder.TagCloudBuilder;

namespace TagCloudBuilder.CommandController
{
	public class ColorChangeCommand : ICommand
	{
		private readonly WordDrawer _drawer;

		public ColorChangeCommand(WordDrawer drawer)
		{
			_drawer = drawer;
		}

		public Result<None> Execute(string[] args)
		{
			return Result.OfAction(() =>
			{
				if (!IsCorrectArgs(args))
					throw new Exception("Incorrect syntax \n" + GetCommandSyntax());
				var rgb = args.Select(int.Parse).ToList();
				_drawer.Brush = new SolidBrush(Color.FromArgb(100, rgb[0], rgb[1], rgb[2]));
			});	
		}

		private bool IsCorrectArgs(string[] args)
		{
			if (args.Length != 3) return false;
			if (args.Select(arg => int.TryParse(arg, out _)).Any(arg => arg == false)) return false;
			if (args.Select(int.Parse).Any(arg => arg < 0 || arg > 255)) return false;
			return true;
		}

		public string GetCommandName()
		{
			return "set color";
		}

		public string GetCommandSyntax()
		{
			return "set color <int: MaxValue 255>R <int: MaxValue 255>G <int: MaxValue 255>B";
		}

		public string GetSuccessMessage()
		{
			return $"Text color is {_drawer.Brush}";
		}
	}
}