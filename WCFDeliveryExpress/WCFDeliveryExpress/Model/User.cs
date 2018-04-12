using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryExpress.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Pass { get; set; }
        public UserType Type { get; set; }
        public List<Log> Logs { get; set; }
    }
}
