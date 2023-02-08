using System;
namespace _433_PA1
{
    public class TestTime
    {

        static Random rand;
        static int[] LARGE_POINT_SIZES = { 10, 50, 100, 500, 1000, 2000, 5000, 10000, 20000, 50000, 100000 };

        private static void testIfSorted(int[] A, int len, char s)
        {
            for (int i = 0; i < len - 1; i++)
                if (A[i] > A[i + 1])
                {
                    if (s == 'M')
                        throw new Exception("MergeSort code is incorrect");
                    else if (s == '3')
                        throw new Exception("QuickSort (median of 3) code is incorrect");
                    else if (s == 'Q')
                        throw new Exception("QuickSort (randomized) code is incorrect");
                    else if (s == 'R')
                        throw new Exception("RadixSort code is incorrect");
                }
        }

        private static int getRandom()
        {
            return (rand.Next() - Int32.MaxValue / 2) * 2 / 3;
        }

        private static long currentTimeMillis()
        {
            return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }

        private static void compareSorting()
        {
            Console.WriteLine("Length, MergeSort, QuickSort (median of 3), QuickSort (randomized), RadixSort");
            double mergeAvg = 0, quickMedianOf3Avg = 0, quickRandomAvg = 0, radixRandomAvg = 0;
            int numExec = 0;
            for (int num = 500000; num <= 50000000; num = (int)(num * 1.3))
            {
                int[] array = new int[num];
                int[] temp = new int[num];
                for (int i = 0; i < num; i++)
                    array[i] = getRandom();

                double timeStart, timeEnd;

                for (int i = 0; i < num; i++)
                    temp[i] = array[i];
                timeStart = currentTimeMillis();
                new MergeSort<int>(temp, num).mergesort(new IntegerComparator());
                timeEnd = currentTimeMillis();
                testIfSorted(temp, num, 'M');
                mergeAvg += (timeEnd - timeStart);
                Console.Write(num + ", " + (timeEnd - timeStart));

                for (int i = 0; i < num; i++)
                    temp[i] = array[i];
                timeStart = currentTimeMillis();
                new QuickSort(temp, num).quicksortMedianOf3();
                timeEnd = currentTimeMillis();
                testIfSorted(temp, num, '3');
                quickMedianOf3Avg += (timeEnd - timeStart);
                Console.Write(", " + (timeEnd - timeStart));

                for (int i = 0; i < num; i++)
                    temp[i] = array[i];
                timeStart = currentTimeMillis();
                new QuickSort(temp, num).quicksortRandom();
                timeEnd = currentTimeMillis();
                testIfSorted(temp, num, 'Q');
                quickRandomAvg += (timeEnd - timeStart);
                Console.Write(", " + (timeEnd - timeStart));

                for (int i = 0; i < num; i++)
                    temp[i] = array[i];
                timeStart = currentTimeMillis();
                new RadixSort(temp, num).radixSort();
                timeEnd = currentTimeMillis();
                testIfSorted(temp, num, 'R');
                radixRandomAvg += (timeEnd - timeStart);
                Console.WriteLine(", " + (timeEnd - timeStart));

                numExec++;
            }
            Console.WriteLine("\nMergeSort average time is: {0:F2} millisecs", mergeAvg / numExec);
            Console.WriteLine("QuickSort (median of 3) average time is: {0:F2} millisecs", quickMedianOf3Avg / numExec);
            Console.WriteLine("QuickSort (randomized) average time is: {0:F2} millisecs", quickRandomAvg / numExec);
            Console.WriteLine("RadixSort average time is: {0:F2} millisecs", radixRandomAvg / numExec);
        }

