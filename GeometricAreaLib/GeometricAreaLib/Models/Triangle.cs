using GeometricAreaLib.Interfaces;
using System;

namespace GeometricAreaLib.Models
{
    /// Represents a triangle shape that implements <see cref="IShape"/>.
    public class Triangle : IShape
    {
        /// Gets or sets the length of side A of the triangle.
        public double A { get; set; }
        /// Gets or sets the length of side B of the triangle.
        public double B { get; set; }
        /// Gets or sets the length of side C of the triangle.
        public double C { get; set; }

        /// Initializes a new instance of the <see cref="Triangle"/> class with the specified side lengths.
        /// <param name="a">Length of side A. Must be positive.</param>
        /// <param name="b">Length of side B. Must be positive.</param>
        /// <param name="c">Length of side C. Must be positive.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when any side length is non-positive or the triangle inequality is violated.
        /// </exception>
        public Triangle(double a, double b, double c)
        {
            if (a <= 0 || b <= 0 || c <= 0)
                throw new ArgumentException("All sides must be positive.");

            if (a + b <= c || a + c <= b || b + c <= a)
                throw new ArgumentException("Triangle inequality violated.");

            A = a;
            B = b;
            C = c;
        }

        /// Calculates and returns the area of the triangle using Heron's formula.
        /// <returns>The area of the triangle.</returns>
        public double GetArea()
        {
            double s = (A + B + C) / 2;
            return Math.Sqrt(s * (s - A) * (s - B) * (s - C));
        }

        /// Determines whether the triangle is right-angled.
        /// <returns>
        /// <c>true</c> if the triangle satisfies the Pythagorean theorem within a tolerance; otherwise, <c>false</c>.
        /// </returns>
        public bool IsRightAngled()
        {
            double[] sides = new[] { A, B, C };
            Array.Sort(sides);
            return Math.Abs(sides[0] * sides[0] + sides[1] * sides[1] - sides[2] * sides[2]) < 1e-6;
        }
    }
}
