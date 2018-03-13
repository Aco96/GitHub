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
        public static IContainer[] proxy = new IContainer[4];
        //string[] portovi = new string[] { "10010", "10020", "10030", "10040" };
        public static Dictionary<int, bool> portovi = new Dictionary<int, bool>() { {1,false }, { 2, false }, { 3, false }, { 4, false } };
        public static string path = @"D:\GitHub\Cloud Computing\Projekat\MainFolder\FolderForChecking";
        public static int containersCnt = 4;
        static void Main(string[] args)
        {
            startContainers();

            Connect();



            List<string> XMLs = new List<string>();
            List<string> DLLs = new List<string>();
            int i = 0;
            while (true)
            {
                while (DLLs.Capacity < 1 || XMLs.Capacity < 1)
                {
                    DLLs = Directory.GetFiles(path, "*.dll").ToList<string>();
                    XMLs = Directory.GetFiles(path, "*.xml").ToList<string>();
                    Console.WriteLine("empty");
                    Thread.Sleep(2000);
                }

                Console.WriteLine("not empty");


                int instace = readXML(XMLs[0]);

                if (containersCnt >= instace)
                {
                    int[] pom = new int[4] { -1, -1, -1, -1 };
                    
                    foreach (KeyValuePair<int, bool> port in portovi)
                    {
                        if (port.Value == false && instace != 0)
                        {
                            Task.Factory.StartNew(() => proxy[port.Key - 1].Load(DLLs[0], port.Key));
                            //proxy[port.Key ].Load(DLLs[0], port.Key+1);
                            //portovi[port.Key] = true;
                            pom[i] = port.Key; // PROVERITI ZASTO i NE RADI KAKO TREBA LOGIKA!!!!
                            i++;
                            instace--;

                        }
                    }

                    for (int j = 0; i < 4; i++)
                    {
                        if(pom[j] != -1)
                        {
                            portovi[pom[j]] = true;
                        }
                    }
                }

                //proxy[0].Load(DLLs[0], 1);


                //for (int i = 0; i < DLLs.Length; i++)
                //{
                //    Console.WriteLine(DLLs[i]);
                //}





                //writeXML();
                //

                //copyFile("../../", "../", "File.xml");


                Console.ReadKey();
            }
            
        }

        public static void startContainers()
        {
            WorkerRole wr = new WorkerRole();
            for (int i = 1; i <= 4; i++)
            {
                wr.Start(i.ToString());
                Console.WriteLine($"Container {i} is opened.");
            }
        }

 
        public static void writeXML()
        {
            XmlWriterSettings set = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t",
                NewLineOnAttributes = true
            };
            using (XmlWriter writer = XmlWriter.Create("../../File.xml", set))
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

        public static int readXML(string path)
        {
            int instanci = -1;
            
            using (XmlReader reader = XmlReader.Create(path))
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

            return instanci;
        }

        public static void copyFile(string src, string dest, string file)
        {
            // Use Path class to manipulate file and directory paths.
            string sourceFile = System.IO.Path.Combine(src, file);
            string destFile = System.IO.Path.Combine(dest, file);

            if (System.IO.Directory.Exists(src) && System.IO.Directory.Exists(dest))
            {
                System.IO.File.Copy(sourceFile, destFile, true);
            }
            else
            {
                Console.WriteLine("source or destination path doesn't wxists.");
            }
        }
        public static void Connect()
        {
            ChannelFactory<IContainer> factory1 = new ChannelFactory<IContainer>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:10010/IContainer"));
            proxy[0] = factory1.CreateChannel();

            ChannelFactory<IContainer> factory2 = new ChannelFactory<IContainer>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:10020/IContainer"));
            proxy[1] = factory2.CreateChannel();

            ChannelFactory<IContainer> factory3 = new ChannelFactory<IContainer>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:10030/IContainer"));
            proxy[2] = factory3.CreateChannel();

            ChannelFactory<IContainer> factory4 = new ChannelFactory<IContainer>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:10040/IContainer"));
            proxy[3] = factory4.CreateChannel();
        }
    }
}
