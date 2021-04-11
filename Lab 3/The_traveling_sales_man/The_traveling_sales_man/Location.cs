using System;

namespace The_traveling_sales_man
{
    public class Location
    {
        public Location(int id, double x, double y)
        {
            Id = id;
            X = x;
            Y = y;
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        public Location(Location other)
        {
            Id = other.Id;
            X = other.X;
            Y = other.Y;
        }

        public int Id { get; }
        public double X { get; }
        public double Y { get; }

        public static double Distance(Location location1, Location location2) =>
            Math.Sqrt(Math.Pow(location2.X - location1.X, 2) + Math.Pow(location2.Y - location1.Y, 2));
    }
}
