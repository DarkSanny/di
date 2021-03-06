﻿using System;
using System.Drawing;

namespace TagCloudBuilder.TagCloudBuilder.PolarFunctions
{
	public class PolarSpiral : PolarFunction
	{
		private readonly double _coefficient;

		public PolarSpiral(Point center, double coefficient) : base(center)
		{
			_coefficient = coefficient;
		}

		public override Point GetNextPoint()
		{
			Angle += Math.PI / 180;
			Length = Angle * _coefficient;
			return GetCartesianPoint(Length, Angle);
		}
	}
}
