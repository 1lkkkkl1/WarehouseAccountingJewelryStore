using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WarehouseAccountingJewelryStoreService;

namespace Host
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var authHost = new ServiceHost(typeof(AuthService)))
            using (var host = new ServiceHost(typeof(Service)))
            {
                try
                {
                    authHost.Open();
                    Console.WriteLine("Auth service started...");
                    host.Open();
                    Console.WriteLine("Service started...");
                    Console.ReadKey();
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR:\n" + e.Message);
                    Console.ReadKey();
                    return;
                }
            }
        }
    }
}
