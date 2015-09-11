using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulcher.OneSource.CustomerService.DataContext.VO
{
    public class LocalEquipmentTypeVO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int LocalEquipmentTypeID { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public int? CreationID { get; set; }
    }
}
