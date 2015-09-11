using System;
using System.Linq;
using System.Collections.Generic;
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_View_JobSummaryRepository
	{
		// Add your own data access methods.
		// This file should not get overridden

        public IList<CS_SP_GetJobSummary_Result> GetJobSummary(int? JobStatusID, int? JobID, int? DivisionID, int? CustomerID,
                                          int? DateFilterTypeID, DateTime BeginDate, DateTime EndDate, string PersonName)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                return db.CS_SP_GetJobSummary(JobStatusID, JobID, DivisionID, CustomerID, DateFilterTypeID, BeginDate, EndDate, PersonName).ToList();
            }
        }
	}
}