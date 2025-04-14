using System;
using System.Collections.Generic;

class Student
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public int Course { get; set; }
    public uint ID { get; set; } // студентський квиток
    public DateTime BirthDate { get; set; }

    public void Print()
    {
        Console.WriteLine($"{LastName,-10} {FirstName,-10} {Course,-5} {ID,-10} {BirthDate:dd.MM.yyyy}");
    }
}

class TreeNode
{
    public Student Data;
    public TreeNode Left;
    public TreeNode Right;

    public TreeNode(Student data)
    {
        Data = data;
    }
}

class StudentTree
{
    private TreeNode root;

    public void Add(Student student)
    {
        root = AddRecursive(root, student);
    }

    private TreeNode AddRecursive(TreeNode node, Student student)
    {
        if (node == null) return new TreeNode(student);
        if (student.ID < node.Data.ID)
            node.Left = AddRecursive(node.Left, student);
        else if (student.ID > node.Data.ID)
            node.Right = AddRecursive(node.Right, student);
        return node;
    }

    public void PostOrderTraversal()
    {
        Console.WriteLine("\nПоточне дерево (Postorder):");
        Console.WriteLine("Прізвище   Ім’я       Курс  Квиток     Дата народження");
        PostOrderTraversal(root);
    }

    private void PostOrderTraversal(TreeNode node)
    {
        if (node != null)
        {
            PostOrderTraversal(node.Left);
            PostOrderTraversal(node.Right);
            node.Data.Print();
        }
    }

    public List<Student> FindWinterSecondYearStudents()
    {
        var result = new List<Student>();
        FindRecursive(root, result);
        return result;
    }

    private void FindRecursive(TreeNode node, List<Student> result)
    {
        if (node != null)
        {
            FindRecursive(node.Left, result);
            FindRecursive(node.Right, result);
            if (node.Data.Course == 2 &&
                (node.Data.BirthDate.Month == 12 || node.Data.BirthDate.Month == 1 || node.Data.BirthDate.Month == 2))
            {
                result.Add(node.Data);
            }
        }
    }

    public void DeleteWinterSecondYearStudents()
    {
        root = DeleteMatching(root);
    }

    private TreeNode DeleteMatching(TreeNode node)
    {
        if (node == null) return null;
        node.Left = DeleteMatching(node.Left);
        node.Right = DeleteMatching(node.Right);

        if (node.Data.Course == 2 &&
            (node.Data.BirthDate.Month == 12 || node.Data.BirthDate.Month == 1 || node.Data.BirthDate.Month == 2))
        {
            return DeleteNode(node);
        }
        return node;
    }

    private TreeNode DeleteNode(TreeNode node)
    {
        if (node.Left == null) return node.Right;
        if (node.Right == null) return node.Left;

        TreeNode min = FindMin(node.Right);
        node.Data = min.Data;
        node.Right = RemoveMin(node.Right);
        return node;
    }

    private TreeNode FindMin(TreeNode node)
    {
        while (node.Left != null) node = node.Left;
        return node;
    }

    private TreeNode RemoveMin(TreeNode node)
    {
        if (node.Left == null) return node.Right;
        node.Left = RemoveMin(node.Left);
        return node;
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        var tree = new StudentTree();

        tree.Add(new Student { LastName = "Іванов", FirstName = "Іван", Course = 1, ID = 1001, BirthDate = new DateTime(2005, 3, 10) });
        tree.Add(new Student { LastName = "Петренко", FirstName = "Олег", Course = 2, ID = 1005, BirthDate = new DateTime(2004, 12, 15) }); // зимовий
        tree.Add(new Student { LastName = "Сидорчук", FirstName = "Анна", Course = 2, ID = 1003, BirthDate = new DateTime(2005, 1, 20) });  // зимовий
        tree.Add(new Student { LastName = "Мельник", FirstName = "Оля", Course = 3, ID = 1002, BirthDate = new DateTime(2003, 7, 5) });
        tree.Add(new Student { LastName = "Ткаченко", FirstName = "Богдан", Course = 2, ID = 1004, BirthDate = new DateTime(2004, 5, 25) });
        tree.Add(new Student { LastName = "Захарченко", FirstName = "Марія", Course = 1, ID = 1007, BirthDate = new DateTime(2005, 11, 2) });
        tree.Add(new Student { LastName = "Коваленко", FirstName = "Сергій", Course = 2, ID = 1009, BirthDate = new DateTime(2004, 2, 28) }); // зимовий
        tree.Add(new Student { LastName = "Литвин", FirstName = "Юлія", Course = 3, ID = 1006, BirthDate = new DateTime(2003, 8, 17) });
        tree.Add(new Student { LastName = "Шевченко", FirstName = "Денис", Course = 2, ID = 1008, BirthDate = new DateTime(2004, 1, 5) });  // зимовий
        tree.Add(new Student { LastName = "Гриценко", FirstName = "Тетяна", Course = 1, ID = 1010, BirthDate = new DateTime(2005, 6, 30) });
        tree.Add(new Student { LastName = "Гриценко", FirstName = "Тетяна", Course = 1, ID = 1011, BirthDate = new DateTime(2005, 6, 30) });


        tree.PostOrderTraversal();

        var found = tree.FindWinterSecondYearStudents();
        Console.WriteLine("\nЗнайдені студенти 2-го курсу, народжені взимку:");
        if (found.Count == 0)
            Console.WriteLine("Нічого не знайдено.");
        else
        {
            Console.WriteLine("Прізвище   Ім’я       Курс  Квиток     Дата народження");
            foreach (var student in found)
                student.Print();
        }

        // Видалення
        tree.DeleteWinterSecondYearStudents();

        // Дерево після видалення
        tree.PostOrderTraversal();
    }
}
