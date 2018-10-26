using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThreadPool;

namespace Threading
{
    class Program
    {
        private const byte ARG_COUNT = 1;

        static void Main(string[] args)
        {
            byte numOfTasks;

            if (args.Length != ARG_COUNT)
            {
                Console.WriteLine("Error: invalid input.");
                Console.WriteLine("Usage: Parallel.exe 'number_of_threads'");
                return;
            }

            if (!Byte.TryParse(args[0], out numOfTasks) && numOfTasks != 0)
            {
                Console.WriteLine("Error: invalid number of threads.");
                Console.WriteLine("Please, enter integer number from 1..255 range as a parameter.'");
                return;
            }
            
            var tasks = new TaskQueue.TaskDelegate[numOfTasks];
            for (int i = 0; i < numOfTasks; i++)
            {
                tasks[i] = Task;
            }

            Parallel.WaitAll(tasks);
            Console.WriteLine("All tasks are completed");
        }

        private static void Task()
        {
            const int timeToWait = 2000;
            int threadId = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"Thread #{threadId} started");
            Console.WriteLine($"Thread #{threadId} is waiting for {timeToWait} milliseconds");
            Thread.Sleep(timeToWait);
            Console.WriteLine($"Thread #{threadId} finished");
        } 
    }
}
