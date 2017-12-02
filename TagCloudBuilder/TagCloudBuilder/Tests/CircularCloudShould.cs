using System;
using System.Drawing;
using Autofac;
using FluentAssertions;
using NUnit.Framework;
using TagCloudBuilder.TagCloudBuilder;
using TagCloudBuilder.TagCloudBuilder.PolarFunctions;

namespace TagCloudBuilder.Tests
{

	[TestFixture]
	public class CircularCloudShould
	{

		private CircularCloudBuilder _builder;
		private Point _center;

		[SetUp]
		public void SetUp()
		{
			var containerBuilder = new ContainerBuilder();
			_center = new Point(500, 500);
			containerBuilder.RegisterType<PolarSpiral>().As<PolarFunction>()
				.WithParameter("center", _center)
				.WithParameter("coefficient", 0.5);
			containerBuilder.RegisterType<CircularCloudBuilder>();
			var container = containerBuilder.Build();
			_builder = container.Resolve<CircularCloudBuilder>();
		}

		[Test]
		public void RectShouldHaveSameSize()
		{
			var size = new Size(5, 4);
			var rect = _builder.PutNextRectangle(size);

			rect.Size.Should().Be(size);
		}

		[Test]
		public void Cloud_ShouldThrowWhenDefaultSize()
		{
			Action act = () => _builder.PutNextRectangle(default(Size));

			act.ShouldThrow<ArgumentException>();
		}

		[Test]
		public void Cloud_ShouldThrowWhenNegativeSize()
		{
			Size size = new Size(-5, -1);
			Action act = () => _builder.PutNextRectangle(size);

			act.ShouldThrow<ArgumentException>();
		}

		[Test]
		public void FirstRectShouldIntersectCenter()
		{
			var firstRect = _builder.PutNextRectangle(new Size(5, 4));

			firstRect.IntersectsWith(new Rectangle(_center.X, _center.Y, 1, 1)).Should().BeTrue();
		}

		[Test]
		public void SecondRectShouldNotIntersectsFirstRect()
		{
			var firstRect = _builder.PutNextRectangle(new Size(5, 4));
			var secondRect = _builder.PutNextRectangle(new Size(5, 4));

			firstRect.IntersectsWith(secondRect).Should().BeFalse();
		}

		[Test]
		public void RectsShouldBeNearToCenter()
		{
			var size = new Size(1, 1);
			for (var i = 0; i < 9; i++)
				_builder.PutNextRectangle(size);

			DistanceToCenter(_builder.PutNextRectangle(size), _center).Should().BeLessOrEqualTo(5);
		}



		public static double DistanceToCenter(Rectangle rect, Point center)
		{
			var rectCenter = GetRectCenter(rect);
			return Math.Sqrt((center.X - rectCenter.X) * (center.X - rectCenter.X) +
			                 (center.Y - rectCenter.Y) * (center.Y - rectCenter.Y));
		}

		public static Point GetRectCenter(Rectangle rect) => new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);


	}
}
