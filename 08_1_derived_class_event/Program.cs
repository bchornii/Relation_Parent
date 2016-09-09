using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _08_1_derived_class_event
{
    class Program
    {
        static void Main(string[] args)
        {
            Circle c1 = new Circle(54);
            Rectangle r1 = new Rectangle(12, 9);
            ShapeContainer sc = new ShapeContainer();
            
            // add shapes into container
            sc.AddShape(c1);
            sc.AddShape(r1);

            // update shapes
            c1.Update(23);
            r1.Update(34, 42);

            Console.Read();
            
        }
    }

    public class ShapeEventArgs : EventArgs
    {
        public double NewArea { get; private set; }
        public ShapeEventArgs(double d)
        {
            NewArea = d;
        }
    }

    public abstract class Shape
    {
        public virtual double Area { get; set; }

        public event EventHandler<ShapeEventArgs> ShapeChanged;

        protected virtual void OnShapeChanged(ShapeEventArgs e)
        {
            var handler = ShapeChanged;
            handler?.Invoke(this, e);
        }
        
        public abstract void Draw();
    }

    public class Circle : Shape
    {
        private double radius;
        public override double Area => 3.14 * radius * radius;        
        public Circle(double d)
        {
            Update(d);            
        }
        public void Update(double d)
        {
            radius = d;
            OnShapeChanged(new ShapeEventArgs(Area));
        }
        protected override void OnShapeChanged(ShapeEventArgs e)
        {
            // circle specific processing
            base.OnShapeChanged(e);
        }

        public override void Draw()
        {
            Console.WriteLine("Draw a circle");
        }
    }

    public class Rectangle : Shape
    {
        private double len;
        private double w;

        public override double Area => len * w;

        public Rectangle(double len, double w)
        {
            Update(len, w);
        }

        public void Update(double len, double w)
        {
            this.len = len;
            this.w = w;
            OnShapeChanged(new ShapeEventArgs(Area));
        }

        protected override void OnShapeChanged(ShapeEventArgs e)
        {
            // specific rectangle processing
            base.OnShapeChanged(e);
        }

        public override void Draw()
        {
            Console.WriteLine("Drawing rectangle");
        }
    }

    public class ShapeContainer
    {
        List<Shape> _shapesList;

        public ShapeContainer()
        {
            _shapesList = new List<Shape>();
        }

        public void AddShape(Shape s)
        {
            _shapesList.Add(s);
            s.ShapeChanged += HandleShapeChanged;
        }

        private void HandleShapeChanged(object sender, ShapeEventArgs e)
        {
            Shape s = sender as Shape;

            Console.WriteLine("Received event. Shape area is {0}", e.NewArea);

            s.Draw();
        }
    }
}
