using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicList
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new DynamicList<int>();
            const int listLength = 10;

            for (int i = 0; i < listLength; i++)
            {
                list.Add(i);
            }
            Console.WriteLine("Initial list:");
            foreach (int i in list)
            {
                Console.WriteLine(i);
            }

            for (int i = listLength - 1; i >= 0; i -= 2)
            {
                list.RemoveAt(i);
            }
            Console.WriteLine("\nRemove numbers with odd indexes:");
            foreach (int i in list)
            {
                Console.WriteLine(i);
            }

            foreach (int i in list)
            {
                if (i < 5)
                    list.Remove(i);
            }
            Console.WriteLine("\nRemove numbers less than 5:");
            foreach (int i in list)
            {
                Console.WriteLine(i);
            }

            list.Clear();
            Console.WriteLine("\nCleared list:");
            foreach (int i in list)
            {
                Console.WriteLine(i);
            }

            var strings = new DynamicList<String>();
            for (int i = 0; i < listLength; i++)
            {
                string str = "My value is " + i;
                strings.Add(str);
            }

            Console.WriteLine("\nList of strings:");
            foreach (string str in strings)
            {
                Console.WriteLine(str);
            }

            Console.ReadKey();
        }
    }
}
