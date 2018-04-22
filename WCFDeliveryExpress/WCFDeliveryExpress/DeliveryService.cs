using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DeliveryExpress.Model;
using System.Data.SqlClient;
using DeliveryExpress.Config;

namespace WCFDeliveryExpress
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class DeliveryService : IDeliveryService
    {
        public int CreateItem(Item i)
        {
            SqlConnection con = DbConfig.connection;
            SqlCommand cmd = new SqlCommand
            ("insert into Item(name,description,price) values('" + i.Name + "','" + i.Desc + "'," + i.Price + ")", con);
            con.Open();
            int rows_aff = cmd.ExecuteNonQuery();
            if (rows_aff <= 0)
            {
                Fault fault = new Fault();
                fault.code = 101;
                fault.Error_detail = "Unable to insert item to database !!";
                fault.Error_reason = "Something wrong with the Query !!";
                throw new FaultException<Fault>(fault, new FaultReason("Something wrong with the Query !!"));
            }
            con.Close();
            return rows_aff;
        }

        public int DeleteItem(int id)
        {
            SqlConnection con = DbConfig.connection;
            SqlCommand cmd = new SqlCommand
            ("delete from Item where item_id=" + id, con);
            con.Open();
            int rows = cmd.ExecuteNonQuery();
            if (rows <= 0)
            {
                Fault fault = new Fault();
                fault.code = 102;
                fault.Error_detail = "Unable to delete item from database !!";
                fault.Error_reason = "Something wrong with the Query or Table is empty !!";
                throw new FaultException<Fault>(fault, new FaultReason("Table Empty!! or no table exists"));
            }
            con.Close();
            return rows;


        }

        public int DeleteOrder(int id)
        {
            SqlConnection con = DbConfig.connection;
            SqlCommand cmd = new SqlCommand
            ("delete from Order where order_id=" + id, con);
            con.Open();
            int rows = cmd.ExecuteNonQuery();
            if (rows <= 0)
            {
                Fault fault = new Fault();
                fault.code = 103;
                fault.Error_detail = "Unable to delete Order from database !!";
                fault.Error_reason = "Something wrong with the Query or Table is empty !!";
                throw new FaultException<Fault>(fault, new FaultReason("Something wrong with the Query or Table is empty !!"));
            }
            con.Close();
            return rows;
        }

        public Item GetItem(int id)
        {
            SqlConnection con = DbConfig.connection;
            SqlCommand cmd = new SqlCommand
            ("select * from Item where item_id=" + id, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            Item item = null;
            if (!reader.HasRows)
            {
                Fault fault = new Fault();
                fault.code = 104;
                fault.Error_detail = "Unable to get Item from database";
                fault.Error_reason = "Someting Wrong with the Query Or Table is empty !!";
                throw new FaultException<Fault>(fault, new FaultReason("Table Empty!! or no table exists"));
            }
            else
            {
                
                while (reader.Read())
                {
                    item = new Item();
                    item.ItemId = Convert.ToInt32(reader["item_id"]);
                    item.Name = reader["name"].ToString();
                    item.Desc = reader["description"].ToString();
                    item.Price = Convert.ToDouble(reader["price"]);
                    //item.Image.ImgId = Convert.ToInt32(reader["img_id"]);
                }
            }
            con.Close();
            return item;

        }

        public List<Item> GetItems()
        {
            List<Item> ItemList = null;
            SqlConnection con = DbConfig.connection;
            SqlCommand cmd = new SqlCommand
            ("select * from Item", con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            Item item = null;
            if (!reader.HasRows)
            {
                Fault fault = new Fault();
                fault.code = 105;
                fault.Error_detail = "Unable to get items from the database!!";
                fault.Error_reason = "Table Empty!! or no table exists";
                throw new FaultException<Fault>(fault, new FaultReason("Table Empty!! or no table exists"));
            }
            else
            {
                ItemList = new List<Item>();
                
                while (reader.Read())
                {
                    item = new Item();
                    item.ItemId = Convert.ToInt32(reader["item_id"]);
                    item.Name = reader["name"].ToString();
                    item.Desc = reader["description"].ToString();
                    item.Price = Convert.ToDouble(reader["price"]);
                    //item.Image.ImgId = Convert.ToInt32(reader["img_id"]);
                    ItemList.Add(item);
                }
            }
            con.Close();
            return ItemList;
        }

        public int UpdateItem(Item i)
        {
            SqlConnection con = DbConfig.connection;
            SqlCommand cmd = new SqlCommand
                ("UPDATE Item SET name = @name, description = @desc, price = @price Where item_id = @item_id", con);
            cmd.Parameters.AddWithValue("@name", i.Name);
            cmd.Parameters.AddWithValue("@desc", i.Desc);
            cmd.Parameters.AddWithValue("@price", i.Price);
            cmd.Parameters.AddWithValue("@item_id", i.ItemId);
            con.Open();
            int rows_aff = cmd.ExecuteNonQuery();
            if (rows_aff <= 0)
            {
                Fault fault = new Fault();
                fault.code = 106;
                fault.Error_detail = "Unable to update item inside database !!";
                fault.Error_reason = "Something wrong with the Query or either data / table doesnot exist !!";
                throw new FaultException<Fault>(fault,new FaultReason("Something wrong with the Query or either data / table doesnot exist !!"));
            }
            con.Close();
            return rows_aff;
        }
        public List<Item> GetItemsByName(string value)
        {
            List<Item> itemList = new List<Item>();
            SqlConnection con = DbConfig.connection;
            SqlCommand cmd = new SqlCommand
               ("select * from Item where name like'%" + value + "%'", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Item item = null;
                if (!reader.HasRows)
                {
                    Fault df = new Fault();
                    df.code = 404;
                    df.Error_detail = "SORRY!! NO DATA FOUND ";
                    df.Error_reason = "Data Not present";
                    throw new FaultException<Fault>(df, new FaultReason("Item not Present"));
            }
                else
                {
                    while (reader.Read())
                    {
                    item = new Item();
                    item.ItemId = Convert.ToInt32(reader["item_id"]);
                    item.Name = reader["name"].ToString();
                    item.Desc = reader["description"].ToString();
                    item.Price = Convert.ToDouble(reader["price"]);
                    //item.Image.ImgId = Convert.ToInt32(reader["img_id"]);
                    itemList.Add(item);
                }

                }
                con.Close();
                return itemList;
        }

        public bool Authenticate(string username, string password, int typeid)
        {
            bool status = false;
            SqlConnection con = DbConfig.connection;
            SqlCommand cmd = new SqlCommand
            ("select * from [dbo].[user] where name='" + username + "' and pass='" + password + "' and type_id="+ typeid, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            
            if (!reader.HasRows)
            {
                /* Fault fault = new Fault();
                 fault.code = 104;
                 fault.Error_detail = "DATA NOT PRESENT";
                 fault.Error_reason = "INVALID LOGIN DETAILS";
                 throw new FaultException<Fault>(fault, new FaultReason("INVALID LOGIN DETAILS"));*/
                status = false;
            }
            else
            {
                status = true;
            }
            return status;

        }
        public List<UserType> getTypes()
        {
            List<UserType> typeList = null;
            SqlConnection con = DbConfig.connection;
            SqlCommand cmd = new SqlCommand
          ("select * from UserType", con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            UserType type = null;
            if (!reader.HasRows)
            {
                Fault fault = new Fault();
                fault.code = 105;
                fault.Error_detail = "Unable to get User Types from the database!!";
                fault.Error_reason = "Table Empty!! or no table exists";
                throw new FaultException<Fault>(fault, new FaultReason("Table Empty!! or no table exists"));
            }
            else
            {
                typeList = new List<UserType>();

                while (reader.Read())
                {
                    type = new UserType();
                    type.TypeId = Convert.ToInt32(reader["type_id"]);
                    type.Type = reader["type"].ToString();
                    typeList.Add(type);
                }
            }
            con.Close();
            return typeList;

        }

        public List<Order> GetOrders()
        {
            List<Order> orders = null;
            List<OrderItem> oItems = null;
            Customer cus = null;
            Address addr = null;
            OrderStatus status = null;
            Order order = null;
            SqlConnection con = DbConfig.connection;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            // Get Order and related other table data
            cmd = new SqlCommand
            ("SELECT * "
             + " FROM  [dbo].[Order]  "
             + "INNER JOIN OrderStatus ON [order].status_id = OrderStatus.status_id "
             + "INNER JOIN customer ON [order].customer_id = customer.customer_id "
              + "INNER JOIN address ON customer.address_id = address.address_id "
              + "WHERE NOT description = 'completed' OR description = 'cancelled'", con);
            con.Open();
            reader = cmd.ExecuteReader();

            if (!reader.HasRows)
            {
                Fault fault = new Fault();
                fault.code = 105;
                fault.Error_detail = "Unable to get info from the database!!";
                fault.Error_reason = "Table Empty!! or no table exists";
                throw new FaultException<Fault>(fault);
            }
            else
            {
                orders = new List<Order>();
                while (reader.Read())
                {
                    // instatiate objects
                    cus = new Customer();
                    addr = new Address();
                    order = new Order();
                    status = new OrderStatus();
                    // assign values
                    //order
                    order.OrderId = Convert.ToInt32(reader["order_id"]);
                    order.OrderNo = reader["order_no"].ToString();
                    order.DateTime = Convert.ToDateTime(reader["date"]);
                    order.Total = Convert.ToDecimal(reader["total"]);
                    order.Subtotal = Convert.ToDecimal(reader["subtotal"]);
                    order.Tax = Convert.ToDecimal(reader["tax"]);
                    //status 
                    status.StatusId = Convert.ToInt32(reader["status_id"]);
                    status.Code = reader["code"].ToString();
                    status.Desc = reader["description"].ToString();
                    //address 
                    addr.AddressId = Convert.ToInt32(reader["address_id"]);
                    addr.Street = reader["street"].ToString();
                    addr.City = reader["city"].ToString();
                    addr.Region = reader["region"].ToString();
                    addr.Code = reader["addr_code"].ToString();
                    //customer 
                    cus.CustomerId = Convert.ToInt32(reader["customer_id"]);
                    cus.Name = reader["name"].ToString();
                    cus.Phone = reader["contact"].ToString();
                    //Combine Objects
                    cus.Address = addr;
                    order.Customer = cus;
                    order.Status = status;
                    orders.Add(order);
                }
            }

            con.Close();
            return orders;
        }
        //CreateOrder
        public int CreateOrder(Order order)
        {

            SqlConnection con = DbConfig.connection;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            // Get Order and related other table data
            cmd = new SqlCommand
            ("INSERT INTO [dbo].[Order] "
           + "(order_no, date , total, subtotal, tax, status_id, customer_id, user_id)"
           + " OUTPUT INSERTED.order_id  VALUES"
           + "('@orderNo' , '@date', '@total',"
           + " '@subtotal', '@tax', '@statusId', '@cusId', '@userId')", con);
            cmd.Parameters.AddWithValue("@orderNo", order.OrderNo);
            cmd.Parameters.AddWithValue("@date", order.DateTime);
            cmd.Parameters.AddWithValue("@total", order.Total);
            cmd.Parameters.AddWithValue("@subtotal", order.Subtotal);
            cmd.Parameters.AddWithValue("@statusId", order.Status.StatusId);
            cmd.Parameters.AddWithValue("@tax", order.Tax);
            cmd.Parameters.AddWithValue("@cusId", order.Customer.CustomerId);
            cmd.Parameters.AddWithValue("@userId", order.User.UserId);


            con.Open();
            var orderId = 0;
            orderId = (int)cmd.ExecuteScalar();

            if (orderId < 1)
            {
                Fault fault = new Fault();
                fault.code = 105;
                fault.Error_detail = "Unable to insert into the database!!";
                fault.Error_reason = "Problem with insertion query";
                throw new FaultException<Fault>(fault);
            }


            con.Close();
            return orderId;
        }

        // CreateCustomer 
        public int CreateCustomer(Customer cu)
        {
            SqlConnection con = DbConfig.connection;
            SqlCommand cmd = null;
            cmd = new SqlCommand ("INSERT INTO Address (street, city, region, code)"
            +" OUTPUT INSERTED.address_id  VALUES"
           +"('490 Lobby Road', 'Toronto', 'Ontario', 'L7J7k9')", con);
            con.Open();
            int add_id =(int) cmd.ExecuteScalar();
            if (add_id <= 0)
            {
                Fault fault = new Fault();
                fault.code = 101;
                fault.Error_detail = "Unable to insert address into database !!";
                fault.Error_reason = "Something wrong with the Query !!";
                throw new FaultException<Fault>(fault, new FaultReason("Something wrong with the Query !!"));
            }
            else
            {

               cmd = new SqlCommand("INSERT INTO Customer(name, contact, address_id)" +
                   " OUTPUT INSERTED.customer_id VALUES('John', '4168836879', '1')", con);
                int cus_id = (int)cmd.ExecuteScalar();
             
                if (cus_id <= 0)
                {
                    Fault fault = new Fault();
                    fault.code = 101;
                    fault.Error_detail = "Unable to insert customer record into database !!";
                    fault.Error_reason = "Something wrong with the Query !!";
                    throw new FaultException<Fault>(fault, new FaultReason("Something wrong with the Query !!"));
                }
                else
                {
                    con.Close();
                    return cus_id;
                }

            }
            



            
        }

        //UpdateCustomer 
        public int UpdateCustomer(Customer cu)
        {
            var custId = 0;
            return custId;
        }
    }




}
