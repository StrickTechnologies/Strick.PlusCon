﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

using Strick.PlusCon.Models;

using static Strick.PlusCon.Test.Expectations;


namespace Strick.PlusCon.Test;


[TestClass]
public class GridTests
{
	[TestMethod]
	public void Basics()
	{
		Grid g = null!;

		Assert.IsNull(g);

		g = new Grid();
		CheckGridState(g, 0, 0);
		Assert.IsNotNull(g.CellStyle);
		Assert.IsNotNull(g.CellContentStyle);
		Assert.IsNotNull(g.ColumnHeaderCellStyle);
		Assert.IsNotNull(g.ColumnHeaderContentStyle);
		Assert.IsNotNull(g.Columns);
		Assert.AreEqual(0, g.Width);
		Assert.IsNull(g.Title);
		Assert.AreEqual(0, g.TitleLength);
		Assert.IsNull(g.Subtitle);
		Assert.AreEqual(0, g.SubTitleLength);
		Assert.IsNull(g.Footer);
		Assert.AreEqual(0, g.FooterLength);

		Assert.ThrowsException<InvalidOperationException>(() => g.Show()); //no columns (or rows)

		var col = g.Columns.Add("");
		CheckGridState(g, 1, 0);
		CheckColumnState(col, 0, null, 0, 0, 2);
		Assert.AreEqual(2, g.Width); //2=margins

		Assert.ThrowsException<InvalidOperationException>(() => g.Show()); //no rows

		col = g.Columns.Add("foo");
		CheckGridState(g, 2, 0);
		CheckColumnState(col, 1, "foo", 3, 3, 5);
		Assert.AreEqual(7, g.Width);


		var row = g.AddRow();
		CheckGridState(g, 2, 1);
		Assert.AreEqual(7, g.Width);

		//invalid handle trying to write to console -- this is just to check that it's trying to display the grid...
		Assert.ThrowsException<IOException>(() => g.Show());

		row = g.AddRow("", "");
		CheckGridState(g, 2, 2);
		Assert.AreEqual(7, g.Width);

		CheckCellState(g.Rows[0].Cells[0], 0, 0, null, g.CellStyle, g.CellContentStyle, g.Columns[0].CellLayout);
		g.CellStyle.BackColor = red;
		CheckCellState(g.Rows[0].Cells[0], 0, 0, null, g.CellStyle, g.CellContentStyle, g.Columns[0].CellLayout);
		g.Columns[0].CellStyle = new(green);
		CheckCellState(g.Rows[0].Cells[0], 0, 0, null, g.Columns[0].CellStyle!, g.CellContentStyle, g.Columns[0].CellLayout);
		g.Rows[0].CellStyle = new(blue);
		CheckCellState(g.Rows[0].Cells[0], 0, 0, null, g.Rows[0].CellStyle!, g.CellContentStyle, g.Columns[0].CellLayout);
		g.Rows[0].Cells[0].CellStyle = new(white);
		CheckCellState(g.Rows[0].Cells[0], 0, 0, null, g.Rows[0].Cells[0].CellStyle!, g.CellContentStyle, g.Columns[0].CellLayout);

		g.CellContentStyle.BackColor = red;
		CheckCellState(g.Rows[0].Cells[0], 0, 0, null, g.Rows[0].Cells[0].CellStyle!, g.CellContentStyle, g.Columns[0].CellLayout);
		g.Columns[0].ContentStyle = new(green);
		CheckCellState(g.Rows[0].Cells[0], 0, 0, null, g.Rows[0].Cells[0].CellStyle!, g.Columns[0].ContentStyle!, g.Columns[0].CellLayout);
		g.Rows[0].ContentStyle = new(blue);
		CheckCellState(g.Rows[0].Cells[0], 0, 0, null, g.Rows[0].Cells[0].CellStyle!, g.Rows[0].ContentStyle!, g.Columns[0].CellLayout);
		g.Rows[0].Cells[0].ContentStyle = new(white);
		CheckCellState(g.Rows[0].Cells[0], 0, 0, null, g.Rows[0].Cells[0].CellStyle!, g.Rows[0].Cells[0].ContentStyle!, g.Columns[0].CellLayout);

		//g.Columns[0].CellLayout.HorizontalAlignment = HorizontalAlignment.Right;
		//CheckCellState(g.Rows[0].Cells[0], g.Rows[0].Cells[0].CellStyle!, g.Rows[0].Cells[0].ContentStyle!, g.Columns[0].CellLayout);
		//g.Rows[0].Cells[0].Layout = new();
		//CheckCellState(g.Rows[0].Cells[0], g.Rows[0].Cells[0].CellStyle!, g.Rows[0].Cells[0].ContentStyle!, g.Rows[0].Cells[0].Layout!);

		//test object overload for adding row cells
		var now = DateTime.Now;
		var r = g.AddRow(1, now);
		CheckGridState(g, 2, 3);
		CheckRowState(r, 2, 2);
		CheckCellState(r.Cells[0], 2, 0, "1", g.Columns[0].CellStyle!, g.Columns[0].ContentStyle!, g.Columns[0].CellLayout);
		CheckCellState(r.Cells[1], 2, 1, now.ToString(), g.CellStyle!, g.CellContentStyle, g.Columns[1].CellLayout);
	}

