using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Core.Utils;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.DataContext.VO;
using Hulcher.OneSource.CustomerService.Core;
using System.Transactions;

namespace Hulcher.OneSource.CustomerService.Business.ViewModel
{
    public class EmailViewModel : IDisposable
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of the Email View Interface
        /// </summary>
        private IEmailView _view;

        /// <summary>
        /// Instance of the CallLog Model
        /// </summary>
        private CallLogModel _callLogModel;

        /// <summary>
        /// Instance of the CallCriteria Model
        /// </summary>
        private CallCriteriaModel _callCriteriaModel;

        /// <summary>
        /// Instance of the Email Model
        /// </summary>
        private EmailModel _emailModel;

        private JobModel _jobModel;


        #endregion

        #region [ Constructors ]

        public EmailViewModel(IEmailView view)
        {
            _view = view;
            _callLogModel = new CallLogModel();
            _callCriteriaModel = new CallCriteriaModel();
            _emailModel = new EmailModel();
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Format the body and subject to be set on the page
        /// </summary>
        public void FormatCallLogHistory()
        {
            List<CS_CallLog> lstCallLog = _callLogModel.ListCallLogByIdList(_view.CallLogListId).ToList();

            if (lstCallLog.Count == 1 && lstCallLog.FirstOrDefault().CallTypeID == (int)Globals.CallEntry.CallType.InitialLog)
            {
                _view.JobID = lstCallLog.FirstOrDefault().CS_Job.ID;
            }
            else if (lstCallLog.Count > 0)
            {                
                string callType = string.Empty;
                if (lstCallLog.Count == 1)
                    callType = lstCallLog.FirstOrDefault().CS_CallType.Description;
                else
                    callType = "Multiple Call Types";

                string subject = _callCriteriaModel.GenerateSubjectForCallCriteria(
                    lstCallLog.FirstOrDefault().CS_Job,
                    lstCallLog.FirstOrDefault().CS_Job.CS_JobInfo,
                    lstCallLog.FirstOrDefault().CS_Job.CS_CustomerInfo,
                    lstCallLog.FirstOrDefault().CS_Job.CS_LocationInfo,
                    callType);

                _view.EmailSubject = subject;
                _view.EmailBody = _callCriteriaModel.GenerateBodyCallLogEmailTable(lstCallLog);
            }
        }

        public void ListReceiptsByCallLog()
        {
            using (_callLogModel = new CallLogModel())
            {
                _view.Receipts = _callLogModel.ListCallCriteriaEmails(_view.CallLogListId);
            }
        }

        public void JobRecordEmailFilling()
        {
            _jobModel = new JobModel();
            CS_Job job = _jobModel.GetJobById(_view.JobID.Value);



            this._jobModel = new JobModel()
                    {
                        NewJob = job,
                        NewCustomer = job.CS_CustomerInfo,
                        NewJobDivision = job.CS_JobDivision.ToList(),
                        NewPresetInfo = job.CS_PresetInfo,
                        NewLostJobInfo = job.CS_LostJobInfo,
                        NewSpecialPricing = job.CS_SpecialPricing,
                        NewJobInfo = job.CS_JobInfo,
                        NewLocationInfo = job.CS_LocationInfo,
                        NewJobDescription = job.CS_JobDescription,
                        NewScopeOfWork = job.CS_ScopeOfWork.ToList(),
                        NewPhotoReport = job.CS_JobPhotoReport.ToList(),
                        NewRequestedEquipment = _jobModel.GetEquipmentRequestedVOByJob(_view.JobID.Value),
                        JobStatusID = job.CS_JobInfo.LastJobStatus.ID,
                        NewJobStatusHistory = job.CS_JobInfo.CS_Job_JobStatus.FirstOrDefault(e => e.Active)
                    };

            string subject = _callCriteriaModel.GenerateSubjectForCallCriteria(
     job,
     job.CS_JobInfo,
     job.CS_CustomerInfo,
     job.CS_LocationInfo,
     "Initial Advise");

            IList<EmailVO> receipts = _callCriteriaModel.ListValidReceiptsByCallLog(job.CS_CallLog.FirstOrDefault(e => e.CallTypeID == (int)Globals.CallEntry.CallType.InitialLog).ID, job.ID);

            _view.EmailVOReceipts = receipts;
            _view.EmailSubject = subject;
            _view.EmailBody = _jobModel.BodyEmailJobRecordTable();

        }

        public void SendMail()
        {
            _jobModel = new JobModel();
            string[] receipts = _view.ReceiptsString.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            using (_emailModel = new EmailModel())
            {               

                if (_view.JobID.HasValue)
                {
                    CS_Job job = _jobModel.GetJobById(_view.JobID.Value);
                    _emailModel.SaveEmailList(string.Join(";", receipts), _view.EmailSubject, _view.EmailBody, _view.UserName, _view.CreationId);
                    _jobModel.SaveCallCriteriaInitialAdviseCallCriteriaEmailResources(_callLogModel.GetCallLogById(job.CS_CallLog.FirstOrDefault(e => e.CallTypeID == (int)Globals.CallEntry.CallType.InitialLog).ID));
                }
                else
                {
                    _emailModel.SaveEmailList(receipts, _view.ReceiptsIds, _view.EmailSubject, _view.EmailBody, _view.UserName, _view.CreationId);
                }
            }
        }

        #endregion

        #region [ IDisposable Implementation ]

        public void Dispose()
        {
            _view = null;
            _callLogModel = null;
            _callCriteriaModel = null;
            _emailModel = null;
        }

        #endregion
    }
}
