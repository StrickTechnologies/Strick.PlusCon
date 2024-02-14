namespace Strick.PlusCon.Models;


/// <summary>
/// A <see cref="MenuOption"/> that can be added to a <see cref="Menu"/>. It is NOT selectable by the user, 
/// but simply provides a visual separation on the <see cref="Menu"/>.
/// </summary>
public class MenuSeperator : MenuOption
{
	/// <summary>
	/// <inheritdoc cref="MenuOption(string)"/>
	/// </summary>
	/// <param name="caption"><inheritdoc cref="MenuOption(string)" path="/param[@name='caption']"/></param>
	public MenuSeperator(string caption) : base(caption) { }

	/// <summary>
	/// Returns the <see cref="MenuOption.Caption"/> formatted for the <see cref="Menu"/>. 
	/// If <see cref="MenuOption.Caption"/> is set to any single character, the character 
	/// is repeated <paramref name="length"/> times. Otherwise, <see cref="MenuOption.Caption"/> 
	/// is returned.
	/// </summary>
	/// <param name="length"></param>
	/// <returns></returns>
	public override string GetText(int length)
	{
		if (string.IsNullOrEmpty(Caption) || Caption.Length > 1)
		{ return Caption; }

		return new string(Caption[0], length);
	}
}
