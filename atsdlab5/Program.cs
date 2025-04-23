using System;
using System.Collections.Generic;

class Student
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public int Course { get; set; }
    public string Gender { get; set; }
    public bool LivesInDorm { get; set; }

    public override string ToString()
    {
        return $"{FirstName} {LastName}, Курс: {Course}, Стать: {Gender}, Проживання в гуртожитку: {(LivesInDorm ? "так" : "ні")}";
    }
}

class StudentArrayProcessor
{
    public static void PrintStudents(List<Student> students)
    {
        foreach (var student in students)
            Console.WriteLine(student);
    }

    public static void RemoveDormitoryMaleSixthCourse(List<Student> students)
    {
        students.RemoveAll(s => s.Gender == "ч" && s.Course == 6 && s.LivesInDorm);
    }
}

class BSTNode
{
    public Student Data;
    public BSTNode Left, Right;

    public BSTNode(Student student)
    {
        Data = student;
    }
}

class BST
{
    public BSTNode Root;

    public void Insert(Student student)
    {
        Root = InsertAtRoot(Root, student);
    }

    private BSTNode InsertAtRoot(BSTNode root, Student student)
    {
        if (root == null)
            return new BSTNode(student);

        int comparisonResult = string.Compare(student.FirstName, root.Data.FirstName, StringComparison.OrdinalIgnoreCase);
        if (comparisonResult < 0)
        {
            root.Left = InsertAtRoot(root.Left, student);
            root = RotateRight(root);
        }
        else
        {
            root.Right = InsertAtRoot(root.Right, student);
            root = RotateLeft(root);
        }
        return root;
    }


    public Student Search(string firstName)
    {
        var node = SearchRec(Root, firstName);
        return node?.Data;
    }

    private BSTNode SearchRec(BSTNode root, string firstName)
    {
        if (root == null)
            return null;

        int comparisonResult = string.Compare(firstName, root.Data.FirstName, StringComparison.OrdinalIgnoreCase);
        if (comparisonResult == 0)
            return root;
        else if (comparisonResult < 0)
            return SearchRec(root.Left, firstName);
        else
            return SearchRec(root.Right, firstName);
    }

    public void PrintLevelOrder()
    {
        if (Root == null) return;
        Queue<BSTNode> queue = new Queue<BSTNode>();
        queue.Enqueue(Root);

        while (queue.Count > 0)
        {
            BSTNode current = queue.Dequeue();
            Console.WriteLine(current.Data);

            if (current.Left != null) queue.Enqueue(current.Left);
            if (current.Right != null) queue.Enqueue(current.Right);
        }
    }

    public BSTNode RotateLeft(BSTNode root)
    {
        if (root == null || root.Right == null) return root;

        BSTNode newRoot = root.Right;
        root.Right = newRoot.Left;
        newRoot.Left = root;
        return newRoot;
    }

    public BSTNode RotateRight(BSTNode root)
    {
        if (root == null || root.Left == null) return root;

        BSTNode newRoot = root.Left;
        root.Left = newRoot.Right;
        newRoot.Right = root;
        return newRoot;
    }

    public void BalanceDSW()
    {
        BSTNode pseudoRoot = new BSTNode(null) { Right = Root };
        int size = TreeToVine(pseudoRoot);
        int m = (int)Math.Pow(2, Math.Floor(Math.Log(size + 1) / Math.Log(2))) - 1;

        Compress(pseudoRoot, size - m);
        while (m > 1)
        {
            m /= 2;
            Compress(pseudoRoot, m);
        }

        Root = pseudoRoot.Right;
    }

    private int TreeToVine(BSTNode pseudoRoot)
    {
        int size = 0;
        BSTNode tail = pseudoRoot;
        BSTNode rest = tail.Right;

        while (rest != null)
        {
            if (rest.Left == null)
            {
                tail = rest;
                rest = rest.Right;
                size++;
            }
            else
            {
                BSTNode temp = rest.Left;
                rest.Left = temp.Right;
                temp.Right = rest;
                rest = temp;
                tail.Right = temp;
            }
        }
        return size;
    }

