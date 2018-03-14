using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContract;

namespace Compute
{
    class WorkerRole : IWorkerRole
    {
        public static Process[] processes = new Process[4];
        public void Start(string containerId)
        {
            processes[int.Parse(containerId) - 1] = new Process();
            processes[int.Parse(containerId)-1].StartInfo.Arguments = $"100{containerId}0";


            processes[int.Parse(containerId) - 1].StartInfo.FileName = @"D:\GitHub\Cloud Computing\Projekat\Project\Container\bin\Debug\Container.exe";

            processes[int.Parse(containerId) - 1].EnableRaisingEvents = true;



            processes[int.Parse(containerId) - 1].Start();
            
        }

        public void Stop(string containerId)
        {
            try {
                processes[int.Parse(containerId) - 1].Kill() ;
                WorkerRole wr = new WorkerRole();
                wr.Start(containerId);
            }
            catch(Exception e) { }
        }
    }
}