	[TestMethod]
	public void ColManipulation()
	{
		Grid g = new Grid();
		CheckGridState(g, 0, 0);

		var c1 = g.Columns.Add("c1");
		CheckGridState(g, 1, 0);
		CheckColumnState(c1, 0, "c1", 2, 2, 4);
		var c2 = g.Columns.Add("c2");
		CheckGridState(g, 2, 0);
		CheckColumnState(c2, 1, "c2", 2, 2, 4);
		var c3 = g.Columns.Add("c3");
		CheckGridState(g, 3, 0);
		CheckColumnState(c3, 2, "c3", 2, 2, 4);

		Assert.IsTrue(g.Columns.Remove(c1));
		CheckGridState(g, 2, 0);
		Assert.IsFalse(g.Columns.Contains(c1));
		CheckColumnState(c2, 0, "c2", 2, 2, 4);
		CheckColumnState(c3, 1, "c3", 2, 2, 4);
		Assert.IsFalse(g.Columns.Remove(c1)); //cannot remove column that's not in the collection

		g.Columns.RemoveAt(0);
		CheckGridState(g, 1, 0);
		Assert.IsFalse(g.Columns.Contains(c2));
		CheckColumnState(c3, 0, "c3", 2, 2, 4);

		Assert.IsTrue(g.Columns.Remove(c3));
		CheckGridState(g, 0, 0);
		Assert.IsFalse(g.Columns.Contains(c3));
		var r1 = g.AddRow();
		CheckGridState(g, 0, 1);
		CheckRowState(r1, 0, 0);

		c1 = g.AddColumn("c1");
		CheckGridState(g, 1, 1);
		CheckColumnState(c1, 0, "c1", 2, 2, 4);
		CheckRowState(r1, 0, 1);
		CheckCellState(r1.Cells[0], 0, 0, null, g.CellStyle, g.CellContentStyle, g.Columns[0].CellLayout);
		r1.Cells[0].Content = "r1-c1";
		CheckCellState(r1.Cells[0], 0, 0, "r1-c1", g.CellStyle, g.CellContentStyle, g.Columns[0].CellLayout);

		c2 = g.AddColumn("c2");
		CheckGridState(g, 2, 1);
		CheckColumnState(c2, 1, "c2", 2, 2, 4);
		CheckRowState(g.Rows[0], 0, 2);
		CheckCellState(r1.Cells[0], 0, 0, "r1-c1", g.CellStyle, g.CellContentStyle, g.Columns[0].CellLayout);
		CheckCellState(r1.Cells[1], 0, 1, null, g.CellStyle, g.CellContentStyle, g.Columns[1].CellLayout);
		r1.Cells[1].Content = "r1-c2";
		CheckCellState(r1.Cells[0], 0, 0, "r1-c1", g.CellStyle, g.CellContentStyle, g.Columns[0].CellLayout);
		CheckCellState(r1.Cells[1], 0, 1, "r1-c2", g.CellStyle, g.CellContentStyle, g.Columns[1].CellLayout);

		var r2 = new GridRow(g, "r2-c1", "r2-c2");
		g.Rows.Add(r2);
		CheckGridState(g, 2, 2);
		CheckRowState(r1, 0, 2);
		CheckRowState(r2, 1, 2);
		CheckCellState(r1.Cells[0], 0, 0, "r1-c1", g.CellStyle, g.CellContentStyle, g.Columns[0].CellLayout);
		CheckCellState(r1.Cells[1], 0, 1, "r1-c2", g.CellStyle, g.CellContentStyle, g.Columns[1].CellLayout);
		CheckCellState(r2.Cells[0], 1, 0, "r2-c1", g.CellStyle, g.CellContentStyle, g.Columns[0].CellLayout);
		CheckCellState(r2.Cells[1], 1, 1, "r2-c2", g.CellStyle, g.CellContentStyle, g.Columns[1].CellLayout);

		g.Columns.RemoveAt(1);
		CheckGridState(g, 1, 2);
		CheckRowState(r1, 0, 1);
		CheckRowState(r2, 1, 1);
		CheckCellState(r1.Cells[0], 0, 0, "r1-c1", g.CellStyle, g.CellContentStyle, g.Columns[0].CellLayout);
		CheckCellState(r2.Cells[0], 1, 0, "r2-c1", g.CellStyle, g.CellContentStyle, g.Columns[0].CellLayout);

		c3 = g.Columns.Add("c3");
		CheckGridState(g, 2, 2);
		CheckColumnState(c3, 1, "c3", 2, 2, 4);
		CheckRowState(r1, 0, 2);
		CheckRowState(r2, 1, 2);
		CheckCellState(r1.Cells[0], 0, 0, "r1-c1", g.CellStyle, g.CellContentStyle, g.Columns[0].CellLayout);
		CheckCellState(r1.Cells[1], 0, 1, null, g.CellStyle, g.CellContentStyle, g.Columns[1].CellLayout);
		CheckCellState(r2.Cells[0], 1, 0, "r2-c1", g.CellStyle, g.CellContentStyle, g.Columns[0].CellLayout);
		CheckCellState(r2.Cells[1], 1, 1, null, g.CellStyle, g.CellContentStyle, g.Columns[1].CellLayout);

		Assert.IsTrue(g.Columns.Remove(c1));
		CheckGridState(g, 1, 2);
		CheckColumnState(c3, 0, "c3", 2, 2, 4);
		Assert.IsFalse(g.Columns.Contains(c1));
		Assert.IsFalse(g.Columns.Remove(c1));
		CheckGridState(g, 1, 2);
		Assert.ThrowsException<ArgumentOutOfRangeException>(() => g.Columns.RemoveAt(1));
		CheckGridState(g, 1, 2);
		CheckRowState(r1, 0, 1);
		CheckRowState(r2, 1, 1);
		CheckCellState(r1.Cells[0], 0, 0, null, g.CellStyle, g.CellContentStyle, g.Columns[0].CellLayout);
		CheckCellState(r2.Cells[0], 1, 0, null, g.CellStyle, g.CellContentStyle, g.Columns[0].CellLayout);

		var r3 = g.AddRow();
		CheckGridState(g, 1, 3);
		CheckRowState(r1, 0, 1);
		CheckRowState(r2, 1, 1);
		CheckRowState(r3, 2, 1);
		CheckCellState(r1.Cells[0], 0, 0, null, g.CellStyle, g.CellContentStyle, g.Columns[0].CellLayout);
		CheckCellState(r2.Cells[0], 1, 0, null, g.CellStyle, g.CellContentStyle, g.Columns[0].CellLayout);
		CheckCellState(r3.Cells[0], 2, 0, null, g.CellStyle, g.CellContentStyle, g.Columns[0].CellLayout);

		g.Rows.Clear();
		CheckGridState(g, 1, 0);

		g.Columns.RemoveAt(0);
		CheckGridState(g, 0, 0);
		g.AddRow();
		CheckGridState(g, 0, 1);

		g = new Grid();
		CheckGridState(g, 0, 0);
		c1 = g.AddColumn();
		CheckGridState(g, 1, 0);
		CheckColumnState(c1, 0, null, 0, 0, 2);
		c2 = g.AddColumn("c2");
		CheckGridState(g, 2, 0);
		CheckColumnState(c2, 1, "c2", 2, 2, 4);
		c3 = g.AddColumn("c3", HorizontalAlignment.Center);
		CheckGridState(g, 3, 0);
		CheckColumnState(c3, 2, "c3", 2, 2, 4);
		Assert.AreEqual(HorizontalAlignment.Center, c3.CellLayout.HorizontalAlignment);
	}

