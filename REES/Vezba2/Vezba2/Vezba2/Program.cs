using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Vezba2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines;
            try
            {
                lines = File.ReadAllLines(@"..\..\text.txt");
                func(lines);
            }
            catch
            {
                Console.WriteLine("text.txt doesn't exists.");
            }
           
        }

        public static void func(string[] lines)
        {
            int brojac = 1, i = 0;
            char[] niz = new char[100];
            while (brojac < 2)
            {
                lines[i] = Regex.Replace(lines[i], @"\s+", " ");
                niz = lines[i].ToCharArray();
                if (lines[i].Length > 30)
                {
                    if (niz[30] == ' ')
                    {
                        lines[i] = string.Join("", niz.Skip(0).Take(30).ToArray()) + "\n" + string.Join("", niz.Skip(31).Take(niz.Length - 31).ToArray());                   // odseca se visak teksta (preko 30 reci u redu)
                    }
                    else
                    {
                        lines[i] = string.Join("", niz.Skip(0).Take(30).ToArray()) + "-\n" + string.Join("", niz.Skip(30).Take(niz.Length - 31).ToArray());
                    }
                }

                Console.WriteLine(lines[i]);
                if (String.IsNullOrEmpty(lines[++i]) && String.IsNullOrEmpty(lines[i+1])) { brojac++; }
            }
        }
    }
}
