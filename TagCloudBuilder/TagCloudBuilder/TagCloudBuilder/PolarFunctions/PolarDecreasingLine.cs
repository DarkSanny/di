using System;
using System.Drawing;

namespace TagCloudBuilder.TagCloudBuilder.PolarFunctions
{
	public class PolarDecreasingLine  : PolarFunction
	{
		public PolarDecreasingLine(Point center, double length, double angle) : base(center)
		{
			Length = length;
			Angle = angle;
		}

		public override Point GetNextPoint()
		{
			Length = Math.Max(0, Length - 1);
			return GetCartesianPoint(Length, Angle);
		}
	}
}
