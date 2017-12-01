﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloudBuilder.TagCloudBuilder.PolarFunctions;

namespace TagCloudBuilder.TagCloudBuilder
{
	public class CircularCloudBuilder : ICloudBuilder
	{
		private readonly IFunction _function;
		private readonly List<Rectangle> _cloud;

		public CircularCloudBuilder(IFunction function)
		{
			_function = function;
			_cloud = new List<Rectangle>();
		}

		public Rectangle PutNextRectangle(Size rectangleSize)
		{
			var rectangle = FindPlaceForNextWord(rectangleSize);
			_cloud.Add(rectangle);
			return rectangle;
		}

		private Rectangle FindPlaceForNextWord(Size size)
		{
			if (IsShouldThrowArgumentException(size)) throw new ArgumentException();
			var intersectRect = default(Rectangle);
			while (true)
			{
				var point = _function.GetNextPoint();
				var rectangle = CreateRectangle(point, size);
				if (intersectRect != default(Rectangle) && rectangle.IntersectsWith(intersectRect))
					continue;
				intersectRect = GetIntersect(rectangle);
				if (intersectRect != default(Rectangle)) continue;
				return rectangle;
			}
		}

		private Rectangle GetIntersect(Rectangle rectangle) => _cloud
			.FirstOrDefault(rect => rect.IntersectsWith(rectangle));

		private static Rectangle CreateRectangle(Point point, Size size) =>
			new Rectangle(point.X - size.Width / 2, point.Y - size.Height / 2, size.Width, size.Height);

		private static bool IsShouldThrowArgumentException(Size rectangleSize)
		{
			return rectangleSize.Width <= 0 || rectangleSize.Height <= 0;
		}
	}
}
