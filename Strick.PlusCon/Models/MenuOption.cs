using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Strick.PlusCon.Models;


public class MenuOption
{
	public MenuOption(char key, string caption)
	{
		Key = key;
		Caption = caption;
	}

	public MenuOption(char key, string caption, Action function) : this(key, caption)
	{
		Function = function;
	}

	public MenuOption(char key, string caption, Menu subMenu) : this(key, caption)
	{
		this.Submenu = subMenu;
	}


	public char Key { get; set; }

	public string Caption { get; set; }

	public Action? Function { get; }

	public Menu? Submenu { get; }
}
