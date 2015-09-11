using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;

namespace Hulcher.OneSource.CustomerService.Data
{
    public class ReserveEquipmentDao : IReserveEquipmentDao
    {
        #region [ Attributes ]

        /// <summary>
        /// Static attribute to store the Compiled Query for listing all Equipments for Reserve
        /// </summary>
        private static Func<CustomerServiceModelContainer, IQueryable<CS_View_ReserveEquipment>> _listAllQuery;

        /// <summary>
        /// Static attribute to store the Compiled Query for listing filtered Equipments for Reserve
        /// </summary>
        private static Func<CustomerServiceModelContainer, int?, int?, int?, IQueryable<CS_View_ReserveEquipment>> _listFilteredQuery;

        /// <summary>
        /// Singleton attribute
        /// </summary>
        private static IReserveEquipmentDao _singleton;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        public ReserveEquipmentDao() { }

        #endregion

        #region [ Singleton ]

        /// <summary>
        /// Singleton Property - Contains an instance of this Class
        /// </summary>
        public static IReserveEquipmentDao Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new ReserveEquipmentDao();

                return _singleton;
            }
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Property for a Compiled Query to List All itens of an Entity
        /// </summary>
        public Func<CustomerServiceModelContainer, IQueryable<CS_View_ReserveEquipment>> ListAllQuery
        {
            get
            {
                if (null == _listAllQuery)
                {
                    _listAllQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx) => from e in ctx.CS_View_ReserveEquipment
                                                               select e);

                }
                return _listAllQuery;
            }
        }

        /// <summary>
        /// Property for a Compiled Query to List Filtered itens of an Entity
        /// </summary>
        public Func<CustomerServiceModelContainer, int?, int?, int?, IQueryable<CS_View_ReserveEquipment>> ListFilteredQuery
        {
            get
            {
                if (null == _listFilteredQuery)
                {
                    _listFilteredQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, int? equipmentTypeId, int? stateId, int? divisionId) => 
                            from e in ctx.CS_View_ReserveEquipment
                            where (equipmentTypeId.HasValue == false || e.EquipmentTypeID == equipmentTypeId.Value)
                            && (stateId.HasValue == false || e.StateID == stateId.Value)
                            && (divisionId.HasValue == false || e.DivisionID == divisionId.Value)
                            select e);

                }
                return _listFilteredQuery;
            }
        }

        #endregion

        #region [ IReserveEquipmentDao Implementation ]

        /// <summary>
        /// Returns a list of all equipment types available for reserve
        /// </summary>
        /// <returns>List of Equipments</returns>
        public IList<CS_View_ReserveEquipment> ListAll()
        {
            using (var db = new CustomerServiceModelContainer())
            {
                return ListAllQuery.Invoke(db).ToList<CS_View_ReserveEquipment>();
            }
        }

        /// <summary>
        /// List filterd items for the Reserve Equipment View
        /// </summary>
        /// <param name="equipmentTypeId">Equipment Type filter</param>
        /// <param name="stateId">State filter</param>
        /// <param name="divisionId">Division filter</param>
        /// <returns>List of Equipments</returns>
        public IList<CS_View_ReserveEquipment> ListAllFiltered(int? equipmentTypeId, int? stateId, int? divisionId)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                return ListFilteredQuery.Invoke(db, equipmentTypeId, stateId, divisionId).ToList<CS_View_ReserveEquipment>();
            }
        }

        #endregion
    }
}
