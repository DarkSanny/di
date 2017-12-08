using System.Drawing;
using System.IO;
using System.Linq;
using TagCloudBuilder.TagCloudBuilder;

namespace TagCloudBuilder.CommandController
{
	public class ColorChangeCommand : ICommand
	{
		private readonly TextWriter _writer;
		private readonly WordDrawer _drawer;

		public ColorChangeCommand(TextWriter writer, WordDrawer drawer)
		{
			_writer = writer;
			_drawer = drawer;
		}

		public void Execute(string[] args)
		{
			if (!IsCorrectArgs(args))
				_writer.WriteLine(GetErrorMessage());
			else
			{
				var rgb = args.Select(int.Parse).ToList();
				 _drawer.SetBrush(new SolidBrush(Color.FromArgb(100, rgb[0], rgb[1], rgb[2])));
			}
		}

		private bool IsCorrectArgs(string[] args)
		{
			if (args.Length != 3) return false;
			if (args.Select(arg => int.TryParse(arg, out _)).Any(arg => arg == false)) return false;
			if (args.Select(int.Parse).Any(arg => arg < 0 || arg > 255)) return false;
			return true;
		}

		private string GetErrorMessage()
		{
			return "Incorrect arguments! Should be: \n" + GetCommandSyntax();
		}

		public string GetCommandName()
		{
			return "set color ";
		}

		public string GetCommandSyntax()
		{
			return "set color <int: MaxValue 255>R <int: MaxValue 255>G <int: MaxValue 255>B";
		}
	}
}