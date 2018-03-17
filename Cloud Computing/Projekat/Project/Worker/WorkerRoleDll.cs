using ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Worker
{
    class WorkerRoleDll : IWorkerRole
    {
        public void Start(string containerId)
        {
            throw new NotImplementedException();
        }

        public void Stop(string containerId)
        {
            throw new NotImplementedException();
        }
        public void Begin()
        {
            while (true)
            {
                Console.WriteLine("Working...");
                Thread.Sleep(1000);
            }
        }
    }
}
