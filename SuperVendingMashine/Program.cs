using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

class Program
{
    public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


    static void Main()
    {
        XmlConfigurator.Configure();
        log.Info("Program start");

        VendingMachine machine = new VendingMachine(log);
      
        machine.Run();

        log.Info("Program end");
    }
}