using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Strick.PlusCon.Test;


[TestClass]
internal class ExtensionTests
{
	[TestMethod]
	public void ColorSpace_AsString()
	{
		Assert.AreEqual("38", ColorSpace.fore.AsString());
		Assert.AreEqual("39", ColorSpace.foreReset.AsString());
		Assert.AreEqual("48", ColorSpace.back.AsString());
		Assert.AreEqual("49", ColorSpace.backReset.AsString());

		Assert.ThrowsException<ArgumentException>(() => ((ColorSpace)0).AsString());
		Assert.ThrowsException<ArgumentException>(() => ((ColorSpace)99).AsString());
	}
}