	[TestMethod]
	public void RowManipulation()
	{
		var g = new Grid();
		var c1 = g.Columns.Add("c1");
		var c2 = g.Columns.Add("c2");
		var c3 = g.Columns.Add("c3");
		CheckGridState(g, 3, 0);
		CheckColumnState(c1, 0, "c1", 2, 2, 4);
		CheckColumnState(c2, 1, "c2", 2, 2, 4);
		CheckColumnState(c3, 2, "c3", 2, 2, 4);

		var r1 = g.AddRow();
		CheckGridState(g, 3, 1);
		CheckRowState(r1, 0, 3);
		CheckCellState(r1.Cells[0], 0, 0, null, g.CellStyle, g.CellContentStyle, g.Columns[0].CellLayout);
		CheckCellState(r1.Cells[1], 0, 1, null, g.CellStyle, g.CellContentStyle, g.Columns[1].CellLayout);
		CheckCellState(r1.Cells[2], 0, 2, null, g.CellStyle, g.CellContentStyle, g.Columns[2].CellLayout);

		r1.Cells[0].Content = "r1-c1";
		r1.Cells[1].Content = "r1-c2";
		r1.Cells[2].Content = "r1-c3";
		CheckCellState(r1.Cells[0], 0, 0, "r1-c1", g.CellStyle, g.CellContentStyle, g.Columns[0].CellLayout);
		CheckCellState(r1.Cells[1], 0, 1, "r1-c2", g.CellStyle, g.CellContentStyle, g.Columns[1].CellLayout);
		CheckCellState(r1.Cells[2], 0, 2, "r1-c3", g.CellStyle, g.CellContentStyle, g.Columns[2].CellLayout);

		var r2 = g.AddRow();
		CheckGridState(g, 3, 2);
		CheckRowState(r2, 1, 3);
		CheckCellState(r2.Cells[0], 1, 0, null, g.CellStyle, g.CellContentStyle, g.Columns[0].CellLayout);
		CheckCellState(r2.Cells[1], 1, 1, null, g.CellStyle, g.CellContentStyle, g.Columns[1].CellLayout);
		CheckCellState(r2.Cells[2], 1, 2, null, g.CellStyle, g.CellContentStyle, g.Columns[2].CellLayout);

		r2.Cells[0].Content = "r2-c1";
		r2.Cells[1].Content = "r2-c2";
		r2.Cells[2].Content = "r2-c3";
		CheckCellState(r2.Cells[0], 1, 0, "r2-c1", g.CellStyle, g.CellContentStyle, g.Columns[0].CellLayout);
		CheckCellState(r2.Cells[1], 1, 1, "r2-c2", g.CellStyle, g.CellContentStyle, g.Columns[1].CellLayout);
		CheckCellState(r2.Cells[2], 1, 2, "r2-c3", g.CellStyle, g.CellContentStyle, g.Columns[2].CellLayout);

		var r3 = g.AddRow();
		CheckGridState(g, 3, 3);
		CheckRowState(r3, 2, 3);
		CheckCellState(r3.Cells[0], 2, 0, null, g.CellStyle, g.CellContentStyle, g.Columns[0].CellLayout);
		CheckCellState(r3.Cells[1], 2, 1, null, g.CellStyle, g.CellContentStyle, g.Columns[1].CellLayout);
		CheckCellState(r3.Cells[2], 2, 2, null, g.CellStyle, g.CellContentStyle, g.Columns[2].CellLayout);

		r3.Cells[0].Content = "r3-c1";
		r3.Cells[1].Content = "r3-c2";
		r3.Cells[2].Content = "r3-c3";
		CheckCellState(r3.Cells[0], 2, 0, "r3-c1", g.CellStyle, g.CellContentStyle, g.Columns[0].CellLayout);
		CheckCellState(r3.Cells[1], 2, 1, "r3-c2", g.CellStyle, g.CellContentStyle, g.Columns[1].CellLayout);
		CheckCellState(r3.Cells[2], 2, 2, "r3-c3", g.CellStyle, g.CellContentStyle, g.Columns[2].CellLayout);

		g.Rows.RemoveAt(1);
		CheckGridState(g, 3, 2);
		Assert.IsFalse(g.Rows.Contains(r2));
		Assert.IsFalse(g.Rows.Remove(r2)); //cannot remove a row that's not in the collection
		CheckGridState(g, 3, 2);
		CheckRowState(r1, 0, 3);
		CheckRowState(r3, 1, 3);
		CheckCellState(r1.Cells[0], 0, 0, "r1-c1", g.CellStyle, g.CellContentStyle, g.Columns[0].CellLayout);
		CheckCellState(r1.Cells[1], 0, 1, "r1-c2", g.CellStyle, g.CellContentStyle, g.Columns[1].CellLayout);
		CheckCellState(r1.Cells[2], 0, 2, "r1-c3", g.CellStyle, g.CellContentStyle, g.Columns[2].CellLayout);
		CheckCellState(r3.Cells[0], 1, 0, "r3-c1", g.CellStyle, g.CellContentStyle, g.Columns[0].CellLayout);
		CheckCellState(r3.Cells[1], 1, 1, "r3-c2", g.CellStyle, g.CellContentStyle, g.Columns[1].CellLayout);
		CheckCellState(r3.Cells[2], 1, 2, "r3-c3", g.CellStyle, g.CellContentStyle, g.Columns[2].CellLayout);

		g.Rows.RemoveAt(0);
		CheckGridState(g, 3, 1);
		Assert.IsFalse(g.Rows.Contains(r1));
		Assert.IsFalse(g.Rows.Remove(r1)); //cannot remove a row that's not in the collection
		CheckGridState(g, 3, 1);
		CheckRowState(r3, 0, 3);
		CheckCellState(r3.Cells[0], 0, 0, "r3-c1", g.CellStyle, g.CellContentStyle, g.Columns[0].CellLayout);
		CheckCellState(r3.Cells[1], 0, 1, "r3-c2", g.CellStyle, g.CellContentStyle, g.Columns[1].CellLayout);
		CheckCellState(r3.Cells[2], 0, 2, "r3-c3", g.CellStyle, g.CellContentStyle, g.Columns[2].CellLayout);
	}

