namespace TagCloudBuilder.CommandController
{
	public interface ICommand
	{
		void Execute(string[] args);
		string GetCommandName();
		string GetCommandSyntax();
	}
}
