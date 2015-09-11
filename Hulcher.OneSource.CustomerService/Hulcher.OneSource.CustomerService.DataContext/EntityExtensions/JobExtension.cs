using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;

namespace Hulcher.OneSource.CustomerService.DataContext
{
    public partial class CS_Job : EntityObject
    {
        public string NumberOrInternalTracking
        {
            get
            {
                // 06/23/11 - ACCORDING TO CYNTHIA, ONCE ACTIVE, KEEP JOB NUMBER
                // TODO: Find a way to use Globals in this one
                //if (this.CS_JobInfo.LastJobStatusID.Equals(1) ||
                //    this.CS_JobInfo.LastJobStatusID.Equals(8))
                //    return Number;
                //else
                //    return Internal_Tracking;
                if (string.IsNullOrEmpty(Number))
                    return Internal_Tracking;
                else
                    return Number;
            }
        }

        public string PrefixedJobNumber
        {
            get
            {
                StringBuilder prefixedJobNumberBuilder = new StringBuilder();
                if (
                    (this.CS_JobInfo != null && this.CS_JobInfo.CS_PriceType != null && this.CS_JobInfo.CS_JobType != null)
                    && (!string.IsNullOrEmpty(this.NumberOrInternalTracking))
                    )
                {
                    prefixedJobNumberBuilder.Append(this.CS_JobInfo.CS_PriceType.Acronym);
                    prefixedJobNumberBuilder.Append(this.CS_JobInfo.CS_JobType.Description);
                    prefixedJobNumberBuilder.Append(this.Number);
                }

                return prefixedJobNumberBuilder.ToString();
            }
        }

        public string PrefixedNumber
        {
            get
            {
                StringBuilder prefixedJobNumberBuilder = new StringBuilder();
                if (
                    (this.CS_JobInfo != null && this.CS_JobInfo.CS_PriceType != null && this.CS_JobInfo.CS_JobType != null) 
                    && (!string.IsNullOrEmpty(this.NumberOrInternalTracking))
                    )
                {
                    prefixedJobNumberBuilder.Append(this.CS_JobInfo.CS_PriceType.Acronym);
                    prefixedJobNumberBuilder.Append(this.CS_JobInfo.CS_JobType.Description);
                    prefixedJobNumberBuilder.Append(this.NumberOrInternalTracking);
                }
                else
                    prefixedJobNumberBuilder.Append(this.NumberOrInternalTracking);
                
                return prefixedJobNumberBuilder.ToString();
            }
        }
    }
}