	[TestMethod]
	public void CellRendering()
	{
		Grid g = new Grid();
		var col1 = g.Columns.Add();
		col1.CellLayout.MarginLeft = 0;
		col1.CellLayout.MarginRight = 0;
		col1.CellLayout.PaddingLeft = 0;
		col1.CellLayout.PaddingRight = 0;
		CheckColumnState(col1, 0, "", 0, 0, 0);
		CheckColHead(col1, 0, "", g.ColumnHeaderCellStyle, g.ColumnHeaderContentStyle, col1.CellLayout, "");
		var r1 = g.AddRow();
		CheckCellRendering(col1.Cells.ElementAt(0), 0, 0, null, g.CellStyle, g.CellContentStyle, col1.CellLayout, "");
		CheckColHead(col1, 0, "", g.ColumnHeaderCellStyle, g.ColumnHeaderContentStyle, col1.CellLayout, "");

		col1.Header.Content = "X";
		CheckCellRendering(col1.Cells.ElementAt(0), 0, 0, null, g.CellStyle, g.CellContentStyle, col1.CellLayout, ForeColorWhite + " " + ForeColorReset);
		CheckColHead(col1, 0, "X", g.ColumnHeaderCellStyle, g.ColumnHeaderContentStyle, col1.CellLayout, Underline + ForeColorWhite + "X" + ForeColorReset + UnderlineReset);

		r1.Cells[0].Content = "Y";
		CheckCellRendering(col1.Cells.ElementAt(0), 0, 0, "Y", g.CellStyle, g.CellContentStyle, col1.CellLayout, ForeColorWhite + "Y" + ForeColorReset);
		CheckColHead(col1, 0, "X", g.ColumnHeaderCellStyle, g.ColumnHeaderContentStyle, col1.CellLayout, Underline + ForeColorWhite + "X" + ForeColorReset + UnderlineReset);
		r1.Cells[0].Content = "foo";
		CheckCellRendering(col1.Cells.ElementAt(0), 0, 0, "foo", g.CellStyle, g.CellContentStyle, col1.CellLayout, ForeColorWhite + "foo" + ForeColorReset);
		CheckColHead(col1, 0, "X", g.ColumnHeaderCellStyle, g.ColumnHeaderContentStyle, col1.CellLayout, $"{Underline}{ForeColorWhite}X{ForeColorReset}{UnderlineReset}{Underline}{ForeColorWhite}  {ForeColorReset}{UnderlineReset}");
		col1.CellLayout.MarginLeft = 1;
		col1.CellLayout.MarginRight = 1;
		CheckCellRendering(col1.Cells.ElementAt(0), 0, 0, "foo", g.CellStyle, g.CellContentStyle, col1.CellLayout, $" {ForeColorWhite}foo{ForeColorReset} ");
		CheckColHead(col1, 0, "X", g.ColumnHeaderCellStyle, g.ColumnHeaderContentStyle, col1.CellLayout, $" {Underline}{ForeColorWhite}X{ForeColorReset}{UnderlineReset}{Underline}{ForeColorWhite}  {ForeColorReset}{UnderlineReset} ");
		col1.CellLayout.PaddingLeft = 1;
		col1.CellLayout.PaddingRight = 1;
		CheckCellRendering(col1.Cells.ElementAt(0), 0, 0, "foo", g.CellStyle, g.CellContentStyle, col1.CellLayout, $" {ForeColorWhite} {ForeColorReset}{ForeColorWhite}foo{ForeColorReset}{ForeColorWhite} {ForeColorReset} ");
		CheckColHead(col1, 0, "X", g.ColumnHeaderCellStyle, g.ColumnHeaderContentStyle, col1.CellLayout, $" {Underline}{ForeColorWhite} {ForeColorReset}{UnderlineReset}{Underline}{ForeColorWhite}X{ForeColorReset}{UnderlineReset}{Underline}{ForeColorWhite}   {ForeColorReset}{UnderlineReset} ");

		var r2 = g.AddRow("longer");
		CheckCellRendering(col1.Cells.ElementAt(0), 0, 0, "foo", g.CellStyle, g.CellContentStyle, col1.CellLayout, $" {ForeColorWhite} {ForeColorReset}{ForeColorWhite}foo{ForeColorReset}{ForeColorWhite}    {ForeColorReset} ");
		CheckCellRendering(col1.Cells.ElementAt(1), 1, 0, "longer", g.CellStyle, g.CellContentStyle, col1.CellLayout, $" {ForeColorWhite} {ForeColorReset}{ForeColorWhite}longer{ForeColorReset}{ForeColorWhite} {ForeColorReset} ");
		CheckColHead(col1, 0, "X", g.ColumnHeaderCellStyle, g.ColumnHeaderContentStyle, col1.CellLayout, $" {Underline}{ForeColorWhite} {ForeColorReset}{UnderlineReset}{Underline}{ForeColorWhite}X{ForeColorReset}{UnderlineReset}{Underline}{ForeColorWhite}      {ForeColorReset}{UnderlineReset} ");

		col1.CellLayout.HorizontalAlignment = HorizontalAlignment.Center;
		CheckCellRendering(col1.Cells.ElementAt(0), 0, 0, "foo", g.CellStyle, g.CellContentStyle, col1.CellLayout, $" {ForeColorWhite}  {ForeColorReset}{ForeColorWhite}foo{ForeColorReset}{ForeColorWhite}   {ForeColorReset} ");
		CheckCellRendering(col1.Cells.ElementAt(1), 1, 0, "longer", g.CellStyle, g.CellContentStyle, col1.CellLayout, $" {ForeColorWhite} {ForeColorReset}{ForeColorWhite}longer{ForeColorReset}{ForeColorWhite} {ForeColorReset} ");
		CheckColHead(col1, 0, "X", g.ColumnHeaderCellStyle, g.ColumnHeaderContentStyle, col1.CellLayout, $" {Underline}{ForeColorWhite}   {ForeColorReset}{UnderlineReset}{Underline}{ForeColorWhite}X{ForeColorReset}{UnderlineReset}{Underline}{ForeColorWhite}    {ForeColorReset}{UnderlineReset} ");

		col1.CellLayout.HorizontalAlignment = HorizontalAlignment.Right;
		CheckCellRendering(col1.Cells.ElementAt(0), 0, 0, "foo", g.CellStyle, g.CellContentStyle, col1.CellLayout, $" {ForeColorWhite}    {ForeColorReset}{ForeColorWhite}foo{ForeColorReset}{ForeColorWhite} {ForeColorReset} ");
		CheckCellRendering(col1.Cells.ElementAt(1), 1, 0, "longer", g.CellStyle, g.CellContentStyle, col1.CellLayout, $" {ForeColorWhite} {ForeColorReset}{ForeColorWhite}longer{ForeColorReset}{ForeColorWhite} {ForeColorReset} ");
		CheckColHead(col1, 0, "X", g.ColumnHeaderCellStyle, g.ColumnHeaderContentStyle, col1.CellLayout, $" {Underline}{ForeColorWhite}      {ForeColorReset}{UnderlineReset}{Underline}{ForeColorWhite}X{ForeColorReset}{UnderlineReset}{Underline}{ForeColorWhite} {ForeColorReset}{UnderlineReset} ");

		r1.Cells[0].ContentStyle = new TextStyle(blue);
		CheckCellRendering(col1.Cells.ElementAt(0), 0, 0, "foo", g.CellStyle, r1.Cells[0].ContentStyle!, col1.CellLayout, $" {ForeColorWhite}    {ForeColorReset}{ForeColorBlue}foo{ForeColorReset}{ForeColorWhite} {ForeColorReset} ");
		CheckCellRendering(col1.Cells.ElementAt(1), 1, 0, "longer", g.CellStyle, g.CellContentStyle, col1.CellLayout, $" {ForeColorWhite} {ForeColorReset}{ForeColorWhite}longer{ForeColorReset}{ForeColorWhite} {ForeColorReset} ");
		CheckColHead(col1, 0, "X", g.ColumnHeaderCellStyle, g.ColumnHeaderContentStyle, col1.CellLayout, $" {Underline}{ForeColorWhite}      {ForeColorReset}{UnderlineReset}{Underline}{ForeColorWhite}X{ForeColorReset}{UnderlineReset}{Underline}{ForeColorWhite} {ForeColorReset}{UnderlineReset} ");
		r1.Cells[0].ContentStyle = null;
		CheckCellRendering(col1.Cells.ElementAt(0), 0, 0, "foo", g.CellStyle, g.CellContentStyle, col1.CellLayout, $" {ForeColorWhite}    {ForeColorReset}{ForeColorWhite}foo{ForeColorReset}{ForeColorWhite} {ForeColorReset} ");
		CheckCellRendering(col1.Cells.ElementAt(1), 1, 0, "longer", g.CellStyle, g.CellContentStyle, col1.CellLayout, $" {ForeColorWhite} {ForeColorReset}{ForeColorWhite}longer{ForeColorReset}{ForeColorWhite} {ForeColorReset} ");
		CheckColHead(col1, 0, "X", g.ColumnHeaderCellStyle, g.ColumnHeaderContentStyle, col1.CellLayout, $" {Underline}{ForeColorWhite}      {ForeColorReset}{UnderlineReset}{Underline}{ForeColorWhite}X{ForeColorReset}{UnderlineReset}{Underline}{ForeColorWhite} {ForeColorReset}{UnderlineReset} ");

		col1.ContentStyle = new(blue);
		CheckCellRendering(col1.Cells.ElementAt(0), 0, 0, "foo", g.CellStyle, col1.ContentStyle, col1.CellLayout, $" {ForeColorWhite}    {ForeColorReset}{ForeColorBlue}foo{ForeColorReset}{ForeColorWhite} {ForeColorReset} ");
		CheckCellRendering(col1.Cells.ElementAt(1), 1, 0, "longer", g.CellStyle, col1.ContentStyle, col1.CellLayout, $" {ForeColorWhite} {ForeColorReset}{ForeColorBlue}longer{ForeColorReset}{ForeColorWhite} {ForeColorReset} ");
		CheckColHead(col1, 0, "X", g.ColumnHeaderCellStyle, g.ColumnHeaderContentStyle, col1.CellLayout, $" {Underline}{ForeColorWhite}      {ForeColorReset}{UnderlineReset}{Underline}{ForeColorWhite}X{ForeColorReset}{UnderlineReset}{Underline}{ForeColorWhite} {ForeColorReset}{UnderlineReset} ");
		col1.ContentStyle = null;
		CheckCellRendering(col1.Cells.ElementAt(0), 0, 0, "foo", g.CellStyle, g.CellContentStyle, col1.CellLayout, $" {ForeColorWhite}    {ForeColorReset}{ForeColorWhite}foo{ForeColorReset}{ForeColorWhite} {ForeColorReset} ");
		CheckCellRendering(col1.Cells.ElementAt(1), 1, 0, "longer", g.CellStyle, g.CellContentStyle, col1.CellLayout, $" {ForeColorWhite} {ForeColorReset}{ForeColorWhite}longer{ForeColorReset}{ForeColorWhite} {ForeColorReset} ");
		CheckColHead(col1, 0, "X", g.ColumnHeaderCellStyle, g.ColumnHeaderContentStyle, col1.CellLayout, $" {Underline}{ForeColorWhite}      {ForeColorReset}{UnderlineReset}{Underline}{ForeColorWhite}X{ForeColorReset}{UnderlineReset}{Underline}{ForeColorWhite} {ForeColorReset}{UnderlineReset} ");

		r1.Cells[0].CellStyle = new TextStyle(blue);
		CheckCellRendering(col1.Cells.ElementAt(0), 0, 0, "foo", r1.Cells[0].CellStyle!, g.CellContentStyle, col1.CellLayout, $" {ForeColorBlue}    {ForeColorReset}{ForeColorWhite}foo{ForeColorReset}{ForeColorBlue} {ForeColorReset} ");
		CheckCellRendering(col1.Cells.ElementAt(1), 1, 0, "longer", g.CellStyle, g.CellContentStyle, col1.CellLayout, $" {ForeColorWhite} {ForeColorReset}{ForeColorWhite}longer{ForeColorReset}{ForeColorWhite} {ForeColorReset} ");
		CheckColHead(col1, 0, "X", g.ColumnHeaderCellStyle, g.ColumnHeaderContentStyle, col1.CellLayout, $" {Underline}{ForeColorWhite}      {ForeColorReset}{UnderlineReset}{Underline}{ForeColorWhite}X{ForeColorReset}{UnderlineReset}{Underline}{ForeColorWhite} {ForeColorReset}{UnderlineReset} ");
		r1.Cells[0].CellStyle = null;
		CheckCellRendering(col1.Cells.ElementAt(0), 0, 0, "foo", g.CellStyle, g.CellContentStyle, col1.CellLayout, $" {ForeColorWhite}    {ForeColorReset}{ForeColorWhite}foo{ForeColorReset}{ForeColorWhite} {ForeColorReset} ");
		CheckCellRendering(col1.Cells.ElementAt(1), 1, 0, "longer", g.CellStyle, g.CellContentStyle, col1.CellLayout, $" {ForeColorWhite} {ForeColorReset}{ForeColorWhite}longer{ForeColorReset}{ForeColorWhite} {ForeColorReset} ");
		CheckColHead(col1, 0, "X", g.ColumnHeaderCellStyle, g.ColumnHeaderContentStyle, col1.CellLayout, $" {Underline}{ForeColorWhite}      {ForeColorReset}{UnderlineReset}{Underline}{ForeColorWhite}X{ForeColorReset}{UnderlineReset}{Underline}{ForeColorWhite} {ForeColorReset}{UnderlineReset} ");

		col1.CellStyle = new(blue);
		CheckCellRendering(col1.Cells.ElementAt(0), 0, 0, "foo", col1.CellStyle!, g.CellContentStyle, col1.CellLayout, $" {ForeColorBlue}    {ForeColorReset}{ForeColorWhite}foo{ForeColorReset}{ForeColorBlue} {ForeColorReset} ");
		CheckCellRendering(col1.Cells.ElementAt(1), 1, 0, "longer", col1.CellStyle, g.CellContentStyle, col1.CellLayout, $" {ForeColorBlue} {ForeColorReset}{ForeColorWhite}longer{ForeColorReset}{ForeColorBlue} {ForeColorReset} ");
		CheckColHead(col1, 0, "X", g.ColumnHeaderCellStyle, g.ColumnHeaderContentStyle, col1.CellLayout, $" {Underline}{ForeColorWhite}      {ForeColorReset}{UnderlineReset}{Underline}{ForeColorWhite}X{ForeColorReset}{UnderlineReset}{Underline}{ForeColorWhite} {ForeColorReset}{UnderlineReset} ");
		col1.CellStyle = null;
		CheckCellRendering(col1.Cells.ElementAt(0), 0, 0, "foo", g.CellStyle, g.CellContentStyle, col1.CellLayout, $" {ForeColorWhite}    {ForeColorReset}{ForeColorWhite}foo{ForeColorReset}{ForeColorWhite} {ForeColorReset} ");
		CheckCellRendering(col1.Cells.ElementAt(1), 1, 0, "longer", g.CellStyle, g.CellContentStyle, col1.CellLayout, $" {ForeColorWhite} {ForeColorReset}{ForeColorWhite}longer{ForeColorReset}{ForeColorWhite} {ForeColorReset} ");
		CheckColHead(col1, 0, "X", g.ColumnHeaderCellStyle, g.ColumnHeaderContentStyle, col1.CellLayout, $" {Underline}{ForeColorWhite}      {ForeColorReset}{UnderlineReset}{Underline}{ForeColorWhite}X{ForeColorReset}{UnderlineReset}{Underline}{ForeColorWhite} {ForeColorReset}{UnderlineReset} ");

		var col2 = g.Columns.Add("Y");
		CheckCellRendering(col1.Cells.ElementAt(0), 0, 0, "foo", g.CellStyle, g.CellContentStyle, col1.CellLayout, $" {ForeColorWhite}    {ForeColorReset}{ForeColorWhite}foo{ForeColorReset}{ForeColorWhite} {ForeColorReset} ");
		CheckCellRendering(col1.Cells.ElementAt(1), 1, 0, "longer", g.CellStyle, g.CellContentStyle, col1.CellLayout, $" {ForeColorWhite} {ForeColorReset}{ForeColorWhite}longer{ForeColorReset}{ForeColorWhite} {ForeColorReset} ");
		CheckColHead(col1, 0, "X", g.ColumnHeaderCellStyle, g.ColumnHeaderContentStyle, col1.CellLayout, $" {Underline}{ForeColorWhite}      {ForeColorReset}{UnderlineReset}{Underline}{ForeColorWhite}X{ForeColorReset}{UnderlineReset}{Underline}{ForeColorWhite} {ForeColorReset}{UnderlineReset} ");
		CheckCellRendering(col2.Cells.ElementAt(0), 0, 1, null, g.CellStyle, g.CellContentStyle, col2.CellLayout, $" {ForeColorWhite} {ForeColorReset} ");
		CheckCellRendering(col2.Cells.ElementAt(1), 1, 1, null, g.CellStyle, g.CellContentStyle, col2.CellLayout, $" {ForeColorWhite} {ForeColorReset} ");
		CheckColHead(col2, 1, "Y", g.ColumnHeaderCellStyle, g.ColumnHeaderContentStyle, col2.CellLayout, $" {Underline}{ForeColorWhite}Y{ForeColorReset}{UnderlineReset} ");

		r2.ContentStyle = new(blue);
		CheckCellRendering(col1.Cells.ElementAt(0), 0, 0, "foo", g.CellStyle, g.CellContentStyle, col1.CellLayout, $" {ForeColorWhite}    {ForeColorReset}{ForeColorWhite}foo{ForeColorReset}{ForeColorWhite} {ForeColorReset} ");
		CheckCellRendering(col1.Cells.ElementAt(1), 1, 0, "longer", g.CellStyle, r2.ContentStyle, col1.CellLayout, $" {ForeColorWhite} {ForeColorReset}{ForeColorBlue}longer{ForeColorReset}{ForeColorWhite} {ForeColorReset} ");
		CheckColHead(col1, 0, "X", g.ColumnHeaderCellStyle, g.ColumnHeaderContentStyle, col1.CellLayout, $" {Underline}{ForeColorWhite}      {ForeColorReset}{UnderlineReset}{Underline}{ForeColorWhite}X{ForeColorReset}{UnderlineReset}{Underline}{ForeColorWhite} {ForeColorReset}{UnderlineReset} ");
		CheckCellRendering(col2.Cells.ElementAt(0), 0, 1, null, g.CellStyle, g.CellContentStyle, col2.CellLayout, $" {ForeColorWhite} {ForeColorReset} ");
		//this doesn't seem right -- seems like it should be blue. however, the cell has no content and the entire cell is padding. white is the padding (cell) color.
		CheckCellRendering(col2.Cells.ElementAt(1), 1, 1, null, g.CellStyle, r2.ContentStyle, col2.CellLayout, $" {ForeColorWhite} {ForeColorReset} ");
		r2.Cells[1].Content = "Z";
		CheckCellRendering(col2.Cells.ElementAt(1), 1, 1, "Z", g.CellStyle, r2.ContentStyle, col2.CellLayout, $" {ForeColorBlue}Z{ForeColorReset} ");
		r2.Cells[1].Content = "";
		CheckCellRendering(col2.Cells.ElementAt(1), 1, 1, "", g.CellStyle, r2.ContentStyle, col2.CellLayout, $" {ForeColorWhite} {ForeColorReset} ");
		CheckColHead(col2, 1, "Y", g.ColumnHeaderCellStyle, g.ColumnHeaderContentStyle, col2.CellLayout, $" {Underline}{ForeColorWhite}Y{ForeColorReset}{UnderlineReset} ");
		r2.ContentStyle = null;
		CheckCellRendering(col1.Cells.ElementAt(0), 0, 0, "foo", g.CellStyle, g.CellContentStyle, col1.CellLayout, $" {ForeColorWhite}    {ForeColorReset}{ForeColorWhite}foo{ForeColorReset}{ForeColorWhite} {ForeColorReset} ");
		CheckCellRendering(col1.Cells.ElementAt(1), 1, 0, "longer", g.CellStyle, g.CellContentStyle, col1.CellLayout, $" {ForeColorWhite} {ForeColorReset}{ForeColorWhite}longer{ForeColorReset}{ForeColorWhite} {ForeColorReset} ");
		CheckColHead(col1, 0, "X", g.ColumnHeaderCellStyle, g.ColumnHeaderContentStyle, col1.CellLayout, $" {Underline}{ForeColorWhite}      {ForeColorReset}{UnderlineReset}{Underline}{ForeColorWhite}X{ForeColorReset}{UnderlineReset}{Underline}{ForeColorWhite} {ForeColorReset}{UnderlineReset} ");
		CheckCellRendering(col2.Cells.ElementAt(0), 0, 1, null, g.CellStyle, g.CellContentStyle, col2.CellLayout, $" {ForeColorWhite} {ForeColorReset} ");
		CheckCellRendering(col2.Cells.ElementAt(1), 1, 1, "", g.CellStyle, g.CellContentStyle, col2.CellLayout, $" {ForeColorWhite} {ForeColorReset} ");
		CheckColHead(col2, 1, "Y", g.ColumnHeaderCellStyle, g.ColumnHeaderContentStyle, col2.CellLayout, $" {Underline}{ForeColorWhite}Y{ForeColorReset}{UnderlineReset} ");
	}

