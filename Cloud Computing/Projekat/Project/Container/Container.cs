using ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Container
{
    public class Container : IContainer
    {

        public String Load(string dll,int i)
        {
            
            //string src = @"D:\GitHub\Cloud Computing\Projekat\MainFolder\FolderForChecking";
            string dest = $@"D:\GitHub\Cloud Computing\Projekat\MainFolder\Port100{i.ToString()}0Folder";

            // Use Path class to manipulate file and directory paths.
            string sourceFile = dll;
            string[] part = dll.Split('\\');
            string destFile = System.IO.Path.Combine(dest, part[part.Length-1]);

            if (System.IO.Directory.Exists(dest))
            {
                System.IO.File.Copy(sourceFile, destFile, true);
            }
            else
            {
                Console.WriteLine("source or destination path doesn't exists.");
            }

            var DLL = Assembly.LoadFile(destFile);



            dynamic c = Activator.CreateInstance(DLL.GetExportedTypes()[1]);
            c.begin();

            return "success";
        }

      
    }
}
