using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Core.Utils;

using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Hulcher.OneSource.CustomerService.Business.Model
{
    public class EmailModel : IDisposable
    {
        #region [ Attributes ]

        /// <summary>
        /// Repository Class for CS_Email
        /// </summary>
        private IRepository<CS_Email> _emailRepository;

        /// <summary>
        /// Unit of Work used to call the database/unit tests in-memory database
        /// </summary>
        private IUnitOfWork _unitOfWork;

        private MailUtility mailUtility;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Default Constructor
        /// </summary>
        public EmailModel()
        {
            _unitOfWork = new EFUnitOfWork();

            _emailRepository = new EFRepository<CS_Email>() { UnitOfWork = _unitOfWork };
        }

        /// <summary>
        /// Unit Tests constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of Work for in-memory database</param>
        public EmailModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            _emailRepository = new EFRepository<CS_Email>() { UnitOfWork = _unitOfWork };
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Generates a list of Email Records with a status of Pending to be sent by the Email Service
        /// </summary>
        /// <param name="receipts">Receipts email address (separated by ";")</param>
        /// <param name="subject">Email subject</param>
        /// <param name="body">Email body (HTML)</param>
        /// <param name="username">Username that requested the email creation</param>
        /// <param name="employeeId">EmployeeId of the Username that requested the email creation</param>
        public IList<CS_Email> SaveEmailList(string receipts, string subject, string body, string username, int? employeeId)
        {
            IList<CS_Email> returnList = new List<CS_Email>();

            using (TransactionScope scope = new TransactionScope())
            {
                string[] receiptsList = receipts.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < receiptsList.Length; i++)
                {
                    returnList.Add(SaveEmail(receiptsList[i], subject, body, username, employeeId));
                }

                scope.Complete();
            }

            return returnList;
        }

        /// <summary>
        /// Generates an Email Record with a status of Pending to be sent by the Email Service
        /// </summary>
        /// <param name="receipt">Receipt email address (only one)</param>
        /// <param name="subject">Email subject</param>
        /// <param name="body">Email body (HTML)</param>
        /// <param name="username">Username that requested the email creation</param>
        /// <param name="employeeId">EmployeeId of the Username that requested the email creation</param>
        public CS_Email SaveEmail(string receipt, string subject, string body, string username, int? employeeId)
        {
            string senderEmail = string.Empty;
            SettingsModel model = new SettingsModel(_unitOfWork);

            string[] emailConfiguration = model.GetEmailConfiguration();
            senderEmail = string.Format("{0}@{1}", emailConfiguration[0], emailConfiguration[2]);

            CS_Email email = new CS_Email();
            email.Sender = senderEmail;
            email.Receipts = receipt;
            email.Subject = subject;
            email.Body = body;
            email.Status = (short)Globals.EmailService.Status.Pending;
            email.StatusDate = DateTime.Now;

            email.CreatedBy = username;
            email.CreationID = employeeId;
            email.CreationDate = DateTime.Now;

            email.ModifiedBy = username;
            email.ModificationID = employeeId;
            email.ModificationDate = DateTime.Now;

            email.Active = true;

            return _emailRepository.Add(email);
        }

        /// <summary>
        /// Generates Email Recordr with a status of Pending to be sent by the Email Service for all receipts
        /// </summary>
        /// <param name="receipts">Receipt email address list</param>
        /// <param name="receiptsIds">Receipt email id list (if it is an employee, for call criteria)</param>
        /// <param name="subject">Email subject</param>
        /// <param name="body">Email body (HTML)</param>
        /// <param name="username">Username that requested the email creation</param>
        /// <param name="employeeId">EmployeeId of the Username that requested the email creation</param>
        public void SaveEmailList(string[] receipts, List<int> receiptsIds, string subject, string body, string username, int? employeeId)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (CallLogModel _callLogModel = new CallLogModel(_unitOfWork))
                {
                    for (int i = 0; i < receipts.Length; i++)
                    {
                        CS_Email email = SaveEmail(receipts[i], subject, body, username, employeeId);
                        if (i < receiptsIds.Count)
                            _callLogModel.AttachEmailToCallCriteria(receiptsIds[i], email.ID);
                    }
                }
                
                scope.Complete();
            }
        }

        /// <summary>
        /// Returns a list of all pending emails
        /// </summary>
        /// <returns>list of all pending emails</returns>
        public IList<CS_Email> ListAllPendingEmails()
        {
            return _emailRepository.ListAll(
                e => e.Active && e.Status == (short)Globals.EmailService.Status.Pending);
        }

        /// <summary>
        /// Returns a list of Emails using a ID Filter
        /// </summary>
        /// <param name="emailsIdList"></param>
        /// <returns></returns>
        public IList<CS_Email> ListEmailsById(IList<int> emailsIdList)
        {
            return _emailRepository.ListAll(e => e.Active && emailsIdList.Contains(e.ID));
        }

        /// <summary>
        /// Updates the Email status
        /// </summary>
        /// <param name="email">Email row</param>
        /// <param name="status">New Status</param>
        public CS_Email UpdateStatusEmail(CS_Email email, Globals.EmailService.Status status)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                email.Status = (short)status;
                email.StatusDate = DateTime.Now;

                CS_Email emailReturn = _emailRepository.Update(email);

                Globals.CallCriteria.CallCriteriaEmailStatus callCriteriaStatus = Globals.CallCriteria.CallCriteriaEmailStatus.Sent;
                switch (status)
                {
                    case Globals.EmailService.Status.Pending:
                        callCriteriaStatus = Globals.CallCriteria.CallCriteriaEmailStatus.Pending;
                        break;
                    case Globals.EmailService.Status.Sent:
                        callCriteriaStatus = Globals.CallCriteria.CallCriteriaEmailStatus.Sent;
                        break;
                    case Globals.EmailService.Status.Error:
                        callCriteriaStatus = Globals.CallCriteria.CallCriteriaEmailStatus.Error;
                        break;
                    case Globals.EmailService.Status.ConfirmationReceived:
                        callCriteriaStatus = Globals.CallCriteria.CallCriteriaEmailStatus.ConfirmationReceived;
                        break;
                    case Globals.EmailService.Status.ConfirmedAndRead:
                        callCriteriaStatus = Globals.CallCriteria.CallCriteriaEmailStatus.ReadConfirmationReceived;
                        break;
                    default:
                        callCriteriaStatus = Globals.CallCriteria.CallCriteriaEmailStatus.Sent;
                        break;
                }

                CallLogModel model = new CallLogModel(_unitOfWork);

                model.UpdateEmailStatusByEmailId(emailReturn.ID, callCriteriaStatus);

                scope.Complete();

                return emailReturn;
            }
        }

        #endregion

        #region [ Service Methods ]

        /// <summary>
        /// Executes the service process to send emails
        /// </summary>
        /// <returns>True if process ran OK, False if there was an error</returns>
        public bool EmailServiceWork()
        {
            try
            {
                string emailAccount = string.Empty;
                string emailPassword = string.Empty;
                string emailDomain = string.Empty;
                SettingsModel model = new SettingsModel(_unitOfWork);

                string[] emailConfiguration = model.GetEmailConfiguration();
                emailAccount = emailConfiguration[0];
                emailPassword = emailConfiguration[1];
                emailDomain = emailConfiguration[2];

                using (mailUtility = new MailUtility(emailAccount, emailPassword, emailDomain))
                {

                    // Gets the list of pendingl emails
                    IList<CS_Email> pendingEmails = ListAllPendingEmails();
                    foreach (CS_Email email in pendingEmails)
                    {
                        try
                        {
                            bool emailed = mailUtility.SendEmail(email.Subject, email.Body, email.Receipts, "", email.ID);

                            if (emailed)
                                UpdateStatusEmail(email, Globals.EmailService.Status.Sent);
                        }
                        catch (Exception ex)
                        {
                            Logger.Write(string.Format("An error has ocurred while trying to send the Email with ID {0}!\n{1}\n{2}", email.ID, ex.InnerException, ex.StackTrace));
                            UpdateStatusEmail(email, Globals.EmailService.Status.Error);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to send the Emails!\n{0}\n{1}", ex.InnerException, ex.StackTrace));

                // This process will not send any emails if there's an error, because it might be an exchange server problem

                return false;
            }
        }

        /// <summary>
        /// Executes the service process to check for delivery Confirmation
        /// </summary>
        /// <returns>True if process ran OK, False if there was an error</returns>
        public bool DeliveryServiceWork()
        {
            try
            {
                string emailAccount = string.Empty;
                string emailPassword = string.Empty;
                string emailDomain = string.Empty;

                SettingsModel model = new SettingsModel(_unitOfWork);

                string[] emailConfiguration = model.GetEmailConfiguration();
                emailAccount = emailConfiguration[0];
                emailPassword = emailConfiguration[1];
                emailDomain = emailConfiguration[2];

                using (mailUtility = new MailUtility(emailAccount, emailPassword, emailDomain))
                {
                    List<int> deliveredList = mailUtility.getEmailDeliveryConfirmation();
                    List<int> nonDeliveredList = mailUtility.GetEmailNonDeliveryConfirmation();

                    // Gets the list of pendingl emails
                    IList<CS_Email> DeliveredEmails = ListEmailsById(deliveredList);
                    foreach (CS_Email email in DeliveredEmails)
                    {
                        try
                        {
                            UpdateStatusEmail(email, Globals.EmailService.Status.ConfirmationReceived);
                        }
                        catch (Exception ex)
                        {
                            Logger.Write(string.Format("An error has ocurred while trying to update the Email with ID {0}!\n{1}\n{2}", email.ID, ex.InnerException, ex.StackTrace));
                        }
                    }

                    // Gets the list of nonDelivered emails
                    IList<CS_Email> nonDeliveredEmails = ListEmailsById(nonDeliveredList);
                    foreach (CS_Email email in nonDeliveredEmails)
                    {
                        try
                        {
                            UpdateStatusEmail(email, Globals.EmailService.Status.Error);
                        }
                        catch (Exception ex)
                        {
                            Logger.Write(string.Format("An error has ocurred while trying to update the Email with ID {0}!\n{1}\n{2}", email.ID, ex.InnerException, ex.StackTrace));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to confirm de delivery of the Emails!\n{0}\n{1}", ex.InnerException, ex.StackTrace));

                // This process will not send any emails if there's an error, because it might be an exchange server problem

                return false;
            }

            return true;
        }

        /// <summary>
        /// Executes the service process to check for read Confirmation
        /// </summary>
        /// <returns>True if process ran OK, False if there was an error</returns>
        public bool ReadServiceWork()
        {
            try
            {
                string emailAccount = string.Empty;
                string emailPassword = string.Empty;
                string emailDomain = string.Empty;

                SettingsModel model = new SettingsModel(_unitOfWork);

                string[] emailConfiguration = model.GetEmailConfiguration();
                emailAccount = emailConfiguration[0];
                emailPassword = emailConfiguration[1];
                emailDomain = emailConfiguration[2];

                using (mailUtility = new MailUtility(emailAccount, emailPassword, emailDomain))
                {
                    List<int> readList = mailUtility.getEmailReadConfirmation();

                    // Gets the list of nonDelivered emails
                    IList<CS_Email> readEmails = ListEmailsById(readList);
                    foreach (CS_Email email in readEmails)
                    {
                        try
                        {
                            UpdateStatusEmail(email, Globals.EmailService.Status.ConfirmedAndRead);
                        }
                        catch (Exception ex)
                        {
                            Logger.Write(string.Format("An error has ocurred while trying to update the Email with ID {0}!\n{1}\n{2}", email.ID, ex.InnerException, ex.StackTrace));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to confirm de delivery of the Emails!\n{0}\n{1}", ex.InnerException, ex.StackTrace));

                // This process will not send any emails if there's an error, because it might be an exchange server problem

                return false;
            }

            return true;
        }

        #endregion

        #region [ IDisposable Implementation ]

        public void Dispose()
        {
            _emailRepository = null;

            _unitOfWork.Dispose();
            _unitOfWork = null;
        }

        #endregion
    }
}
