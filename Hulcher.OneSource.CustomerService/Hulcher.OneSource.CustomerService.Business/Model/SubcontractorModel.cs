using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Business.Model
{
    public class SubcontractorModel : IDisposable
    {
        #region [ Attributes ]

        /// <summary>
        /// Unit of Work used to access the database/in-memory context
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// Repository class for CS_Subcontractor
        /// </summary>
        private IRepository<CS_Subcontractor> _subcontractorRepository;

        #endregion

        #region [ Constructor ]

        public SubcontractorModel()
        {
            _unitOfWork = new EFUnitOfWork();
            InstanceRepositories();
        }

        #endregion

        #region [ Methods ]

        private void InstanceRepositories()
        {
            _subcontractorRepository = new EFRepository<CS_Subcontractor>();
            _subcontractorRepository.UnitOfWork = _unitOfWork;
        }

        /// <summary>
        /// Returns a list of all Active Subcontractors
        /// </summary>
        /// <returns>Subcontractor list</returns>
        public IList<CS_Subcontractor> ListAllSubcontractors()
        {
            return _subcontractorRepository.ListAll(e => e.Active, order => order.Name, true);
        }

        public void Dispose()
        {
            _subcontractorRepository = null;
            _unitOfWork.Dispose();
            _unitOfWork = null;
        }

        public CS_Subcontractor SaveSubcontractor(CS_Subcontractor subcontractorToSave)
        {
            CS_Subcontractor savedSubcontractor = null;
            if (subcontractorToSave != null)
            {
                savedSubcontractor = _subcontractorRepository.Add(subcontractorToSave);
            }
            return savedSubcontractor;
        }

        public CS_Subcontractor UpdateSubcontractor(CS_Subcontractor subcontractorToUpdate)
        {
            CS_Subcontractor updatedSubcontractor = null;
            if (subcontractorToUpdate != null)
            {
                updatedSubcontractor = _subcontractorRepository.Update(subcontractorToUpdate);
            }
            return updatedSubcontractor;
        }

        public CS_Subcontractor GetSubcontractorById(int id)
        {
            return _subcontractorRepository.Get(e => e.Id == id && e.Active);
        }
        #endregion        
    }
}
