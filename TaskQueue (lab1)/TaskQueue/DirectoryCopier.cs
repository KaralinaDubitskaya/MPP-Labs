using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskQueue
{
    public class DirectoryCopier
    {
        private string _sourcePath;
        private string _targetPath;

        private byte _threadCount;

        // DirectoryCopier is used to copy files from one directory to another
        // threadCount - count of threads used for copying
        public DirectoryCopier(byte threadCount)
        {
            _threadCount = threadCount;
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

            _sourcePath = sourcePath;
            _targetPath = targetPath;

            uint copiedFiles = 0;

            try
            {
                // Return count of copied files
                copiedFiles = CopyFiles();
            }
            catch
            {
                throw;
            }

            return copiedFiles; 
        }

        private uint CopyFiles()
        {
            // Count of copied files
            uint filesCopied = 0;

            // Get file's paths
            string[] files = Directory.GetFiles(_sourcePath);

            // Create task queue
            var taskQueue = new TaskQueue(_threadCount);

            try
            {
                // Copy each file using taskQueue
                foreach (string file in files)
                {
                    taskQueue.EnqueueTask(() => CopyFile(file));  
                    filesCopied++;
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
        private void CopyFile(string sourceFile)
        {
            // Get a destination file path
            string fileName = Path.GetFileName(sourceFile);
            string destFile = Path.Combine(_targetPath, fileName);

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
