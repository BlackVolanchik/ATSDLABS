using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atsdfull2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            CustomStack();

            CustomLinkedList();
            
            LinkedListAndStack();
        }

        static void CustomStack()
        {
            CustomStack stack = new CustomStack(5);

            stack.Push(10);
            stack.Push(20);
            stack.Push(30);
            stack.Push(40);
            stack.Push(50);


            stack.Print();

            Console.WriteLine("Видалені елементи: " + stack.Pop() + "," + stack.Pop());

            stack.Print();

            Console.WriteLine("\n");
        }

        static void CustomLinkedList()
        {
            LinkedList list = new LinkedList();

            list.AddToHead(10);
            list.AddToHead(20);
            list.AddToHead(30);
            list.AddToHead(40);
            list.AddToHead(50);
            list.Print();

            Console.WriteLine("Видалені елементи: " + list.Remove(0) + "," + list.Remove(0));

            list.Print();

            Console.WriteLine("\n");
        }

        static void LinkedListAndStack()
        {
            LinkedListToStack listToStack = new LinkedListToStack();


            listToStack.Add(10);
            listToStack.Add(-10);
            listToStack.Add(20);
            listToStack.Add(-20);
            listToStack.Add(30);
            listToStack.Add(-40);

            Console.WriteLine("Початковий список:");
            listToStack.Print();

            listToStack.RemoveNegative();
            Console.WriteLine("Список після видалення від'ємних елементів:");
            listToStack.Print();

            Stack<string> stack = listToStack.ConvertToStack();

            Console.WriteLine("Стек(до видалення):");
            foreach (string item in stack)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();


            Console.WriteLine("Стек:");
            while (stack.Count > 0)
            {
                Console.WriteLine(stack.Pop());
            }
        }
    }
}
