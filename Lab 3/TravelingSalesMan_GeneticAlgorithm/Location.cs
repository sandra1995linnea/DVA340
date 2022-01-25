using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelingSalesMan_GeneticAlgorithm
{
    class Location
    {
        public Location(int id, double x, double y)
        {
            Id = id;
            X = x;
            Y = y;
        }

        public Location(Location other)
        {
            Id = other.Id;
            X = other.X;
            Y = other.Y;
        }
        public int Id { get; }
        public double X { get; }
        public double Y { get; }

        //calculates the distance between two locations
        public  double DistanceTo(Location other) => 
            Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
    }
}
