# Grids
[home](index.md)

A `Grid` consists of rows and columns of data that is displayed in a tabular format. 
Each Column is sized automatically to fit its widest content. 
In addition to the rows and columns, grids can also have a `Title`, `Subtitle` and `Footer`. 
The titles are centered above the rows/columns, the footer is centered beneath the rows/columns. 

The appearance and layout of the grid's columns, rows, and cells can be customized through the 
use of properties available at the various levels. 
The styling for the titles and footer can also be customized. 

## `Grid` Class
To create a grid, use the `Grid` class. Add columns and rows. 
Then use the `Show` method to display the grid.

Styling for all the grid's cells can be set via the `CellStyle`, and `CellContentStyle` properties. 
These styles can be overridden at the column, row, and cell level.

Styling for all the grid's column headers can be set via the `ColumnHeaderCellStyle`, and `ColumnHeaderContentStyle` properties. 
These styles can be overridden for each column.

*Note: Calling the `Show` method if the grid does not have at least one column and one row will 
result in an exception being thrown. After all, it is somewhat pointless to display a grid 
without at least one of each.*

## `GridColumn` Class
Represents a column in a grid.

A column has a `Header` property that returns a `GridHeaderCell` object for its header cell. 
The various properties of the `Header` object can be used to control the content, appearance and layout 
of the column's header.

The column's `CellLayout` property controls the layout for the column's cells, including 
the `Header` cell.

The styling for a column's cells can be set through its `CellStyle` and `ContentStyle` properties. 
Setting these styles will override the styles inherited from the grid.

A column always has a `Cells` collection, which always contains the same number of 
`GridCell` objects as the grid's `Rows` collection (even if zero). 
The `HasCells` property returns a boolean indicating whether or not the column has any cells 
(i.e. whether or not the grid has any rows). *The `Cells` sequence is readonly -- to add rows
to a grid, use the `Rows` collection, or one of the `AddRow` methods of the `Grid` object.*

## `GridColumns` Class
The `GridColumns` class represents a collection of `GridColumn` objects for a grid (`Grid.Columns`). 
`GridColumns` implements the `IReadOnlyList<GridColumn>` interface and also includes `IndexOf`, 
`Add`, `Remove`, and `RemoveAt` methods to allow limited manipulation of the collection.

If a column is added to the grid when it has rows, cells are automatically added to each 
row's `Cells` collection. 
If a column is removed from the grid when it has rows, the corresponding cell is automatically 
removed from each row's `Cells` collection.

## `GridRow` Class
Represents a row in a grid.

A row always has a `Cells` collection, which always contains the same number of 
`GridCell` objects as the grid's `Columns` collection (even if zero). 
The `HasCells` property returns a boolean indicating whether or not the row has any cells 
(i.e. whether or not the grid has any columns).
If a column is added to the grid, cells are automatically added to each row. 
If a column is removed from the grid, the corresponding cell is automatically removed from each row. 
*The `Cells` sequence is readonly -- to add columns to a grid, use the `Columns` 
collection of the `Grid` object.*

The styling for a row's cells can be set through its `CellStyle` and `ContentStyle` properties. 
Setting these styles will override the styles inherited from the column or grid.

## `GridCell` Class
The content to be displayed in a given cell can be accessed via it's (read/write) `Content` property. 
The `Content` property is a string value, so to display a value from any other type of 
object, convert the value to a string. An empty string or `null` value are acceptable 
and both result in the cell rendering as empty (i.e. having no content).

A cell has two types of text styling: cell styling and content styling. 
Cell styling applies to the whole cell (i.e. the non-content area of the cell). 
Content styling applies **only** to the cell's content. 
A cell's text styling is inherited from its row, column or the grid (in that order), 
however the styling can be overridden for an individual cell using the cell's 
`CellStyle` and `ContentStyle` properties.

A cell's layout is inherited from the column (`GridColumn.CellLayout`). 
The horizontal alignment of an individual cell can be overridden via the 
cell's `HorizontalAlignment` property. The cell's other layout properties (margins, padding) 
cannot be overridden.

