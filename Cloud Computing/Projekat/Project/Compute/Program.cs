using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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
        public static Dictionary<int, bool> portovi = new Dictionary<int, bool>() { {1,false }, { 2, false }, { 3, false }, { 4, false } };
        public static int containersCnt = 4;

        public static string path = @"D:\GitHub\Cloud Computing\Projekat\MainFolder\FolderForChecking";
        
        public static List<string> XMLs = new List<string>();
        public static List<string> DLLs = new List<string>();

        public static WorkerRole wr = new WorkerRole();

        static void Main(string[] args)
        {
            startContainers();

            Connect();

            meni();
            
            Console.WriteLine("End of Compute.");
            Console.ReadKey();
        }

        public static void closeContainer(int flag)
        {
            if (flag == 0)
            {
                Console.WriteLine("Enter number of container(1-4).");
                string s = Console.ReadLine();
                portovi[int.Parse(s)] = false;
                containersCnt++;
                wr.Stop(s);
            }
            else
            {
                for (int i = 1;i<= 4 ;i ++)
                {
                    wr.Stop(i.ToString());
                }
                
            }
        }

        public static void meni()
        {
            int i = 0;
            bool end = false;
            int[] pom = new int[4] { -1, -1, -1, -1 };
            while (!end)
            {
                Console.Clear();
                Console.WriteLine("Meni");
                Console.WriteLine("1. Check Packet.");
                Console.WriteLine("2. Close Container.");
                Console.WriteLine("3. Exit.");

                int answer = -1;

                try
                {
                    answer = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {

                }
                switch (answer)
                {
                    case 1:
                        checkPacket(i, pom);
                        break;
                    case 2:
                        closeContainer(0);
                        break;
                    case 3:
                        closeContainer(1);
                        end = true;
                        break;
                    default:
                        break;
                }

            }
        }

        public static void startContainers()
        {
            //WorkerRole wr = new WorkerRole();
            for (int i = 1; i <= 4; i++)
            {
                wr.Start(i.ToString());
                Console.WriteLine($"Container {i} is opened.");
            }
        }

        public static void checkPacket(int i,int[] pom)
        {
            while (DLLs.Count < 1 || XMLs.Count < 1)
            {
                DLLs = Directory.GetFiles(path, "*.dll").ToList<string>();
                XMLs = Directory.GetFiles(path, "*.xml").ToList<string>();
                Console.WriteLine("empty");
                Thread.Sleep(2000);
            }

            Console.WriteLine("not empty");
            //File.Delete(DLLs[0]);

            int instace = readXML(XMLs[0]);
            if (containersCnt == 0)
            {
                Console.WriteLine("There is no containers available.");
                File.Delete(DLLs[0]);
                
            }
            else if (containersCnt >= instace)
            {
                Assembly dll = Assembly.Load(File.ReadAllBytes(DLLs[0]));
                int flag = -1;

                foreach (var type in dll.GetTypes())
                {
                    var myInterfaceType = typeof(IWorkerRole);
                    if (type.Name == "IWorkerRole")
                    {
                        Console.WriteLine("{0} implements IWorkerRole", type);
                        flag = 1;
                        foreach (KeyValuePair<int, bool> port in portovi)
                        {
                            if (port.Value == false && instace != 0)
                            {
                                Task.Factory.StartNew(() => proxy[port.Key - 1].Load(DLLs[0], port.Key));
                                Console.WriteLine("sent");

                                pom[i] = port.Key;
                                i++;
                                instace--;
                                containersCnt--;
                            }
                        }
                        Console.ReadKey();

                        for (int j = 0; j < 4; j++)
                        {
                            if (pom[j] != -1)
                            {
                                portovi[pom[j]] = true;
                            }
                        }
                    }

                }
                if(flag == -1)
                {
                    Console.WriteLine("Founded file doesn't implement IWorkerRole inteface,please implement it and add your packet again.\nPress any key to get back to meni.");
                    dll = null;

                    Console.ReadKey();
                    File.Delete(DLLs[0]);
                    DLLs.Clear();
                    XMLs.Clear();
                }
            }
            else
            {
                Console.WriteLine("There is not enought Containers left for your aplication.");
                File.Delete(DLLs[0]);
                DLLs.Clear();
                XMLs.Clear();
            }

            DLLs.Clear();
            XMLs.Clear();
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

            //Console.Write(instanci + "\n" +path);

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
