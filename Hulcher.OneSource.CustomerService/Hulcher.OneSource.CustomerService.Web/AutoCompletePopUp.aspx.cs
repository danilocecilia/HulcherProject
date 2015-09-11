using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Business.WebControls.ServerControls;

namespace Hulcher.OneSource.CustomerService.Web
{
    public partial class AutoCompletePopUp : System.Web.UI.Page
    {
        private object _viewStateList;
        private object ViewStateList
        {
            get
            {
                //if (null == ViewState["ViewStateList"])
                //    return null;
                //return ViewState["ViewStateList"];
                return _viewStateList;
            }
            //set { ViewState["ViewStateList"] = value; }
            set { _viewStateList = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            Title = Request.QueryString.Get("WindowTitle");

            BindList();

            GenerateColumns();
            DataBindGridView();
        }

        private void BindList()
        {
            CustomerModel customerModel;
            EmployeeModel employeeModel;
            LocationModel locationModel;
            DivisionModel divisionModel;
            JobModel jobModel;
            CallLogModel callLogModel;
            EquipmentModel equipmentModel;

            IList<CS_Customer> customerDataSource;
            IList<CS_Contact> contactDataSource;
            IList<CS_Employee> employeeDataSource;
            IList<CS_JobStatus> jobStatusDataSource;
            IList<CS_City> cityDataSource;
            IList<CS_Division> divisionDataSource;
            IList<CS_PriceType> priceTypeDataSource;
            IList<CS_JobAction> jobActionDataSource;
            IList<CS_Job> jobNumberDataSource;
            IList<CS_ZipCode> zipCodeDataSource;
            IList<CS_State> stateDataSource;
            IList<CS_CallType> callTypeDataSource;
            IList<CS_EquipmentType> equipmentTypeDataSource;
            IList<CS_Equipment> equipmentDataSource;
            IList<CS_LocalEquipmentType> localEquipmentTypeDataSource;

            switch (Request.QueryString["AutoCompleteSource"])
            {
                case "Customer":
                    customerModel = new CustomerModel();
                    customerDataSource = customerModel.ListAllCustomers().OrderBy(e => e.Name).ToList();
                    ViewStateList = customerDataSource;

                    break;

                case "Contact":
                    customerModel = new CustomerModel();
                    contactDataSource = customerModel.ListAllFilteredContactsByName(long.Parse(Request.QueryString["FilterId"]), "").OrderBy(e => e.LastName).ThenBy(e => e.Name).ToList();
                    ViewStateList = contactDataSource;

                    break;

                case "DynamicsContact":
                    customerModel = new CustomerModel();
                    contactDataSource = customerModel.ListFilteredContactsByName(long.Parse(Request.QueryString["FilterId"]), true, "").OrderBy(e => e.Attn).ToList();
                    ViewStateList = contactDataSource;

                    break;

                case "CustomerServiceContact":
                    customerModel = new CustomerModel();
                    contactDataSource = customerModel.ListFilteredContactsByName(long.Parse(Request.QueryString["FilterId"]), false, "").OrderBy(e => e.LastName).ThenBy(e => e.Name).ToList();
                    ViewStateList = contactDataSource;

                    break;

                case "Employee":
                    employeeModel = new EmployeeModel();

                    if (Request.QueryString["FilterId"] != "0")
                        employeeDataSource = employeeModel.ListAllFilteredEmployee(long.Parse(Request.QueryString["FilterId"])).OrderBy(e => e.CS_Division.Name).ThenBy(e => e.FullName).ToList();
                    else
                        employeeDataSource = employeeModel.ListAllEmployee().OrderBy(e => e.CS_Division.Name).ThenBy(e => e.FullName).ToList();
                    ViewStateList = employeeDataSource;

                    break;
                case "EmployeeWithDivision":
                    employeeModel = new EmployeeModel();

                    if (Request.QueryString["FilterId"] != "0")
                        employeeDataSource = employeeModel.ListAllFilteredEmployee(long.Parse(Request.QueryString["FilterId"])).OrderBy(e => e.CS_Division.Name).ThenBy(e => e.FullName).ToList();
                    else
                        employeeDataSource = employeeModel.ListAllEmployee().OrderBy(e => e.CS_Division, new Globals.JobRecord.EmployeeComparer()).ThenBy(e => e.FullName).ToList();
                    ViewStateList = employeeDataSource;
                    break;

                case "JobStatus":
                    jobModel = new JobModel();

                    if (!string.IsNullOrEmpty(Request.QueryString.Get("FilterId")) && Request.QueryString["FilterId"] != "0")
                    {
                        jobStatusDataSource = new List<CS_JobStatus>();
                        jobStatusDataSource.Add(jobModel.GetJobStatus(int.Parse(Request.QueryString["FilterId"])));
                    }
                    else
                        jobStatusDataSource = jobModel.ListAllJobStatus();
                    ViewStateList = jobStatusDataSource;
                    break;
                case "JobStatusJobRecord":
                    jobModel = new JobModel();

                    if (!string.IsNullOrEmpty(Request.QueryString.Get("FilterId")) && Request.QueryString["FilterId"] != "0" && Request.QueryString["FilterId"] != Globals.JobRecord.JobStatus.ClosedHold.ToString())
                    {
                        jobStatusDataSource = new List<CS_JobStatus>();
                        jobStatusDataSource.Add(jobModel.GetJobStatus(int.Parse(Request.QueryString["FilterId"])));
                    }
                    else
                    {
                        jobStatusDataSource = jobModel.ListAllJobStatus();

                        if (jobStatusDataSource.Count > 0)
                        {
                            CS_JobStatus jobStatus = jobStatusDataSource.Where(w => w.ID == (int)Globals.JobRecord.JobStatus.ClosedHold).FirstOrDefault();
                            if (null != jobStatus)
                                jobStatusDataSource.Remove(jobStatus);
                        }
                    }
                    ViewStateList = jobStatusDataSource;
                    break;
                case "State":
                    locationModel = new LocationModel();
                    if (!string.IsNullOrEmpty(Request.QueryString.Get("FilterId")) && Request.QueryString.Get("FilterId") != "0")
                        stateDataSource = locationModel.GetStateByCountryId(int.Parse(Request.QueryString.Get("FilterId")));
                    else
                        stateDataSource = locationModel.ListAllStates();
                    ViewStateList = stateDataSource;
                    break;
                case "StateByDivision":
                    locationModel = new LocationModel();
                    stateDataSource = locationModel.ListAllStatesByAllocatedDivision();

                    ViewStateList = stateDataSource;
                    break;
                case "City":
                    locationModel = new LocationModel();

                    if (!string.IsNullOrEmpty(Request.QueryString.Get("FilterId")) && Request.QueryString.Get("FilterId") != "0")
                        cityDataSource = locationModel.GetCityByState(int.Parse(Request.QueryString.Get("FilterId"))).OrderBy(e => e.Name).ToList();
                    else
                        cityDataSource = locationModel.ListAllCities().OrderBy(e => e.Name).ToList();
                    ViewStateList = cityDataSource;
                    break;

                case "Division":
                    divisionModel = new DivisionModel();
                    divisionDataSource = divisionModel.ListAllDivision();
                    ViewStateList = divisionDataSource;
                    break;
                case "PriceType":
                    jobModel = new JobModel();
                    priceTypeDataSource = jobModel.ListAllPriceTypes();
                    ViewStateList = priceTypeDataSource;
                    break;

                case "JobAction":
                    jobModel = new JobModel();
                    jobActionDataSource = jobModel.ListAllJobActions().OrderBy(w => w.Description).ToList();
                    ViewStateList = jobActionDataSource;
                    break;
                case "ZipCode":
                    locationModel = new LocationModel();
                    if (!string.IsNullOrEmpty(Request.QueryString.Get("FilterId")) && Request.QueryString.Get("FilterId") != "0")
                        zipCodeDataSource = locationModel.GetZipCodeByCityId(int.Parse(Request.QueryString.Get("FilterId"))).OrderBy(e => e.ZipCodeNameEdited).ToList();
                    else
                        zipCodeDataSource = locationModel.ListAllZipCodes();
                    ViewStateList = zipCodeDataSource;
                    break;
                case "JobNumber":
                    jobModel = new JobModel();

                    if (!string.IsNullOrEmpty(Request.QueryString.Get("FilterId")) && Request.QueryString["FilterId"] != "0")
                    {
                        jobNumberDataSource = new List<CS_Job>();
                        jobNumberDataSource.Add(jobModel.GetJobById(int.Parse(Request.QueryString["FilterId"])));
                    }
                    else
                        jobNumberDataSource = jobModel.ListAllJobs();

                    ViewStateList = jobNumberDataSource;
                    break;
                case "JobNumberByStatus":
                    jobModel = new JobModel();

                    if (!string.IsNullOrEmpty(Request.QueryString.Get("FilterId")) && Request.QueryString["FilterId"] != "0")
                    {
                        jobNumberDataSource = new List<CS_Job>();
                        jobNumberDataSource = jobModel.ListAllJobsByNumber("", Request.QueryString["FilterId"]);
                    }
                    else
                        jobNumberDataSource = jobModel.ListAllJobs();

                    ViewStateList = jobNumberDataSource;
                    break;
                case "JobNumberWithGeneral":
                    jobModel = new JobModel();

                    if (!string.IsNullOrEmpty(Request.QueryString.Get("FilterId")) && Request.QueryString["FilterId"] != "0")
                    {
                        jobNumberDataSource = new List<CS_Job>();
                        jobNumberDataSource.Add(jobModel.GetJobById(int.Parse(Request.QueryString["FilterId"])));
                    }
                    else
                    {
                        List<CS_Job> source = new List<CS_Job>();

                        source.Add(jobModel.GetGeneralJob());
                        source.AddRange(jobModel.ListAllJobs());

                        jobNumberDataSource = source;
                    }

                    ViewStateList = jobNumberDataSource;
                    break;
                case "BillableJobNumber":
                    jobModel = new JobModel();

                    if (!string.IsNullOrEmpty(Request.QueryString.Get("FilterId")) && Request.QueryString["FilterId"] != "0")
                    {
                        jobNumberDataSource = new List<CS_Job>();
                        jobNumberDataSource.Add(jobModel.GetJobById(int.Parse(Request.QueryString["FilterId"])));
                    }
                    else
                        jobNumberDataSource = jobModel.ListAllBillableJobs();

                    ViewStateList = jobNumberDataSource;
                    break;
                case "CallType":
                    callLogModel = new CallLogModel();

                    if (!string.IsNullOrEmpty(Request.QueryString.Get("FilterId")) && Request.QueryString["FilterId"] != "0")
                    {
                        callTypeDataSource = new List<CS_CallType>();
                        callTypeDataSource.Add(callLogModel.GetCallType(int.Parse(Request.QueryString["FilterId"])));
                    }
                    else
                        callTypeDataSource = callLogModel.ListAllCallType();

                    ViewStateList = callTypeDataSource;
                    break;
                case "EquipmentType":
                    equipmentModel = new EquipmentModel();

                    if (!string.IsNullOrEmpty(Request.QueryString.Get("FilterId")) && Request.QueryString["FilterId"] != "0")
                    {
                        equipmentTypeDataSource = new List<CS_EquipmentType>();
                        equipmentTypeDataSource.Add(equipmentModel.GetEquipmentType(int.Parse(Request.QueryString["FilterId"])));
                    }
                    else
                        equipmentTypeDataSource = equipmentModel.ListAllEquipmentType();

                    ViewStateList = equipmentTypeDataSource;
                    break;
                case "Equipment":
                    equipmentModel = new EquipmentModel();

                    if (!string.IsNullOrEmpty(Request.QueryString.Get("FilterId")) && Request.QueryString["FilterId"] != "0")
                    {
                        equipmentDataSource = new List<CS_Equipment>();
                        equipmentDataSource = equipmentModel.GetEquipmentByEqType(int.Parse(Request.QueryString["FilterId"])).OrderBy(w => w.Name).ToList();
                    }
                    else
                        equipmentDataSource = equipmentModel.ListAllEquipment().OrderBy(w => w.Name).ToList(); ;

                    ViewStateList = equipmentDataSource;
                    break;
                case "LocalEquipmentType":
                    equipmentModel = new EquipmentModel();

                    if (!string.IsNullOrEmpty(Request.QueryString.Get("FilterId")) && Request.QueryString["FilterId"] != "0")
                    {
                        localEquipmentTypeDataSource = new List<CS_LocalEquipmentType>();
                        localEquipmentTypeDataSource.Add(equipmentModel.GetLocalEquipmentTypeByID(int.Parse(Request.QueryString["FilterId"])));
                    }
                    else
                        localEquipmentTypeDataSource = equipmentModel.ListAllLocalEquipmentType();

                    ViewStateList = localEquipmentTypeDataSource;
                    break;
                case "ProjectManager":
                    employeeModel = new EmployeeModel();

                    employeeDataSource = employeeModel.ListAllEmployeeProjectManager();

                    ViewStateList = employeeDataSource;

                    break;
                default:
                    break;
            }
        }

