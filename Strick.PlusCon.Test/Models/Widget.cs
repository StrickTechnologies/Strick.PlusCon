namespace Strick.PlusCon.Test.Models;


/// <summary>
/// A simple class to represent a widget. Used for testing and examples.
/// </summary>
/// <param name="id"><inheritdoc cref="Id" path="/summary"/></param>
/// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
/// <param name="price"><inheritdoc cref="Price" path="/summary"/></param>
internal class Widget(int id, string name, decimal price)
{
	public static Widget SmallWidget() => new Widget(1001, "Small Widget", 1.25M);
	public static Widget MediumWidget() => new Widget(1002, "Medium Widget", 2.33M);
	public static Widget LargeWidget() => new Widget(1003, "Large Widget", 3.49M);

	public static IEnumerable<Widget> AllWidgets() => [SmallWidget(), MediumWidget(), LargeWidget()];


	public override string ToString() => $"{GetType().Name} {Id}-{Name} ({Price:C2})";


	/// <summary>
	/// The Id of the widget
	/// </summary>
	public int Id { get; } = id;

	/// <summary>
	/// The Name of the widget
	/// </summary>
	public string Name { get; } = name;

	/// <summary>
	/// The Price of the widget
	/// </summary>
	public decimal Price { get; } = price;
}
