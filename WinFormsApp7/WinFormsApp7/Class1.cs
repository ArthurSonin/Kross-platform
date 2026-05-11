using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp7
{
    public abstract class Shape
    {
        public int X { get; set; } 
        public int Y { get; set; } 
        public int Radius { get; set; }
        public Color Color { get; set; }

        protected void DrawCircle(Graphics g)
        {
            using (Pen circlePen = new Pen(Color.LightGray, 2))
            {
                g.DrawEllipse(circlePen, X - Radius, Y - Radius, Radius * 2, Radius * 2);
            }
        }

        public abstract void Draw(Graphics g);

        protected Point[] GetVertices(int n)
        {
            Point[] pts = new Point[n];
            for (int i = 0; i < n; i++)
            {
                double angle = i * 2 * Math.PI / n - Math.PI / 2;
                pts[i] = new Point(
                    X + (int)(Radius * Math.Cos(angle)),
                    Y + (int)(Radius * Math.Sin(angle))
                );
            }
            return pts;
        }
    }

    public class MyRectangle : Shape
    {
        public override void Draw(Graphics g)
        {
            DrawCircle(g); 
            using (Pen pen = new Pen(Color, 2))
            {
                Point[] points = GetVertices(4);
                g.DrawPolygon(pen, points);
            }
        }
    }

    public class MyTriangle : Shape
    {
        public override void Draw(Graphics g)
        {
            DrawCircle(g);
            using (Pen pen = new Pen(Color, 2))
            {
                Point[] points = GetVertices(3);
                g.DrawPolygon(pen, points);
            }
        }
    }

    public class MyPentagon : Shape
    {
        public override void Draw(Graphics g)
        {
            DrawCircle(g);
            using (Pen pen = new Pen(Color, 2))
            {
                Point[] points = GetVertices(5);
                g.DrawPolygon(pen, points);
            }
        }
    }
}
