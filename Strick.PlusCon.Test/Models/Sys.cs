using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Strick.PlusCon.Models;


namespace Strick.PlusCon.Test.Models;


internal static class Sys
{
	static Sys()
	{
		BannerText = $" {About.ProductName} ".SpaceOut();

		TitleStyle = new TextStyle(Color.White, Color.Red, Color.White)
		{
			BackColor = Color.DarkSlateGray,
			Reverse = true
		};

		TitleStyle_Alt = new TextStyle(TitleStyle)
		{
			Reverse = false,
			BackColor = null
		};
	}

	internal static string BannerText { get; }

	internal static TextStyle TitleStyle { get; }

	internal static TextStyle TitleStyle_Alt { get; }
}
