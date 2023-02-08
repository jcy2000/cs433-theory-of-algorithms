using System;
using System.Collections.Generic;

namespace _433_PA1
{
	public class PointComparator : IComparer<Point>
	{
		public int Compare(Point arg1, Point arg2)
		{
			return arg1.x <= arg2.x ? -1 : 1;
		}
	}
}