        private static void compareSelection()
        {
            double randomAvg = 0, radixSortedAvg = 0;
            int numExec = 0;
            Console.WriteLine("Length, Selection via RadixSort, Randomized Selection");
            for (int num = 500000; num <= 50000000; num = (int)(num * 1.3))
            {
                int[] array = new int[num];
                int[] radix = new int[num];
                for (int i = 0; i < num; i++)
                    radix[i] = array[i] = getRandom();

                double timeStart, timeEnd;
                int[] K = new int[(int)Math.Log(num)];
                int lenK = K.Length;
                for (int i = 0; i < lenK; i++)
                    K[i] = rand.Next(num);

                timeStart = currentTimeMillis();
                new RadixSort(radix, num).radixSort();
                timeEnd = currentTimeMillis();
                radixSortedAvg += (timeEnd - timeStart) * lenK;
                Console.Write(num + ", " + (timeEnd - timeStart));

                double selTime = 0;
                int[] selArray = new int[num];
                for (int i = 0; i < lenK; i++)
                {
                    for (int j = 0; j < num; j++)
                        selArray[j] = array[j];

                    timeStart = currentTimeMillis();
                    Selection selection = new Selection(selArray, num);
                    int answer = selection.select(K[i]);
                    timeEnd = currentTimeMillis();

                    selTime += (timeEnd - timeStart);
                    if (answer != radix[K[i] - 1])
                        throw new Exception("Selection code is incorrect");
                }
                randomAvg += selTime;
                numExec += lenK;
                Console.WriteLine(", {0:F2}", selTime / lenK);
            }
            Console.WriteLine("\nSelection using RadixSort average time is: {0:F2} millisecs", radixSortedAvg / numExec);
            Console.WriteLine("Selection using random pivot average time is: {0:F2} millisecs", randomAvg / numExec);
        }

        private static void compareInversion()
        {
            double mergeSortAvg = 0, bruteForceAvg = 0;
            int numExec = 0;
            Console.WriteLine("Length, BruteForce Inversion, MergeSort Inversion");
            for (int num = 10000; num <= 300000; num = (int)(num * 1.3))
            {
                int[] array = new int[num];
                for (int i = 0; i < num; i++)
                    array[i] = getRandom();

                double timeStart, timeEnd;
                InversionCounting invCount = new InversionCounting(array, num);

                timeStart = currentTimeMillis();
                int count = invCount.bruteForce();
                timeEnd = currentTimeMillis();
                bruteForceAvg += (timeEnd - timeStart);
                Console.Write(num + ", " + (timeEnd - timeStart));

                timeStart = currentTimeMillis();
                if (count != invCount.countInversions())
                    throw new Exception("Inversion Counting code is incorrect");
                timeEnd = currentTimeMillis();
                mergeSortAvg += (timeEnd - timeStart);
                Console.WriteLine(", " + (timeEnd - timeStart));
                numExec++;
            }
            Console.WriteLine("\nBruteForce average time is: {0:F2} millisecs", bruteForceAvg / numExec);
            Console.WriteLine("MergeSort Inversion average time is: {0:F2} millisecs", mergeSortAvg / numExec);
        }

        private static void testClosestPoints()
        {
            Random rand = new Random((int)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond));
            foreach (int numPoints in LARGE_POINT_SIZES)
            {

                Point[] points = new Point[numPoints];
                for (int j = 0; j < numPoints; j++)
                    points[j] = new Point(rand.Next(), rand.Next());

                long startTime = currentTimeMillis();
                Point[] bruteForce = ClosestPairOfPoints.bruteForce(points);
                long timeBruteForce = currentTimeMillis() - startTime;

                startTime = currentTimeMillis();
                Point[] nlogn = ClosestPairOfPoints.findClosestPair(points);
                long timenlogn = currentTimeMillis() - startTime;

                double distBruteForce = bruteForce[0].distance(bruteForce[1]);
                double distnlogn = nlogn[0].distance(nlogn[1]);
                if (distBruteForce == distnlogn)
                {
                    Console.WriteLine("Time to find closest pair among " + numPoints
                + " points using brute-force strategy = " + timeBruteForce + " (may vary with each execution)");
                    Console.WriteLine("Time to find closest pair among " + numPoints
                + " points using divide & conquer strategy = " + timenlogn + " (may vary with each execution)");
                    Console.WriteLine();
                }
                else
                    throw new Exception("Something is wrong!");
            }
        }

        public static void Main(String[] args)
        {
            rand = new Random((int)(currentTimeMillis()));
            Console.WriteLine("*** Time Test Sorting ***\n");
            compareSorting();
            Console.WriteLine("\n*** Time Test Selection ***\n");
            compareSelection();
            Console.WriteLine("\n*** Time Test Inversion ***\n");
            compareInversion();
            Console.WriteLine("\n****************** Time Test Closest Pair of Points ******************\n");
            testClosestPoints();
        }
    }
}
