using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;

namespace Hulcher.OneSource.CustomerService.DataContext
{
    public partial class CS_JobInfo : EntityObject
    {
        public int LastJobStatusID
        {
            get
            {
                if (this.CS_Job_JobStatus != null && this.CS_Job_JobStatus.Count > 0)
                {
                    CS_Job_JobStatus status = this.CS_Job_JobStatus.Where(e => e.Active).FirstOrDefault();
                    return status.JobStatusId;
                }
                else
                    return 0;
            }
        }

        public CS_Job_JobStatus LastJobStatusEntity
        {
            get
            {
                if (this.CS_Job_JobStatus != null && this.CS_Job_JobStatus.Count > 0)
                {
                    CS_Job_JobStatus status = this.CS_Job_JobStatus.Where(e => e.Active).FirstOrDefault();
                    return status;
                }
                else
                    return null;
            }
        }

        public CS_JobStatus LastJobStatus
        {
            get
            {
                if (this.CS_Job_JobStatus != null && this.CS_Job_JobStatus.Count > 0)
                {
                    CS_Job_JobStatus status = this.CS_Job_JobStatus.Where(e => e.Active).FirstOrDefault();
                    if (null != status)
                        return status.CS_JobStatus;
                    else
                        return null;
                }
                else
                    return null;
            }
            set
            {
                this.CS_Job_JobStatus.Where(e => e.Active).FirstOrDefault().CS_JobStatus = value;
            }
        }
    }
}
