using System;

namespace _433_PA1
{
    public class TestCorrectness
    {
        private static string toString(int[] array)
        {
            if (array.Length == 0)
                return "[]";
            else
            {
                string output = "[";
                for (int i = 0; i < array.Length - 1; i++)
                    output += array[i] + ", ";
                output += array[array.Length - 1] + "]";
                return output;
            }
        }

        private static void testSorting(int[] array, int n)
        {

            int[] temp = new int[n];

            Console.WriteLine("Original array:                  " + toString(array));

            for (int i = 0; i < n; i++)
                temp[i] = array[i];
            new MergeSort<int>(temp, n).mergesort(new IntegerComparator());
            Console.WriteLine("MergeSorted array:               " + toString(temp));

            for (int i = 0; i < n; i++)
                temp[i] = array[i];
            new QuickSort(temp, n).quicksortMedianOf3();
            Console.WriteLine("QuickSorted (median of 3) array: " + toString(temp));

            for (int i = 0; i < n; i++)
                temp[i] = array[i];
            new QuickSort(temp, n).quicksortRandom();
            Console.WriteLine("QuickSorted (random) array:      " + toString(temp));

            for (int i = 0; i < n; i++)
                temp[i] = array[i];
            new RadixSort(temp, n).radixSort();
            Console.WriteLine("RadixSorted array:               " + toString(temp));
        }

        private static void testSelection(int[] array, int n)
        {
            int[] mergeArray = new int[n];

            for (int i = 0; i < n; i++)
                mergeArray[i] = array[i];
            new MergeSort<int>(mergeArray, n).mergesort(new IntegerComparator());

            int[] selArray = new int[n];
            for (int k = 1; k <= n; k++)
            {
                for (int j = 0; j < n; j++)
                    selArray[j] = array[j];
                Selection selection = new Selection(selArray, n);
                int answer = selection.select(k);
                Console.Write(k + "th smallest: {0}", answer);
                if (answer != mergeArray[k - 1])
                    Console.Write("; Selection code does not work for k = " + k);
                Console.WriteLine();
            }
        }

        private static void testInversions(int[] array, int n)
        {
            Console.WriteLine("Array is: " + toString(array));
            Console.WriteLine("Number of inversions is: " + new InversionCounting(array, n).countInversions());
        }

        public static void Main(String[] args)
        {
            Console.WriteLine("*** Correctness Test ***\n");
            int[] sorting = { 19, 1, 12, 100, 7, 8, 4, -10, 14, -1, 97, -1009, 4210 };
            int n = sorting.Length;
            int[] selection = new int[n];
            for (int i = 0; i < n; i++)
                selection[i] = sorting[i];
            testSorting(sorting, n);
            Console.WriteLine();
            testSelection(selection, n);
            Console.WriteLine();
            testInversions(sorting, n);
        }
    }
}
