using System.Collections.Generic;
using System.Linq;

namespace Common.CopyModules
{
	/// <summary>
    /// This code demonstrates how to calculate the Mean, Median, and Mode from a list of items.  In this example,
    /// the target is a list of Invoice objects where the object provides the property DiscountPercent
    /// and the result is the Mean (Average), Median, or Mode of the values in DiscountPercent
    /// </summary>
    public static class MeanMedianMode
    {
		public static decimal CalculateMean(List<Invoice> invoiceList)
		{
			return invoiceList.Average(inv => inv.DiscountPercent);
		}

		public static decimal CalculateMedian(List<Invoice> invoiceList)
		{
			var sortedList = invoiceList.OrderBy(inv => inv.DiscountPercent);

			int count = invoiceList.Count();
			int position = count / 2;

			decimal median;
			if ((count % 2) == 0)
			{
				median = (sortedList.ElementAt(position).DiscountPercent +
						   sortedList.ElementAt(position - 1).DiscountPercent) / 2;
			}
			else
			{
				median = sortedList.ElementAt(position).DiscountPercent;
			}

			return median;
		}

		public static decimal CalculateMode(List<Invoice> invoiceList)
		{
			var mode = invoiceList.GroupBy(inv => inv.DiscountPercent)
						.OrderByDescending(group => group.Count())
						.Select(group => group.Key)
						.FirstOrDefault();
			return mode;
		}
	}
}
