using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Base;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Extender;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Xml;
using Hulcher.OneSource.CustomerService.Business.WebControls.ServerControls;
using Hulcher.OneSource.CustomerService.Business.WebControls.Utils;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.DataContext.EntityExtensions;
using Hulcher.OneSource.CustomerService.Core.Utils;
using Hulcher.OneSource.CustomerService.DataContext.VO;

namespace Hulcher.OneSource.CustomerService.Business.ViewModel
{
    /// <summary>
    /// Call Entry ViewModel class
    /// </summary>
    public class CallEntryViewModel : IDisposable
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of the Call Entry View Interface
        /// </summary>
        private ICallEntryView _view;

        /// <summary>
        /// Instance of the Employee Model for Database Access
        /// </summary>
        private EmployeeModel _employeeModel;

        /// <summary>
        /// Instance of the Equipment Model for Database Access
        /// </summary>
        private EquipmentModel _equipmentModel;

        /// <summary>
        /// Instance of the CallCriteria Model for Database Access
        /// </summary>
        private CallCriteriaModel _callCriteriaModel;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="view">Instance of the CallEntry View Interface</param>
        public CallEntryViewModel(ICallEntryView view)
        {
            _view = view;

            _employeeModel = new EmployeeModel();
            _equipmentModel = new EquipmentModel();
            _callCriteriaModel = new CallCriteriaModel();
        }

        #endregion

        #region [ Methods ]

        #region [ Initial Advise ]

        public string FormatResourcesOrPersonsData()
        {
            StringBuilder formatBuilder = new StringBuilder();

            if (_view.SelectedResources.Count > 0)
            {
                formatBuilder.Append("Resources:");

                for (int i = 0; i < _view.SelectedResources.Count; i++)
                {
                    if (_view.SelectedResources[i].EmployeeID.HasValue)
                    {
                        CS_Employee emp = _employeeModel.GetEmployee(_view.SelectedResources[i].EmployeeID.Value);
                        formatBuilder.AppendFormat("<Text>{0}<BL>", emp.FullName);
                    }
                    else
                    {
                        CS_Equipment eq = _equipmentModel.GetEquipment(_view.SelectedResources[i].EquipmentID.Value);
                        formatBuilder.AppendFormat("<Text>{0}<BL>", eq.Name);
                    }
                }
            }
            else
            {
                if (_view.SelectedPersons.Count > 0)
                {
                    List<CS_CallLogResource> contactsList = _view.SelectedPersons.Where(e => e.CS_Contact != null).ToList();

                    if (contactsList.Count > 0)
                    {
                        formatBuilder.AppendFormat("Persons:<Text>{0}<BL>", contactsList[0].CS_Contact.FullName);

                        for (int i = 1; i < contactsList.Count; i++)
                        {
                            formatBuilder.AppendFormat(" <Text>{0}<BL>", contactsList[i].CS_Contact.FullName);
                        }
                    }

                    List<CS_CallLogResource> employeesList = _view.SelectedPersons.Where(e => e.CS_Employee != null).ToList();

                    if (employeesList.Count > 0)
                    {
                        if (formatBuilder.Length == 0)
                            formatBuilder.AppendFormat("Persons:<Text>{0}<BL>", employeesList[0].CS_Employee.FullName);
                        else
                            formatBuilder.AppendFormat(" <Text>{0}<BL>", employeesList[0].CS_Employee.FullName);

                        for (int i = 1; i < employeesList.Count; i++)
                        {
                            formatBuilder.AppendFormat(" <Text>{0}<BL>", employeesList[i].CS_Employee.FullName);
                        }
                    }
                }
            }

            return formatBuilder.ToString();
        }

