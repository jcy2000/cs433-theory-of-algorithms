using System;

namespace _433_PA1
{
	public class Point
	{

		public double x, y;

		public Point(double x, double y)
		{
			this.x = x;
			this.y = y;
		}

		public double distance(Point arg)
		{
			double diffX = arg.x - x;
			double diffY = arg.y - y;
			double dist = Math.Sqrt(diffX * diffX + diffY * diffY);
			return dist;
		}
	}
}
