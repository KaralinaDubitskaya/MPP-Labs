using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyLoader
{
    public class AssemblyInfo
    {
        private Assembly _assembly;

        public void LoadAssembly(string path)
        {
            string fileExtention = Path.GetExtension(path);

            try
            {
                _assembly = Assembly.LoadFrom(path);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("Please, enter full path to the assembly.");
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"Unable to find the file {path}.");
            }
            catch (BadImageFormatException)
            {
                throw new BadImageFormatException($"The file {path} is invalid assembly.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Return array that contains info about public types of the assembly
        // sorted by the namespace and by the name
        public IEnumerable<Type> GetPublicTypes()
        {
            return _assembly.GetTypes()
                .Where(type => type.IsPublic)
                .OrderBy(type => type.Namespace + type.Name);
        }
    }
}
