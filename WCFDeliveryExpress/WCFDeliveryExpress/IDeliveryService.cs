
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
        [XmlSerializerFormatAttribute(SupportFaults = true)]
        [ServiceKnownTypeAttribute(typeof(Fault))]
        [OperationContract]
        [FaultContract(typeof(Fault))]
        int DeleteOrder(int id);
        [OperationContract]
        [FaultContract(typeof(Fault))]
        int UpdateItem(Item i);
        [OperationContract]
        [FaultContract(typeof(Fault))]
        int CreateItem(Item i);
        [OperationContract]
        [FaultContract(typeof(Fault))]
        List<Item> GetItems();
        [OperationContract]
        [FaultContract(typeof(Fault))]
        Item GetItem(int id);
        [OperationContract]
        [FaultContract(typeof(Fault))]
        int DeleteItem(int id);
        [OperationContract]
        [FaultContract(typeof(Fault))]
        List<Item> GetItemsByName(string value);
        [OperationContract]
        [FaultContract(typeof(Fault))]
        bool Authenticate(string username, string password, int typeid);
        [OperationContract]
        [FaultContract(typeof(Fault))]
        List<UserType> getTypes();
        [OperationContract]
        [FaultContract(typeof(Fault))]
        List<Order> GetOrders();
        [OperationContract]
        [FaultContract(typeof(Fault))]
        int CreateOrder(Order order);
        [OperationContract]
        [FaultContract(typeof(Fault))]
        int CreateCustomer(Customer cu);
        [OperationContract]
        [FaultContract(typeof(Fault))]
        int UpdateCustomer(Customer cu);

    }
}
