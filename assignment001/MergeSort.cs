using System;
using System.Collections.Generic;

namespace _433_PA1
{
	public class MergeSort<T>
	{
		public MergeSort()
		{
		}

		private T[] array;
		private T[] mergedArray;
		private int n;

		public MergeSort(T[] array, int n)
		{
			this.array = array;
			this.n = n;
			this.mergedArray = new T[n];
		}

		public void mergesort(IComparer<T> comparator)
		{
			mergesort(0, n - 1, comparator);
		}

		private void mergesort(int left, int right, IComparer<T> comparator)
		{
			if (left == right)
				return;
			int mid = (left + right) / 2;
			mergesort(left, mid, comparator);
			mergesort(mid + 1, right, comparator);
			int i = left;
			while (i <= right)
			{
				mergedArray[i] = array[i];
				i++;
			}
			i = left;
			int j = mid + 1, k = left;
			while (i <= mid && j <= right)
				array[k++] = comparator.Compare(mergedArray[j], mergedArray[i]) < 0 ? mergedArray[j++] : mergedArray[i++];
			while (i <= mid)
				array[k++] = mergedArray[i++];
		}
	}
}
