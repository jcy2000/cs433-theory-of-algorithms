using System;
using System.Collections.Generic;

namespace _433_PA1
{
	public class ClosestPairOfPoints
	{
		public ClosestPairOfPoints()
		{
		}

		public static Point[] bruteForce(Point[] points)
		{
			return bruteForce(points, 0, points.Length - 1);
		}

		public static Point[] findClosestPair(Point[] points)
		{
			/**
			 * Part 0:
			 */
			sortPointsByX(points);
			/**
			 * end Part 0
			 */
			return findClosestPairHelper(points, 0, points.Length - 1);
		}

		private static Point[] findClosestPairHelper(Point[] points, int left, int right)
		{

			/**
			 * Part 1:
			 */
			if (right - left <= 3)
			{
				insertionSortPointsByY(points, left, right);
				return bruteForce(points, left, right);
			}
			/**
			 * end Part 1
			 */

			/**
			 * Part 2
			 */
			int mid = (right - left) / 2 + left;
			double xmid = points[mid].x;
			/**
			 * end Part 2
			 */

			/**
			 * Part 3
			 */
			Point[] closestPointsLeft = findClosestPairHelper(points, left, mid);
			Point[] closestPointsRight = findClosestPairHelper(points, mid + 1, right);
			/**
			 * end Part 3
			 */

			/**
			 * Part 4
			 */
			double dLeft = closestPointsLeft[0].distance(closestPointsLeft[1]);
			double dRight = closestPointsRight[0].distance(closestPointsRight[1]);

			double dMin;
			Point[] closest;
			if (dLeft < dRight)
			{
				dMin = dLeft;
				closest = closestPointsLeft;
			}
			else
			{
				dMin = dRight;
				closest = closestPointsRight;
			}
			/**
			 * end Part 4
			 */

			/**
			 * Part 5
			 */
			mergeYSortedPoints(points, left, right, mid);
			/**
			 * end Part 5
			 */

			/**
			 * Part 6
			 */
			List<Point> pointsInRange = getPointsInRange(points, left, right, dMin, xmid);
			/**
			 * end Part 6
			 */

			/**
			 * Part 7
			 */
			for (int i = 0; i < pointsInRange.Count; i++)
			{
				for (int j = 1; j <= 7; j++)
				{
					if (i + j == pointsInRange.Count)
						break;
					double dist = pointsInRange[i].distance(pointsInRange[i + j]);
					if (dist < dMin)
					{
						dMin = dist;
						closest[0] = pointsInRange[i];
						closest[1] = pointsInRange[i + j];
					}
				}
			}
			/**
			 * end Part 7
			 */
			return closest;
		}

		private static Point[] bruteForce(Point[] points, int left, int right)
		{
			double minDist = Double.PositiveInfinity;
			Point[] answer = new Point[2];
			for (int i = left; i <= right; i++)
				for (int j = i + 1; j <= right; j++)
				{
					double dist = points[i].distance(points[j]);
					if (dist < minDist)
					{
						minDist = dist;
						answer[0] = points[i];
						answer[1] = points[j];
					}
				}
			return answer;
		}

		private static void sortPointsByX(Point[] points)
		{
			MergeSort<Point> mgSort = new MergeSort<Point>(points, points.Length);
			mgSort.mergesort(new PointComparator());
		}

		private static void insertionSortPointsByY(Point[] points, int left, int right)
		{
			for (int i = left; i <= right; i++)
			{
				int j = i;
				Point temp = points[j];
				while (j > left && temp.y < points[j - 1].y)
				{
					points[j] = points[j - 1];
					j--;
				}
				points[j] = temp;
			}
		}


		private static List<Point> getPointsInRange(Point[] ySortedPoints, int left, int right, double dMin,
				double xmid)
		{

			List<Point> pointsInRange = new List<Point>();
			for (int i = left; i <= right; i++)
				if (ySortedPoints[i].x >= xmid - dMin && ySortedPoints[i].x <= xmid + dMin)
					pointsInRange.Add(ySortedPoints[i]);
			return pointsInRange;
		}

		private static void mergeYSortedPoints(Point[] points, int left, int right, int mid)
		{
			Point[] ySortedPoints = new Point[right - left + 1];
			int i = left, j = mid + 1, k = 0;
			while (i <= mid && j <= right)
			{
				if (points[j].y < points[i].y)
					ySortedPoints[k++] = points[j++];
				else
					ySortedPoints[k++] = points[i++];
			}
			while (i <= mid)
				ySortedPoints[k++] = points[i++];

			while (j <= right)
				ySortedPoints[k++] = points[j++];

			for (i = left, k = 0; i <= right; i++, k++)
				points[i] = ySortedPoints[k];
		}
	}
}