	[TestMethod]
	public void CellPadding()
	{
		Grid g = new Grid();
		g.CellContentStyle = new TextStyle();
		g.CellStyle = new TextStyle();
		g.ShowColumnHeaders = false;
		var col1 = g.Columns.Add();
		col1.CellLayout.MarginLeft = 0;
		col1.CellLayout.MarginRight = 0;
		col1.CellLayout.PaddingLeft = 0;
		col1.CellLayout.PaddingRight = 0;
		CheckColumnState(col1, 0, "", 0, 0, 0);
		CheckColHead(col1, 0, "", g.ColumnHeaderCellStyle, g.ColumnHeaderContentStyle, col1.CellLayout, "");
		var r1 = g.AddRow();
		CheckCellRendering(col1.Cells.ElementAt(0), 0, 0, null, g.CellStyle, g.CellContentStyle, col1.CellLayout, "");
		r1.Cells[0].Content = "";
		CheckCellRendering(col1.Cells.ElementAt(0), 0, 0, "", g.CellStyle, g.CellContentStyle, col1.CellLayout, "");
		r1.Cells[0].Content = " ";
		CheckCellRendering(col1.Cells.ElementAt(0), 0, 0, " ", g.CellStyle, g.CellContentStyle, col1.CellLayout, " ");

		col1.CellLayout.PaddingLeft = 1;
		col1.CellLayout.PaddingRight = 1;
		col1.CellLayout.PaddingLeftChar = 'p';
		col1.CellLayout.PaddingRightChar = 'p';

		r1.Cells[0].Content = null;
		CheckCellRendering(col1.Cells.ElementAt(0), 0, 0, null, g.CellStyle, g.CellContentStyle, col1.CellLayout, "  ");
		r1.Cells[0].Content = "";
		CheckCellRendering(col1.Cells.ElementAt(0), 0, 0, "", g.CellStyle, g.CellContentStyle, col1.CellLayout, "pp");
		r1.Cells[0].Content = " ";
		CheckCellRendering(col1.Cells.ElementAt(0), 0, 0, " ", g.CellStyle, g.CellContentStyle, col1.CellLayout, "p p");
		r1.Cells[0].Content = "x";
		CheckCellRendering(col1.Cells.ElementAt(0), 0, 0, "x", g.CellStyle, g.CellContentStyle, col1.CellLayout, "pxp");

		g.ShowColumnHeaders = true;
		g.ColumnHeaderCellStyle = new TextStyle();
		g.ColumnHeaderContentStyle = new TextStyle();
		col1.Header.Content = "foo";
		CheckColHead(col1, 0, "foo", g.ColumnHeaderCellStyle, g.ColumnHeaderContentStyle, col1.CellLayout, "pfoop");
		r1.Cells[0].Content = null;
		CheckCellRendering(col1.Cells.ElementAt(0), 0, 0, null, g.CellStyle, g.CellContentStyle, col1.CellLayout, "     ");
		r1.Cells[0].Content = "";
		CheckCellRendering(col1.Cells.ElementAt(0), 0, 0, "", g.CellStyle, g.CellContentStyle, col1.CellLayout, "pp   ");
		r1.Cells[0].Content = " ";
		CheckCellRendering(col1.Cells.ElementAt(0), 0, 0, " ", g.CellStyle, g.CellContentStyle, col1.CellLayout, "p p  ");
		r1.Cells[0].Content = "x";
		CheckCellRendering(col1.Cells.ElementAt(0), 0, 0, "x", g.CellStyle, g.CellContentStyle, col1.CellLayout, "pxp  ");
		r1.Cells[0].Content = "bar";
		CheckCellRendering(col1.Cells.ElementAt(0), 0, 0, "bar", g.CellStyle, g.CellContentStyle, col1.CellLayout, "pbarp");
		CheckColHead(col1, 0, "foo", g.ColumnHeaderCellStyle, g.ColumnHeaderContentStyle, col1.CellLayout, "pfoop");
		r1.Cells[0].Content = "long";
		CheckCellRendering(col1.Cells.ElementAt(0), 0, 0, "long", g.CellStyle, g.CellContentStyle, col1.CellLayout, "plongp");
		CheckColHead(col1, 0, "foo", g.ColumnHeaderCellStyle, g.ColumnHeaderContentStyle, col1.CellLayout, "pfoop ");
	}

