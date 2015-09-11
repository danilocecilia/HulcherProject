using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Data;

namespace Hulcher.OneSource.CustomerService.Test.Dao_Tests
{
    [TestClass]
    public class ContactDaoTest
    {
        private const long CUSTOMER_ID = 1;
        private const int ROWS_RETURNED = 5;

        /// <summary>   
        /// Prepare the database generating sample data
        /// </summary>
        //[TestInitialize]
        //public void Initialize()
        //{
        //    //DatabaseTestClass.TestService.DeployDatabaseProject();
        //    //DatabaseTestClass.TestService.GenerateData();
        //}

        //[TestMethod] TODO: Fix Contact table to sync with OneSource DB to enable this test
        //public void TestListAllFilteredContactMethodReturn()
        //{
        //    var contactList = ContactDao.Singleton.ListAllFIltered(CUSTOMER_ID);
        //    Assert.AreEqual(ROWS_RETURNED, contactList.Count());
        //}

        [TestMethod]
        public void TestAddContact()
        {
            var contact = ContactDao.Singleton.Add(new CS_Contact()
                                             {
                                                 CreationDate = DateTime.Now,
                                                 ModificationDate = DateTime.Now,
                                                 CreatedBy = "Dcecilia",
                                                 Active = true,
                                                 ModifiedBy = "Dcecilia"
                                             });

            var contactNumber = ContactDao.Singleton.Get(contact.ID);

            Assert.AreEqual(contactNumber.ID, contact.ID);
        }

        [TestMethod]
        public void TestUpdateContact()
        {
            var contactAdded = ContactDao.Singleton.Add(new CS_Contact()
            {
                CreationDate = DateTime.Now,
                ModificationDate = DateTime.Now,
                CreatedBy = "Dcecilia",
                Active = true,
                ModifiedBy = "Dcecilia"
            });


            var contact = ContactDao.Singleton.Update(new CS_Contact()
                                            {
                                                ID = contactAdded.ID,
                                                CreationDate = DateTime.Now,
                                                ModificationDate = DateTime.Now,
                                                CreatedBy = "DceciliaTest",
                                                Active = true,
                                                ModifiedBy = "Dcecilia"
                                            });
        
            Assert.AreNotEqual(contactAdded.CreatedBy, contact.CreatedBy);

        }
    }
}
