using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadPool
{
    public class TaskQueue : IDisposable
    {
        private List<Thread> _threadPool;
        private BlockingQueue<TaskDelegate> _tasks;
        private int _activeThreads;


        public TaskQueue(byte count)
        {
            _threadPool = new List<Thread>();
            _tasks = new BlockingQueue<TaskDelegate>();

            // create and start *count* threads 
            for (int i = 0; i < count; i++)
            {
                var thread = new Thread(PerformTask);
                thread.Name = "Thread" + i;
                thread.IsBackground = true;
                _threadPool.Add(thread);
                thread.Start();
            }
        }

        // type of tasks processed by the threads
        public delegate void TaskDelegate();

        // enqueue task for execution 
        public void EnqueueTask(TaskDelegate task)
        {
            _tasks.Enqueue(task);
        }

        // method to be invoked when a thread begins executing
        private void PerformTask()
        {
            while (true)
            {
                while (_tasks.IsEmpty) { }

                // get another task
                TaskDelegate task = _tasks.Dequeue();

                if (task == null) { continue; }

                Interlocked.Increment(ref _activeThreads);
                try
                {
                    // execute the task
                    task();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                Interlocked.Decrement(ref _activeThreads);
            }
        }

        public void Dispose()
        {
            // Wait until tasks are complited
            while (_activeThreads != 0 || _tasks.Count != 0) { }

            // Abort all threads
            foreach (var thread in _threadPool)
            {
                thread.Abort();
            }
        }
    }
}
