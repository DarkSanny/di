using System.Windows.Forms;
using TagCloudBuilder;

namespace CloudVisualization
{
	public class MyForm : Form
	{
		public MyForm()
		{
			var cloud = CloudFactory.CreateCloud();
			cloud.Then(c => Size = c.Size);
			Paint += (sender, args) =>
			{
				cloud.Then(c => args.Graphics.DrawImage(c, 0, 0));
			};
			cloud.Then(c => c.SaveImage(CloudFactory.CreateSaver(), "Image1.png"));
		}
	}
}