	[TestMethod]
	public void FillerChar()
	{
		Grid g = new Grid();
		g.CellStyle = new();
		g.CellContentStyle = new();
		g.Columns.Add("foo");
		g.Columns.Add("bar", HorizontalAlignment.Center);
		g.Columns.Add("baz", HorizontalAlignment.Right);
		g.Columns.Last().CellLayout.MarginLeft = 0;
		g.Columns.Last().CellLayout.MarginRight = 0;
		g.Columns.Add("");

		g.AddRow("1", "2", "3", "4");
		CheckGridState(g, 4, 1);
		foreach (GridCell cell in g.Rows[0].Cells)
		{ cell.FillerChar = '*'; }
		CheckCellRendering(g.Rows[0].Cells[0], 0, 0, "1", g.CellStyle, g.CellContentStyle, g.Columns[0].CellLayout, " 1** ");
		CheckCellRendering(g.Rows[0].Cells[1], 0, 1, "2", g.CellStyle, g.CellContentStyle, g.Columns[1].CellLayout, " *2* ");
		CheckCellRendering(g.Rows[0].Cells[2], 0, 2, "3", g.CellStyle, g.CellContentStyle, g.Columns[2].CellLayout, "**3");
		CheckCellRendering(g.Rows[0].Cells[3], 0, 3, "4", g.CellStyle, g.CellContentStyle, g.Columns[3].CellLayout, " 4 ");

		g.AddSeparatorRow('*');
		CheckGridState(g, 4, 2);
		CheckCellRendering(g.Rows[1].Cells[0], 1, 0, null, g.CellStyle, g.CellContentStyle, g.Columns[0].CellLayout, " *** ");
		Assert.AreEqual(0, g.Rows[1].Cells[0].ContentWidth);
		CheckCellRendering(g.Rows[1].Cells[1], 1, 1, null, g.CellStyle, g.CellContentStyle, g.Columns[1].CellLayout, " *** ");
		Assert.AreEqual(0, g.Rows[1].Cells[1].ContentWidth);
		CheckCellRendering(g.Rows[1].Cells[2], 1, 2, null, g.CellStyle, g.CellContentStyle, g.Columns[2].CellLayout, "***");
		Assert.AreEqual(0, g.Rows[1].Cells[2].ContentWidth);
		CheckCellRendering(g.Rows[1].Cells[3], 1, 3, null, g.CellStyle, g.CellContentStyle, g.Columns[3].CellLayout, " * ");
		Assert.AreEqual(0, g.Rows[1].Cells[3].ContentWidth);

		g.Rows[0].Cells.SetFillerChar('f');
		CheckCellRendering(g.Rows[0].Cells[0], 0, 0, "1", g.CellStyle, g.CellContentStyle, g.Columns[0].CellLayout, " 1ff ");
		CheckCellRendering(g.Rows[0].Cells[1], 0, 1, "2", g.CellStyle, g.CellContentStyle, g.Columns[1].CellLayout, " f2f ");
		CheckCellRendering(g.Rows[0].Cells[2], 0, 2, "3", g.CellStyle, g.CellContentStyle, g.Columns[2].CellLayout, "ff3");
		CheckCellRendering(g.Rows[0].Cells[3], 0, 3, "4", g.CellStyle, g.CellContentStyle, g.Columns[3].CellLayout, " 4 ");
	}

