using System.Drawing;

using Strick.PlusCon.Models;

using static Strick.PlusCon.Helpers;


namespace Strick.PlusCon.Test.Models.Examples;


internal static class TextStyleExamples
{
	static TextStyleExamples()
	{
		Samples = new List<DocSample>()
		{
			new DocSample("textstyle1", "Example - TextStyle", Ex_TextStyle_1),
			new DocSample("styledtext1", "Example - StyledText", Ex_StyledText_1),
		};

	}

	internal static List<DocSample> Samples;

	internal static string MenuTitle => "TextStyle";


	internal static void Ex_TextStyle_1()
	{
		TextStyle ts = new(Color.White, Color.DodgerBlue, Color.White);
		//same style, different content
		foreach (string s in new[] { "content 1", "level 2" })
		{ WL(ts.StyleText(s)); }

		ts.ForeColor = Color.Red;
		ts.Underline = true;
		ts.ClearGradient();
		WL(ts.StyleText("Hello World!"));

		ts.Underline = false;
		ts.ForeColor = null;
		ts.BackColor = Color.White;
		ts.SetGradientColors(Color.Black, Color.White);
		WL(ts.StyleText("***fade-out***"));

		ts.SetGradientColors(Color.White, Color.Black);
		WL(ts.StyleText("***fade-in!***"));

		ts.BackColor = null;
		ts.Reverse = true;
		ts.SetGradientColors(Color.SandyBrown, Color.FromArgb(3, 240, 165), Color.FromArgb(145, 193, 255));
		WL(ts.StyleText("-- ** down on the beach ** --"));

		ts.Reverse = false;
		ts.Underline = true;
		W(ts.StyleText("-- ** down on the beach ** --"));
	}

	internal static void Ex_StyledText_1()
	{
		StyledText st = new("Hello World!", Color.Blue);

		//same content, different styling
		foreach (Color c in ColorUtilities.GetGradientColors(Color.FromArgb(0, 255, 0), Color.FromArgb(0, 128, 0), 4))
		{
			st.Style.BackColor = c;
			WL(st.TextStyled);
		}
		//same styling, alternate content
		WL(st.StyleText("Blue in Green"));

		//back to default
		st.Style = new();
		st.Text = "Default styling";
		WL(st.TextStyled);

		//different content, different styling
		st.Style.BackColor = Color.DarkGray;
		st.Style.ForeColor = Color.White;
		st.Text = "(not) " + st.Text;
		W(st.TextStyled);
	}
}
