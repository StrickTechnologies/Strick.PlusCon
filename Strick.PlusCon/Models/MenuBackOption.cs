namespace Strick.PlusCon.Models;


/// <summary>
/// A <see cref="MenuOption"/> that can be added to a <see cref="Menu"/>, and when selected by the user will exit the <see cref="Menu"/>.
/// </summary>
public class MenuBackOption : MenuOption
{
	/// <summary>
	/// Creates an instance of the <see cref="MenuBackOption"/> class 
	/// with its <see cref="MenuOption.Caption"/> property set to <paramref name="caption"/>, and
	/// its <see cref="MenuOption.Keys"/> property set to <paramref name="key"/>.
	/// </summary>
	/// <param name="caption"></param>
	/// <param name="key"></param>
	public MenuBackOption(string caption, char key) : base(caption, key) { }
}