using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TaskQueue
{
    class Program
    {
        public static void PrintInfo()
        {
            Console.WriteLine("Current thread name: " + Thread.CurrentThread.Name);
            Thread.Sleep(100);
        }

        static void Main(string[] args)
        {
            var taskQueue = new TaskQueue(5);

            for (int i = 0; i < 10; i++)
            {
                taskQueue.EnqueueTask(PrintInfo);
            }

            Console.ReadKey();
        }
    }
}
