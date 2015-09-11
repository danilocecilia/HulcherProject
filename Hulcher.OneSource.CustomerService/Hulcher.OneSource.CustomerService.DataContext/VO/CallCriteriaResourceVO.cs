using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulcher.OneSource.CustomerService.DataContext.VO
{
    [Serializable]
    public class CallCriteriaResourceVO
    {
        public int ResourceID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Division
        {
            get;
            set;
        }

        public string Customer
        {
            get;
            set;
        }

        public string InitialAdviseInformation
        {
            get;
            set;
        }

        public string ContactInfo
        {
            get;
            set;
        }

        public bool AdviseInPerson
        {
            get;
            set;
        }

        public bool AdviseByVoicemail
        {
            get;
            set;
        }

        public string Notes { get; set; }

        public int Type { get; set; }
    }
}
