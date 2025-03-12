using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atsdfull2
{
    class Node
    {
        public int Data;
        public Node Next;

        public Node(int data)
        {
            Data = data;
            Next = null;
        }
    }

    class LinkedList
    {
        public Node head;

        public LinkedList()
        {
            head = null;
        }

        public bool IsEmpty()
        {
            return head == null;
        }

        public void AddToHead(int number)
        {
            Node newNode = new Node(number);
            newNode.Next = head;
            head = newNode;
        }

        public int Remove(int index)
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Список порожній!");
            }

            if (index == 0)
            {
                int removedData = head.Data;
                head = head.Next;
                return removedData;
            }

            Node current = head;
            Node prev = null;
            int count = 0;

            while (current != null && count < index)
            {
                prev = current;
                current = current.Next;
                count++;
            }

            prev.Next = current.Next;
            return current.Data;
        }

        public void Print()
        {
            Node current = head;
            while (current != null)
            {
                Console.Write(current.Data + " -> ");
                current = current.Next;
            }
            Console.WriteLine("null");
        }
    }
}
