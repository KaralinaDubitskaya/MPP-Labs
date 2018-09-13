using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TaskQueue
{
    public class TaskQueue
    {
        // task queue
        private BlockingQueue<TaskDelegate> tasks;

        public TaskQueue(byte count)
        {
            tasks = new BlockingQueue<TaskDelegate>();

            // create and start *count* threads 
            for (int i = 0; i < count; i++)
            {
                var thread = new Thread(PerformTask);
                thread.Name = "Thread" + i;
                thread.IsBackground = true;
                thread.Start();
            }
        }

        // type of tasks processed by the threads
        public delegate void TaskDelegate();

        // enqueue task for execution 
        public void EnqueueTask(TaskDelegate task)
        {
            tasks.Enqueue(task);
        }

        // method to be invoked when a thread begins executing
        private void PerformTask()
        {
            while (true)
            {
                while (tasks.IsEmpty) { }

                // get another task
                TaskDelegate task = tasks.Dequeue();

                if (task == null) { continue; }

                try
                {
                    // execute the task
                    task();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}
