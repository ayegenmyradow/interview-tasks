using GeometricAreaLib.Interfaces;

namespace GeometricAreaLib.Services
{
    /// Provides utility methods for calculating the area of shapes implementing <see cref="IShape"/>.
    public static class ShapeAreaCalculator
    {

        /// Calculates the area of the specified shape.
        /// <param name="shape">An object implementing the <see cref="IShape"/> interface.</param>
        /// <returns>The area of the shape as a <see cref="double"/>.</returns>
        public static double CalculateArea(IShape shape)
        {
            return shape.GetArea();
        }
    }
}