        public void FillInitialAdvisePersonsGridFields()
        {
            string initialAdvise = string.Empty;

            _view.InitialAdviseGridDivisionNumber = _view.SelectedPersonInitialAdvise.Division;
            _view.InitialAdviseGridName = _view.SelectedPersonInitialAdvise.Name;
            _view.InitialAdviseGridContactInfo = _view.SelectedPersonInitialAdvise.ContactInfo;
            _view.InitialAdviseGridResourceId = _view.SelectedPersonInitialAdvise.ResourceID.ToString();
            _view.Notes = _view.SelectedPersonInitialAdvise.Notes == null ? "" : _view.SelectedPersonInitialAdvise.Notes;

            if (_view.SelectedPersonInitialAdvise.Type == (int)Globals.CallCriteria.EmailVOType.Employee)
            {
                _view.InitialAdviseGridResourceType = "Employee";
                //initialAdvise = _callCriteriaModel.GetInitalAdviseNote(true, _view.SelectedPersonInitialAdvise.ResourceID);
                IList<CS_CallCriteria> list = _callCriteriaModel.ListCallCriteriaByEmployee(_view.SelectedPersonInitialAdvise.ResourceID, _view.CallCriteriaIDs);

                for (int i = 0; i < list.Count; i++)
                    if (list.Count - 1 == i)
                        initialAdvise += list[i].Notes;
                    else
                        initialAdvise += list[i].Notes + "<BL>";
            }
            else
            {
                _view.InitialAdviseGridResourceType = "Contact";
                //initialAdvise = _callCriteriaModel.GetInitalAdviseNote(false, _view.SelectedPersonInitialAdvise.ResourceID);
                IList<CS_CallCriteria> list = _callCriteriaModel.ListCallCriteriaByContact(_view.SelectedPersonInitialAdvise.ResourceID, _view.CallCriteriaIDs);

                for (int i = 0; i < list.Count; i++)
                    if (list.Count - 1 == i)
                        initialAdvise += list[i].Notes;
                    else
                        initialAdvise += list[i].Notes + "<BL>";
            }

            _view.InitialAdviseGridInPerson = _view.SelectedPersonInitialAdvise.AdviseInPerson;
            _view.InitialAdviseGridVoicemail = _view.SelectedPersonInitialAdvise.AdviseByVoicemail;

            if (!string.IsNullOrEmpty(initialAdvise))
            {
                _view.InitialAdviseGridNote = StringManipulation.TabulateString(initialAdvise);
            }
        }

        #endregion

        #region [ Call Log ]

        public CS_CallLog_LocalEquipmentType ParseVOToCallLogEquipmentTypeEntity(int callLogID, LocalEquipmentTypeVO local, bool update)
        {
            CS_CallLog_LocalEquipmentType type = null;

            type = new CS_CallLog_LocalEquipmentType();
            type.Active = true;
            type.CallLogID = callLogID;
            type.CreatedBy = local.CreatedBy;
            type.CreationDate = local.CreationDate;
            type.CreationID = local.CreationID;
            type.LocalEquipmentTypeID = local.LocalEquipmentTypeID;
            type.Quantity = local.Quantity;
            type.CallLogID = callLogID;
            type.ModificationDate = DateTime.Now;
            type.ModificationID = null;
            type.ModifiedBy = "System";

            if (update)
                type.ID = local.ID;
            return type;
        }

        public LocalEquipmentTypeVO ParseCallLogEquipmentTypeEntityToVO(CS_CallLog_LocalEquipmentType callLogEqType)
        {
            LocalEquipmentTypeVO type = null;

            type = new LocalEquipmentTypeVO();
            type.CreatedBy = callLogEqType.CreatedBy;
            type.CreationDate = callLogEqType.CreationDate;
            type.CreationID = callLogEqType.CreationID;
            type.LocalEquipmentTypeID = callLogEqType.LocalEquipmentTypeID;
            type.Quantity = callLogEqType.Quantity;
            type.Name = callLogEqType.CS_LocalEquipmentType.Name;
            type.ID = callLogEqType.ID;

            return type;
        }

        #endregion

        #endregion

        #region [ IDisposable ]

        public void Dispose()
        {
            if (_callCriteriaModel != null)
                _callCriteriaModel.Dispose();

            if (_employeeModel != null)
                _employeeModel.Dispose();

            if (_equipmentModel != null)
                _equipmentModel.Dispose();

            _equipmentModel = null;
            _employeeModel = null;
            _callCriteriaModel = null;
        }

        #endregion
    }
}
