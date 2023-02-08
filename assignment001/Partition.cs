using System;
namespace _433_PA1
{
    public class Partition
    {

        protected readonly int[] array;
        protected readonly int n;

        static Random rand;

        public Partition(int[] array, int n)
        {
            this.array = array;
            this.n = n;
            rand = new Random((int)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond));
        }

        protected void swap(int x, int y)
        {
            int temp = array[x];
            array[x] = array[y];
            array[y] = temp;
        }

        protected int generateRandomPivot(int left, int right)
        {
            int pivotIndex = left + rand.Next(right - left + 1);
            return array[pivotIndex];
        }

        protected int generateMedianOf3Pivot(int left, int right)
        {
            int mid = (left + right) / 2;

            if (array[left] > array[mid])
                swap(left, mid);

            if (array[left] > array[right])
                swap(left, right);

            if (array[mid] > array[right])
                swap(mid, right);

            return array[mid];
        }

        public int partition(int left, int right, int pivot)
        { // complete this function
        }
    }
}
