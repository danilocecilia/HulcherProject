using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Hulcher.OneSource.CustomerService.DataContext
{
    /// <summary>
    /// Temporary class to reproduce DPI Rate Table
    /// </summary>
    public static class DPIRateData
    {
        #region [ Private Attributtes ]

        private static DataTable dpiRateTable;        

        #endregion

        #region [ Private Methods ]

        private static void CreateDPIRateDataStructure()
        {
            dpiRateTable = new DataTable();
            dpiRateTable.Columns.Add("RateId", typeof(Int32));
            dpiRateTable.Columns.Add("EquipmentTypeId", typeof(Int32));
            dpiRateTable.Columns.Add("Value", typeof(Int32));
        }

        private static void InsertRateTableSampleData(params int[] equipmentTypeIds)
        {
            for (int i = 0; i < equipmentTypeIds.Count(); i++)
            {
                DataRow sampleRow = dpiRateTable.NewRow();
                //Auto Increment fake implementation
                sampleRow["RateId"] = i+1;
                sampleRow["EquipmentTypeId"] = equipmentTypeIds.GetValue(i);
                //Fake hard coded formula to calulate Value column(getting the id value and multiplying by 100)
                sampleRow["Value"] = (i + 1) * 100;
                dpiRateTable.Rows.Add(sampleRow);
            }                        
        }

        #endregion

        #region [ Public Methods ]

        /// <summary>
        /// Generates a DataTable in memory to reproduce Rate Table Data 
        /// </summary>
        /// <param name="equipmentTypeIds">Equipment Type Ids</param>
        /// <returns>DataTable with 3 columns: RateId, EquipmentTypeId and Value</returns>
        public static DataTable GetRateTableData(params int[] equipmentTypeIds)
        {
            CreateDPIRateDataStructure();
            InsertRateTableSampleData(equipmentTypeIds);
            return dpiRateTable;
        }

        #endregion
    }
}
