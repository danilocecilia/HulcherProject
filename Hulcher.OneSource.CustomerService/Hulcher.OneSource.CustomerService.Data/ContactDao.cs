using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;
using System.Data.Objects;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace Hulcher.OneSource.CustomerService.Data    
{
    public class ContactDao : BaseDao<CS_Contact, int>, IContactDao
    {
        #region [ Attributes ]

        /// <summary>
        /// Static attribute to store the Compiled Query for getting a filtered list of Contacts
        /// </summary>
        private static Func<CustomerServiceModelContainer, long, bool, IQueryable<CS_Contact>> _listAllFilteredQuery;

        /// <summary>
        /// Static attribute to store the Compiled Query for getting a filtered list of Contacts
        /// </summary>
        private static Func<CustomerServiceModelContainer, long, bool, string, IQueryable<CS_Contact>> _listFilteredByNameQuery;

        /// <summary>
        /// Static attribute to store the Compiled Query for getting a filtered list of Contacts
        /// </summary>
        private static Func<CustomerServiceModelContainer, bool,string, IQueryable<CS_Contact>> _listAllFilteredByNameQuery;

        /// <summary>
        /// Static attribute to store the Compiled Query for getting a Contact by number
        /// </summary>
        private static Func<CustomerServiceModelContainer, string, IQueryable<CS_Contact>> _getByNumberQuery;

        /// <summary>
        /// Singleton attribute
        /// </summary>
        private static IContactDao _singleton;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        public ContactDao() { }

        #endregion

        #region [ Singleton ]

        /// <summary>
        /// Singleton Property - Contains an instance of this Class
        /// </summary>
        public static IContactDao Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new ContactDao();

                return _singleton;
            }
        }

        #endregion

        #region [ IContactDao Implementation ]

        /// <summary>
        /// Invoke Method for the Compiled Query
        /// <param name="customerId">Customer Identifier</param>
        /// <param name="dynamicsContact">Indicates if the method should load Dynamics contacts or CS contacts</param>
        /// <returns>List of Contacts</returns>
        /// </summary>
        public IList<CS_Contact> ListAllFiltered(long customerId, bool dynamicsContact)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                IList<CS_Contact> returnList = ListAllFilteredQuery.Invoke(db, customerId, dynamicsContact).ToList<CS_Contact>();
                return returnList;
            }
        }

        public CS_Contact GetByNumber(string contactNumber)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                CS_Contact contact = GetByNumberQuery.Invoke(db, contactNumber).First<CS_Contact>();
                return contact;
            }
        }

        /// <summary>
        /// Invoke Method for the Compiled Query
        /// <param name="customerId">Customer Identifier</param>
        /// <param name="dynamicsContact">Indicates if the method should load Dynamics contacts or CS contacts</param>
        /// <param name="name">Name of the Contact</param>
        /// <returns>List of Contacts</returns>
        public IList<CS_Contact> ListFilteredByName(long customerId, bool dynamicsContact,string name)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                IList<CS_Contact> returnList = ListFilteredByNameQuery.Invoke(db, customerId, dynamicsContact, name).ToList<CS_Contact>();
                return returnList;
            }
        }

        /// <summary>
        /// Invoke Method for the Compiled Query
        /// <param name="customerId">Customer Identifier</param>
        /// <param name="dynamicsContact">Indicates if the method should load Dynamics contacts or CS contacts</param>
        /// <param name="name">Name of the Contact</param>
        /// <returns>List of Contacts</returns>
        public IList<CS_Contact> ListAllFilteredByName(bool dynamicsContact, string name)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                IList<CS_Contact> returnList = ListAllFilteredByNameQuery.Invoke(db, dynamicsContact, name).ToList<CS_Contact>();
                return returnList;
            }
        }

        /// <summary>
        /// Property for a Compiled Query to List filtered itens of an Entity
        /// </summary>
        public Func<CustomerServiceModelContainer, long, bool, IQueryable<CS_Contact>> ListAllFilteredQuery
        {
            get
            {
                if (null == _listAllFilteredQuery)
                {
                    _listAllFilteredQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, long customerId, bool dynamicsContact) => from e in ctx.CS_Contact
                                                                                from c in ctx.CS_Customer_Contact
                                                                                where e.ID == c.ContactID
                                                                                && c.CustomerID == customerId
                                                                                && e.Active == true
                                                                                && c.Active == true
                                                                                && e.DynamicsContact == dynamicsContact
                                                                                select e);

                }
                return _listAllFilteredQuery;
            }
        }

        /// <summary>
        /// Property for a Compiled Query to get a Contact by number
        /// </summary>
        public Func<CustomerServiceModelContainer, string, IQueryable<CS_Contact>> GetByNumberQuery
        {
            get
            {
                if (null == _getByNumberQuery)
                {
                    _getByNumberQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, string contactNumber) => from e in ctx.CS_Contact
                                                                                     where e.ContactNumber == contactNumber
                                                                                     select e);
                }
                return _getByNumberQuery;
            }
        }

        /// <summary>
        /// Property for a Compiled Query to List filtered itens of an Entity by name
        /// </summary>
        public Func<CustomerServiceModelContainer, long, bool,string, IQueryable<CS_Contact>> ListFilteredByNameQuery
        {
            get
            {
                if (null == _listFilteredByNameQuery)
                {
                    _listFilteredByNameQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, long customerId, bool dynamicsContact,string name) => (from e in ctx.CS_Contact
                                                                                                      from c in ctx.CS_Customer_Contact
                                                                                                      where e.ID == c.ContactID
                                                                                                      && c.CustomerID == customerId
                                                                                                      && e.Active == true
                                                                                                      && c.Active == true
                                                                                                      && e.DynamicsContact == dynamicsContact
                                                                                                      && e.Name.StartsWith(name)
                                                                                                      select e).Distinct());

                }
                return _listFilteredByNameQuery;
            }
        }

        /// <summary>
        /// Property for a Compiled Query to List itens of an Entity by name
        /// </summary>
        public Func<CustomerServiceModelContainer, bool, string, IQueryable<CS_Contact>> ListAllFilteredByNameQuery
        {
            get
            {
                if (null == _listAllFilteredByNameQuery)
                {
                    _listAllFilteredByNameQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, bool dynamicsContact, string name) => (from e in ctx.CS_Contact
                                                                                                                   from c in ctx.CS_Customer_Contact
                                                                                                                   where e.ID == c.ContactID
                                                                                                                   && e.Active == true
                                                                                                                   && c.Active == true
                                                                                                                   && e.DynamicsContact == dynamicsContact
                                                                                                                   && e.Name.StartsWith(name)
                                                                                                                   select e).Distinct());

                }
                return _listAllFilteredByNameQuery;
            }
        } 

        #endregion
    }
}
