using System;
using System.Drawing;
using Autofac;
using TagCloudBuilder.CommandController;
using TagCloudBuilder.TagCloudBuilder;
using TagCloudBuilder.TagCloudBuilder.PolarFunctions;
using TagCloudBuilder.WordsConverter;

namespace TagCloudBuilder
{
	public class CloudFactory
	{
		public static Result<Bitmap> CreateCloud()
		{
			var builder = ConfigurateBuilder();
			var container = builder.Build();
			return container.Resolve<IImageBuilder>().BuildImage();
		}

		public static ICommandController CreateCommandController()
		{
			var builder = ConfigurateBuilder();
			builder.RegisterType<ColorChangeCommand>().As<ICommand>();
			builder.RegisterType<ImageSizeChangeCommand>().As<ICommand>();
			builder.RegisterType<SaveCloudCommand>().As<ICommand>();
			builder.RegisterType<PathToWordsChangeCommand>().As<ICommand>();
			builder.RegisterType<PathToImageChangeCommand>().As<ICommand>();
			builder.RegisterType<HelpCommand>().As<ICommand>();
			builder.RegisterType<CloudCommandController>()
				.As<CloudCommandController>()
				.WithParameter("writer", Console.Out);
			var container = builder.Build();
			return container.Resolve<CloudCommandController>();
		}

		public static IImageSaver CreateSaver()
		{
			var builder = ConfigurateBuilder();
			var container = builder.Build();
			return container.Resolve<IImageSaver>();
		}

		private static ContainerBuilder ConfigurateBuilder()
		{
			var builder = new ContainerBuilder();
			ConfigurateBuilderForSettings(builder);
			ConfigurateBuilderForGettingWords(builder);
			ConfigurateBuilderForCloud(builder);
			ConfigurateBuilderForSavingImage(builder);
			return builder;
		}

		private static void ConfigurateBuilderForSettings(ContainerBuilder builder)
		{
			builder.RegisterType<CloudSettings>()
				.AsSelf()
				.WithParameter("pathToWords", Environment.CurrentDirectory + "\\FileExamples\\TextRu.txt")
				.WithParameter("pathToImage", Environment.CurrentDirectory)
				.WithParameter("imageName", "Image1.png")
				.WithParameter("imageSize", new Size(1000, 1000))
				.SingleInstance();
		}

		private static void ConfigurateBuilderForSavingImage(ContainerBuilder builder)
		{
			builder.RegisterType<PngSaver>().As<IImageSaver>();
		}

		private static void ConfigurateBuilderForCloud(ContainerBuilder builder)
		{
			builder.RegisterType<PolarSpiral>().As<PolarFunction>()
				.WithParameter("center", new Point(500, 500))
				.WithParameter("coefficient", 0.5);
			builder.RegisterType<WordDrawer>().As<IWordDrawer>().As<WordDrawer>()
				.SingleInstance();
			builder.RegisterType<SortedWeightedWords>().As<IWordWeighter>();
			builder.RegisterType<CircularCloudBuilder>().As<ICloudBuilder>();
			builder.RegisterType<CloudImageBuilder>().As<IImageBuilder>()
				.WithParameter("imageSize", new Size(1000, 1000));
		}

		private static void ConfigurateBuilderForGettingWords(ContainerBuilder builder)
		{
			builder.RegisterType<WordReaderFromFile>().As<IWordReader>()
				.WithParameter("filename", Environment.CurrentDirectory + "\\FileExamples\\TextRu.txt");
			builder.RegisterType<BoringWordsAnalyzer>().As<IWordAnalyzer>();
			builder.RegisterType<WordConverter>().As<IWordConverter>();
			builder.RegisterType<WordsFilter>().As<IWordFilter>();
		}
	}
}