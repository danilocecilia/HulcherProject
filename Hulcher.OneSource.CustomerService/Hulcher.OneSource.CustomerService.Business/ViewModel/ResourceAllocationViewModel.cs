using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;

using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Hulcher.OneSource.CustomerService.Business.ViewModel
{
    public class ResourceAllocationViewModel
    {
        #region [ Attributes ]

        private IResourceAllocationView _view;

        private EquipmentModel _equipmentModel;
        private DivisionModel _divisionModel;
        private EmployeeModel _employeeModel;
        private ResourceAllocationModel _resourceAllocationModel;
        private LocationModel _locationModel;
        private JobModel _jobModel;
        private CallLogModel _callLogModel;

        private string _divisionNumber;
        private int _divisionId;
        private string _name;
        private Globals.ResourceAllocation.Type _type;

        #endregion

        #region [ Constructor ]

        public ResourceAllocationViewModel(IResourceAllocationView view)
        {
            _view = view;

            _equipmentModel = new EquipmentModel();
            _divisionModel = new DivisionModel();
            _employeeModel = new EmployeeModel();
            _resourceAllocationModel = new ResourceAllocationModel();
            _locationModel = new LocationModel();
            _jobModel = new JobModel();
            _callLogModel = new CallLogModel();

            _divisionNumber = string.Empty;
            _name = string.Empty;
            _type = Globals.ResourceAllocation.Type.AddEmployee;
        }

        #endregion

        #region [ Methods ]

        #region [ Common ]

        /// <summary>
        /// Updates the page Title Information
        /// </summary>
        public void UpdateTitle()
        {
            CS_Job job = _jobModel.GetJobById(_view.JobID);
            if (null != job)
                _view.PageTitle = string.Format("{0} - Resource Allocation", job.PrefixedNumber);

            _view.ReservedEquipmentsOnly = _view.ResourceConversion;
        }

        /// <summary>
        /// Verify current Job Status and enable/disable Add Resources block
        /// </summary>
        public void VerifyJobStatus()
        {
            CS_JobInfo jobInfo = _jobModel.GetJobInfo(_view.JobID);
            if (null != jobInfo)
            {
                if (jobInfo.LastJobStatusID.Equals((int)Globals.JobRecord.JobStatus.Active))
                    _view.DisplayAddResource = true;
                else
                    _view.DisplayAddResource = false;
            }
        }

        #endregion

        #region [ Resource Conversion ]

        /// <summary>
        /// Set the ReserveList Datasource to show each reserve on the Resource Conversion mode
        /// </summary>
        public void ListReservedList()
        {
            _view.ReserveListDatasource = _resourceAllocationModel.GetReserveListByJob(_view.JobID);
        }

        /// <summary>
        /// Clears the Reserves assigned to a job, after a successful save of a Job in a Conversion Process
        /// </summary>
        public void ClearReservedResources()
        {
            _resourceAllocationModel.ClearReservesByJobId(_view.JobID, _view.UserName);
        }

        /// <summary>
        /// Set the ReserveList Datasource to show each reserve on the Resource Conversion mode
        /// </summary>
        public void ListReservedResourceConversionList()
        {
            _view.ReserveListDatasource = _resourceAllocationModel.GetReserveListByJob(_view.JobID);
        }

        #endregion

        #region [ Add Resource - Equipment ]

        /// <summary>
        /// Loads the Equipment List by default based on Divisions related to the Job Record
        /// </summary>
        public void ListAllEquipmentAddByDivision()
        {
            //List<int> lstDivisionId = new List<int>();
            List<string> lstDivisionName = new List<string>();

            IList<CS_JobDivision> lstJobDivisions = _jobModel.ListAllDivisionsByJob(_view.JobID);
            if (lstJobDivisions.Count > 0)
            {

                lstDivisionName.AddRange(lstJobDivisions.Select(lstJobDivision => lstJobDivision.CS_Division.Name));

                _view.EquipmentFilterAdd = Globals.ResourceAllocation.EquipmentFilters.Division;
                _view.EquipmentFilterValueAdd = string.Join(",", lstDivisionName.ToArray());

                ListFilteredEquipmentGridAdd();
                //lstDivisionId.AddRange(lstJobDivisions.Select(lstJobDivision => lstJobDivision.DivisionID));
                //_view.EquipmentList = _equipmentModel.ListAllComboByDivisionList(lstDivisionId).OrderBy(w => w.DivisionName).ToList();

                BindEquipmentGridAdd();
            }
            else
                _view.EquipmentList = null;
        }

        /// <summary>
        /// Loads the Equipment List by default based on Reserves related to the Job Record
        /// </summary>
        public void ListAllEquipmentAddByReserves()
        {
            List<int> lstDivisionId = new List<int>();
            List<int> lstEquipmentTypeId = new List<int>();
            IList<CS_View_ReserveList> reserveList = _view.ReserveListDatasource;
            if (reserveList.Count > 0)
            {
                lstDivisionId.AddRange(reserveList.Where(e => e.Equipment == 1 && e.DivisionId.HasValue).Select(e => e.DivisionId.Value).Distinct());
                lstEquipmentTypeId.AddRange(reserveList.Where(e => e.Equipment == 1 && e.ResourceID.HasValue).Select(e => e.ResourceID.Value).Distinct());
                string[] divisionNames = reserveList.Select(e => e.DivisionName).Distinct().ToArray();

                _view.EquipmentList = _equipmentModel.ListAllComboByDivisionListAndEquipmentTypeList(lstDivisionId, lstEquipmentTypeId).OrderBy(w => w.DivisionName).ToList();
                _view.EquipmentFilterAdd = Globals.ResourceAllocation.EquipmentFilters.Division;
                _view.EquipmentFilterValueAdd = string.Join(", ", divisionNames);
                BindEquipmentGridAdd();
            }
            else
                _view.EquipmentList = null;
        }

        /// <summary>
        /// Loads the Equipment List based on the Filter applied
        /// </summary>
        public void ListFilteredEquipmentGridAdd()
        {
            _view.SortColumn = Globals.Common.Sort.EquipmentSortColumns.DivisionName;
            _view.SortDirection = Globals.Common.Sort.SortDirection.Ascending;

            if (_view.EquipmentFilterAdd.HasValue)
            {
                if (!_view.ResourceConversion || (_view.ResourceConversion && !_view.ReservedEquipmentsOnly))
                    _view.EquipmentList = _equipmentModel.ListFilteredEquipmentInfo(_view.EquipmentFilterAdd.Value, _view.EquipmentFilterValueAdd, "");
                else
                {
                    List<int> lstEquipmentTypeId = new List<int>();
                    IList<CS_View_ReserveList> reserveList = _view.ReserveListDatasource;
                    if (reserveList.Count > 0)
                    {
                        lstEquipmentTypeId.AddRange(reserveList.Where(e => e.Equipment == 1 && e.ResourceID.HasValue).Select(e => e.ResourceID.Value).Distinct());
                        _view.EquipmentList = _equipmentModel.ListFilteredEquipmentInfo(_view.EquipmentFilterAdd.Value, _view.EquipmentFilterValueAdd, lstEquipmentTypeId, "");
                    }
                    else
                        _view.EquipmentList = null;
                }

            }
            else
            {
                if (!_view.ResourceConversion || (_view.ResourceConversion && !_view.ReservedEquipmentsOnly))
                    _view.EquipmentList = _equipmentModel.ListAllHeavyCombo("");
                else
                {
                    List<int> lstEquipmentTypeId = new List<int>();
                    IList<CS_View_ReserveList> reserveList = _view.ReserveListDatasource;
                    if (reserveList.Count > 0)
                    {
                        lstEquipmentTypeId.AddRange(reserveList.Where(e => e.Equipment == 1 && e.ResourceID.HasValue).Select(e => e.ResourceID.Value).Distinct());
                        _view.EquipmentList = _equipmentModel.ListAllHeavyComboByEquipmentTypeList(lstEquipmentTypeId);
                    }
                    else
                        _view.EquipmentList = null;
                }
            }

            BindEquipmentGridAdd();
        }

        /// <summary>
        /// Binds the Equipment Grid based on the Equipment List and the Sorting
        /// </summary>
        public void BindEquipmentGridAdd()
        {
            List<CS_View_EquipmentInfo> comboList;

            switch (_view.SortColumn)
            {
                case Globals.Common.Sort.EquipmentSortColumns.None:
                    comboList = new List<CS_View_EquipmentInfo>();
                    comboList.AddRange(_view.EquipmentList.Where(e => e.ComboID.HasValue && e.IsPrimary == 1 && e.DisplayInResourceAllocation).OrderBy(e => e.ComboName).Distinct(new Globals.QuickReference.CS_View_EquipmentInfo_Equipment_Comparer()).ToList());
                    comboList.AddRange(_view.EquipmentList.Where(e => !e.ComboID.HasValue && e.DisplayInResourceAllocation).Distinct(new Globals.QuickReference.CS_View_EquipmentInfo_Equipment_Comparer()).ToList());
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.DivisionName:
                    DoComboSort(e => e.DivisionName, _view.SortDirection, out comboList);
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.DivisionState:
                    DoComboSort(e => e.DivisionState, _view.SortDirection, out comboList);
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.ComboName:
                    DoComboSort(e => e.ComboName, _view.SortDirection, out comboList);
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.UnitNumber:
                    DoComboSort(e => e.UnitNumber, _view.SortDirection, out comboList);
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.Descriptor:
                    DoComboSort(e => e.Descriptor, _view.SortDirection, out comboList);
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.Status:
                    DoComboSort(e => e.Status, _view.SortDirection, out comboList);
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.JobLocation:
                    DoComboSort(e => e.JobLocation, _view.SortDirection, out comboList);
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.Type:
                    DoComboSort(e => e.Type, _view.SortDirection, out comboList);
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.OperationStatus:
                    DoComboSort(e => e.EquipmentStatus, _view.SortDirection, out comboList);
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.JobNumber:
                    DoComboSort(e => e.JobNumber, _view.SortDirection, out comboList);
                    break;
                default:
                    comboList = _view.EquipmentList.Where(e => (!e.ComboID.HasValue || (e.ComboID.HasValue && e.IsPrimary == 1)) && e.DisplayInResourceAllocation).Distinct(new Globals.QuickReference.CS_View_EquipmentInfo_Equipment_Comparer()).ToList();
                    break;
            }

            _view.EquipmentsAddGridDataSource = comboList;
        }

        /// <summary>
        /// Fills the fields for the Equipments Row
        /// </summary>
        public void SetEquipmentsAddRow()
        {
            _view.EquipmentsAddDivision = _view.EquipmentRowDataItem.DivisionName;
            _view.EquipmentsAddDivisionState = _view.EquipmentRowDataItem.DivisionState;
            _view.EquipmentsAddComboName = _view.EquipmentRowDataItem.ComboName;
            _view.EquipmentsAddUnitNumber = _view.EquipmentRowDataItem.UnitNumber;
            _view.EquipmentsAddDescriptor = _view.EquipmentRowDataItem.Descriptor;
            _view.EquipmentsAddStatus = _view.EquipmentRowDataItem.Status;
            _view.EquipmentsAddJobLocation = _view.EquipmentRowDataItem.JobLocation;
            _view.EquipmentsAddOperationStatus = _view.EquipmentRowDataItem.EquipmentStatus;
            _view.EquipmentsAddEquipmentId = _view.EquipmentRowDataItem.EquipmentID.ToString();
            _view.EquipmentsAddIsCombo = _view.EquipmentRowDataItem.ComboID.HasValue.ToString();
            _view.EquipmentsAddIsComboUnit = false.ToString();
            _view.EquipmentsAddJobNumber = _view.EquipmentRowDataItem.PrefixedNumber;
            _view.EquipmentsAddType = _view.EquipmentRowDataItem.Type;
            _view.EquipmentsAddComboID = _view.EquipmentRowDataItem.ComboID;

            if (_view.EquipmentRowDataItem.IsWhiteLight.HasValue)
                _view.EquipmentsAddWhiteLight = _view.EquipmentRowDataItem.IsWhiteLight.Value;
            else
                _view.EquipmentsAddWhiteLight = false;

            if (_view.EquipmentRowDataItem.DivisionConflicted.HasValue)
                _view.EquipmentsAddIsDivConf = _view.EquipmentRowDataItem.DivisionConflicted.Value;
            else
                _view.EquipmentsAddIsDivConf = false;

            if (_view.EquipmentRowDataItem.PermitExpired.HasValue)
                _view.EquipmentsAddPermitExpired = _view.EquipmentRowDataItem.PermitExpired.Value;
            else
                _view.EquipmentsAddPermitExpired = false;

            if (_view.EquipmentRowDataItem.JobID.HasValue)
            {
                _view.EquipmentsAddJobNumberNavigateUrl = string.Format("javascript: var newWindow = window.open('/JobRecord.aspx?JobId={0}', '', 'width=870, height=600, scrollbars=1, resizable=yes');", _view.EquipmentRowDataItem.JobID.Value);

                if (!_view.EquipmentRowDataItem.CallLogID.Equals(0))
                {
                    if (!_callLogModel.GetCallTypeByDescription(_view.EquipmentRowDataItem.Type).IsAutomaticProcess)
                        _view.EquipmentsAddTypeNavigateUrl = string.Format("javascript: var newWindow = window.open('/CallEntry.aspx?JobId={0}&CallEntryId={1}', '', 'width=800, height=600, scrollbars=1, resizable=yes');", _view.EquipmentRowDataItem.JobID.Value, _view.EquipmentRowDataItem.CallLogID);
                    else
                        _view.EquipmentsAddTypeNavigateUrl = "javascript: alert('This Call Entry is automatic generated and can not be updated.');";
                }
            }

            KeyValuePair<string, bool> selectedEquip = _view.SelectedEquipmentAddList.FirstOrDefault(e => e.Key == _view.EquipmentRowDataItem.EquipmentID.ToString());
            bool available = (string.IsNullOrEmpty(selectedEquip.Key)) ? true : selectedEquip.Value;

            _view.EquipmentsAddchkEquipmentAdd = !available;
        }

        /// <summary>
        /// Gets the List of Equipments inside a Combo
        /// </summary>
        public void GetEquipmentComboRow()
        {
            List<CS_View_EquipmentInfo> equipmentList;
            switch (_view.SortColumn)
            {
                case Globals.Common.Sort.EquipmentSortColumns.None:
                    equipmentList = new List<CS_View_EquipmentInfo>();
                    equipmentList.AddRange(_view.EquipmentList.Where(e => e.ComboID.HasValue && e.ComboID == _view.EquipmentRowDataItem.ComboID && e.DisplayInResourceAllocation).Distinct(new Globals.QuickReference.CS_View_EquipmentInfo_Equipment_Comparer()).ToList());
                    equipmentList = equipmentList.OrderByDescending(e => e.IsPrimary).ThenBy(e => e.UnitNumber).ToList();
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.DivisionName:
                    DoEquipmentSort(e => e.DivisionName, _view.SortDirection, out equipmentList);
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.DivisionState:
                    DoEquipmentSort(e => e.DivisionState, _view.SortDirection, out equipmentList);
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.ComboName:
                    DoEquipmentSort(e => e.ComboName, _view.SortDirection, out equipmentList);
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.UnitNumber:
                    DoEquipmentSort(e => e.UnitNumber, _view.SortDirection, out equipmentList);
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.Descriptor:
                    DoEquipmentSort(e => e.Descriptor, _view.SortDirection, out equipmentList);
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.Status:
                    DoEquipmentSort(e => e.Status, _view.SortDirection, out equipmentList);
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.JobLocation:
                    DoEquipmentSort(e => e.JobLocation, _view.SortDirection, out equipmentList);
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.Type:
                    DoEquipmentSort(e => e.Type, _view.SortDirection, out equipmentList);
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.OperationStatus:
                    DoEquipmentSort(e => e.EquipmentStatus, _view.SortDirection, out equipmentList);
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.JobNumber:
                    DoEquipmentSort(e => e.JobNumber, _view.SortDirection, out equipmentList);
                    break;
                default:
                    equipmentList = _view.EquipmentList.Where(e => e.ComboID.HasValue && e.ComboID == _view.EquipmentRowDataItem.ComboID).ToList();
                    break;
            }

            _view.EquipmentsComboGridDataSource = equipmentList;
        }

        /// <summary>
        /// Fills the fields for the Equipments Inside a Combo row
        /// </summary>
        public void FillEquipmentGridAddRowCombo()
        {
            _view.EquipmentsComboDivision = _view.EquipmentComboDataItem.DivisionName;
            _view.EquipmentsComboDivisionState = _view.EquipmentComboDataItem.DivisionState;
            _view.EquipmentsComboComboName = _view.EquipmentComboDataItem.ComboName;
            _view.EquipmentsComboUnitNumber = _view.EquipmentComboDataItem.UnitNumber;
            _view.EquipmentsComboDescriptor = _view.EquipmentComboDataItem.Descriptor;
            _view.EquipmentsComboStatus = _view.EquipmentComboDataItem.Status;
            _view.EquipmentsComboJobLocation = _view.EquipmentComboDataItem.JobLocation;
            _view.EquipmentsComboOperationStatus = _view.EquipmentComboDataItem.EquipmentStatus;
            _view.EquipmentsComboEquipmentId = _view.EquipmentComboDataItem.EquipmentID.ToString();
            _view.EquipmentsComboIsCombo = false.ToString();
            _view.EquipmentsComboIsComboUnit = true.ToString();
            _view.EquipmentsComboJobNumber = _view.EquipmentComboDataItem.PrefixedNumber;
            _view.EquipmentsComboType = _view.EquipmentComboDataItem.Type;
            _view.EquipmentsComboComboID = _view.EquipmentComboDataItem.ComboID;

            if (_view.EquipmentComboDataItem.IsWhiteLight.HasValue)
                _view.EquipmentsComboWhiteLight = _view.EquipmentComboDataItem.IsWhiteLight.Value;
            else
                _view.EquipmentsComboWhiteLight = false;

            if (_view.EquipmentComboDataItem.DivisionConflicted.HasValue)
                _view.EquipmentsComboIsDivConf = _view.EquipmentComboDataItem.DivisionConflicted.Value;
            else
                _view.EquipmentsComboIsDivConf = false;

            if (_view.EquipmentComboDataItem.PermitExpired.HasValue)
                _view.EquipmentsComboPermitExpired = _view.EquipmentComboDataItem.PermitExpired.Value;
            else
                _view.EquipmentsComboIsDivConf = false;

            if (_view.EquipmentComboDataItem.JobID.HasValue)
            {
                _view.EquipmentsComboJobNumberNavigateUrl = string.Format("javascript: var newWindow = window.open('/JobRecord.aspx?JobId={0}', '', 'width=870, height=600, scrollbars=1, resizable=yes');", _view.EquipmentComboDataItem.JobID.Value);

                if (!_view.EquipmentComboDataItem.CallLogID.Equals(0))
                {
                    if (!_callLogModel.GetCallTypeByDescription(_view.EquipmentComboDataItem.Type).IsAutomaticProcess)
                        _view.EquipmentsComboTypeNavigateUrl = string.Format("javascript: var newWindow = window.open('/CallEntry.aspx?JobId={0}&CallEntryId{1}', '', 'width=800, height=600, scrollbars=1, resizable=yes');", _view.EquipmentComboDataItem.JobID.Value, _view.EquipmentComboDataItem.CallLogID);
                    else
                        _view.EquipmentsComboTypeNavigateUrl = "javascript: alert('This Call Entry is automatic generated and can not be updated.');";
                }
            }

            KeyValuePair<string, bool> selectedEquip = _view.SelectedEquipmentAddList.FirstOrDefault(e => e.Key == _view.EquipmentComboDataItem.EquipmentID.ToString());
            _view.EquipmentsCombochkEquipmentAdd = (string.IsNullOrEmpty(selectedEquip.Key)) ? false : !selectedEquip.Value;
        }

        /// <summary>
        /// Adds a new Equipment Entity to ShoppingCart Structure
        /// </summary>
        public void AddEquipmentToShoppingCart()
        {
            if (null == _view.ShoppingCart)
                _view.ShoppingCart = FillShoppingCartColumns();

            DataTable shoppingCartTable = _view.ShoppingCart;
            IList<string> selectedEquipments = _view.SelectedEquipmentsAdd.Where(e => _view.TransferShoppingCart.Select(string.Format("EquipmentID = {0} AND Type = {1}", e.Split(':')[0], (int)Globals.ResourceAllocation.ResourceType.Equipment)).Count() == 0).ToList();
            List<ShoppingCartVO> existingItemsList = new List<ShoppingCartVO>();
            List<ShoppingCartVO> newItemsList = new List<ShoppingCartVO>();

            if (shoppingCartTable.Rows.Count > 0)
            {
                DataRow[] addedEquipments = shoppingCartTable.Select(string.Format("Type = '{0}'", (int)Globals.ResourceAllocation.Type.AddEquipment));

                //Fill the itemsList with items already added on Shopping Cart
                for (int i = 0; i < addedEquipments.Length; i++)
                {
                    DataRow row = addedEquipments[i];

                    existingItemsList.Add(new ShoppingCartVO
                    {
                        ID = (int)row["EquipmentID"]
                    });
                }
            }

            for (int i = 0; i < selectedEquipments.Count; i++)
            {
                string[] values = selectedEquipments[i].Split(':');
                int equipmentId = Int32.Parse(values[0]);
                bool isCombo = Boolean.Parse(values[1]);
                bool isComboUnit = Boolean.Parse(values[3]);

                //If the selected item is a Combo, load all it's children from DB and insert into the list
                if (isCombo)
                {
                    int comboId = Int32.Parse(values[2]);

                    IList<CS_View_SecondaryEquipmentInfo> comboItems = _equipmentModel.ListAllDetailedEquipmentInfo(comboId);
                    for (int n = 0; n < comboItems.Count; n++)
                    {
                        CS_View_SecondaryEquipmentInfo item = comboItems[n];

                        if (!existingItemsList.Exists(delegate(ShoppingCartVO match) { return match.ID == item.EquipmentID; })
                            && !newItemsList.Exists(delegate(ShoppingCartVO match) { return match.ID == item.EquipmentID; }))
                        {
                            newItemsList.Add(new ShoppingCartVO
                            {
                                ID = item.EquipmentID,
                                Type = (int)Globals.ResourceAllocation.Type.AddEquipment,
                                Name = string.Format("Added Resource - {0} {1}", item.DivisionName, item.Descriptor),
                                AssignmentType = "A",
                                DivisionID = item.DivisionID.Value,
                                UnitNumber = item.ComboName + " - " + item.UnitNumber
                            });
                        }
                    }
                }
                //If the selected item is a combo unit, verify that it's not already loaded, if not, insert it into the list
                else if (isComboUnit
                    && !existingItemsList.Exists(delegate(ShoppingCartVO match) { return match.ID == equipmentId; })
                    && !newItemsList.Exists(delegate(ShoppingCartVO match) { return match.ID == equipmentId; }))
                {
                    CS_View_SecondaryEquipmentInfo item = _equipmentModel.GetSpecificSecondaryEquipmentFromView(equipmentId);

                    newItemsList.Add(new ShoppingCartVO
                    {
                        ID = item.EquipmentID,
                        Type = (int)Globals.ResourceAllocation.Type.AddEquipment,
                        Name = string.Format("Added Resource - {0} {1}", item.DivisionName, item.Descriptor),
                        AssignmentType = "A",
                        DivisionID = item.DivisionID.Value,
                        UnitNumber = item.ComboName + " - " + item.UnitNumber
                    });
                }
                //If it is not a Combo, nor a Unit from one. It is a single, separate equipment
                else if (!isCombo && !isComboUnit)
                {
                    CS_View_EquipmentInfo item = _equipmentModel.GetSpecificEquipmentFromView(equipmentId);

                    if (!existingItemsList.Exists(delegate(ShoppingCartVO match) { return match.ID == item.EquipmentID; })
                            && !newItemsList.Exists(delegate(ShoppingCartVO match) { return match.ID == item.EquipmentID; }))
                    {

                        newItemsList.Add(new ShoppingCartVO
                        {
                            ID = item.EquipmentID,
                            Type = (int)Globals.ResourceAllocation.Type.AddEquipment,
                            Name = string.Format("Added Resource - {0} {1}", item.DivisionName, item.Descriptor),
                            AssignmentType = "A",
                            DivisionID = item.DivisionID,
                            UnitNumber = item.UnitNumber
                        });
                    }
                }
            }

            for (int i = 0; i < newItemsList.Count; i++)
            {
                ShoppingCartVO item = newItemsList[i];

                DataRow newRow = shoppingCartTable.NewRow();

                newRow["UnitNumber"] = item.UnitNumber;
                newRow["Type"] = (Globals.ResourceAllocation.Type)item.Type;
                newRow["EquipmentId"] = item.ID;
                newRow["Name"] = item.Name;
                newRow["AssignmentType"] = item.AssignmentType;
                newRow["DivisionId"] = item.DivisionID;
                shoppingCartTable.Rows.Add(newRow);

                //For now, it only disables the button, later we'll implement a method to check if it is possible to add
                //or if the equipment should be transfered
                if (!_view.SelectedEquipmentAddList.ContainsKey(item.ID.ToString()))
                    _view.SelectedEquipmentAddList.Add(item.ID.ToString(), false);
                else
                    _view.SelectedEquipmentAddList[item.ID.ToString()] = false;
            }

            _view.ShoppingCart = shoppingCartTable;

            //Clear pointers
            shoppingCartTable = null;
            selectedEquipments = null;
            existingItemsList = null;
            newItemsList = null;
        }

        /// <summary>
        /// Resets Equipment Add grid checkboxes to make them selectable.
        /// </summary>
        public void ResetEquipmentAddGridCheckboxes()
        {
            for (int i = 0; i < _view.SelectedEquipmentAddList.Count; i++)
            {
                KeyValuePair<string, bool> equipment = _view.SelectedEquipmentAddList.ToList()[i];
                if (!equipment.Value)
                {
                    DataRow[] rows = _view.ShoppingCart.Select(string.Format("EquipmentId = {0} and Type = {1}", equipment.Key, Convert.ToInt32(Globals.ResourceAllocation.Type.AddEmployee)));
                    if (rows == null || (rows != null && rows.Length.Equals(0)))
                        _view.SelectedEquipmentAddList[equipment.Key] = true;
                }
            }
            BindEquipmentGridAdd();
        }

        #endregion

        #region [ Add Resource - Employee ]

        /// <summary>
        /// Loads the Employee List by default based on Divisions related to the Job Record
        /// </summary>
        public void ListAllEmployeeAddByDivision()
        {
            DataTable employeeTable = InsertEmployeeListOnDatatable(ListAllEmployeeInfoByDivision());
            _view.EmployeeListAdd = employeeTable;
            _view.EmployeeDataTable = employeeTable;
        }

        /// <summary>
        /// Loads the Employee List by default based on Reserves related to the Job Record
        /// </summary>
        public void ListAllEmployeeAddByReserves()
        {
            DataTable employeeTable = InsertEmployeeListOnDatatable(ListAllEmployeeInfoByReserves());
            _view.EmployeeListAdd = employeeTable;
            _view.EmployeeDataTable = employeeTable;
        }

        /// <summary>
        /// List the Employee list based on filters from View
        /// </summary>
        public void ListFilteredEmployeeGridAdd()
        {
            DataTable employeeTable = null;
            if (_view.EmployeeFilterAdd.HasValue)
                employeeTable = InsertEmployeeListOnDatatable(
                    _employeeModel.ListFilteredEmployeeInfo(_view.EmployeeFilterAdd.Value, _view.EmployeeFilterValueAdd));
            else
                employeeTable = InsertEmployeeListOnDatatable(
                    _employeeModel.ListAllEmployeeInfo());

            _view.EmployeeListAdd = employeeTable;
            _view.EmployeeDataTable = employeeTable;
        }

        /// <summary>
        /// Goes through the rows of the DataTable and updates the 'Enabled' property, according to parameters
        /// </summary>
        public void UpdateEmployeeGridAddButton()
        {
            foreach (KeyValuePair<string, bool> employee in _view.SelectedEmployeeAddList)
            {
                DataRow[] rows = _view.EmployeeDataTable.Select(string.Format("EmployeeId = {0}", employee.Key));
                foreach (DataRow row in rows)
                    row["SelectAvailable"] = employee.Value.ToString();
            }
            _view.EmployeeListAdd = _view.EmployeeDataTable;
        }

        /// <summary>
        /// Adds Employee Entity to ShoppingCart structure
        /// </summary>
        public void AddEmployeeToShoppingCart()
        {
            IList<int> selectedEmployees = _view.SelectedEmployeesAdd.Where(e => _view.TransferShoppingCart.Select(string.Format("EmployeeID = {0} AND Type = {1}", e, (int)Globals.ResourceAllocation.ResourceType.Employee)).Count() == 0).ToList();

            DataTable shoppingCartTable = _view.ShoppingCart;

            foreach (int employeeId in selectedEmployees)
            {
                //For now, it only disables the button, later we'll implement a method to check if it is possible to add
                //or if the equipment should be transfered
                if (!_view.SelectedEmployeeAddList.ContainsKey(employeeId.ToString()))
                    _view.SelectedEmployeeAddList.Add(employeeId.ToString(), false);
                else
                    _view.SelectedEmployeeAddList[employeeId.ToString()] = false;

                CS_Employee employeeEntity = _employeeModel.GetEmployee(employeeId);
                if (null != employeeEntity)
                {
                    string divisionNumber = string.Empty;
                    if (employeeEntity.DivisionID.HasValue)
                    {
                        CS_Division division = _divisionModel.GetDivision(employeeEntity.DivisionID.Value);
                        if (null != division)
                        {
                            _divisionId = division.ID;
                            divisionNumber = division.Name;
                            if (division.StateID.HasValue)
                            {
                                CS_State state = _locationModel.GetState(division.StateID.Value);
                                if (null != state)
                                    divisionNumber += " " + state.Acronym;
                            }
                        }
                    }

                    if (shoppingCartTable.Select(string.Format("EmployeeID = {0} AND Type = {1}", employeeId, (int)Globals.ResourceAllocation.ResourceType.Employee)).Count() == 0)
                    {
                        DataRow newRow = shoppingCartTable.NewRow();
                        newRow["Type"] = Globals.ResourceAllocation.Type.AddEmployee;
                        newRow["EmployeeId"] = employeeEntity.ID;
                        newRow["Name"] = string.Format("Added Resource - {0} {1}", divisionNumber, employeeEntity.FullName);
                        newRow["AssignmentType"] = "A";
                        newRow["DivisionId"] = _divisionId;
                        shoppingCartTable.Rows.Add(newRow);
                    }
                }
            }

            _view.ShoppingCart = shoppingCartTable;
        }

        /// <summary>
        /// Resets Employee Add grid checkboxes to make them selectable.
        /// </summary>
        public void ResetEmployeeAddGridCheckboxes()
        {
            DataRow[] rows;
            rows = _view.EmployeeDataTable.Select("SelectAvailable = false");
            foreach (DataRow row in rows)
                row["SelectAvailable"] = "true";
            _view.EmployeeListAdd = _view.EmployeeDataTable;
        }

        #endregion

        #region [ Reserve Resource - Equipment ]

        /// <summary>
        /// Returns a list of Equipment Type related to divisions related to the job record
        /// </summary>
        public void ListAllEquipmentReserveByDivision()
        {
            List<int> lstDivisionId = new List<int>();
            IList<CS_JobDivision> lstJobDivisions = _jobModel.ListAllDivisionsByJob(_view.JobID);
            if (lstJobDivisions.Count > 0)
                lstDivisionId.AddRange(lstJobDivisions.Select(lstJobDivision => lstJobDivision.DivisionID));

            if (lstDivisionId.Count > 0)
            {
                _view.ReserveEquipmentDataSource = _equipmentModel.ListFilteredEquipmentType(_view.EquipmentTypeId, _view.StateId, lstDivisionId);
                BuildReserveEquipmentLocalCount();
                _view.BindReserveEquipmentGrid();
            }
        }

        /// <summary>
        /// Returns a list of Equipment Type based on view filters
        /// </summary>
        public void ListFilteredEquipmentGridReserve()
        {
            List<int> lstDivisionId = new List<int>();
            if (_view.DivisionId.HasValue)
                lstDivisionId.Add(_view.DivisionId.Value);

            _view.ReserveEquipmentDataSource = _equipmentModel.ListFilteredEquipmentType(_view.EquipmentTypeId, _view.StateId, lstDivisionId);
        }

        /// <summary>
        /// Generates an internal list to keep counting the reserved resources
        /// </summary>
        public void BuildReserveEquipmentLocalCount()
        {
            _view.ReserveEquipmentLocalCount.Clear();
            foreach (CS_View_ReserveEquipment item in _view.ReserveEquipmentDataSource)
            {
                string entry = string.Format("{0};{1}", item.EquipmentTypeID, item.DivisionID);
                if (item.Reserve.HasValue)
                    _view.ReserveEquipmentLocalCount.Add(entry, item.Reserve.Value);
                else
                    _view.ReserveEquipmentLocalCount.Add(entry, 0);
            }
        }

        /// <summary>
        /// List all jobs related to the equipment type and division informed
        /// </summary>
        public void ListAllJobsByEquipmentTypeAndDivision()
        {
            _view.JobsRelatedToEquipmentType = _jobModel.ListAllJobsByEquipmentTypeAndDivision(_view.SelectedEquipmentType, _view.SelectedDivision);
        }

        /// <summary>
        /// Adds Equipment Entity to Reserve List of Shopping Cart
        /// </summary>
        public void ReserveEquipmentTypeToShoppingCart()
        {
            DataTable shoppingCartTable = _view.ShoppingCart;

            foreach (int[] equipment in _view.SelectedEquipmentsReserve)
            {
                int selectedEquipmentType = equipment[0];
                int selectedDivision = equipment[1];
                int quantity = equipment[2];

                CS_EquipmentType equipmentTypeEntity = _equipmentModel.GetEquipmentType(selectedEquipmentType);
                if (null != equipmentTypeEntity)
                {
                    string divisionNumber = string.Empty;
                    CS_Division division = _divisionModel.GetDivision(selectedDivision);
                    if (null != division)
                    {
                        divisionNumber = division.Name;
                        if (division.StateID.HasValue)
                        {
                            CS_State state = _locationModel.GetState(division.StateID.Value);
                            if (null != state)
                                divisionNumber += " " + state.Acronym;
                        }

                        for (int i = 0; i < quantity; i++)
                        {
                            DataRow newRow = shoppingCartTable.NewRow();
                            newRow["Type"] = Globals.ResourceAllocation.Type.ReserveEquipment;
                            newRow["EquipmentTypeId"] = equipmentTypeEntity.ID;
                            newRow["DivisionId"] = division.ID;
                            newRow["Name"] = string.Format("Reserved Resource - {0} {1}", divisionNumber, equipmentTypeEntity.Name);
                            newRow["AssignmentType"] = "R";
                            newRow["UnitNumber"] = equipmentTypeEntity.CompleteName;
                            shoppingCartTable.Rows.Add(newRow);

                            string reserveEntry = string.Format("{0};{1}", selectedEquipmentType, selectedDivision);
                            int reserveCount = 0;
                            bool exists = _view.ReserveEquipmentLocalCount.TryGetValue(reserveEntry, out reserveCount);
                            if (exists)
                                _view.ReserveEquipmentLocalCount[reserveEntry] = reserveCount + 1;
                            else
                                _view.ReserveEquipmentLocalCount.Add(reserveEntry, 1);
                        }
                    }
                }
            }

            _view.ShoppingCart = shoppingCartTable;
        }

        #endregion

        #region [ Reserve Resource - Employee ]

        /// <summary>
        /// Returns a list of Employees related to divisions related to the job record
        /// </summary>
        public void ListAllEmployeeReserveByDivision()
        {
            _view.ReserveEmployeeDataSource = ListAllEmployeeInfoByDivision();
        }

        /// <summary>
        /// Returns a list of Employees based on view filters
        /// </summary>
        public void ListFilteredEmployeeGridReserve()
        {
            if (_view.EmployeeFilterReserve.HasValue)
                _view.ReserveEmployeeDataSource = _employeeModel.ListFilteredEmployeeInfo(_view.EmployeeFilterReserve.Value, _view.EmployeeFilterValueReserve);
            else
                _view.ReserveEmployeeDataSource = _employeeModel.ListAllEmployeeInfo();
        }

        /// <summary>
        /// List all equipment type
        /// </summary>
        public void ListAllEquipmentType()
        {
            _view.EquipmentTypeFilterSource = _equipmentModel.ListAllEquipmentType();
        }

        /// <summary>
        /// Adds Employee Entity to Shopping Cart Reserve List
        /// </summary>
        public void ReserveEmployeeToShoppingCart()
        {
            DataTable shoppingCartTable = _view.ShoppingCart;
            foreach (int employeeId in _view.SelectedEmployeesReserve)
            {
                CS_Employee employeeEntity = _employeeModel.GetEmployee(Convert.ToInt32(employeeId));
                if (null != employeeEntity)
                {
                    string divisionNumber = string.Empty;
                    if (employeeEntity.DivisionID.HasValue)
                    {
                        CS_Division division = _divisionModel.GetDivision(employeeEntity.DivisionID.Value);
                        if (null != division)
                        {
                            divisionNumber = division.Name;
                            if (division.StateID.HasValue)
                            {
                                CS_State state = _locationModel.GetState(division.StateID.Value);
                                if (null != state)
                                    divisionNumber += " " + state.Acronym;
                            }
                        }
                    }
                    DataRow newRow = shoppingCartTable.NewRow();
                    newRow["Type"] = Globals.ResourceAllocation.Type.ReserveEmployee;
                    newRow["EmployeeId"] = employeeEntity.ID;
                    newRow["Name"] = string.Format("Reserved Resource - {0} {1}", divisionNumber, employeeEntity.FullName);
                    newRow["AssignmentType"] = "R";
                    newRow["DivisionId"] = employeeEntity.DivisionID;
                    shoppingCartTable.Rows.Add(newRow);
                }
            }

            _view.ShoppingCart = shoppingCartTable;
        }

        #endregion

        #region [ Transfer Shopping Cart ]

        /// <summary>
        /// Fills the Shopping cart Grid with data related to the Job Record
        /// </summary>
        public void ListTransferShoppingCart()
        {
            _view.TransferShoppingCart = GetShoppingCartDataTable();
        }


        #endregion

        #region [ Shopping Cart ]

        /// <summary>
        /// Fills the shopping cart with the columns
        /// </summary>
        public void FillShoppingCart()
        {
            _view.ShoppingCart = FillShoppingCartColumns();
        }

        /// <summary>
        /// Removes a list of rows from the shopping cart datatable
        /// </summary>
        public void RemoveItemsFromShoppingCart()
        {
            DataTable shoppingCart = new DataTable();
            shoppingCart.Merge(_view.ShoppingCart);

            Dictionary<string, bool> selectedEquipmentAddList = _view.SelectedEquipmentAddList;
            Dictionary<string, bool> selectedEmployeeAddList = _view.SelectedEmployeeAddList;
            Dictionary<string, int> reserveEquipmentLocalCount = _view.ReserveEquipmentLocalCount;

            bool updateAdd = false;
            bool updateReserve = false;
            int rowIndex = 0;

            for (int i = 0; i < _view.SelectedRowsToRemove.Count; i++)
            {
                rowIndex = _view.SelectedRowsToRemove[i];
                switch ((Globals.ResourceAllocation.Type)int.Parse(shoppingCart.Rows[rowIndex]["Type"].ToString()))
                {
                    //Updates the item in the equipment Dictionary 
                    case Globals.ResourceAllocation.Type.AddEquipment:
                        selectedEquipmentAddList[shoppingCart.Rows[rowIndex]["EquipmentId"].ToString()] = true;
                        updateAdd = true;
                        break;

                    case Globals.ResourceAllocation.Type.AddEmployee:
                        selectedEmployeeAddList[shoppingCart.Rows[rowIndex]["EmployeeId"].ToString()] = true;
                        updateAdd = true;
                        break;

                    case Globals.ResourceAllocation.Type.ReserveEquipment:
                        string entry = string.Format("{0};{1}", shoppingCart.Rows[rowIndex]["EquipmentTypeId"].ToString(), shoppingCart.Rows[rowIndex]["DivisionId"].ToString());
                        reserveEquipmentLocalCount[entry] = reserveEquipmentLocalCount[entry] - 1;
                        updateReserve = true;
                        break;
                }
                shoppingCart.Rows[rowIndex].Delete();
            }

            if (updateAdd)
            {
                _view.SelectedEquipmentAddList = selectedEquipmentAddList;
                _view.SelectedEmployeeAddList = selectedEmployeeAddList;
                BindEquipmentGridAdd();
                UpdateEmployeeGridAddButton();
            }
            if (updateReserve)
            {
                _view.ReserveEquipmentLocalCount = reserveEquipmentLocalCount;
                _view.BindReserveEquipmentGrid();
            }

            _view.ShoppingCart = shoppingCart;
        }

        public void TransferResourcesFromShoppingCart()
        {
            DataTable shoppingCart = new DataTable();
            shoppingCart.Merge(_view.TransferShoppingCart);

            IList<int> SelectedResourcesIdList = _view.SelectedRowsToTransfer;

            IList<int> ResourcesToTransferList = new List<int>();

            int rowIndex = 0;

            for (int i = 0; i < SelectedResourcesIdList.Count; i++)
            {
                rowIndex = SelectedResourcesIdList[i];

                if (null != shoppingCart.Rows[rowIndex]["Id"])
                {
                    ResourcesToTransferList.Add(int.Parse(shoppingCart.Rows[rowIndex]["Id"].ToString()));
                }
            }

            _view.ResourceIdToTransfer = ResourcesToTransferList;
        }

        /// <summary>
        /// Persists changes to Resource Allocation
        /// </summary>
        public void SaveResourceAllocation()
        {
            IList<CS_Resource> lstResource = new List<CS_Resource>();
            IList<CS_Reserve> lstReserve = new List<CS_Reserve>();

            IList<int> lstDivisions = new List<int>();

            DataTable shoppingCart = _view.ShoppingCart;

            for (int i = 0; i < shoppingCart.Rows.Count; i++)
            {
                DataRow row = shoppingCart.Rows[i];

                CS_Resource resource = new CS_Resource();
                CS_Reserve reserve = new CS_Reserve();

                if (Convert.ToInt32(row["Type"]).Equals((int)Globals.ResourceAllocation.Type.AddEmployee))
                {
                    if (row["ID"] != DBNull.Value)
                        resource.ID = Convert.ToInt32(row["ID"]);
                    if (row["EmployeeId"] != DBNull.Value)
                        resource.EmployeeID = Convert.ToInt32(row["EmployeeId"]);
                    if (row["Duration"] != DBNull.Value)
                        resource.Duration = Convert.ToInt32(row["Duration"]);
                    if (row["StartDateTime"] != DBNull.Value)
                        resource.StartDateTime = Convert.ToDateTime(row["StartDateTime"]);
                    if (row["JobId"] != DBNull.Value)
                        resource.JobID = Convert.ToInt32(row["JobId"]);

                    resource.CreatedBy = _view.UserName;
                    resource.ModifiedBy = _view.UserName;
                    resource.CreationDate = DateTime.Now;
                    resource.ModificationDate = DateTime.Now;
                    resource.Active = true;
                    resource.Type = (int)Globals.ResourceAllocation.ResourceType.Employee;

                    if (row["DivisionId"] != DBNull.Value && Convert.ToInt32(row["DivisionId"]) > 0)
                        lstDivisions.Add(Convert.ToInt32(row["DivisionId"]));

                    lstResource.Add(resource);
                }
                else if (Convert.ToInt32(row["Type"]).Equals((int)Globals.ResourceAllocation.Type.AddEquipment))
                {
                    if (row["ID"] != DBNull.Value)
                        resource.ID = Convert.ToInt32(row["ID"]);
                    if (row["EquipmentId"] != DBNull.Value)
                        resource.EquipmentID = Convert.ToInt32(row["EquipmentId"]);
                    if (row["Duration"] != DBNull.Value)
                        resource.Duration = Convert.ToInt32(row["Duration"]);
                    if (row["StartDateTime"] != DBNull.Value)
                        resource.StartDateTime = Convert.ToDateTime(row["StartDateTime"]);
                    if (row["JobId"] != DBNull.Value)
                        resource.JobID = Convert.ToInt32(row["JobId"]);
                    resource.CreatedBy = _view.UserName;
                    resource.ModifiedBy = _view.UserName;
                    resource.CreationDate = DateTime.Now;
                    resource.ModificationDate = DateTime.Now;
                    resource.Active = true;
                    resource.Type = (int)Globals.ResourceAllocation.ResourceType.Equipment;

                    if (row["DivisionId"] != DBNull.Value && Convert.ToInt32(row["DivisionId"]) > 0)
                        lstDivisions.Add(Convert.ToInt32(row["DivisionId"]));

                    lstResource.Add(resource);
                }
                else if (Convert.ToInt32(row["Type"]).Equals((int)Globals.ResourceAllocation.Type.ReserveEquipment))
                {
                    if (row["ID"] != DBNull.Value)
                        reserve.ID = Convert.ToInt32(row["ID"]);
                    if (row["EquipmentTypeId"] != DBNull.Value)
                        reserve.EquipmentTypeID = Convert.ToInt32(row["EquipmentTypeId"]);
                    if (row["JobId"] != DBNull.Value)
                        reserve.JobID = Convert.ToInt32(row["JobId"]);
                    if (row["Duration"] != DBNull.Value)
                        reserve.Duration = Convert.ToInt32(row["Duration"]);
                    if (row["StartDateTime"] != DBNull.Value)
                        reserve.StartDateTime = Convert.ToDateTime(row["StartDateTime"]);
                    if (row["DivisionId"] != DBNull.Value)
                        reserve.DivisionID = Convert.ToInt32(row["DivisionId"]);
                    reserve.CreateBy = _view.UserName;
                    reserve.CreationDate = DateTime.Now;
                    reserve.ModificationDate = DateTime.Now;
                    reserve.ModifiedBy = _view.UserName;
                    reserve.Active = true;
                    reserve.Type = (int)Globals.ResourceAllocation.ResourceType.Equipment;

                    if (row["DivisionId"] != DBNull.Value && Convert.ToInt32(row["DivisionId"]) > 0)
                        lstDivisions.Add(Convert.ToInt32(row["DivisionId"]));

                    lstReserve.Add(reserve);
                }
                else if (Convert.ToInt32(row["Type"]).Equals((int)Globals.ResourceAllocation.Type.ReserveEmployee))
                {
                    if (row["ID"] != DBNull.Value)
                        reserve.ID = Convert.ToInt32(row["ID"]);
                    if (row["EmployeeId"] != DBNull.Value)
                        reserve.EmployeeID = Convert.ToInt32(row["EmployeeId"]);
                    if (row["JobId"] != DBNull.Value)
                        reserve.JobID = Convert.ToInt32(row["JobId"]);
                    if (row["Duration"] != DBNull.Value)
                        reserve.Duration = Convert.ToInt32(row["Duration"]);
                    if (row["StartDateTime"] != DBNull.Value)
                        reserve.StartDateTime = Convert.ToDateTime(row["StartDateTime"]);
                    if (row["DivisionId"] != DBNull.Value)
                        reserve.DivisionID = Convert.ToInt32(row["DivisionId"]);
                    reserve.CreateBy = _view.UserName;
                    reserve.ModifiedBy = _view.UserName;
                    reserve.CreationDate = DateTime.Now;
                    reserve.ModificationDate = DateTime.Now;
                    reserve.Active = true;
                    reserve.Type = (int)Globals.ResourceAllocation.ResourceType.Employee;

                    if (row["DivisionId"] != DBNull.Value && Convert.ToInt32(row["DivisionId"]) > 0)
                        lstDivisions.Add(Convert.ToInt32(row["DivisionId"]));

                    lstReserve.Add(reserve);
                }
            }

            _resourceAllocationModel.SaveOrUpdateResourceAllocation(_view.JobID, lstReserve, lstResource, _view.UserName, lstDivisions, _view.Notes, true, _view.CallDate, _view.IsSubContractor, _view.SubContractorInfo, _view.FieldPO);
        }

        /// <summary>
        /// Runs validations prior to Save Employee
        /// </summary>
        /// <returns>True if it's valid, false otherwise</returns>
        public bool ValidateShoppingCartBeforeSave()
        {
            if (!_view.IsSubContractor)
            {
                bool AddOk = false;
                bool ResOk = false;

                DataTable dt = _view.ShoppingCart;

                dt.Merge(_view.TransferShoppingCart);

                int countAddEquipment = 0;

                int countAddEmp = 0;

                int countResEquipment = 0;

                int countResEmpl = 0;

                foreach (DataRow sc in dt.Rows)
                {
                    if (Convert.ToInt32(sc["Type"]).Equals((int)Globals.ResourceAllocation.Type.AddEquipment))
                    {
                        countAddEquipment++;
                    }
                    else if (Convert.ToInt32(sc["Type"]).Equals((int)Globals.ResourceAllocation.Type.AddEmployee))
                    {
                        countAddEmp++;
                    }
                    else if (Convert.ToInt32(sc["Type"]).Equals((int)Globals.ResourceAllocation.Type.ReserveEquipment))
                    {
                        countResEquipment++;
                    }
                    else if (Convert.ToInt32(sc["Type"]).Equals((int)Globals.ResourceAllocation.Type.ReserveEmployee))
                    {
                        countResEmpl++;
                    }
                }

                if ((countAddEquipment > 0 && countAddEmp > 0) || countAddEquipment == 0)
                    AddOk = true;

                if ((countResEquipment > 0 && countResEmpl > 0) || countResEquipment == 0)
                    ResOk = true;

                return (AddOk && ResOk);
            }
            else
                return true;
        }

        /// <summary>
        /// Gets the Resource Allocation Notes
        /// </summary>
        public void GetResourceAllocationNotes()
        {
            CS_ResourceAllocationDetails details = _resourceAllocationModel.GetResourceAllocationDetails(_view.JobID);
            if (null == details)
            {
                _view.Notes = string.Empty;
                _view.IsSubContractor = false;
                _view.SubContractorInfo = string.Empty;
                _view.FieldPO = string.Empty;
            }
            else
            {
                _view.Notes = details.Notes;
                
                if (details.IsSubContractor.HasValue)
                    _view.IsSubContractor = details.IsSubContractor.Value;

                _view.SubContractorInfo = details.SubContractorInfo;
                _view.FieldPO = details.FieldPO;
            }
        }

        #endregion

        #endregion

        #region [ Private Methods ]

        /// <summary>
        /// Executes the sorting function for the Equipment Add Grid
        /// </summary>
        /// <param name="keySelector"></param>
        /// <param name="sortDirection"></param>
        /// <param name="comboList"></param>
        private void DoComboSort(Func<CS_View_EquipmentInfo, string> keySelector, Globals.Common.Sort.SortDirection sortDirection, out List<CS_View_EquipmentInfo> comboList)
        {
            comboList = new List<CS_View_EquipmentInfo>();

            comboList.AddRange(_view.EquipmentList.Where(e => e.ComboID.HasValue && e.IsPrimary == 1 && e.DisplayInResourceAllocation).Distinct(new Globals.QuickReference.CS_View_EquipmentInfo_Combo_Comparer()));
            comboList.AddRange(_view.EquipmentList.Where(e => !e.ComboID.HasValue && e.DisplayInResourceAllocation));

            if (sortDirection == Globals.Common.Sort.SortDirection.Ascending)
            {
                comboList = comboList.OrderBy(keySelector).ToList();
            }
            else
            {
                comboList = comboList.OrderByDescending(keySelector).ToList();
            }
        }

        /// <summary>
        /// Executes the sorting function for the Equipment Inside a Combo Add Grid Row
        /// </summary>
        /// <param name="keySelector"></param>
        /// <param name="sortDirection"></param>
        /// <param name="equipmentList"></param>
        private void DoEquipmentSort(Func<CS_View_EquipmentInfo, string> keySelector, Globals.Common.Sort.SortDirection sortDirection, out List<CS_View_EquipmentInfo> equipmentList)
        {
            equipmentList = new List<CS_View_EquipmentInfo>();

            equipmentList.AddRange(_view.EquipmentList.Where(e => e.ComboID.HasValue && e.ComboID == _view.EquipmentRowDataItem.ComboID && e.DisplayInResourceAllocation).Distinct(new Globals.QuickReference.CS_View_EquipmentInfo_Equipment_Comparer()).ToList());

            if (sortDirection == Globals.Common.Sort.SortDirection.Ascending)
            {
                equipmentList = equipmentList.OrderBy(keySelector).ToList();
            }
            else
            {
                equipmentList = equipmentList.OrderByDescending(keySelector).ToList();
            }
        }

        /// <summary>
        /// Loads the employee List inside a DataTable
        /// </summary>
        /// <param name="employeeInfoList">List of Employee Info</param>
        /// <returns>DataTable inserted</returns>
        private DataTable InsertEmployeeListOnDatatable(IList<CS_View_EmployeeInfo> employeeInfoList)
        {
            DataTable dtEmployee = _view.EmployeeDataTable;

            dtEmployee.Rows.Clear();

            List<DataRow> drList = new List<DataRow>();

            foreach (CS_View_EmployeeInfo employeeInfo in employeeInfoList)
            {
                DataRow dr = dtEmployee.NewRow();

                dr["EmployeeId"] = employeeInfo.EmployeeId;
                dr["DivisionId"] = employeeInfo.DivisionId;
                dr["DivisionName"] = employeeInfo.DivisionName;
                dr["DivisionState"] = employeeInfo.State;
                dr["EmployeeName"] = employeeInfo.EmployeeName;
                dr["Position"] = employeeInfo.Position;
                dr["Assigned"] = employeeInfo.Assigned;
                dr["JobId"] = employeeInfo.JobId;
                dr["JobNumber"] = employeeInfo.PrefixedNumber;
                dr["SelectAvailable"] = true;

                dtEmployee.Rows.Add(dr);
            }

            return dtEmployee;
        }

        /// <summary>
        /// Returns a list of Employees related to the divisions related to the Job
        /// </summary>
        /// <returns>Employee List</returns>
        private IList<CS_View_EmployeeInfo> ListAllEmployeeInfoByDivision()
        {
            IList<CS_View_EmployeeInfo> returnList = new List<CS_View_EmployeeInfo>();
            List<int> lstDivisionId = new List<int>();

            IList<CS_JobDivision> lstJobDivisions = _jobModel.ListAllDivisionsByJob(_view.JobID);
            if (lstJobDivisions.Count > 0)
            {
                lstDivisionId.AddRange(lstJobDivisions.Select(lstJobDivision => lstJobDivision.DivisionID));
                returnList = _employeeModel.ListAllEmployeeInfoByDivision(lstDivisionId);
            }

            return returnList;
        }

        /// <summary>
        /// Returns a list of Employees related to the reserves related to the Job
        /// </summary>
        /// <returns>Employee List</returns>
        private IList<CS_View_EmployeeInfo> ListAllEmployeeInfoByReserves()
        {
            IList<CS_View_EmployeeInfo> returnList = new List<CS_View_EmployeeInfo>();
            List<int> lstDivisionId = new List<int>();
            List<int> lstEmployeeId = new List<int>();
            IList<CS_View_ReserveList> reserveList = _view.ReserveListDatasource;
            if (reserveList.Count > 0)
            {
                lstDivisionId.AddRange(reserveList.Where(e => e.Equipment == 0 && e.DivisionId.HasValue).Select(e => e.DivisionId.Value).Distinct());
                lstEmployeeId.AddRange(reserveList.Where(e => e.Equipment == 0 && e.ResourceID.HasValue).Select(e => e.ResourceID.Value).Distinct());
                string[] divisionNames = reserveList.Select(e => e.DivisionName).Distinct().ToArray();

                returnList = _employeeModel.ListAllEmployeeInfoByDivisionAndEmployee(lstDivisionId, lstEmployeeId);
                _view.EmployeeFilterAdd = Globals.ResourceAllocation.EmployeeFilters.Division;
                _view.EmployeeFilterValueAdd = string.Join(", ", divisionNames);
            }

            return returnList;
        }

        /// <summary>
        /// Populates Shopping Cart with Initial Column Structure
        /// </summary>
        /// <returns>DataTable with columns structured</returns>
        private DataTable FillShoppingCartColumns()
        {
            DataTable dtShoppingCart = new DataTable();
            dtShoppingCart.Columns.AddRange(
                new DataColumn[] { 
                    new DataColumn("Id", typeof(int)), 
                    new DataColumn("EquipmentId", typeof(int)), 
                    new DataColumn("EmployeeId", typeof(int)), 
                    new DataColumn("EquipmentTypeId", typeof(int)), 
                    new DataColumn("DivisionId", typeof(int)),
                    new DataColumn("JobId", typeof(int)),
                    new DataColumn("Type", typeof(short)), 
                    new DataColumn("AssignmentType", typeof(string)),
                    new DataColumn("Name", typeof(string)), 
                    new DataColumn("Duration", typeof(int)),
                    new DataColumn("StartDateTime", typeof(DateTime)),
                    new DataColumn("UnitNumber", typeof(string))
                });

            return dtShoppingCart;
        }

        /// <summary>
        /// Get Shopping Cart List
        /// </summary>
        /// <returns>DataTable populated with Shopping Cart Info</returns>
        private DataTable GetShoppingCartDataTable()
        {
            DataTable dtShoppingCart = FillShoppingCartColumns();

            if (!_view.ResourceConversion)
                LoadShoppingCartWithReserves(dtShoppingCart);

            LoadShoppingCartWithResources(dtShoppingCart);
            return dtShoppingCart;
        }

        /// <summary>
        /// Fills the Shopping Cart List with Resource Entity
        /// </summary>
        /// <param name="dtShoppingCart">Shopping Cart List</param>
        /// <param name="csResource">Resource Entity to fill Shopping Cart</param>
        private void FillShoppingCartWithResourceInfo(DataTable dtShoppingCart, CS_Resource csResource)
        {
            DataRow newRow = dtShoppingCart.NewRow();

            newRow["Id"] = csResource.ID;
            if (csResource.EmployeeID.HasValue)
            {
                newRow["EmployeeId"] = csResource.EmployeeID.Value;
                newRow["UnitNumber"] = string.Empty;
            }
            if (csResource.EquipmentID.HasValue)
            {
                newRow["EquipmentID"] = csResource.EquipmentID.Value;
                if (csResource.CS_Equipment.ComboID.HasValue)
                    newRow["UnitNumber"] = csResource.CS_Equipment.CS_EquipmentCombo.Name + " - " + csResource.CS_Equipment.Name;
                else
                    newRow["UnitNumber"] = csResource.CS_Equipment.Name;
            }
            newRow["JobID"] = csResource.JobID;
            newRow["Type"] = (int)_type;
            newRow["Duration"] = csResource.Duration;
            newRow["StartDateTime"] = csResource.StartDateTime;
            newRow["Name"] = string.Format("Added Resource - {0} {1}", _divisionNumber, _name);
            newRow["DivisionId"] = (int)_divisionId;
            dtShoppingCart.Rows.Add(newRow);
        }

        /// <summary>
        /// Fills the Shopping Cart List with Reserve Entity
        /// </summary>
        /// <param name="dtShoppingCart">Shopping Cart List</param>
        /// <param name="csReserve">Reserve Entity to fill Shopping Cart</param>
        private void FillShoppingCartWithReserveInfo(DataTable dtShoppingCart, CS_Reserve csReserve)
        {
            DataRow newRow = dtShoppingCart.NewRow();
            newRow["Id"] = csReserve.ID;
            if (csReserve.EquipmentTypeID.HasValue)
            {
                newRow["EquipmentTypeId"] = csReserve.EquipmentTypeID.Value;
                newRow["UnitNumber"] = csReserve.CS_EquipmentType.Name;
            }
            if (csReserve.EmployeeID.HasValue)
            {
                newRow["EmployeeId"] = csReserve.EmployeeID.Value;
                newRow["UnitNumber"] = string.Empty;
            }
            newRow["Type"] = (int)_type;
            newRow["Duration"] = csReserve.Duration;
            newRow["StartDateTime"] = csReserve.StartDateTime;
            newRow["JobId"] = csReserve.JobID;
            if (csReserve.DivisionID.HasValue)
                newRow["DivisionId"] = csReserve.DivisionID;
            newRow["Name"] = string.Format("Reserved Resource - {0} {1}", _divisionNumber, _name);
            dtShoppingCart.Rows.Add(newRow);
        }

        /// <summary>
        /// Fill Shopping Cart List with Resource Entity
        /// </summary>
        /// <param name="dtShoppingCart">Shopping Cart List</param>
        private void LoadShoppingCartWithResources(DataTable dtShoppingCart)
        {
            IList<CS_Resource> lstResource = _resourceAllocationModel.ListResourcesByJob(_view.JobID);

            if (lstResource.Count() > 0)
            {
                foreach (CS_Resource csResource in lstResource)
                {
                    if (csResource.Type == (int)Globals.ResourceAllocation.ResourceType.Employee)
                    {
                        FillResourcesWithEmployeeInfo(csResource);

                        //For now, it only disables the button, later we'll implement a method to check if it is possible to add
                        //or if the equipment should be transfered
                        if (!_view.SelectedEmployeeAddList.ContainsKey(csResource.EmployeeID.ToString()))
                            _view.SelectedEmployeeAddList.Add(csResource.EmployeeID.ToString(), false);
                        else
                            _view.SelectedEmployeeAddList[csResource.EmployeeID.ToString()] = false;
                    }
                    else if (csResource.Type == (int)Globals.ResourceAllocation.ResourceType.Equipment)
                    {
                        FillResourcesWithEquipmentInfo(csResource);

                        //For now, it only disables the button, later we'll implement a method to check if it is possible to add
                        //or if the equipment should be transfered
                        if (!_view.SelectedEquipmentAddList.ContainsKey(csResource.EquipmentID.ToString()))
                            _view.SelectedEquipmentAddList.Add(csResource.EquipmentID.ToString(), false);
                        else
                            _view.SelectedEquipmentAddList[csResource.EquipmentID.ToString()] = false;
                    }

                    FillShoppingCartWithResourceInfo(dtShoppingCart, csResource);
                }
            }
        }

        /// <summary>
        /// Fill Shopping Cart List with Reserve Entity
        /// </summary>
        /// <param name="dtShoppingCart">Shopping Cart List</param>
        private void LoadShoppingCartWithReserves(DataTable dtShoppingCart)
        {
            IList<CS_Reserve> lstReserve = _resourceAllocationModel.ListReserveByJob(_view.JobID);

            foreach (CS_Reserve csReserve in lstReserve)
            {
                if (csReserve.Type.Equals((int)Globals.ResourceAllocation.ResourceType.Employee))
                {
                    FillReserveWithEmployeeInfo(csReserve);
                }
                else if (csReserve.Type.Equals((int)Globals.ResourceAllocation.ResourceType.Equipment))
                {
                    FillReserveWithEquipmentInfo(csReserve);
                }

                FillShoppingCartWithReserveInfo(dtShoppingCart, csReserve);
            }
        }

        /// <summary>
        /// Fills Resource Entity with Equipment Entity
        /// </summary>
        /// <param name="csResource">Resource Entity to be populated</param>
        private void FillResourcesWithEquipmentInfo(CS_Resource csResource)
        {
            _type = Globals.ResourceAllocation.Type.AddEquipment;
            CS_Equipment equipment = _equipmentModel.GetEquipment(csResource.EquipmentID.Value);
            if (null != equipment)
            {
                CS_Division division = equipment.CS_Division;//_divisionModel.GetDivision(equipment.DivisionID);
                if (null != division)
                {
                    _divisionId = division.ID;
                    _divisionNumber = division.Name;
                    if (division.StateID.HasValue)
                    {
                        CS_State state = division.CS_State;//_locationModel.GetState(division.StateID.Value);
                        if (null != state)
                            _divisionNumber += " " + state.Acronym;
                    }
                }
                _name = equipment.Description;
            }
        }

        /// <summary>
        /// Fills Resource Entity with Employee Entity
        /// </summary>
        /// <param name="csResource">Resource Entity to be populated</param>
        private void FillResourcesWithEmployeeInfo(CS_Resource csResource)
        {
            _type = Globals.ResourceAllocation.Type.AddEmployee;
            CS_Employee employeeEntity = _employeeModel.GetEmployee(csResource.EmployeeID.Value);
            if (null != employeeEntity)
            {
                if (employeeEntity.DivisionID.HasValue)
                {
                    CS_Division division = employeeEntity.CS_Division;//_divisionModel.GetDivision(employeeEntity.DivisionID.Value);
                    if (null != division)
                    {
                        _divisionNumber = division.Name;
                        if (division.StateID.HasValue)
                        {
                            CS_State state = division.CS_State;//_locationModel.GetState(division.StateID.Value);
                            if (null != state)
                                _divisionNumber += " " + state.Acronym;
                        }
                    }
                }
                _name = employeeEntity.FullName;
            }
        }

        /// <summary>
        /// Fills Reserve Entity with Employee Entity
        /// </summary>
        /// <param name="csReserve">Reserve Entity to be populated</param>
        private void FillReserveWithEmployeeInfo(CS_Reserve csReserve)
        {
            _type = Globals.ResourceAllocation.Type.ReserveEmployee;
            CS_Employee employeeEntity = _employeeModel.GetEmployee(csReserve.EmployeeID.Value);
            if (null != employeeEntity)
            {
                if (employeeEntity.DivisionID.HasValue)
                {
                    CS_Division division = employeeEntity.CS_Division;//_divisionModel.GetDivision(employeeEntity.DivisionID.Value);
                    if (null != division)
                    {
                        _divisionNumber = division.Name;
                        if (division.StateID.HasValue)
                        {
                            CS_State state = division.CS_State;//_locationModel.GetState(division.StateID.Value);
                            if (null != state)
                                _divisionNumber += " " + state.Acronym;
                        }
                    }
                }
                _name = employeeEntity.FullName;
            }
        }

        /// <summary>
        /// Fills Reserve Entity with Equipment Entity
        /// </summary>
        /// <param name="csReserve">Reserve Entity to be populated</param>
        private void FillReserveWithEquipmentInfo(CS_Reserve csReserve)
        {
            _type = Globals.ResourceAllocation.Type.ReserveEquipment;
            CS_EquipmentType equipmentTypeEntity = _equipmentModel.GetEquipmentType(csReserve.EquipmentTypeID.Value);
            if (null != equipmentTypeEntity)
            {
                if (csReserve.DivisionID.HasValue)
                {
                    CS_Division division = _divisionModel.GetDivision(csReserve.DivisionID.Value);
                    if (null != division)
                    {
                        _divisionNumber = division.Name;
                        if (division.StateID.HasValue)
                        {
                            CS_State state = division.CS_State;//_locationModel.GetState(division.StateID.Value);
                            if (null != state)
                                _divisionNumber += " " + state.Acronym;
                        }
                    }
                }
                _name = equipmentTypeEntity.Name;
            }
        }

        #endregion
    }
}
