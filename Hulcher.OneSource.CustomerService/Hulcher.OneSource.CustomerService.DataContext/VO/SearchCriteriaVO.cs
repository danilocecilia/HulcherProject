using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulcher.OneSource.CustomerService.DataContext.VO
{
    [Serializable]
    public class SearchCriteriaVO
    {
        public string customerInfoType { get; set; }
        public string jobInfoType { get; set; }
        public string locationInfoType { get; set; }
        public string jobDescriptionType { get; set; }
        public int equipmentType { get; set; }
        public string resourceType { get; set; }
        
        public string customerInfoValue { get; set; }
        public string jobInfoValue { get; set; }
        public string locationInfoValue { get; set; }
        public string jobDescriptionValue { get; set; }
        public string equipmentValue { get; set; }
        public string resourceValue { get; set; }

        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
}
