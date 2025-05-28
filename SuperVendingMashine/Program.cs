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
        //var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
        //XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        log.Info("Program started");
        VendingMachine machine = new VendingMachine(log);
        log.Info("Program started");
        machine.Run();
    }
}