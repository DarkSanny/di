using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloudBuilder
{
	public static class BitmapExtensions
	{

		public static void SaveImage(this Bitmap bitmap, IImageSaver saver)
		{
			saver.SaveImage(bitmap);
		}

	}
}
