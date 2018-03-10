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
        public void Start(string containerId)
        {
            Process firstProc = new Process();
            switch (containerId)
            {
                case "1":
                    firstProc.StartInfo.Arguments = "10010";
                    break;
                case "2":
                    firstProc.StartInfo.Arguments = "10020";
                    break;
                case "3":
                    firstProc.StartInfo.Arguments = "10030";
                    break;
                case "4":
                    firstProc.StartInfo.Arguments = "10040";
                    break;
                default:
                    break;
            }
            
                
                firstProc.StartInfo.FileName = @"D:\GitHub\Cloud Computing\Projekat\Project\Container\bin\Debug\Container.exe";
                
                firstProc.EnableRaisingEvents = true;

                firstProc.Start();
            
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
