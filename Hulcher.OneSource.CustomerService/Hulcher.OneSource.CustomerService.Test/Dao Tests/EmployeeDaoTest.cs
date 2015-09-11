using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hulcher.OneSource.CustomerService.DataContext;
using Microsoft.Data.Schema.UnitTesting;
using Hulcher.OneSource.CustomerService.Data;
using System.Data.Common;
using System.Data;
namespace Hulcher.OneSource.CustomerService.Test.Dao_Tests
{
    [TestClass]
    public class EmployeeDaoTest
    {
        //private const long DIVISION_ID = 1;
        //private const int ROWS_RETURNED = 8;

        ///// <summary>
        ///// Prepare the database generating sample data
        ///// </summary>
        [TestInitialize]
        public void Initialize()
        {
            CustomerServiceModelContainer ctx = new CustomerServiceModelContainer();
            EmployeeDao.Singleton.ExecuteSql(ctx, "DELETE FROM CS_EMPLOYEE");
        }

        //[TestMethod]
        //public void TestListAllFilteredEmployeeMethodReturn()
        //{
        //    var employeeList = EmployeeDao.Singleton.ListAllFIltered(DIVISION_ID);
        //    Assert.AreEqual(ROWS_RETURNED, employeeList.Count());
        //}

        [TestMethod]
        public void TestListAllRVP()
        {
            CS_Employee firstEmployee = EmployeeDao.Singleton.Add(new CS_Employee
            {
                Name = "Ruziska",
                FirstName = "Danilo",
                CreatedBy = "Load",
                CreationDate = DateTime.Now,
                ModifiedBy = "Load",
                ModificationDate = DateTime.Now,
                Active = true,
                JobCode = "12345"
            });

            CS_Employee secondEmployee = EmployeeDao.Singleton.Add(new CS_Employee
            {
                Name = "Sandler",
                FirstName = "Adam",
                CreatedBy = "Load",
                CreationDate = DateTime.Now,
                ModifiedBy = "Load",
                ModificationDate = DateTime.Now,
                Active = true,
                JobCode = "OPSRVP"
            });

            CS_Employee thirdEmployee = EmployeeDao.Singleton.Add(new CS_Employee
            {
                Name = "Wayne",
                FirstName = "Bruce",
                CreatedBy = "Load",
                CreationDate = DateTime.Now,
                ModifiedBy = "Load",
                ModificationDate = DateTime.Now,
                Active = true,
                JobCode = "REGVPS"
            });

            IList<CS_Employee> listRVP = EmployeeDao.Singleton.ListAllRVP();

            Assert.AreEqual<int>(listRVP.Count, 2);

            
        }
    }
}