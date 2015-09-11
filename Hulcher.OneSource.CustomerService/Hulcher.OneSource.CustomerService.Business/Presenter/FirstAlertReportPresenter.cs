using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Model;

using Microsoft.Practices.EnterpriseLibrary.Logging;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    public class FirstAlertReportPresenter
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of First Alert Report Interface View
        /// </summary>
        private IFirstAlertReportView _view;

        /// <summary>
        /// Instance of First Alert Model class
        /// </summary>
        private FirstAlertModel _model;

        /// <summary>
        /// Instance of Settings Model class
        /// </summary>
        private SettingsModel _settingsModel;

        #endregion

        #region [ Constructors ]

        public FirstAlertReportPresenter(IFirstAlertReportView view)
        {
            _view = view;

            _model = new FirstAlertModel();
            _settingsModel = new SettingsModel();
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Configures the receipts that will receive the Email
        /// </summary>
        public void ConfigureEmail()
        {
            try
            {
                _view.ReceiptsList = _settingsModel.GetFirstAlertEmailList();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to get the email receipts!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to get the email receipts.", false);
            }
        }

        /// <summary>
        /// Binds the Report
        /// </summary>
        public void BindReport()
        {
            try
            {
                CS_FirstAlert firstAlert = _model.GetFirstAlertByIdForReport(_view.FirstAlertId);
                IList<CS_FirstAlertPerson> persons = _model.ListFirstAlertPersonByFirstAlertID(_view.FirstAlertId);
                IList<CS_View_FirstAlertReportHulcherVehicles> hulcherVehicles = _model.ListFirstAlertHulcherVehiclesByFirstAlertIDForReport(_view.FirstAlertId);
                IList<CS_View_FirstAlertReportOtherVehicle> otherVehicles = _model.ListFirstAlertOtherVehiclesByFirstAlertIDForReport(_view.FirstAlertId);
                IList<CS_View_FirstAlertReportContactPersonal> contactPersonal = _model.ListFirstAlertContactPersonalForReport(_view.FirstAlertId);
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("Source", firstAlert.CS_Job.PrefixedJobNumber);
                if (firstAlert.JobID == Globals.GeneralLog.ID)
                    parameters.Add("SourceLocation", string.Empty);
                else
                    parameters.Add("SourceLocation", string.Format("{0}, {1}",
                        firstAlert.CS_Job.CS_LocationInfo.CS_City.Name,
                        firstAlert.CS_Job.CS_LocationInfo.CS_State.Acronym));

                StringBuilder divisionBuilder = new StringBuilder();
                for (int i = 0; i < firstAlert.CS_FirstAlertDivision.Count; i++)
                {
                    if (firstAlert.CS_FirstAlertDivision.ElementAt(i).Active)
                    {
                        divisionBuilder.Append(firstAlert.CS_FirstAlertDivision.ElementAt(i).CS_Division.Name);
                        if (i != firstAlert.CS_FirstAlertDivision.Count - 1)
                            divisionBuilder.Append(", ");
                    }
                }
                parameters.Add("Divisions", divisionBuilder.ToString());
                parameters.Add("Date", firstAlert.Date.ToString("MM/dd/yyyy"));
                parameters.Add("Time", firstAlert.Date.ToString("HH:mm"));
                parameters.Add("ReportedBy", firstAlert.ReportedBy);
                if (null != firstAlert.CS_Employee_CompletedBy)
                    parameters.Add("CompletedBy", firstAlert.CS_Employee_CompletedBy.FullName);
                else
                    parameters.Add("CompletedBy", string.Empty);
                parameters.Add("Details", firstAlert.Details);
                parameters.Add("PoliceReport", firstAlert.HasPoliceReport.ToString());
                parameters.Add("PoliceAgency", firstAlert.PoliceAgency);
                parameters.Add("FirstAlertId", _view.FirstAlertId.ToString());
                parameters.Add("FirstAlertNumber", firstAlert.Number.ToString());
                StringBuilder firstAlertType = new StringBuilder();
                for (int i = 0; i < firstAlert.CS_FirstAlertFirstAlertType.Count; i++)
                {
                    if (firstAlert.CS_FirstAlertFirstAlertType.ElementAt(i).Active)
                    {
                        firstAlertType.Append(firstAlert.CS_FirstAlertFirstAlertType.ElementAt(i).CS_FirstAlertType.Description);
                        if (i != firstAlert.CS_FirstAlertFirstAlertType.Count - 1)
                            firstAlertType.Append(", ");
                    }
                }
                parameters.Add("FirstAlertType", firstAlertType.ToString());

                if (null != firstAlert.CS_Customer)
                    parameters.Add("Customer", firstAlert.CS_Customer.FullCustomerInformation);
                else
                    parameters.Add("Customer", string.Empty);

                if (null != firstAlert.CS_Employee_InCharge)
                    parameters.Add("EIC", firstAlert.CS_Employee_InCharge.FullName);
                else
                    parameters.Add("EIC", string.Empty);

                if (null != firstAlert.CS_City)
                    parameters.Add("City", firstAlert.CS_City.Name);
                else
                    parameters.Add("City", string.Empty);

                if (null != firstAlert.CS_State)
                    parameters.Add("State", firstAlert.CS_State.Name);
                else
                    parameters.Add("State", string.Empty);

                if (null != firstAlert.CS_Country)
                    parameters.Add("Country", firstAlert.CS_Country.Name);
                else
                    parameters.Add("Country", string.Empty);

                parameters.Add("ReportNumber", firstAlert.PoliceReportNumber);

                if (null != hulcherVehicles)
                    parameters.Add("VehiclesCount", hulcherVehicles.Count.ToString());
                else
                    parameters.Add("VehiclesCount", "0");

                if (null != otherVehicles)
                    parameters.Add("OtherVehiclesCount", otherVehicles.Count.ToString());
                else
                    parameters.Add("OtherVehiclesCount", "0");

                if (null != persons)
                    parameters.Add("PersonCount", persons.Count.ToString());
                else
                    parameters.Add("PersonCount", "0");

                if (null != contactPersonal)
                    parameters.Add("ContactPersonalCount", contactPersonal.Count.ToString());
                else
                    parameters.Add("ContactPersonalCount", "0");

                for (int i = 0; i < persons.Count; i++)
                {
                    if (null != persons[i].MedicalSeverity)
                    {
                        Globals.FirstAlert.MedicalSeverity severity = (Globals.FirstAlert.MedicalSeverity)persons[i].MedicalSeverity;

                        switch (severity)
                        {
                            case Globals.FirstAlert.MedicalSeverity.MedicalTreatment:
                                persons[i].MedicalSeverityDescription = "Medical Treatment";
                                break;
                            case Globals.FirstAlert.MedicalSeverity.FirstAid:
                                persons[i].MedicalSeverityDescription = "First Aid";
                                break;
                            case Globals.FirstAlert.MedicalSeverity.Fatality:
                                persons[i].MedicalSeverityDescription = "Fatality";
                                break;
                            case Globals.FirstAlert.MedicalSeverity.NoneOfAbove:
                                persons[i].MedicalSeverityDescription = "None of above";
                                break;
                            default:
                                persons[i].MedicalSeverityDescription = string.Empty;
                                break;
                        }
                    }
                }

                _view.FirsAlertReportDataSource = new List<CS_FirstAlert>() { firstAlert };
                _view.FirstAlertReportHulcherPersonsDataSource = persons.Where(e => e.IsHulcherEmployee).ToList();
                _view.FirstAlertReportNonHulcherPersonsDataSource = persons.Where(e => e.IsHulcherEmployee == false).ToList();
                _view.FirstAlertReportHulcherVehicleDataSource = hulcherVehicles;
                _view.FirstAlertReportOtherVehicleDataSource = otherVehicles;
                _view.FirstAlertReportContactPersonalDataSource = contactPersonal;

                _view.ReportParameters = parameters;
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to bind the Report!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to bind the Report.", false);
            }
        }

        #endregion
    }
}
