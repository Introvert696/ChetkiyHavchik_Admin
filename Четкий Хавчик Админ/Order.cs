using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Четкий_Хавчик_Админ
{
    class Order
    {
        public int Order_id { get; set; }
        public string Order_FIO { get; set; }
        public string Order_Number { get; set; }
        public string Order_Email { get; set; }
        public string Order_Address { get; set; }
        public string Order_Purchases { get; set; }
        public int Order_Price { get; set; }
        public string Order_Comment { get; set; }
        public int Order_State { get; set; }
        public string Order_Date { get; set; }
    }
}
