using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Model;
using System.Web.UI.WebControls;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Business.ViewModel
{
    public class DefaultPageViewModel
    {
        #region [ Attributes ]

        /// <summary>
        /// Page that holds the controls
        /// </summary>
        IDefaultPageView _view;

        /// <summary>
        /// Gives access to Equipment Related Database Items
        /// </summary>
        EquipmentModel _equipmentModel;

        #endregion

        #region [ Constructors ]

        public DefaultPageViewModel(IDefaultPageView view)
        {
            _view = view;
            _equipmentModel = new EquipmentModel();
        }

        #endregion

        #region [ Methods ]

        #region [ Search Filters ]

        public void FillSearchFields()
        {
            FillDatePickers();
            FillContactInfo();
            FillJobInfo();
            FillLocationInfo();
            FillJobDescription();
            FillEquipmentType();
            FillResource();
        }

        /// <summary>
        /// Sets the default value of the Datepickers
        /// </summary>
        private void FillDatePickers()
        {
            DateTime date = DateTime.Now;
            _view.StartDate = date.AddDays(-5);
            _view.EndDate = date;
        }

        /// <summary>
        /// Fills Resource DropDown Items
        /// </summary>
        private void FillResource()
        {
            _view.ResourceDropDownItems.Add(new ListItem("", "None"));
            _view.ResourceDropDownItems.Add(new ListItem("All", "All"));
            _view.ResourceDropDownItems.Add(new ListItem("Status", "Status"));
            _view.ResourceDropDownItems.Add(new ListItem("Call Type", "CallType"));
            _view.ResourceDropDownItems.Add(new ListItem("Employee", "Employee"));
        }

        /// <summary>
        /// Fills Equipment Type DropDown Items
        /// </summary>
        private void FillEquipmentType()
        {
            _view.EquipmentTypeDropDownItems.Add(new ListItem("", "None"));
            _view.EquipmentTypeDropDownItems.Add(new ListItem("All", "All"));

            IList<CS_EquipmentType> typeList = _equipmentModel.ListAllEquipmentType();

            for (int i = 0; i < typeList.Count; i++)
            {
                ListItem li = new ListItem(typeList[i].CompleteName, typeList[i].ID.ToString());
                li.Attributes.Add("title", li.Text.ToString());
                _view.EquipmentTypeDropDownItems.Add(li);
            }
        }

        /// <summary>
        /// Fills Job Description DropDown Items
        /// </summary>
        private void FillJobDescription()
        {
            _view.JobDescriptionDropDownItems.Add(new ListItem("", "None"));
            _view.JobDescriptionDropDownItems.Add(new ListItem("All", "All"));
            _view.JobDescriptionDropDownItems.Add(new ListItem("# of engines", "Engines"));
            _view.JobDescriptionDropDownItems.Add(new ListItem("# of loads", "Loads"));
            _view.JobDescriptionDropDownItems.Add(new ListItem("# of empties", "Empties"));
            _view.JobDescriptionDropDownItems.Add(new ListItem("Lading", "Lading"));
            _view.JobDescriptionDropDownItems.Add(new ListItem("UN #", "UN"));
            _view.JobDescriptionDropDownItems.Add(new ListItem("STCC info", "STCC"));
            _view.JobDescriptionDropDownItems.Add(new ListItem("Hazmat", "Hazmat"));
        }

        /// <summary>
        /// Fills Location Info DropDown Items
        /// </summary>
        private void FillLocationInfo()
        {
            _view.LocationInfoDropDownItems.Add(new ListItem("", "None"));
            _view.LocationInfoDropDownItems.Add(new ListItem("All", "All"));
            _view.LocationInfoDropDownItems.Add(new ListItem("Country", "Country"));
            _view.LocationInfoDropDownItems.Add(new ListItem("State", "State"));
            _view.LocationInfoDropDownItems.Add(new ListItem("City", "City"));
            _view.LocationInfoDropDownItems.Add(new ListItem("Zip code", "ZipCode"));
            _view.LocationInfoDropDownItems.Add(new ListItem("Site name", "SiteName"));
            _view.LocationInfoDropDownItems.Add(new ListItem("Alternate location", "Alternate"));
        }

        /// <summary>
        /// Fills Job Info DropDown Items
        /// </summary>
        private void FillJobInfo()
        {
            _view.JobInfoDropDownItems.Add(new ListItem("", "None"));
            _view.JobInfoDropDownItems.Add(new ListItem("All", "All"));
            _view.JobInfoDropDownItems.Add(new ListItem("Initial call date", "InitialCallDate"));
            _view.JobInfoDropDownItems.Add(new ListItem("Initial call time", "InitialCallTime"));
            _view.JobInfoDropDownItems.Add(new ListItem("Job status", "JobStatus"));
            _view.JobInfoDropDownItems.Add(new ListItem("Division", "Division"));
            _view.JobInfoDropDownItems.Add(new ListItem("Price type", "PriceType"));
            _view.JobInfoDropDownItems.Add(new ListItem("Job category", "JobCategory"));
            _view.JobInfoDropDownItems.Add(new ListItem("Job type", "JobType"));
            _view.JobInfoDropDownItems.Add(new ListItem("Job action", "JobAction"));
            _view.JobInfoDropDownItems.Add(new ListItem("Job start date", "JobStartDate"));
            _view.JobInfoDropDownItems.Add(new ListItem("Job end date", "JobEndDate"));
            _view.JobInfoDropDownItems.Add(new ListItem("Preset date", "PresetDate"));
            _view.JobInfoDropDownItems.Add(new ListItem("Preset time", "PresetTime"));
            _view.JobInfoDropDownItems.Add(new ListItem("Lost reason", "LostJobReason"));
            _view.JobInfoDropDownItems.Add(new ListItem("Lost competitor", "LostCompetitor"));
            _view.JobInfoDropDownItems.Add(new ListItem("Lost Customer Contact", "LostPOC"));
            _view.JobInfoDropDownItems.Add(new ListItem("Interim billing frequency", "Frequency"));
        }

        /// <summary>
        /// Fills Contact Info DropDown Items
        /// </summary>
        private void FillContactInfo()
        {
            _view.ContactInfoDropDownItems.Add(new ListItem("", "None"));
            _view.ContactInfoDropDownItems.Add(new ListItem("All", "All"));
            _view.ContactInfoDropDownItems.Add(new ListItem("Company", "Customer"));
            _view.ContactInfoDropDownItems.Add(new ListItem("Primary Contact", "InitialCustomerContact"));
            _view.ContactInfoDropDownItems.Add(new ListItem("On-Site", "EIC"));
            _view.ContactInfoDropDownItems.Add(new ListItem("Secondary Contact", "Secondary"));
            _view.ContactInfoDropDownItems.Add(new ListItem("Bill To", "BillTo"));
            _view.ContactInfoDropDownItems.Add(new ListItem("Division", "Division"));
            _view.ContactInfoDropDownItems.Add(new ListItem("Hulcher Contact", "POC"));
            _view.ContactInfoDropDownItems.Add(new ListItem("Project Manager", "ProjectManager"));
        }

        #endregion

        #region [ Search Execution ]

        public void SearchJob()
        {
            using (JobModel model = new JobModel())
            {
                IList<int> searchJobListNonNullable = new List<int>();
                IList<int?> searchJobListNullable = model.ListJobBySearchCriteria(_view.SearchFiltersVO);
                foreach (var item in searchJobListNullable)
                {
                    if (item.HasValue)
                        searchJobListNonNullable.Add(item.Value);
                }
                _view.SearchJobList = searchJobListNonNullable;

                _view.IsSearchJobListEmpty = (_view.SearchJobList.Count == 0);
            }
        }

        #endregion

        #endregion
    }
}
