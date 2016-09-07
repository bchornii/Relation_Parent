using System;
using System.Linq;

namespace simple
{
    public enum Sex
    {
        None,
        Man,
        Woman
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SexAttribute : Attribute
    {
        public Sex humanSex { get; set; }
        public SexAttribute(Sex humanSex)
        {
            this.humanSex = humanSex;
        }
    }

    public interface CommonPossibilities
    {
        void Walk();
    }

    public interface AdditionalPossibilities : CommonPossibilities
    {
        void Work();
    }

    public abstract class Human : CommonPossibilities
    {
        public virtual string Name { get; set; }
        public int Age { get; set; }

        public Sex HumanSex
        {
            get
            {
                var attr = GetType().GetCustomAttributes(typeof(SexAttribute), false)
                                    .FirstOrDefault();
                return attr != null ? (attr as SexAttribute).humanSex : Sex.None;
            }
        }

        public virtual void Walk()
        {
            Console.WriteLine("Human can walk.");
        }
    }

    [Sex(Sex.Woman)]
    public class Mother : Human, AdditionalPossibilities
    {
        string name;

        public Mother(string name)
        {
            this.name = name;
        }

        public void Work()
        {
            Console.WriteLine("Mother can work.");
        }

        public override void Walk()
        {
            Console.WriteLine("Mother can walk.");
        }

        public override string Name
        {
            get { return name; }
            set { name = "Mother : " + name; }
        }
    }

    [Sex(Sex.Man)]
    public class Dad : Human, AdditionalPossibilities
    {
        string name;

        public override void Walk()
        {
            Console.WriteLine("Dad can walk");
        }

        public void Work()
        {
            Console.WriteLine("Dad can work");
        }

        public override string Name
        {
            get { return name; }
            set { name = "Dad : " + name; }
        }
    }

    [Sex(Sex.Woman)]
    public class Kid : Human, CommonPossibilities
    {
        public Kid(string name)
        {
            Name = name;
        }
        public override void Walk()
        {
            Console.WriteLine("Kid can walk");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Mother mother = new Mother("Alice");
            //mother.Walk();
            //mother.Work();
            //Console.WriteLine(mother.HumanSex.ToString());

            string message = string.Empty;
            string a = "sfsf";
            string b = "dfdg";
            Console.WriteLine(a + b);
            Console.Read();

            return;

            A[] source = { new A { Property = 12 }, new A { Property = 13 }, new A { Property = 15 } };
            A[] dest = source.Clone() as A[];

            source.Clone();

            Console.WriteLine("Dest");
            foreach (var item in dest)
            {
                Console.WriteLine(item.Property);
            }
            Console.WriteLine("Source");
            foreach (var item in source)
            {
                Console.WriteLine(item.Property);
            }

            Console.WriteLine(new string('-', 80));

            source[0].Property = 1223;

            Console.WriteLine("Dest");
            foreach (var item in dest)
            {
                Console.WriteLine(item.Property);
            }

            Console.WriteLine("Source");
            foreach (var item in source)
            {
                Console.WriteLine(item.Property);
            }

            Console.Read();
        }
    }

    class A
    {
        public int Property { get; set; }
    }
}
