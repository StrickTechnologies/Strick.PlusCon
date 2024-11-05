using System.Drawing;

using Strick.PlusCon.Models;


namespace Strick.PlusCon.Test.Models;


internal class PCMenu : Menu
{
	public PCMenu(string subTitle) : base()
	{
		Title = new(Sys.BannerText, Sys.TitleStyle);
		Subtitle = new(subTitle.SpaceOut(), Sys.TitleStyle);
		OptionsStyle = new(Color.White);

		Prompt!.Style = new(Color.LightGreen);
	}
}
