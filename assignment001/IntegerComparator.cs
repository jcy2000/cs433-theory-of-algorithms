using System;
using System.Collections.Generic;

namespace _433_PA1
{
	public class IntegerComparator : IComparer<int>
	{
		public int Compare(int arg1, int arg2)
		{
			return arg1 - arg2;
		}

	}
}
