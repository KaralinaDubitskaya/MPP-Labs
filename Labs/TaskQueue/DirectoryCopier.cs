using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadPool
{
    public class DirectoryCopier
    {
        private TaskQueue _taskQueue;

        // DirectoryCopier is used to copy files from one directory to another
        // threadCount - count of threads used for copying
        public DirectoryCopier(byte threadCount)
        {
            // Create task queue
            _taskQueue = new TaskQueue(threadCount);
        }

        // Copy all files from sourcePath to targetPath overwriting files with the same name
        public uint Copy(string sourcePath, string targetPath)
        {
            // Check source path
            if (!Directory.Exists(sourcePath))
            {
                throw new DirectoryNotFoundException("Source directory couldn't be found.");
            }

            // Create target directory if it doesn't exist
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }

            uint copiedFiles = 0;

            try
            {
                // Return count of copied files
                copiedFiles = CopyFiles(sourcePath, targetPath);
            }
            catch
            {
                throw;
            }

            return copiedFiles; 
        }

        public void Dispose()
        {
            _taskQueue.Dispose();
        }

        private uint CopyFiles(string source, string destination)
        {
            // Count of copied files
            uint filesCopied = 0;

            // Get file's paths
            string[] files = Directory.GetFiles(source);

            string[] directories = Directory.GetDirectories(source);

            try
            {
                // Copy each file using taskQueue
                foreach (string file in files)
                {
                    _taskQueue.EnqueueTask(() => CopyFile(file, destination));  
                    filesCopied++;
                }

                foreach (string directory in directories)
                {
                    string targetPath = directory.Replace(source, destination);
                    filesCopied += Copy(directory, targetPath);
                }
            }
            catch 
            {
                // re-throw exception
                throw;
            }

            // Return count of copied files
            return filesCopied;
        }

        // Copy source file to the destination. Overwrites a file of the same name.
        private void CopyFile(string sourceFile, string targetPath)
        {
            // Get a destination file path
            string fileName = Path.GetFileName(sourceFile);
            string destFile = Path.Combine(targetPath, fileName);

            // Copy the file to destination path
            try
            {
                File.Copy(sourceFile, destFile, true);
            }
            catch (PathTooLongException ex)
            {
                throw new PathTooLongException($@"The specified path is too long. 
                               File {sourceFile} couldn't be copied.");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