        private void DataBindGridView()
        {
            gvList.DataSource = ViewStateList;
            gvList.DataBind();
        }

        private void GenerateColumns()
        {
            string[] headerList = Request.QueryString["HeaderList"].Split(',');
            string[] valueList = Request.QueryString["ValueList"].Split(',');

            for (int i = 0; i < headerList.Length; i++)
            {
                CompositeBoundField column = new CompositeBoundField();

                column.HeaderText = headerList[i];
                column.DataField = valueList[i];
                column.Visible = false;

                gvList.Columns.Insert(i, column);
            }

            TemplateField templateField = new TemplateField();

            templateField.HeaderTemplate = new Globals.DynamicGridViewButtonTemplate("", "", DataControlRowType.Header);

            switch (Request.QueryString["AutoCompleteSource"])
            {
                case "Customer":
                    templateField.ItemTemplate = new Globals.DynamicGridViewButtonTemplate("Name", "ID", Request.QueryString.Get("ReturnValueId"), Request.QueryString.Get("ReturnTextId"), Request.QueryString.Get("ReturnHfId"), Request.QueryString.Get("ParentField"), DataControlRowType.DataRow);
                    break;
                case "Contact":
                    templateField.ItemTemplate = new Globals.DynamicGridViewButtonTemplate("FullContactInformation", "ID", Request.QueryString.Get("ReturnValueId"), Request.QueryString.Get("ReturnTextId"), Request.QueryString.Get("ReturnHfId"), Request.QueryString.Get("ParentField"), DataControlRowType.DataRow);
                    break;
                case "DynamicsContact":
                    templateField.ItemTemplate = new Globals.DynamicGridViewButtonTemplate("FullContactInformation", "ID", Request.QueryString.Get("ReturnValueId"), Request.QueryString.Get("ReturnTextId"), Request.QueryString.Get("ReturnHfId"), Request.QueryString.Get("ParentField"), DataControlRowType.DataRow);
                    break;
                case "CustomerServiceContact":
                    templateField.ItemTemplate = new Globals.DynamicGridViewButtonTemplate("FullContactInformation", "ID", Request.QueryString.Get("ReturnValueId"), Request.QueryString.Get("ReturnTextId"), Request.QueryString.Get("ReturnHfId"), Request.QueryString.Get("ParentField"), DataControlRowType.DataRow);
                    break;
                case "Employee":
                    templateField.ItemTemplate = new Globals.DynamicGridViewButtonTemplate("DivisionAndFullName", "ID", Request.QueryString.Get("ReturnValueId"), Request.QueryString.Get("ReturnTextId"), Request.QueryString.Get("ReturnHfId"), Request.QueryString.Get("ParentField"), DataControlRowType.DataRow);
                    break;
                case "EmployeeWithDivision":
                    templateField.ItemTemplate = new Globals.DynamicGridViewButtonTemplate("DivisionAndFullName", "ID,DivisionName", Request.QueryString.Get("ReturnValueId"), Request.QueryString.Get("ReturnTextId"), Request.QueryString.Get("ReturnHfId"), Request.QueryString.Get("ParentField"), DataControlRowType.DataRow);
                    break;
                case "JobStatus":
                    templateField.ItemTemplate = new Globals.DynamicGridViewButtonTemplate("Description", "ID", Request.QueryString.Get("ReturnValueId"), Request.QueryString.Get("ReturnTextId"), Request.QueryString.Get("ReturnHfId"), Request.QueryString.Get("ParentField"), DataControlRowType.DataRow);
                    break;
                case "JobStatusJobRecord":
                    templateField.ItemTemplate = new Globals.DynamicGridViewButtonTemplate("Description", "ID", Request.QueryString.Get("ReturnValueId"), Request.QueryString.Get("ReturnTextId"), Request.QueryString.Get("ReturnHfId"), Request.QueryString.Get("ParentField"), DataControlRowType.DataRow);
                    break;
                case "Division":
                    templateField.ItemTemplate = new Globals.DynamicGridViewButtonTemplate("ExtendedDivisionName", "ID", Request.QueryString.Get("ReturnValueId"), Request.QueryString.Get("ReturnTextId"), Request.QueryString.Get("ReturnHfId"), Request.QueryString.Get("ParentField"), DataControlRowType.DataRow);
                    break;
                case "PriceType":
                    templateField.ItemTemplate = new Globals.DynamicGridViewButtonTemplate("Description", "ID", Request.QueryString.Get("ReturnValueId"), Request.QueryString.Get("ReturnTextId"), Request.QueryString.Get("ReturnHfId"), Request.QueryString.Get("ParentField"), DataControlRowType.DataRow);
                    break;
                case "JobAction":
                    templateField.ItemTemplate = new Globals.DynamicGridViewButtonTemplate("Description", "ID", Request.QueryString.Get("ReturnValueId"), Request.QueryString.Get("ReturnTextId"), Request.QueryString.Get("ReturnHfId"), Request.QueryString.Get("ParentField"), DataControlRowType.DataRow);
                    break;
                case "State":
                    templateField.ItemTemplate = new Globals.DynamicGridViewButtonTemplate("AcronymName", "ID", Request.QueryString.Get("ReturnValueId"), Request.QueryString.Get("ReturnTextId"), Request.QueryString.Get("ReturnHfId"), Request.QueryString.Get("ParentField"), DataControlRowType.DataRow);
                    break;
                case "City":
                    templateField.ItemTemplate = new Globals.DynamicGridViewButtonTemplate("Name", "ID", Request.QueryString.Get("ReturnValueId"), Request.QueryString.Get("ReturnTextId"), Request.QueryString.Get("ReturnHfId"), Request.QueryString.Get("ParentField"), DataControlRowType.DataRow);
                    break;
                case "ZipCode":
                    templateField.ItemTemplate = new Globals.DynamicGridViewButtonTemplate("ZipCodeNameEdited", "ID", Request.QueryString.Get("ReturnValueId"), Request.QueryString.Get("ReturnTextId"), Request.QueryString.Get("ReturnHfId"), Request.QueryString.Get("ParentField"), DataControlRowType.DataRow);
                    break;
                case "JobNumber":
                case "JobNumberWithGeneral":
                case "JobNumberByStatus":
                case "BillableJobNumber":
                    templateField.ItemTemplate = new Globals.DynamicGridViewButtonTemplate("PrefixedNumber", "ID", Request.QueryString.Get("ReturnValueId"), Request.QueryString.Get("ReturnTextId"), Request.QueryString.Get("ReturnHfId"), Request.QueryString.Get("ParentField"), DataControlRowType.DataRow);
                    break;
                case "CallType":
                    templateField.ItemTemplate = new Globals.DynamicGridViewButtonTemplate("Description", "ID", Request.QueryString.Get("ReturnValueId"), Request.QueryString.Get("ReturnTextId"), Request.QueryString.Get("ReturnHfId"), Request.QueryString.Get("ParentField"), DataControlRowType.DataRow);
                    break;
                case "ProjectManager":
                    templateField.ItemTemplate = new Globals.DynamicGridViewButtonTemplate("DivisionAndFullName", "ID", Request.QueryString.Get("ReturnValueId"), Request.QueryString.Get("ReturnTextId"), Request.QueryString.Get("ReturnHfId"), Request.QueryString.Get("ParentField"), DataControlRowType.DataRow);
                    break;
                default:
                    templateField.ItemTemplate = new Globals.DynamicGridViewButtonTemplate("Name", "ID", Request.QueryString.Get("ReturnValueId"), Request.QueryString.Get("ReturnTextId"), Request.QueryString.Get("ReturnHfId"), Request.QueryString.Get("ParentField"), DataControlRowType.DataRow);
                    break;
            }

            gvList.Columns.Add(templateField);
        }
    }
}
