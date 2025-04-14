using System;
using System.Collections.Generic;

public class Student
{
    public string Surname { get; set; }
    public int TotalClasses { get; set; }
    public int MissedClasses { get; set; }

    public double MissRatio => TotalClasses == 0 ? 0 : (double)MissedClasses / TotalClasses;

    public override string ToString()
    {
        return $"{MissRatio:F2} | {Surname}, Всього: {TotalClasses}, Пропущено: {MissedClasses}";
    }
}

public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        var students = new Student[]
        {
            new Student { Surname = "Іваненко", TotalClasses = 30, MissedClasses = 3 },
            new Student { Surname = "Петренко", TotalClasses = 25, MissedClasses = 5 },
            new Student { Surname = "Сидоренко", TotalClasses = 40, MissedClasses = 2 },
            new Student { Surname = "Ковальчук", TotalClasses = 35, MissedClasses = 7 },
            new Student { Surname = "Мельник", TotalClasses = 28, MissedClasses = 0 },
            new Student { Surname = "Шевченко", TotalClasses = 30, MissedClasses = 15 },
            new Student { Surname = "Гнатюк", TotalClasses = 20, MissedClasses = 1 },
            new Student { Surname = "Олійник", TotalClasses = 50, MissedClasses = 10 }

        };

        Console.WriteLine("=== 1 РІВЕНЬ: Масив + сортування вставкою ===");
        var arr1 = (Student[])students.Clone();
        PrintStudents(arr1, "До сортування:");
        InsertionSortArray(arr1);
        PrintStudents(arr1, "Після сортування:");

        Console.WriteLine("\n=== 2 РІВЕНЬ: Двозв’язний список + сортування вставкою ===");
        var list = new LinkedList<Student>(students);
        PrintStudents(list, "До сортування:");
        InsertionSortLinkedList(list);
        PrintStudents(list, "Після сортування:");

        Console.WriteLine("\n=== 3 РІВЕНЬ: Масив + кишенькове сортування ===");
        var arr3 = (Student[])students.Clone();
        PrintStudents(arr3, "До сортування:");
        BucketSort(arr3);
        PrintStudents(arr3, "Після сортування:");
    }

    // Вивід студентів
    public static void PrintStudents(IEnumerable<Student> collection, string label)
    {
        Console.WriteLine(label);
        foreach (var student in collection)
            Console.WriteLine(student);
    }

    public static void InsertionSortArray(Student[] array)
    {
        for (int i = 1; i < array.Length; i++)
        {
            Student key = array[i];
            int j = i - 1;
            while (j >= 0 && array[j].MissRatio > key.MissRatio)
            {
                array[j + 1] = array[j];
                j--;
            }
            array[j + 1] = key;
        }
    }

    public static void InsertionSortLinkedList(LinkedList<Student> list)
    {
        if (list == null || list.Count < 2) return;

        var sorted = new LinkedList<Student>();

        foreach (var student in list)
        {
            var current = sorted.First;
            while (current != null && current.Value.MissRatio < student.MissRatio)
            {
                current = current.Next;
            }

            if (current == null)
                sorted.AddLast(student);
            else
                sorted.AddBefore(current, student);
        }

        list.Clear();
        foreach (var student in sorted)
            list.AddLast(student);
    }

    public static void BucketSort(Student[] array)
    {
        int bucketCount = 10;
        List<Student>[] buckets = new List<Student>[bucketCount];
        for (int i = 0; i < bucketCount; i++)
            buckets[i] = new List<Student>();

        foreach (var student in array)
        {
            int index = (int)(student.MissRatio * (bucketCount - 1));
            buckets[index].Add(student);
        }

        int k = 0;
        foreach (var bucket in buckets)
        {
            bucket.Sort((a, b) => a.MissRatio.CompareTo(b.MissRatio));
            foreach (var student in bucket)
                array[k++] = student;
        }
    }
}
