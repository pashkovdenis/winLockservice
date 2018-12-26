using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WinGuardService
{
    static class Program
    {  
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new WinGuardService()
            };
            ServiceBase.Run(ServicesToRun); 
        } 

    }
}
