﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TaskQueue
{
    class Program
    {
<<<<<<< HEAD
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
=======
        private const int ARG_COUNT = 2;

        static void Main(string[] args)
        {
            if (args.Length != ARG_COUNT)
            {
                Console.WriteLine("Error: invalid input.");
                Console.WriteLine("Usage: TaskQueue.exe 'source_path' 'target_path'");
                return;
            }

            string sourcePath = args[0];
            string targetPath = args[1];

            var directoryCopier = new DirectoryCopier(5);

            try
            {
                uint copiedFiles = directoryCopier.Copy(sourcePath, targetPath);
                Console.WriteLine($"{copiedFiles} files were copied successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
>>>>>>> dev
        }
    }
}