    private void Compress(BSTNode pseudoRoot, int count)
    {
        BSTNode scanner = pseudoRoot;
        for (int i = 0; i < count; i++)
        {
            BSTNode child = scanner.Right;
            if (child == null) return;

            scanner.Right = RotateRight(child);
            scanner = scanner.Right;
        }
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;
        var students = new List<Student>
        {
            new Student { FirstName = "Ігор", LastName = "Іваненко", Course = 6, Gender = "ч", LivesInDorm = true },
            new Student { FirstName = "Анна", LastName = "Петренко", Course = 3, Gender = "ж", LivesInDorm = false },
            new Student { FirstName = "Софія", LastName = "Коваленко", Course = 2, Gender = "ж", LivesInDorm = true },
            new Student { FirstName = "Олександр", LastName = "Ткаченко", Course = 6, Gender = "ч", LivesInDorm = true },
            new Student { FirstName = "Артем", LastName = "Гриценко", Course = 6, Gender = "ч", LivesInDorm = true },
            new Student { FirstName = "Юлія", LastName = "Семененко", Course = 4, Gender = "ж", LivesInDorm = false },
            new Student { FirstName = "Денис", LastName = "Литвин", Course = 1, Gender = "ч", LivesInDorm = false },
            new Student { FirstName = "Олена", LastName = "Шевченко", Course = 5, Gender = "ж", LivesInDorm = true },
            new Student { FirstName = "Сергій", LastName = "Кравчук", Course = 6, Gender = "ч", LivesInDorm = false },
            new Student { FirstName = "Тетяна", LastName = "Захаренко", Course = 3, Gender = "ж", LivesInDorm = true },
            new Student { FirstName = "Владислав", LastName = "Мороз", Course = 2, Gender = "ч", LivesInDorm = true },
            new Student { FirstName = "Катерина", LastName = "Бондар", Course = 3, Gender = "ж", LivesInDorm = false },
            new Student { FirstName = "Максим", LastName = "Зінченко", Course = 1, Gender = "ч", LivesInDorm = false },
            new Student { FirstName = "Аліна", LastName = "Сидоренко", Course = 4, Gender = "ж", LivesInDorm = true },
            new Student { FirstName = "Олег", LastName = "Левченко", Course = 5, Gender = "ч", LivesInDorm = false },
            new Student { FirstName = "Наталія", LastName = "Кривенко", Course = 3, Gender = "ж", LivesInDorm = true },
            new Student { FirstName = "Дмитро", LastName = "Коваленко", Course = 6, Gender = "ч", LivesInDorm = false },
            new Student { FirstName = "Світлана", LastName = "Мельник", Course = 2, Gender = "ж", LivesInDorm = true },
            new Student { FirstName = "Роман", LastName = "Ткачук", Course = 4, Gender = "ч", LivesInDorm = true },
            new Student { FirstName = "Оксана", LastName = "Пилипенко", Course = 1, Gender = "ж", LivesInDorm = false },
            new Student { FirstName = "Володимир", LastName = "Романенко", Course = 5, Gender = "ч", LivesInDorm = true },
            new Student { FirstName = "Марія", LastName = "Горбач", Course = 2, Gender = "ж", LivesInDorm = false }
    };

        Console.WriteLine("== Студенти до змін ==");
        StudentArrayProcessor.PrintStudents(students);

        StudentArrayProcessor.RemoveDormitoryMaleSixthCourse(students); 

        Console.WriteLine("\n== Студенти після видалення чоловіків 6-го курсу, які живуть у гуртожитку ==");
        StudentArrayProcessor.PrintStudents(students);

        BST tree = new BST();
        foreach (var student in students)
        {
            tree.Insert(student);
            Console.WriteLine("\n== BST після додавання " + student.FirstName + " ==");
            tree.PrintLevelOrder();
        }


        Console.WriteLine("\n== BST (до балансування, обхід у ширину) ==");
        tree.PrintLevelOrder();

        tree.BalanceDSW();
        Console.WriteLine("\n== BST після балансування (амортизація / DSW) ==");
        tree.PrintLevelOrder();

        Console.Write("\nВведіть ім'я студента для пошуку: ");
        string searchName = Console.ReadLine();

        Console.WriteLine($"\n== Пошук студента з ім’ям '{searchName}' ==");
        var found = tree.Search(searchName);
        Console.WriteLine(found != null ? $"Знайдено: {found}" : "Не знайдено");

    }
}
