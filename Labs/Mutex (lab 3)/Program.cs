using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mutex
{
    class Program
    {
        private const byte ARG_COUNT = 1;

        public static void Main(string[] args)
        {
            byte numOfThreads;
            Mutex mutex = new Mutex();

            if (args.Length != ARG_COUNT)
            {
                Console.WriteLine("Error: invalid input.");
                Console.WriteLine("Usage: Mutex.exe 'number_of_threads'");
                return;
            }

            if (!Byte.TryParse(args[0], out numOfThreads) && numOfThreads != 0)
            {
                Console.WriteLine("Error: invalid number of threads.");
                Console.WriteLine("Please, enter integer number from 1..255 range as a parameter.'");
                return;
            }

            for (int i = 0; i < numOfThreads; i++)
            {
                Thread thread = new Thread(() => GetMutex(mutex));
                thread.Start();
            }
        }

        private static void GetMutex(Mutex mutex)
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;

            Console.WriteLine($"Thread {threadId} is waiting for the mutex");
            mutex.Lock();
            Console.WriteLine($"Thread {threadId} has locked the mutex");
            Thread.Sleep(1000);
            Console.WriteLine($"Thread {threadId} will unlock the mutex");
            mutex.Unlock();
        }
    }
}
