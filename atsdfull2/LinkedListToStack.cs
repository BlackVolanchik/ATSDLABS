using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atsdfull2
{

    class LinkedListToStack
    {
        private LinkedList list = new LinkedList();

        public void Add(int data)
        {
            list.AddToHead(data);
        }

        public void RemoveNegative()
        {
            int index = 0;

            Node current = list.head;
            while (current != null)
            {
                Node next = current.Next;

                if (current.Data < 0)
                {
                    list.Remove(index);

                }
                else
                {
                    index++;
                }

                current = next;
            }
        }




        public Stack<string> ConvertToStack()
        {
            Stack<string> stack = new Stack<string>();
            Node current = list.head;

            while (current != null)
            {
                stack.Push(Convert.ToString(current.Data, 8));
                current = current.Next;
            }

            return stack;
        }

        public void Print()
        {
            list.Print();
        }
    }

}
