using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulcher.OneSource.CustomerService.DataContext
{
    public class ShoppingCartVO
    {
        public int Type { get; set; }

        public int ID { get; set; }

        public string Name { get; set; }

        public string AssignmentType { get; set; }

        public int DivisionID { get; set; }

        public string UnitNumber { get; set; }
    }
}