	[TestMethod]
	public void ShowColumnHeaders()
	{
		Grid g = new Grid();
		g.ColumnHeaderContentStyle = new TextStyle();
		g.ColumnHeaderCellStyle = new TextStyle();
		var col1 = g.Columns.Add("foo");
		col1.CellLayout.MarginLeft = 0;
		col1.CellLayout.MarginRight = 0;
		var col2 = g.Columns.Add("bar");
		col2.CellLayout.MarginLeft = 0;
		col2.CellLayout.MarginRight = 0;
		var col3 = g.Columns.Add("longer");
		col3.CellLayout.MarginLeft = 0;
		col3.CellLayout.MarginRight = 0;
		g.AddRow("11", "12", "13");
		g.AddRow("21", "22", "23");
		g.AddRow("31", "32", "33");

		CheckColHead(col1, 0, "foo", g.ColumnHeaderCellStyle, g.ColumnHeaderContentStyle, col1.CellLayout, "foo");
		CheckColumnState(col1, 0, "foo", 3, 3, 3);
		CheckColHead(col2, 1, "bar", g.ColumnHeaderCellStyle, g.ColumnHeaderContentStyle, col1.CellLayout, "bar");
		CheckColumnState(col2, 1, "bar", 3, 3, 3);
		CheckColHead(col3, 2, "longer", g.ColumnHeaderCellStyle, g.ColumnHeaderContentStyle, col1.CellLayout, "longer");
		CheckColumnState(col3, 2, "longer", 6, 6, 6);

		g.ShowColumnHeaders = false;

		CheckColHead(col1, 0, "foo", g.ColumnHeaderCellStyle, g.ColumnHeaderContentStyle, col1.CellLayout, "");
		CheckColumnState(col1, 0, "foo", 2, 2, 2);
		CheckColHead(col2, 1, "bar", g.ColumnHeaderCellStyle, g.ColumnHeaderContentStyle, col1.CellLayout, "");
		CheckColumnState(col2, 1, "bar", 2, 2, 2);
		CheckColHead(col3, 2, "longer", g.ColumnHeaderCellStyle, g.ColumnHeaderContentStyle, col1.CellLayout, "");
		CheckColumnState(col3, 2, "longer", 2, 2, 2);
	}

	[TestMethod]
	public void Chrome()
	{
		Grid g = new();
		g.ColumnHeaderCellStyle = new TextStyle();
		g.ColumnHeaderContentStyle = new TextStyle();
		g.Columns.Add("1234567");
		g.Columns[0].CellLayout.MarginLeft = 0;
		g.Columns[0].CellLayout.MarginRight = 0;
		g.AddRow("");

		g.Title = new("foo");
		CheckTitleRendering(g, g.Title, "foo    ", "  foo  ", "    foo");
		g.Title.Text = "foobar";
		CheckTitleRendering(g, g.Title, "foobar ", "foobar ", " foobar");
		g.Title.Text = "foo bar";
		CheckTitleRendering(g, g.Title, "foo bar", "foo bar", "foo bar");
		g.Title.Text = "foo bar baz";
		CheckTitleRendering(g, g.Title, "foo bar baz", "foo bar baz", "foo bar baz");
		g.Title.Text = "-";
		string tfill = "-------";
		CheckTitleRendering(g, g.Title, tfill, tfill, tfill);

		g.Title.Text = "foo";
		g.Title.Style.ForeColor = Expectations.red;
		CheckTitleRendering(g, g.Title, "foo    ", "  foo  ", "    foo", Expectations.ForeColorRed, Expectations.ForeColorReset);
		g.Title.Text = "foobar";
		CheckTitleRendering(g, g.Title, "foobar ", "foobar ", " foobar", Expectations.ForeColorRed, Expectations.ForeColorReset);
		g.Title.Text = "foo bar";
		CheckTitleRendering(g, g.Title, "foo bar", "foo bar", "foo bar", Expectations.ForeColorRed, Expectations.ForeColorReset);
		g.Title.Text = "foo bar baz";
		CheckTitleRendering(g, g.Title, "foo bar baz", "foo bar baz", "foo bar baz", Expectations.ForeColorRed, Expectations.ForeColorReset);
		g.Title.Text = "-";
		CheckTitleRendering(g, g.Title, tfill, tfill, tfill, Expectations.ForeColorRed, Expectations.ForeColorReset);
	}


