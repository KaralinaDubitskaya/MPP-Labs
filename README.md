# Modern programming platforms (.NET platform)
BSUIR 2018

#### Task 1. TaskQueue
Create a class in C# which:
- is called TaskQueue and implements the logic of the thread pool;
- creates the specified number of pool threads in the constructor;
- contains the queue of tasks in the form of delegates without parameters:
*delegate void TaskDelegate ();*
- provides the method
*void EnqueueTask (TaskDelegate task);*

#### Task 2. DirectoryCopier
Implement a console program in C# which:
- takes as the command line parameters path to the source and target directories on the disk;
- performs parallel copying of all files from the source directory to the target directory;
- performs copy operations in parallel using the TaskQueue from Task #1;
- waits until all copy operations are completed and displays information about the number of copied files to the console.

#### Task 3. Mutex
Create a class in C# which:
- is called Mutex and implements the binary semaphore using the atomic operation *Interlocked.CompareExchange*;
- provides blocking and unblocking of a binary semaphore using public methods *Lock* and *Unlock*.

#### Task 4. AssemblyLoader
Implement a console program in C# which:
- takes as the command line parameter the path to the .NET assembly (EXE or DLL file);
- loads the specified assembly into memory;
- displays the full names of all public data types of this assembly, ordered by namespace and by name.

#### Task 5. Parallel.WaitAll
Create in C# a static method of the *Parallel.WaitAll* class which:
- takes as the parameter the array of delegates;
- executes all specified delegates in parallel using the *TaskQueue* class from Task #1;
- waits for the completion of all delegates.
Implement the simplest example of using the *Parallel.WaitAll* method.

#### Task 6. Solve the problem.
Create in C # a generic class *DynamicList<T>* which:
- implements a dynamic array using T[] array;
- has the *Count* property, which contains the number of elements;
- implements indexer;
- has the methods Add, Remove, RemoveAt, Clear to add, delete, delete by index and delete all elements, respectively;
- implements the *IEnumerable<T>* interface.
Implement the simplest example of using the *DynamicList<T>* class in C #.

#### 💬 Contact
Karalina Dubitskaya                           
dubitskaya.karalina@gmail.com
