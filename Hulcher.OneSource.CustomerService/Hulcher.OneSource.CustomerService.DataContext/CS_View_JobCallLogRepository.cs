using System;
using System.Linq;
using System.Collections.Generic;
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_View_JobCallLogRepository
	{
		// Add your own data access methods.
		// This file should not get overridden
        
        public IList<CS_View_JobCallLog> GetJobCallLog(int? JobStatusID, int? CallTypeID, int? DivisionID, string ModifiedBy,
                                          bool? ShiftTransferLog, bool? GeneralLog, DateTime StartModificationDate,
                                          DateTime EndModificationDate, string PersonName)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                return db.CS_SP_GetJobCallLog(JobStatusID, CallTypeID, DivisionID, ModifiedBy, ShiftTransferLog, GeneralLog, StartModificationDate, EndModificationDate, PersonName).ToList();
            }
        }
	}
}