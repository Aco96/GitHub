using ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Container
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost svc = new ServiceHost(typeof(Container));
            svc.AddServiceEndpoint(typeof(IContainer),
            new NetTcpBinding(),
            new Uri($"net.tcp://localhost:{args[0]}/IContainer"));

            

            svc.Open();
            Console.WriteLine("Service host is open on " + args[0]+" port.\n_______________________________________________");
            Console.ReadKey();
            svc.Close();
        }
    }
}
