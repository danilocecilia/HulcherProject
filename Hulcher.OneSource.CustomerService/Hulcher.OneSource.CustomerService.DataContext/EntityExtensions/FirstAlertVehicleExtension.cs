using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects.DataClasses;
using Hulcher.OneSource.CustomerService.DataContext.VO;

namespace Hulcher.OneSource.CustomerService.DataContext
{
    public partial class CS_FirstAlertVehicle : EntityObject
    {
        public string Description
        {
            get
            {
                return string.Format("{0} {1} {2}", Make, Model, Year);
            }
        }

        public int TemporaryID
        {
            get;
            set;
        }

        public string UnitNumber
        {
            get;
            set;
        }
    }
}
