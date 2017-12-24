namespace TagCloudBuilder.CommandController
{
	public interface ICommandController
	{
		Result<None> Execute(string commandLine);
	}
}