A cell has readonly properties for `Column`, `ColumnIndex`, `Row`, and `RowIndex`. 
These return information regarding the cell's position within the grid.

When a cell is rendered for display in the grid, the cell's content is styled 
using the cell's `ContentStyle` (or the content style inherited from the 
row, column or grid), and is padded with the number of characters specified 
by the `PaddingLeft`, `PaddingLeftChar`, `PaddingRight`, and `PaddingRightChar` 
properties of the column's `CellLayout`. 
The cell may additionally contain "filler" (spaces) so that the cell's width 
meets the widest cell in the column. The cell is also padded with the 
number of characters specified by  the `MarginLeft`, `MarginLeftChar`, 
`MarginRight`, and `MarginRightChar` properties of the column's `CellLayout`. 
Padding and "filler" are part of the cell and styled using the cell's `CellStyle` 
(or the cell style inherited from the row, column or grid). 
Margins are NOT part of the cell, and no styling is applied to the margins. 

## `GridHeaderCell` Class
A header cell contains the content and styling for a column header. The header for a column 
can be accessed through the column's `Header` property.

A header cell's styling and layout work the same as a normal `GridCell` object.

Note: Since a header is not in a "row" of the grid, a header cell does NOT have 
a Row or RowIndex property.

## `GridCellLayout` Class
Allows alignment, padding and margins to be specified for columns and cells.

When a grid cell is rendered, Padding is part of the cell, Margins are outside the cell.

## Examples

```c#
Grid g = new();
g.Title = new("Grid".SpaceOut());
g.Subtitle = new("Example 1");

g.Columns.Add("C 1");
g.Columns.Add("Column 2", HorizontalAlignment.Center);
g.Columns.Add("Col 3", HorizontalAlignment.Right);

g.AddRow("r1-c1", "row1-c2", "row1-column3");
g.AddRow("row2-column1", "row2-col2", "r2-col3");
g.AddRow("r3-col1", "row3-column2", "r3-c3");

g.Footer = new($"Total Count {g.RowCount}");
g.Show();
```
![Example - Grid 1](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/ex_grid_1.png)


```c#
Grid g = new();
g.Title = new("Grid".SpaceOut(), new(Color.Silver, Color.Gray, Color.Silver) { BackColor = Color.LimeGreen, Reverse = true });
g.Subtitle = new("Example 2 (Styling)", new(Color.LimeGreen, Color.Gray));

//set cell/content styling for entire grid
g.CellStyle = new(Color.DodgerBlue, Color.Silver);
g.CellContentStyle = new(Color.Blue, Color.Silver);
g.ColumnHeaderContentStyle.Underline = false;
g.ColumnHeaderCellStyle.Underline = false;

GridColumn col = g.Columns.Add("C 1");
col.Header.HorizontalAlignment = HorizontalAlignment.Center; //override column header alignment

col = g.Columns.Add("Column 2", HorizontalAlignment.Center);
//override styling for column
col.CellStyle = new() { BackColor = Color.DarkGray };

g.Columns.Add("Col 3", HorizontalAlignment.Right);

g.AddRow("r1-c1", "row1-c2", "row1-column3");
GridRow row = g.AddRow("row2-column1", "row2-column2", "r2-col3");
//override styling for row
row.CellStyle = new(Color.White, Color.SlateGray);
row.ContentStyle = new(Color.White, Color.SlateGray);
//override styling for specific cell
GridCell cell = row.Cells[2];
cell.ContentStyle = new(Color.Red, Color.White);
cell.CellStyle = new(Color.White, Color.SlateGray);

g.AddRow("r3-col1", "row3-col2", "r3-c3");

g.Footer = new("Total Count 3");
g.Show();
```
![Example - Grid 2](https://raw.githubusercontent.com/StrickTechnologies/Strick.PlusCon/master/SampleImages/ex_grid_2.png)
