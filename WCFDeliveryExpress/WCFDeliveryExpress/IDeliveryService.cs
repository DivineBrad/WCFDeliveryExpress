
using DeliveryExpress.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFDeliveryExpress
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IDeliveryService
    {
        [FaultContract(typeof(Fault))]
        [OperationContract]
        int DeleteOrder(int id);
        [OperationContract]
        int UpdateItem(Item i);
        [OperationContract]
        int CreateItem(Item i);
        [OperationContract]
        List<Item> GetItems();
        [OperationContract]
        Item GetItem(int id);
        [OperationContract]
        int DeleteItem(int id);
    }
}
