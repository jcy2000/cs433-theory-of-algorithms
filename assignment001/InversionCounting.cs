using System;
namespace _433_PA1
{
    public class InversionCounting
    {
        private readonly int[] mergedArray;
        private readonly int[] array;
        private readonly int n;

        public InversionCounting(int[] array, int n)
        {
            this.array = array;
            this.mergedArray = new int[n];
            this.n = n;
        }

        public int bruteForce()
        {
            int count = 0;
            for (int i = 0; i < n; i++)
                for (int j = i + 1; j < n; j++)
                    if (array[i] > array[j])
                        count++;
            return count;
        }

        public int countInversions()
        {
            return countInversions(0, n - 1);
        }

        private int countInversions(int left, int right)
        { // complete this function
        }
    }
}
