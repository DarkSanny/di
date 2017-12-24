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

		public Result<Rectangle> PutNextRectangle(Size rectangleSize)
		{
			var rectangle = Result.Of(() => FindPlaceForNextRectangle(rectangleSize), $"Не корректный размер прямоугольника {rectangleSize}");
			if (rectangle.IsSuccess)
				_cloud.Add(rectangle.Value);
			return rectangle;
		}

		private Rectangle FindPlaceForNextRectangle(Size size)
		{
			if (!IsValidSize(size)) throw new ArgumentException();
			var intersectingRectangle = default(Rectangle);
			while (true)
			{
				var point = _function.GetNextPoint();
				var nextRectangle = CreateRectangle(point, size);
				if (intersectingRectangle != default(Rectangle) && nextRectangle.IntersectsWith(intersectingRectangle))
					continue;
				intersectingRectangle = GetIntersection(nextRectangle);
				if (intersectingRectangle != default(Rectangle)) continue;
				return TryMoveToCenter(nextRectangle, _function);
			}
		}

		private Rectangle TryMoveToCenter(Rectangle rectangle, PolarFunction function)
		{
			var correctRectangle = rectangle;
			var line = new PolarDecreasingLine(function.Center, function.Length, function.Angle);
			while (true)
			{
				var point = line.GetNextPoint();
				var rextRectangle = CreateRectangle(point, rectangle.Size);
				var intersectingRectangle = GetIntersection(rextRectangle);
				if (intersectingRectangle == default(Rectangle) && Math.Abs(line.Length) > 1e-10)
					correctRectangle = rextRectangle;
				else return correctRectangle;
			}
		}

		private Rectangle GetIntersection(Rectangle rectangle) => _cloud
			.FirstOrDefault(rect => rect.IntersectsWith(rectangle));

		private static Rectangle CreateRectangle(Point point, Size size) =>
			new Rectangle(point.X - size.Width / 2, point.Y - size.Height / 2, size.Width, size.Height);

		private static bool IsValidSize(Size rectangleSize)
		{
			return rectangleSize.Width > 0 && rectangleSize.Height > 0;
		}
	}
}