	internal static void CheckGridState(Grid g, int expectedColCount, int expectedRowCount)
	{
		Assert.IsNotNull(g);

		Assert.IsNotNull(g.Columns);
		Assert.AreEqual(expectedColCount, g.ColumnCount);
		Assert.AreEqual(expectedColCount, g.Columns.Count);

		Assert.IsNotNull(g.Rows);
		Assert.AreEqual(expectedRowCount, g.RowCount);
		Assert.AreEqual(expectedRowCount, g.Rows.Count);
	}

	private static void CheckColumnState(GridColumn col, int expectedIndex, string? expectedHeaderText, int expectedCellWidth, int expectedContentWidth, int expectedTotalWidth)
	{
		Assert.IsNotNull(col);
		Assert.IsNotNull(col.Grid);
		Assert.IsNotNull(col.Header);

		Assert.IsTrue(ReferenceEquals(col, col.Grid.Columns[expectedIndex]));
		Assert.AreEqual(expectedIndex, col.Index);
		Assert.AreEqual(expectedCellWidth, col.CellWidth);
		Assert.AreEqual(expectedContentWidth, col.ContentWidth);
		Assert.AreEqual(expectedTotalWidth, col.TotalWidth);

		Assert.AreEqual(col.Grid.Rows.Any(), col.HasCells);
		Assert.AreEqual(col.Grid.RowCount, col.Cells.Count());

		if (!string.IsNullOrEmpty(expectedHeaderText))
		{ Assert.AreEqual(expectedHeaderText, col.Header.Content); }
		else
		{ Assert.IsNull(col.Header.Content); }
	}

	private static void CheckRowState(GridRow row, int expectedIndex, int expectedCellCount)
	{
		Assert.IsNotNull(row);
		Assert.IsNotNull(row.Grid);

		Assert.AreEqual(expectedIndex, row.Index);
		Assert.IsTrue(ReferenceEquals(row, row.Grid.Rows[expectedIndex]));

		Assert.IsNotNull(row.Cells);
		Assert.AreEqual(expectedCellCount, row.Cells.Count);
	}

	private static void CheckCellState(GridCell cell, int expectedRowIndex, int expectedColIndex, string? expectedContent, TextStyle expectedCellStyle, TextStyle expectedContentStyle, GridCellLayout expectedLayout)
	{
		Assert.IsNotNull(cell);
		Assert.AreEqual(expectedRowIndex, cell.RowIndex);
		Assert.AreEqual(expectedColIndex, cell.ColumnIndex);
		TextStyleTests.CheckTextStyleEquality(cell.CellStyleI, expectedCellStyle);
		TextStyleTests.CheckTextStyleEquality(cell.ContentStyleI, expectedContentStyle);
		CheckLayoutEquality(expectedLayout, cell.LayoutI);

		if (expectedContent == null)
		{
			Assert.AreEqual(null, cell.Content);
			Assert.IsFalse(cell.HasContent);
		}
		else
		{
			Assert.IsTrue(cell.HasContent);
			Assert.AreEqual(expectedContent, cell.Content);
		}
	}

	private static void CheckCellRendering(GridCell cell, int expectedRowIndex, int expectedColIndex, string? expectedContent, TextStyle expectedCellStyle, TextStyle expectedContentStyle, GridCellLayout expectedLayout, string expectedRenderedContent)
	{
		CheckCellState(cell, expectedRowIndex, expectedColIndex, expectedContent, expectedCellStyle, expectedContentStyle, expectedLayout);

		int colW = cell.Column.TotalWidth;
		if (colW == 0)
		{ Assert.IsTrue(string.IsNullOrEmpty(expectedRenderedContent)); }
		else
		{ Assert.AreEqual(expectedRenderedContent, cell.RenderedContent); }
	}

	private static void CheckColHead(GridColumn col, int expectedColIndex, string? expectedContent, TextStyle expectedCellStyle, TextStyle expectedContentStyle, GridCellLayout expectedLayout, string expectedRenderedContent)
	{
		Assert.IsNotNull(col);

		GridHeaderCell? hd = col.Header;
		Assert.IsNotNull(hd);

		Assert.AreEqual(expectedColIndex, hd.ColumnIndex);
		TextStyleTests.CheckTextStyleEquality(hd.CellStyleI, expectedCellStyle);
		TextStyleTests.CheckTextStyleEquality(hd.ContentStyleI, expectedContentStyle);
		CheckLayoutEquality(expectedLayout, hd.LayoutI);

		if (string.IsNullOrEmpty(expectedContent))
		{ Assert.IsNull(hd.Content); }
		else
		{ Assert.AreEqual(expectedContent, hd.Content); }

		if (col.TotalWidth == 0)
		{ Assert.IsTrue(string.IsNullOrEmpty(expectedRenderedContent)); }
		else
		{ Assert.AreEqual(expectedRenderedContent, hd.RenderedContent); }
	}

	internal static void CheckLayoutEquality(GridCellLayout expectedLayout, GridCellLayout actualLayout)
	{
		if (!ReferenceEquals(expectedLayout, actualLayout))
		{
			Assert.IsNotNull(actualLayout);
			Assert.IsNotNull(expectedLayout);
			Assert.AreEqual(expectedLayout.HorizontalAlignment, actualLayout.HorizontalAlignment);
			Assert.AreEqual(expectedLayout.MarginLeft, actualLayout.MarginLeft);
			Assert.AreEqual(expectedLayout.MarginLeftChar, actualLayout.MarginLeftChar);
			Assert.AreEqual(expectedLayout.MarginRight, actualLayout.MarginRight);
			Assert.AreEqual(expectedLayout.MarginRightChar, actualLayout.MarginRightChar);
			Assert.AreEqual(expectedLayout.PaddingLeft, actualLayout.PaddingLeft);
			Assert.AreEqual(expectedLayout.PaddingLeftChar, actualLayout.PaddingLeftChar);
			Assert.AreEqual(expectedLayout.PaddingRight, actualLayout.PaddingRight);
			Assert.AreEqual(expectedLayout.PaddingRightChar, actualLayout.PaddingRightChar);
		}
	}

	internal static void CheckTitleRendering(Grid g, StyledText title, string expectedLeft, string expectedCenter, string expectedRight, string pre = "", string post = "")
	{
		Assert.AreEqual(pre + expectedLeft + post, g.RenderTitle(title, HorizontalAlignment.Left));
		Assert.AreEqual(pre + expectedCenter + post, g.RenderTitle(title, HorizontalAlignment.Center));
		Assert.AreEqual(pre + expectedRight + post, g.RenderTitle(title, HorizontalAlignment.Right));
	}
}
