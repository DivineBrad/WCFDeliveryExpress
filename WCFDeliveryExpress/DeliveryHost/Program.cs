using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryHost
{
    class Program
    {
        static void Main(string[] args)
        {

            using (ServiceHost host = new ServiceHost(typeof(WCFDeliveryExpress.DeliveryService)))
            {
                host.Open();
                Console.WriteLine("Host Started , Press Any Key To Stop");
                Console.ReadLine();
            }
        }
    }
}
