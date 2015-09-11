using System;
using System.Linq;
using System.Collections.Generic;
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_CallCriteriaValueRepository
	{
		// Add your own data access methods.
		// This file should not get overridden
        public IList<CS_SP_GetCallCriteriaValues_Result> ListCallCriteriaValueByID(int CallCriteriaID)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                return db.CS_SP_GetCallCriteriaValues(CallCriteriaID).ToList();
            }
        }

        // Add your own data access methods.
        // This file should not get overridden
        public IList<CS_SP_GetCallCriteriaValues_Result> ListCallCriteriaCallLogValueByID(int CallCriteriaID)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                return db.CS_SP_GetCallCriteriaCallLogValues(CallCriteriaID).ToList();
            }
        }
	}
}