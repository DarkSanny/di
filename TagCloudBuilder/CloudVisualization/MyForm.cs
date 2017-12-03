using System.Windows.Forms;
using TagCloudBuilder;

namespace CloudVisualization
{
	public class MyForm : Form
	{
		public MyForm()
		{
			var cloud = CloudFactory.CreateCloud();
			Size = cloud.Size;
			Paint += (sender, args) =>
			{
				args.Graphics.DrawImage(cloud, 0, 0);
			};
			cloud.SaveImage(CloudFactory.CreateSaver(), "Image1.png");
		}
	}
}