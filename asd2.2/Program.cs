using System;
using System.Linq;

class Point
{
    public double X, Y;
    public Point(double x, double y)
    {
        X = x; Y = y;
    }
}

class Triangle
{
    public Point P1, P2, P3;
    public double Area, Perimeter;

    public Triangle(Point p1, Point p2, Point p3)
    {
        P1 = p1; P2 = p2; P3 = p3;
        Perimeter = Distance(P1, P2) + Distance(P2, P3) + Distance(P3, P1);
        double s = Perimeter / 2;
        Area = Math.Sqrt(s * (s - Distance(P1, P2)) * (s - Distance(P2, P3)) * (s - Distance(P3, P1)));
    }

    private double Distance(Point a, Point b)
    {
        return Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2));
    }

    public override string ToString()
    {
        return $"Triangle(P1=({P1.X}, {P1.Y}), P2=({P2.X}, {P2.Y}), P3=({P3.X}, {P3.Y}), Area={Area:F2}, Perimeter={Perimeter:F2})";
    }
}

class HashTable
{
    private Triangle[] table;
    private int size;
    private int count = 0;

    public HashTable(int size)
    {
        this.size = size;
        table = new Triangle[size];
    }

    public int HashFunction(double key)
    {
        double A = 0.6180339887;
        return (int)(size * ((key * A) % 1));
    }

    public bool Insert(Triangle triangle)
    {
        int index = HashFunction(triangle.Area);
        for (int i = 0; i < size; i++)
        {
            int pos = (index + i) % size;
            if (table[pos] == null)
            {
                table[pos] = triangle;
                count++;
                return true;
            }
        }

        return false;
    }


    public void Display()
    {
        if (IsEmpty())
        {
            Console.WriteLine("Hash table is empty!");
            return;
        }

        for (int i = 0; i < size; i++)
        {
            if (table[i] != null)
                Console.WriteLine($"[{i}]: {table[i]}");
            else
                Console.WriteLine($"[{i}]: Empty");
        }
    }

    public void DeleteByPerimeter(double minPerimeter)
    {
        for (int i = 0; i < size; i++)
        {
            if (table[i] != null && table[i].Perimeter >= minPerimeter)
            {
                table[i] = null;
                count--;
            }
        }
    }

    public bool IsEmpty()
    {
        return count == 0;
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Enter hash table size: ");
        int size = int.Parse(Console.ReadLine());
        HashTable hashTable = new HashTable(size);

        Random rand = new Random();
        for (int i = 0; i < size * 2; i++)
        {
            Point p1 = new Point(rand.Next(0, 10), rand.Next(0, 10));
            Point p2 = new Point(rand.Next(0, 10), rand.Next(0, 10));
            Point p3 = new Point(rand.Next(0, 10), rand.Next(0, 10));

            if (IsValidTriangle(p1, p2, p3))
            {
                if (!hashTable.Insert(new Triangle(p1, p2, p3)))
                    Console.WriteLine("Insert Failed");
            }
            else Console.WriteLine("Triangle is not valid");
        }

        Console.WriteLine("Initial Hash Table:");
        hashTable.Display();

        Console.Write("Enter min perimeter to delete: ");
        double minPerimeter = double.Parse(Console.ReadLine());
        hashTable.DeleteByPerimeter(minPerimeter);

        Console.WriteLine("Hash Table after deletion:");
        hashTable.Display();
    }

    static bool IsValidTriangle(Point p1, Point p2, Point p3)
    {
        double d1 = Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        double d2 = Math.Sqrt(Math.Pow(p3.X - p2.X, 2) + Math.Pow(p3.Y - p2.Y, 2));
        double d3 = Math.Sqrt(Math.Pow(p1.X - p3.X, 2) + Math.Pow(p1.Y - p3.Y, 2));
        return d1 + d2 > d3 && d1 + d3 > d2 && d2 + d3 > d1;
    }
}
