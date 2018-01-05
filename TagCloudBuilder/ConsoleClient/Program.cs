using System;
using TagCloudBuilder;

namespace ConsoleClient
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var controller = CloudFactory.CreateCommandController();

			while (true)
			{
				var command = Console.ReadLine();
				var result = controller.Execute(command);
				if (!result.IsSuccess)
					Console.WriteLine(result.Error);
			}
		}
	}
}
