using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3
{
    public class Camera
    {
        public Map map;
        public Point3D pos;
        public Vector vector;

        public int photoWidth;
        public int photoHeight;
        public int distToPhoto;

        public Camera(Map map, Point3D pos, Vector vector)
        {
            Size resolution = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;

            photoWidth = resolution.Width;
            photoHeight = resolution.Height;
            distToPhoto = resolution.Width;

            this.map = map;
            this.pos = pos;
            this.vector = vector;
        }

        public void DrawPhoto(Graphics graphics)
        {
            long start = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            List<Triangle> triangles = new List<Triangle>();
            foreach (Item item in map.items)
                foreach (Triangle triangle in item.CurrentImg3D)
                    triangles.Add(triangle);
            foreach (Triangle triangle in triangles)
                DrawTriangle(graphics, triangle);
            long finish = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            long time = finish - start;
        }

        public void Forward(int delta)
        {
            Vector dVector = new Vector(this.vector);
            dVector.Scale(delta);
            pos.x += (int)dVector.x;
            pos.y += (int)dVector.y;
            pos.z += (int)dVector.z;
        }

        public void Back(int delta)
        {
            Forward(-delta);
        }

        public void Up(int delta)
        {
            pos.z += delta;
        }

        public void Down(int delta)
        {
            pos.z -= delta;
        }

        public void RotateRight(double angle)
        {
            RotateLeft(-angle);
        }

        public void RotateLeft(double angle)
        {
            float x = (float)(vector.x * Math.Cos(angle) - vector.y * Math.Sin(angle));
            float y = (float)(vector.x * Math.Sin(angle) + vector.y * Math.Cos(angle));
            vector.x = x;
            vector.y = y;
            vector.Scale(1);
        }

        private void DrawTriangle(Graphics graphics, Triangle triangle)
        {
            
            Point? point1 = GetPoint(triangle.point1);
            Point? point2 = GetPoint(triangle.point2);
            Point? point3 = GetPoint(triangle.point3);
            
            if ((point1 != null) && (point2 != null) && (point3 != null))
            {
                Point[] points = { (Point)point1, (Point)point2, (Point)point3 };
                graphics.FillPolygon(new SolidBrush(triangle.color), points);
            }
        }

        private Point? GetPoint(Point3D point)
        {
            float m = vector.x * (point.x - pos.x) + vector.y * (point.y - pos.y) + vector.x * vector.y * (point.z - pos.z);
            float n = vector.y * (point.x - pos.x) - vector.x * (point.y - pos.y) + vector.y * vector.z * (point.z - pos.z);
            float k = vector.z * (point.x - pos.x) - (vector.x * vector.x + vector.y * vector.y) * (point.z - pos.z);
            if (m <= 0) return null;
            Point result = new Point();
            result.X = (int)(n * distToPhoto / m) + photoWidth / 2;
            result.Y = (int)(k * distToPhoto / m) + photoHeight / 2;
            return result;
        }
    }
}
