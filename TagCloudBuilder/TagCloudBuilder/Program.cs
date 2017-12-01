using System;
using System.Drawing;
using Autofac;
using TagCloudBuilder.TagCloudBuilder;
using TagCloudBuilder.TagCloudBuilder.PolarFunctions;
using TagCloudBuilder.WordsConverter;

namespace TagCloudBuilder
{
	public class Program
	{
		public static void Main()
		{
			var builder = ConfigurateBuilder();
			var container = builder.Build();
			var image = container.Resolve<IImageBuilder>().BuildImage();
			var saver = container.Resolve<IImageSaver>();
			saver.SaveImage(image);
		}

		private static ContainerBuilder ConfigurateBuilder()
		{
			var builder = new ContainerBuilder();
			ConfigurateBuilderForGettingWords(builder);
			ConfigurateBuilderForCloud(builder);
			ConfigurateBuilderForSavingImage(builder);
			return builder;
		}

		private static void ConfigurateBuilderForSavingImage(ContainerBuilder builder)
		{
			builder.RegisterType<PngSaver>().As<IImageSaver>()
				.WithParameter("filepath", "Image1.png");
		}

		private static void ConfigurateBuilderForCloud(ContainerBuilder builder)
		{
			builder.RegisterType<PolarSpiral>().As<IFunction>()
				.WithParameter("center", new Point(500, 500))
				.WithParameter("coefficient", 0.5);
			builder.RegisterType<WordDrawer>().As<IWordDrawer>()
				.WithParameter("fontFamily", new FontFamily("Arial"))
				.WithParameter("style", new FontStyle())
				.WithParameter("brush", Brushes.Black);
			builder.RegisterType<SortedWeightedWords>().As<IWordWeighter>();
			builder.RegisterType<CircularCloudBuilder>().As<ICloudBuilder>();
			builder.RegisterType<CloudImageBuilder>().As<IImageBuilder>()
				.WithParameter("imageSize", new Size(1000, 1000));
		}

		private static void ConfigurateBuilderForGettingWords(ContainerBuilder builder)
		{
			builder.RegisterType<WordReaderFromFile>().As<IWordReader>()
				.WithParameter("filename", Environment.CurrentDirectory + "\\text.txt");
			builder.RegisterType<BoringWordsAnalizator>().As<IWordAnalizator>();
			builder.RegisterType<WordConverter>().As<IWordConverter>();
			builder.RegisterType<WordsFilter>().As<IWordFilter>();
		}
	}
}
