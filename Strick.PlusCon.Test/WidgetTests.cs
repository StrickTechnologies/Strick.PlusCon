using Strick.PlusCon.Test.Models;

namespace Strick.PlusCon.Test;


[TestClass]
public class WidgetTests
{
	[TestMethod]
	public void TestStatics()
	{
		Widget small = WidgetRepository.SmallWidget();
		compareWidget(small, 1001, "Small Widget", 1.25m);

		Widget med = WidgetRepository.MediumWidget();
		compareWidget(med, 1002, "Medium Widget", 2.33m);

		Widget large = WidgetRepository.LargeWidget();
		compareWidget(large, 1003, "Large Widget", 3.49m);

		var all = WidgetRepository.AllWidgets().ToList();
		Assert.AreEqual(3, all.Count);
		compareWidgets(small, all[0]);
		compareWidgets(med, all[1]);
		compareWidgets(large, all[2]);
	}

	private void compareWidget(Widget w, int id, string name, decimal price)
	{
		Assert.AreEqual(id, w.Id);
		Assert.AreEqual(name, w.Name);
		Assert.AreEqual(price, w.Price);
	}

	private void compareWidgets(Widget expected, Widget actual)
	{
		Assert.AreEqual(actual.Id, expected.Id);
		Assert.AreEqual(actual.Name, expected.Name);
		Assert.AreEqual(actual.Price, expected.Price);
	}


	[TestMethod]
	public void TestSales()
	{
		Widget w = WidgetRepository.SmallWidget();
		TestASales(w, 1);
		TestQSales(w, 1);
		TestQSales(w, 2);
		TestQSales(w, 3);
		TestQSales(w, 4);

		w = WidgetRepository.MediumWidget();
		TestASales(w, 2);
		TestQSales(w, 1);
		TestQSales(w, 2);
		TestQSales(w, 3);
		TestQSales(w, 4);

		w = WidgetRepository.LargeWidget();
		TestASales(w, 3);
		TestQSales(w, 1);
		TestQSales(w, 2);
		TestQSales(w, 3);
		TestQSales(w, 4);
	}

	private void TestASales(Widget w, int id)
	{
		List<int> sales = WidgetRepository.GetAnnualSales(w.Id).ToList();
		Assert.AreEqual(12, sales.Count);

		for (int i = 1; i <= 12; i++)
		{ Assert.AreEqual(i * 100 + id, sales[i - 1]); }
	}

	private void TestQSales(Widget w, int quarter)
	{
		var aSales = WidgetRepository.GetAnnualSales(w.Id).ToArray();
		int qSales = WidgetRepository.GetQuarterlySales(w.Id, quarter);

		int q1 = (quarter - 1) * 3;
		int q2 = q1 + 3;

		Assert.AreEqual(qSales, aSales[q1..q2].Sum());
	}
}
