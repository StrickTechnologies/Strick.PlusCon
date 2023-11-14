using System.Drawing;

using Strick.PlusCon.Models;


namespace Strick.PlusCon.Test.Models;


internal class PCMenu : Menu
{
	public PCMenu(string subTitle) : base()
	{
		TextStyle menuTitleStyle = new TextStyle(Color.White, Color.Red, Color.White)
		{
			BackColor = Color.DarkSlateGray,
			Reverse = true
		};

		Title = new(Program.BannerText, menuTitleStyle);
		Subtitle = new(subTitle.SpaceOut(), menuTitleStyle);
		OptionsStyle = new(Color.White);

		Prompt!.Style = new(Color.LightGreen);
	}
}
