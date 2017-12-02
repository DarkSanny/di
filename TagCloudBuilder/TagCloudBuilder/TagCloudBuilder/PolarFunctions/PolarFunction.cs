using System;
using System.Drawing;

namespace TagCloudBuilder.TagCloudBuilder.PolarFunctions
{


	public abstract class PolarFunction : IFunction
	{

		public double Length { get; protected set; }
		public double Angle { get; protected set; }
		public Point Center { get; protected set; }

		protected PolarFunction(Point center)
		{
			Center = center;
		}

		public abstract Point GetNextPoint();

		protected Point GetCartesianPoint(double length, double angle)
		{
			return new Point(Center.X + (int)(length * Math.Cos(angle)), 
							 Center.Y + (int)(length * Math.Sin(angle)));
		}
	}
}
