namespace TagCloudBuilder.CommandController
{
	public interface ICommand
	{
		Result<None> Execute(string[] args);
		string GetCommandName();
		string GetCommandSyntax();
	}
}
