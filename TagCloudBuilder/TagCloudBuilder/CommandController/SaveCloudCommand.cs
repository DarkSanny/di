using System;
using TagCloudBuilder.TagCloudBuilder;

namespace TagCloudBuilder.CommandController
{
	public class SaveCloudCommand : ICommand
	{
		private readonly IImageSaver _saver;
		private readonly Lazy<IImageBuilder> _builder;
		private readonly CloudSettings _settings;

		public SaveCloudCommand(IImageSaver saver, Lazy<IImageBuilder> builder, CloudSettings settings)
		{
			_saver = saver;
			_builder = builder;
			_settings = settings;
		}

		public Result<None> Execute(string[] args)
		{
			return Result.OfAction(() => _saver.SaveImage(_builder.Value.BuildImage().GetValueOrThrow(), _settings.PathToImage + "\\" + _settings.ImageName));
		}

		public string GetCommandName()
		{
			return "buildimage";
		}

		public string GetCommandSyntax()
		{
			return "buildimage";
		}

		public string GetSuccessMessage()
		{
			return $"Image was saved here : {_settings.PathToImage}\\{_settings.ImageName}";
		}
	}
}
