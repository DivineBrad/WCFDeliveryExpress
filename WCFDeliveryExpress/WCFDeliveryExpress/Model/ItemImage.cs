using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DeliveryExpress.Model
{
    public class ItemImage
    {
        public int ImgId { get; set; }
        public Byte[] Image { get; set; }
    }
}
