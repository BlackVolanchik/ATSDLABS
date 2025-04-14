using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atsdfull2
{
    class CustomStack
    {
        private string[] elements;
        private int top;
        private int maxSize;

        public CustomStack(int size)
        {
            maxSize = size;
            elements = new string[maxSize];
            top = -1;
        }

        public bool IsFull()
        {
            return top == maxSize - 1;
        }

        public bool IsEmpty()
        {
            return top == -1;
        }

        public bool Push(int number)
        {
            if (IsFull())
            {
                Console.WriteLine("Стек переповнений!");
                return false;
            }

            string octalNumber = Convert.ToString(number, 8);
            elements[++top] = octalNumber;
            return true;
        }

        public string Pop()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Стек порожній!");
            }
            return elements[top--];
        }

        public void Print()
        {
            if (IsEmpty())
            {
                Console.WriteLine("Стек порожній.");
                return;
            }

            Console.WriteLine("Вміст стеку:");
            for (int i = top; i >= 0; i--)
            {
                Console.WriteLine(elements[i]);
            }
        }
    }
}
