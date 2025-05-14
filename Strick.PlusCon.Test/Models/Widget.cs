using Strick.Utility;

namespace Strick.PlusCon.Test.Models;


/// <summary>
/// A simple class to represent a widget. Used for testing and examples.
/// </summary>
/// <param name="Id"><inheritdoc cref="Id" path="/summary"/></param>
/// <param name="Name"><inheritdoc cref="Name" path="/summary"/></param>
/// <param name="Price"><inheritdoc cref="Price" path="/summary"/></param>
internal record Widget(int Id, string Name, decimal Price)
{
	public override string ToString() => $"{GetType().Name} {Id}-{Name} ({Price:C2})";


	/// <summary>
	/// The Id of the widget
	/// </summary>
	public int Id { get; } = Id;

	/// <summary>
	/// The Name of the widget
	/// </summary>
	public string Name { get; } = Name;

	/// <summary>
	/// The Price of the widget
	/// </summary>
	public decimal Price { get; } = Price;


	#region STATICS

	#region REPOSITORY

	/// <summary>
	/// Retrieves a small widget.
	/// </summary>
	public static Widget SmallWidget() => new Widget(1001, "Small Widget", 1.25M);

	/// <summary>
	/// Retrieves a medium widget.
	/// </summary>
	public static Widget MediumWidget() => new Widget(1002, "Medium Widget", 2.33M);

	/// <summary>
	/// Retrieves a large widget.
	/// </summary>
	public static Widget LargeWidget() => new Widget(1003, "Large Widget", 3.49M);

	/// <summary>
	/// Retrieves a collection of all available widgets.
	/// </summary>
	/// <returns>An <see cref="IEnumerable{T}"/> containing all available widgets.</returns>
	public static IEnumerable<Widget> AllWidgets() => [SmallWidget(), MediumWidget(), LargeWidget()];

	#endregion REPOSITORY

	#region SALES

	/// <summary>
	/// Retrieves annual sales data for a specific widget
	/// </summary>
	/// <param name="widgetId"><inheritdoc cref="Id" path="/summary"/> to retrieve sales data for</param>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	public static IEnumerable<int> GetAnnualSales(int widgetId)
	{
		if (!widgetId.Between(1001, 1003))
		{ throw new ArgumentOutOfRangeException(nameof(widgetId)); }

		int qty = widgetId - 1000;

		for (int i = 1; i <= 12; i++)
		{ yield return i * 100 + qty; }
	}

	/// <summary>
	/// Retrieves the quarterly sales for a specific widget.
	/// </summary>
	/// <param name="widgetId"><inheritdoc cref="GetAnnualSales(int)" path="/param[@name='widgetId']"/></param>
	/// <param name="quarter">The quarter for which sales data is being retrieved.</param>
	public static int GetQuarterlySales(int widgetId, int quarter)
	{
		var sales = Widget.GetAnnualSales(widgetId).ToArray();
		int q1 = (quarter - 1) * 3;
		int q2 = q1 + 3;

		return sales[q1..q2].Sum();
	}
	
	#endregion SALES

	#endregion STATICS
}
