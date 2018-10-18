using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyLoader
{
    class Program
    {
        private const byte ARG_COUNT = 1;

        public static void Main(string[] args)
        {
            if (args.Length != ARG_COUNT)
            {
                Console.WriteLine("Error: invalid input.");
                Console.WriteLine("Usage: AssemblyLoader.exe 'full_path_to_the_assembly'");
                return;
            }

            string path = args[0];
            
            var assembly = new AssemblyInfo();
            try
            {
                assembly.LoadAssembly(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            var types = assembly.GetPublicTypes();
            foreach (var type in types)
            {
                PrintTypeInfo(type);
            }

            Console.ReadKey();
        }

        private static void PrintTypeInfo(Type type)
        {
            Console.WriteLine(type.Namespace + '.' + type.Name);
        }
    }
}
