using System;
using System.Collections.Generic;
using System.Linq;


namespace Strick.PlusCon.Models;

/// <summary>
/// An option that can be added to <see cref="Menu"/>
/// </summary>
public class MenuOption
{
	/// <summary>
	/// Creates an instance with the object's <see cref="MenuOption.Caption"/> property set to <paramref name="caption"/>.
	/// </summary>
	/// <param name="caption">The caption that will be displayed for the <see cref="MenuOption"/></param>
	protected MenuOption(string caption)
	{
		Caption = caption;
	}

	/// <summary>
	/// Creates an instance of the <see cref="MenuOption"/> class 
	/// with its <see cref="Caption"/> property set to <paramref name="caption"/>, and
	/// its <see cref="Keys"/> property set to <paramref name="key"/>.
	/// </summary>
	/// <param name="caption"><inheritdoc cref="MenuOption(string)" path="/param[@name='caption']"/></param>
	/// <param name="key">The key that can be used to invoke the <see cref="MenuOption"/></param>
	protected MenuOption(string caption, char key) : this(caption)
	{
		Keys.Add(key);

		if (char.IsLetter(key))
		{
			if (char.IsUpper(key))
			{ Keys.Add(char.ToLower(key)); }
			else
			{ Keys.Add(char.ToUpper(key)); }
		}
	}

	/// <summary>
	/// Creates a <see cref="MenuOption"/> instance that will execute a function when selected.
	/// <para>If <paramref name="function"/> is null, an <see cref="ArgumentNullException"/> is thrown.</para>
	/// </summary>
	/// <param name="caption"><inheritdoc cref="MenuOption(string)" path="/param[@name='caption']"/></param>
	/// <param name="key"><inheritdoc cref="MenuOption(string, char)" path="/param[@name='key']"/></param>
	/// <param name="function">A method (or lambda) that has no parameters and no return value</param>
	/// <exception cref="ArgumentNullException"></exception>
	public MenuOption(string caption, char key, Action function) : this(caption, key)
	{
		Function = function ?? throw new ArgumentNullException(nameof(function));
	}

	/// <summary>
	/// Creates a <see cref="MenuOption"/> instance that will display a sub menu when selected.
	/// <para>If <paramref name="subMenu"/> is null, an <see cref="ArgumentNullException"/> is thrown.</para>
	/// </summary>
	/// <param name="caption"><inheritdoc cref="MenuOption(string)" path="/param[@name='caption']"/></param>
	/// <param name="key"><inheritdoc cref="MenuOption(string, char)" path="/param[@name='key']"/></param>
	/// <param name="subMenu"></param>
	/// <exception cref="ArgumentNullException"></exception>
	public MenuOption(string caption, char key, Menu subMenu) : this(caption, key)
	{
		Submenu = subMenu ?? throw new ArgumentNullException(nameof(subMenu));
	}


	/// <summary>
	/// A collection of char values that can be used to invoke the menu option.
	/// <para>If multiple values exist in the collection, only the first value is displayed when the menu is rendered.</para>
	/// <para>If multiple <see cref="MenuOption"/> objects in a <see cref="Menu"/> object's <see cref="Menu.Options"/> collection 
	/// contain the same value in their <see cref="Keys"/> collection, only the first option will be executed when that key is pressed.</para>
	/// <para>Note: If a value exists in this collection, it will override the same value in the <see cref="Menu.ExitKeys"/> collection.</para>
	/// </summary>
	public List<char> Keys { get; } = new List<char>();

	/// <summary>
	/// The text to display on the menu.
	/// </summary>
	public string Caption { get; set; }

	/// <summary>
	/// The method to execute when this option is selected on the menu.
	/// </summary>
	public Action? Function { get; }

	/// <summary>
	/// The submenu to display when this option is selected on the menu.
	/// </summary>
	public Menu? Submenu { get; }


	/// <summary>
	/// The text styling to apply to this object when it is displayed. 
	/// If null, the object will get styling from the <see cref="Menu"/> object's <see cref="Menu.OptionsStyle"/> property.
	/// </summary>
	public TextStyle? Style { get; set; }


	/// <summary>
	/// Retrieves the text as it will be displayed on the menu, including the first value in the <see cref="Keys"/> collection, a period (".") and a space (" ").
	/// </summary>
	/// <param name="length"></param>
	/// <returns></returns>
	public virtual string GetText(int length)
	{
		if (Keys == null || !Keys.Any())
		{ return Caption; }

		return $"{Keys[0]}. {Caption}";
	}

	//todo: WIP/Cleanup
	public virtual string GetKeyText() => Keys.HasAny() ? $"{Keys[0]}. " : "";
	public virtual string GetCaptionText => Keys.HasAny() ? $"{Keys[0]}. " : "";

	internal int Width => GetText(1).Length;


	#region EVENTS

	/// <summary>
	/// Occurs <b>before</b> the menu option is shown.
	/// </summary>
	public event EventHandler? BeforeShow;

	/// <summary>
	/// Invokes the <see cref="BeforeShow"/> event
	/// </summary>
	/// <param name="e"></param>
	internal protected virtual void OnBeforeShow(EventArgs e)
	{
		BeforeShow?.Invoke(this, e);
	}

	#endregion EVENTS
}
