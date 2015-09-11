
using System.Data.Objects.DataClasses;

namespace Hulcher.OneSource.CustomerService.DataContext
{
    public partial class CS_Region : EntityObject
    {
        public string RegionAndRVPName
        {
            get
            {
                if (this.RegionalVpID.HasValue)
                    return string.Format("{0} - {1}", this.Name, this.CS_Employee_Region.FullName);
                else
                    return this.Name;
            }
        }
    }
}
