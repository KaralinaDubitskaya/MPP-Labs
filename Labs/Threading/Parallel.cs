using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ThreadPool;
using static ThreadPool.TaskQueue;

namespace Threading
{
    public static class Parallel
    {
        public static void WaitAll(TaskDelegate[] tasks)
        {
            // Used for threads synchronization
            var countDownEvent = new CountdownEvent(tasks.Length);
            try
            {
                using (var taskQueue = new TaskQueue((byte) tasks.Length))
                {
                    foreach (var task in tasks)
                    {
                        taskQueue.EnqueueTask(() =>
                            {
                                // Complete the specified task
                                task();
                                // Signal that the task is completed
                                countDownEvent.Signal();
                            });
                    }
                    // Wait until the tasks will be completed
                    countDownEvent.Wait();
                }
            }
            catch (InvalidCastException ex)
            {
                throw new ArgumentException("Length of the array must be less than 256", ex);
            }
        }
    }
}
