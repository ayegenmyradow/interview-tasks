using GeometricAreaLib.Models;

namespace GeometricAreaTests
{
    /// <summary>
    /// Contains unit tests for verifying the correctness of shape area calculations
    /// and triangle properties in the GeometricAreaLib.Models namespace.
    /// </summary>
    public class ShapeTests
    {
        /// <summary>
        /// Tests that the area calculation for a circle with radius 3 is correct.
        /// </summary>
        [Fact]
        public void CircleArea_Correct()
        {
            var circle = new Circle(3);
            double area = circle.GetArea();
            Assert.Equal(Math.PI * 9, area, 4);  // precision to 4 decimal places
        }

        /// <summary>
        /// Tests that the area calculation for a triangle with sides 3, 4, and 5 is correct.
        /// </summary>
        [Fact]
        public void TriangleArea_Correct()
        {
            var triangle = new Triangle(3, 4, 5);
            double area = triangle.GetArea();
            Assert.Equal(6, area, 4);  // precision to 4 decimal places
        }

        /// <summary>
        /// Tests that a triangle with sides 3, 4, and 5 is identified as right-angled.
        /// </summary>
        [Fact]
        public void Triangle_IsRightAngled_True()
        {
            var triangle = new Triangle(3, 4, 5);
            Assert.True(triangle.IsRightAngled());
        }

        /// <summary>
        /// Tests that a triangle with sides 3, 4, and 6 is not identified as right-angled.
        /// </summary>
        [Fact]
        public void Triangle_IsRightAngled_False()
        {
            var triangle = new Triangle(3, 4, 6);
            Assert.False(triangle.IsRightAngled());
        }
    }
}
