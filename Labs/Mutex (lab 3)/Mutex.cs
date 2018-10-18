using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Mutex
{
    public class Mutex : IMutex
    {
        private const int ID_FREE = -1;   // The resourse is free to use
        private int _id = ID_FREE;        // Contains id of the thread that is currently using the resource

        // Get the resource
        public void Lock()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;  // Unique id of the current thread

            // Wait until the resource become free
            while (Interlocked.CompareExchange(ref _id, threadId, ID_FREE) != ID_FREE) 
            {
                Thread.Yield(); // Start execute another thread
            }
        }

        // Thread can be unlocked only by the thread that locked it
        public bool Unlock()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;  // Unique id of the current thread
            return (Interlocked.CompareExchange(ref _id, ID_FREE, threadId) == threadId);
        }
    }
}
