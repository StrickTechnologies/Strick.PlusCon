using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strick.PlusCon.Test;
internal static class Expectations
{
	public static char esc = (char)27; //escape is char 27

	public static string ResetAll => $"{esc}[0m";

	private static string csFore = "38";  //ColorSpace.fore.AsString();
	private static string csBack = "48";  //ColorSpace.back.AsString();


	//don't use named colors... e.g. green has a green component of 128, not 255
	public static Color red = Color.FromArgb(255, 255, 0, 0);
	public static Color green = Color.FromArgb(255, 0, 255, 0);
	public static Color blue = Color.FromArgb(255, 0, 0, 255);
	public static Color white = Color.FromArgb(255, 255, 255, 255);
	public static Color black = Color.FromArgb(255, 0, 0, 0);


	public static string ForeColorRed = red.ForeColorString();     // $"{esc}[{csFore};2;255;0;0m";
	public static string ForeColorGreen = green.ForeColorString(); // $"{esc}[{csFore};2;0;255;0m";
	public static string ForeColorBlue = blue.ForeColorString();   // $"{esc}[{csFore};2;0;0;255m";
	public static string ForeColorWhite = white.ForeColorString(); // $"{esc}[{csFore};2;255;255;255m";
	public static string ForeColorBlack = black.ForeColorString(); // $"{esc}[{csFore};2;0;0;0m";
	public static string BackColorRed = red.BackColorString();     // $"{esc}[{csBack};2;255;0;0m";
	public static string BackColorGreen = green.BackColorString(); // $"{esc}[{csBack};2;0;255;0m";
	public static string BackColorBlue = blue.BackColorString();   // $"{esc}[{csBack};2;0;0;255m";
	public static string BackColorWhite = white.BackColorString(); // $"{esc}[{csBack};2;255;255;255m";
	public static string BackColorBlack = black.BackColorString(); // $"{esc}[{csBack};2;0;0;0m";


	public static string ForeColorReset => $"{esc}[{ColorSpace.foreReset.AsString()}m";
	public static string BackColorReset => $"{esc}[{ColorSpace.backReset.AsString()}m";


	public static string Underline => $"{esc}[4m";
	public static string UnderlineReset => $"{esc}[24m";

	public static string Reverse => $"{esc}[7m";
	public static string ReverseReset => $"{esc}[27m";


	public static string ForeColorString(this Color color) => $"{esc}[{csFore};2;{color.R};{color.G};{color.B}m";
	public static string BackColorString(this Color color) => $"{esc}[{csBack};2;{color.R};{color.G};{color.B}m";
}
