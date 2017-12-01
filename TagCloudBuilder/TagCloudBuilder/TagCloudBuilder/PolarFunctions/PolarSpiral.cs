using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
