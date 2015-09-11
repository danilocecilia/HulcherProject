using System;
using System.Linq;
using System.Collections.Generic;
using Hulcher.OneSource.CustomerService.DataContext.VO;
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_JobRepository
	{
		// Add your own data access methods.
		// This file should not get overridden
        /// <summary>
        /// Executes the StoredProcedure to update Equipment information from Integration
        /// </summary>
        public List<int?> ListJobsBySearchCriteria(SearchCriteriaVO searchVO)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                return db.CS_SP_ListJobsBySearchCriteria(searchVO.customerInfoType, searchVO.jobInfoType, searchVO.locationInfoType, searchVO.jobDescriptionType, searchVO.equipmentType, searchVO.resourceType, searchVO.customerInfoValue, searchVO.jobInfoValue, searchVO.locationInfoValue, searchVO.jobDescriptionValue, searchVO.equipmentValue, searchVO.resourceValue, searchVO.startDate, searchVO.endDate).ToList();
            }
        }
	}
}