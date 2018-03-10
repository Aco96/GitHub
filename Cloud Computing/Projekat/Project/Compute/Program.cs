using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using ServiceContract;

namespace Compute
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> portovi = new List<int>() { 10010, 10020, 10030, 10040 };

            WorkerRole wr = new WorkerRole();
            for (int i = 1; i <= 4; i++)
            {
                wr.Start(i.ToString());
                Console.WriteLine($"Container {i} is opened.");
            }

            string path = @"D:\GitHub\Cloud Computing\Projekat\Project\Packet";
            while (!Directory.EnumerateFileSystemEntries(path).Any())
            {
                Console.WriteLine("empty");
                Thread.Sleep(5000);
            }

            Console.WriteLine("not empty");

            string[] s = Directory.GetFiles(path, "*.xml");

            for (int i = 0; i < s.Length; i++)
            {
                Console.WriteLine(s[i]);
            }

            writeXML();
            readXML();
            



            Console.ReadKey();
        }

        public static void writeXML()
        {
            XmlWriterSettings set = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t",
                NewLineOnAttributes = true
            };
            using (XmlWriter writer = XmlWriter.Create("File.xml", set))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("File");




                writer.WriteStartElement("File");
                writer.WriteElementString("Instanci", "2");
                writer.WriteElementString("Path", "///////");

                writer.WriteEndElement();

                Console.Clear();

                writer.WriteEndDocument();
            }
        }

        public static void readXML()
        {
            int instanci = -1;
            string path = "";
            using (XmlReader reader = XmlReader.Create("File.xml"))
            {
                
                
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case ("Instanci"):
                                instanci = int.Parse( reader.ReadElementContentAsString());
                                break;
                            case ("Path"):
                                path = (reader.ReadElementContentAsString());
                                break;

                            default:
                                break;
                        }
                    }
                }
            }

            Console.Write(instanci + "\n" +path);
        }
    }
}
