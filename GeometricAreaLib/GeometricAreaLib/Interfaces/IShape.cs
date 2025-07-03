// Represents a geometric shape that can calculate its area.

// This interface defines a contract for all shapes to implement
// the GetArea method, which returns the area of the shape as a double.
// Implementing classes could represent various shapes such as circles, rectangles, triangles, etc.

namespace GeometricAreaLib.Interfaces
{
    public interface IShape
    {
        // Calculates and returns the area of the shape.
        // The area of the shape as a double.
        double GetArea();
    }
}