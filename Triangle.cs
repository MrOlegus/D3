using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3
{
    public class Triangle
    {
        public Point3D point1;
        public Point3D point2;
        public Point3D point3;
        public Color color;

        public Triangle(Point3D point1, Point3D point2, Point3D point3, Color color)
        {
            this.point1 = point1;
            this.point2 = point2;
            this.point3 = point3;
            this.color = color;
        }
    }
}
