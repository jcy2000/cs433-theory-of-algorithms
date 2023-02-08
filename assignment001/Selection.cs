using System;
namespace _433_PA1
{
    public class Selection : Partition
    {
        public Selection(int[] array, int n) : base(array, n)
        {
        }

        public int select(int k)
        {
            return select(0, n - 1, k);
        }

        private int select(int left, int right, int k)
        { // complete this function
        }
    }
}
