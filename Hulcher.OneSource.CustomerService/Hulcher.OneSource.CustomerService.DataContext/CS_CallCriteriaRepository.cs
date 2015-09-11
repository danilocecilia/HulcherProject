using System;
using System.Linq;
using System.Collections.Generic;
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_CallCriteriaRepository
	{
		// Add your own data access methods.
		// This file should not get overridden

        public string GetInitialAdviseNote(bool isEmployee, int personID)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                return db.CS_SP_GetInitialAdviseNote(isEmployee, personID).FirstOrDefault();
            }
        }

        public IList<CS_SP_CheckCallCriteria_Result> CheckCallCriteria(int CallLogID, int callLogTypeId, int jobId)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                return db.CS_SP_CheckCallCriteria(CallLogID, callLogTypeId, jobId).ToList();
            }
        }
	}
}