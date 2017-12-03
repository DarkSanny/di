using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloudBuilder.TagCloudBuilder.PolarFunctions;

namespace TagCloudBuilder.TagCloudBuilder
{
	public class CircularCloudBuilder : ICloudBuilder
	{
		private readonly PolarFunction _function;
		private readonly List<Rectangle> _cloud;

		public CircularCloudBuilder(PolarFunction function)
		{
			_function = function;
			_cloud = new List<Rectangle>();
		}

		public Rectangle PutNextRectangle(Size rectangleSize)
		{
			var rectangle = FindPlaceForNextRectangle(rectangleSize);
			_cloud.Add(rectangle);
			return rectangle;
		}

		private Rectangle FindPlaceForNextRectangle(Size size)
		{
			if (IsShouldThrowArgumentException(size)) throw new ArgumentException();
			var intersectingRectangle = default(Rectangle);
			while (true)
			{
				var point = _function.GetNextPoint();
				var rectangle = CreateRectangle(point, size);
				if (intersectingRectangle != default(Rectangle) && rectangle.IntersectsWith(intersectingRectangle))
					continue;
				intersectingRectangle = GetIntersection(rectangle);
				if (intersectingRectangle != default(Rectangle)) continue;
				return TryMoveToCenter(rectangle, _function);
			}
		}

		private Rectangle TryMoveToCenter(Rectangle rectangle, PolarFunction function)
		{
			var correctRectangle = rectangle;
			var line = new PolarDecreasingLine(function.Center, function.Length, function.Angle);
			while (true)
			{
				var point = line.GetNextPoint();
				var rect = CreateRectangle(point, rectangle.Size);
				var intersectingRect = GetIntersection(rect);
				if (intersectingRect == default(Rectangle) && Math.Abs(line.Length) > 1e-10)
					correctRectangle = rect;
				else return correctRectangle;
			}
		}

		private Rectangle GetIntersection(Rectangle rectangle) => _cloud
			.FirstOrDefault(rect => rect.IntersectsWith(rectangle));

		private static Rectangle CreateRectangle(Point point, Size size) =>
			new Rectangle(point.X - size.Width / 2, point.Y - size.Height / 2, size.Width, size.Height);

		private static bool IsShouldThrowArgumentException(Size rectangleSize)
		{
			return rectangleSize.Width <= 0 || rectangleSize.Height <= 0;
		}
	}
}
