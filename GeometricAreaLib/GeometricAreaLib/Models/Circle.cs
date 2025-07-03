using GeometricAreaLib.Interfaces;
using System;

namespace GeometricAreaLib.Models
{
    // Represents a circle shape that implements IShape
    public class Circle : IShape
    {
        /// Gets or sets the radius of the circle.
        /// Must be a positive number.
        public double Radius { get; set; }

        /// Initializes a new instance of the <see cref="Circle"/> class with the specified radius.
        /// <param name="radius">The radius of the circle. Must be greater than zero.</param>
        /// <exception cref="ArgumentException">Thrown when the radius is zero or negative.</exception>
        public Circle(double radius)
        {
            if (radius <= 0)
                throw new ArgumentException("Radius must be positive.");
            Radius = radius;
        }

        /// Calculates and returns the area of the circle.
        /// <returns>The area calculated as π * radius².</returns>
        public double GetArea()
        {
            return Math.PI * Radius * Radius;
        }
    }
}
