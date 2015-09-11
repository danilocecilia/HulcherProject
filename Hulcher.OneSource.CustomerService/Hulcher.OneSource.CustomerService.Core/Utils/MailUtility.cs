using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;

using Microsoft.Exchange.WebServices;
using Microsoft.Exchange.WebServices.Data;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Hulcher.OneSource.CustomerService.DataContext.VO;

namespace Hulcher.OneSource.CustomerService.Core.Utils
{
    public class MailUtility : IDisposable
    {
        #region [ Attributes ]

        private ExchangeService service;

        private string _emailAccount;
        private string _emailPassword;
        private string _emailDomain;

        #endregion

        #region [ Constructor ]

        public MailUtility(string emailAccount, string emailPassword, string emailDomain)
        {
            this._emailAccount = emailAccount;
            this._emailPassword = emailPassword;
            this._emailDomain = emailDomain;
            GenerateService();
        }

        #endregion

        #region [ Methods ]

        #region [ Private ]

        private void GenerateService()
        {
            ServicePointManager.ServerCertificateValidationCallback = CertificateValidationCallBack;
            service = new ExchangeService(ExchangeVersion.Exchange2010_SP1);
            service.Credentials = new NetworkCredential(_emailAccount, _emailPassword, _emailDomain);
            service.AutodiscoverUrl(string.Format("{0}@{1}", _emailAccount, _emailDomain));
        }

        private FolderId getFolderId(string folderName)
        {
            // Create a view with a page size of 10.
            FolderView view = new FolderView(1);

            // Identify the properties to return in the results set.
            view.PropertySet = new PropertySet(FolderSchema.Id);
            view.PropertySet.Add(FolderSchema.DisplayName);

            // Return only folders that contain items.
            SearchFilter searchFilter = new SearchFilter.IsEqualTo(FolderSchema.DisplayName, folderName);

            // Unlike FindItem searches, folder searches can be deep traversals.
            view.Traversal = FolderTraversal.Deep;

            // Send the request to search the mailbox and get the results.
            FindFoldersResults findFolderResults = service.FindFolders(WellKnownFolderName.Root, searchFilter, view);

            if (findFolderResults.TotalCount > 0)
            {
                return findFolderResults.Folders[0].Id;
            }

            return new FolderId(WellKnownFolderName.SentItems);

        }

        private void SendEmailMessage(EmailMessage emailMessage, string internalID)
        {
            emailMessage.IsDeliveryReceiptRequested = true;
            emailMessage.IsReadReceiptRequested = true;
            emailMessage.SetExtendedProperty(new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "InternalId", MapiPropertyType.String), internalID);
            emailMessage.SendAndSaveCopy();
        }

        private EmailMessage getSingleEmailByInternetID(string id)
        {
            // Add a search filter that searches on the body or subject.
            List<SearchFilter> searchFilterCollection = new List<SearchFilter>();
            searchFilterCollection.Add(new SearchFilter.ContainsSubstring(EmailMessageSchema.InternetMessageId, id));

            // Create the search filter.
            SearchFilter searchFilter = new SearchFilter.SearchFilterCollection(LogicalOperator.And, searchFilterCollection.ToArray());

            // Create a view with a page size of 50.
            ItemView view = new ItemView(1);

            // Identify the Subject and DateTimeReceived properties to return.
            // Indicate that the base property will be the item identifier
            view.PropertySet = new PropertySet(BasePropertySet.FirstClassProperties, ItemSchema.Subject, ItemSchema.Id, ItemSchema.DateTimeReceived, EmailMessageSchema.ItemClass);

            // Order the search results by the DateTimeReceived in descending order.
            view.OrderBy.Add(ItemSchema.DateTimeSent, Microsoft.Exchange.WebServices.Data.SortDirection.Descending);

            // Set the traversal to shallow. (Shallow is the default option; other options are Associated and SoftDeleted.)
            view.Traversal = ItemTraversal.Shallow;

            // Send the request to search the Inbox and get the results.
            FindItemsResults<Item> findResults = service.FindItems(WellKnownFolderName.SentItems, searchFilter, view);

            if (findResults.Items.Count > 0)
            {
                service.LoadPropertiesForItems(findResults, new PropertySet(new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "InternalId", MapiPropertyType.String)));
                return findResults.Items[0] as EmailMessage;
            }

            return new EmailMessage(service);
        }

        #endregion

        #region [ Public ]

        public bool SendEmail(string _Subject, string _Body, string _To, string _CC, int? _InternalId)
        {
            try
            {
                string[] toRecipients = _To.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                string[] ccRecipients = _CC.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                string internalId = "";

                if (_InternalId.HasValue)
                    internalId = _InternalId.Value.ToString();

                EmailMessage email = new EmailMessage(service);
                email.Subject = _Subject + string.Format(" (Email #:{0})", internalId);
                email.Body = _Body;
                email.Body.BodyType = BodyType.HTML;

                for (int i = 0; i < toRecipients.Length; i++)
                {
                    email.ToRecipients.Add(toRecipients[i]);
                }

                for (int i = 0; i < ccRecipients.Length; i++)
                {
                    email.CcRecipients.Add(ccRecipients[i]);
                }

                SendEmailMessage(email, internalId);

                return true;
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error Sending the Email.!\n{0}\n{1}", ex.Message, ex.StackTrace));
                return false;
            }
        }

        public bool SendEmail(string _Subject, string _Body, IList<EmailVO> _To, IList<EmailVO> _CC, int? _InternalId)
        {
            try
            {
                string internalId = "";

                if (_InternalId.HasValue)
                    internalId = _InternalId.Value.ToString();

                EmailMessage email = new EmailMessage(service);
                email.Subject = _Subject + string.Format(" (Email #:{0})", internalId);
                email.Body = _Body;

                for (int i = 0; i < _To.Count; i++)
                {
                    email.ToRecipients.Add(_To[i].Email);
                }

                for (int i = 0; i < _CC.Count; i++)
                {
                    email.CcRecipients.Add(_CC[i].Email);
                }

                SendEmailMessage(email, internalId);

                return true;
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error Sending the Email.!\n{0}\n{1}", ex.Message, ex.StackTrace));
                return false;
            }
        }

        public List<int> getEmailDeliveryConfirmation()
        {
            List<int> returnList = new List<int>();

            // Add a search filter that searches on the body or subject.
            List<SearchFilter> searchFilterCollection = new List<SearchFilter>();
            searchFilterCollection.Add(new SearchFilter.ContainsSubstring(ItemSchema.ItemClass, "REPORT.IPM.Note.DR"));

            // Create the search filter.
            SearchFilter searchFilter = new SearchFilter.SearchFilterCollection(LogicalOperator.And, searchFilterCollection.ToArray());

            // Create a view with a page size of 100.
            ItemView view = new ItemView(100);

            // Identify the Subject and DateTimeReceived properties to return.
            // Indicate that the base property will be the item identifier
            view.PropertySet = new PropertySet(BasePropertySet.FirstClassProperties);

            // Order the search results by the DateTimeReceived in descending order.
            view.OrderBy.Add(ItemSchema.DateTimeSent, Microsoft.Exchange.WebServices.Data.SortDirection.Descending);

            // Set the traversal to shallow. (Shallow is the default option; other options are Associated and SoftDeleted.)
            view.Traversal = ItemTraversal.Shallow;

            // Send the request to search the Inbox and get the results.
            FindItemsResults<Item> findResults = service.FindItems(WellKnownFolderName.Inbox, searchFilter, view);

            if (findResults.Items.Count > 0)
                service.LoadPropertiesForItems(findResults, new PropertySet(ItemSchema.Body, EmailMessageSchema.InReplyTo));

            foreach (EmailMessage mail in findResults.Items)
            {
                EmailMessage parentMail = getSingleEmailByInternetID(mail.InReplyTo);

                if (parentMail.ExtendedProperties.Count > 0)
                    returnList.Add(int.Parse(parentMail.ExtendedProperties[0].Value.ToString()));

                mail.Move(getFolderId("DeliveryConfirmation"));
            }

            return returnList;
        }

        public List<int> GetEmailNonDeliveryConfirmation()
        {
            List<int> returnList = new List<int>();

            // Add a search filter that searches on the body or subject.
            List<SearchFilter> searchFilterCollection = new List<SearchFilter>();
            searchFilterCollection.Add(new SearchFilter.ContainsSubstring(ItemSchema.ItemClass, "REPORT.IPM.Note.NDR"));

            // Create the search filter.
            SearchFilter searchFilter = new SearchFilter.SearchFilterCollection(LogicalOperator.And, searchFilterCollection.ToArray());

            // Create a view with a page size of 100.
            ItemView view = new ItemView(100);

            // Identify the Subject and DateTimeReceived properties to return.
            // Indicate that the base property will be the item identifier
            view.PropertySet = new PropertySet(BasePropertySet.FirstClassProperties);

            // Order the search results by the DateTimeReceived in descending order.
            view.OrderBy.Add(ItemSchema.DateTimeSent, Microsoft.Exchange.WebServices.Data.SortDirection.Descending);

            // Set the traversal to shallow. (Shallow is the default option; other options are Associated and SoftDeleted.)
            view.Traversal = ItemTraversal.Shallow;

            // Send the request to search the Inbox and get the results.
            FindItemsResults<Item> findResults = service.FindItems(WellKnownFolderName.Inbox, searchFilter, view);

            if (findResults.Items.Count > 0)
                service.LoadPropertiesForItems(findResults, new PropertySet(ItemSchema.Body, EmailMessageSchema.InReplyTo));

            foreach (EmailMessage mail in findResults.Items)
            {
                EmailMessage parentMail = getSingleEmailByInternetID(mail.InReplyTo);

                if (parentMail.ExtendedProperties.Count > 0)
                    returnList.Add(int.Parse(parentMail.ExtendedProperties[0].Value.ToString()));

                mail.Move(getFolderId("NonDeliveryConfirmation"));
            }

            return returnList;
        }

        public List<int> getEmailReadConfirmation()
        {
            List<int> returnList = new List<int>();

            // Add a search filter that searches on the body or subject.
            List<SearchFilter> searchFilterCollection = new List<SearchFilter>();
            searchFilterCollection.Add(new SearchFilter.ContainsSubstring(ItemSchema.ItemClass, "REPORT.IPM.Note.IPNRN"));

            // Create the search filter.
            SearchFilter searchFilter = new SearchFilter.SearchFilterCollection(LogicalOperator.And, searchFilterCollection.ToArray());

            // Create a view with a page size of 100.
            ItemView view = new ItemView(100);

            // Identify the Subject and DateTimeReceived properties to return.
            // Indicate that the base property will be the item identifier
            view.PropertySet = new PropertySet(BasePropertySet.FirstClassProperties, ItemSchema.DateTimeSent);

            // Order the search results by the DateTimeReceived in descending order.
            view.OrderBy.Add(ItemSchema.DateTimeSent, Microsoft.Exchange.WebServices.Data.SortDirection.Descending);

            // Set the traversal to shallow. (Shallow is the default option; other options are Associated and SoftDeleted.)
            view.Traversal = ItemTraversal.Shallow;

            // Send the request to search the Inbox and get the results.
            FindItemsResults<Item> findResults = service.FindItems(WellKnownFolderName.Inbox, searchFilter, view);

            if (findResults.Items.Count > 0)
                service.LoadPropertiesForItems(findResults, new PropertySet(ItemSchema.Body, EmailMessageSchema.Subject, EmailMessageSchema.InReplyTo, EmailMessageSchema.ConversationId));

            foreach (EmailMessage mail in findResults.Items)
            {
                int internalId;
                string internalIdString = mail.Subject;

                internalIdString = internalIdString.Substring(internalIdString.IndexOf("Email #:") + 8);
                internalIdString = internalIdString.Substring(0, internalIdString.IndexOf(")"));

                Int32.TryParse(internalIdString, out internalId);

                if (Int32.TryParse(internalIdString, out internalId))
                {
                    returnList.Add(internalId);

                    mail.Move(getFolderId("ReadConfirmation"));
                }
            }

            return returnList;
        }

        #endregion

        #endregion

        #region [ Override ]

        private static bool CertificateValidationCallBack(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        #endregion

        public static void SendMail(string _To, string _CC, string _BCC, string _Body, string _Subject, bool _IsBodyHtml, string[] _AttachmentsPath)
        {
            //if (_To != string.Empty)
            //{
            //    //Configurating the SmtpClient
            //    SmtpClient client = new SmtpClient();

            //    client.Host = "smtp02.stefanini.com.br";
            //    client.Port = 25;
            //    client.Credentials = new NetworkCredential("rbrandao", "lucky1");

            //    //Creating the MailMessage
            //    MailMessage mailMessage = new MailMessage();

            //    mailMessage.To.Add(new MailAddress(_To));

            //    if (_CC != string.Empty)
            //        mailMessage.CC.Add(new MailAddress(_CC));

            //    if (_BCC != string.Empty)
            //        mailMessage.Bcc.Add(new MailAddress(_BCC));

            //    mailMessage.Body = _Body;

            //    mailMessage.Subject = _Subject;

            //    mailMessage.IsBodyHtml = _IsBodyHtml;

            //    mailMessage.Sender = new MailAddress("rbrandao@hulcher.com");

            //    mailMessage.From = new MailAddress("rbrandao@hulcher.com");

            //    if (_AttachmentsPath != null)
            //    {

            //        for (int i = 0; i < _AttachmentsPath.Length; i++)
            //        {
            //            mailMessage.Attachments.Add(new Attachment(_AttachmentsPath[i]));
            //        }
            //    }

            //    client.Send(mailMessage);
            //}
        }

        public void SendUntrackMail(string from, string[] toList, string[] ccList, string body, string subject, string[] attachmentsPathList)
        {
            if (toList.Length > 0)
            {
                EmailMessage email = new EmailMessage(service);
                //email.From = new EmailAddress(from);
                email.Subject = subject;
                email.Body = body;

                if (null != toList)
                {
                    for (int i = 0; i < toList.Length; i++)
                    {
                        email.ToRecipients.Add(toList[i]);
                    }
                }

                if (null != ccList)
                {
                    for (int i = 0; i < ccList.Length; i++)
                    {
                        email.CcRecipients.Add(ccList[i]);
                    }
                }

                if (attachmentsPathList != null)
                {
                    for (int i = 0; i < attachmentsPathList.Length; i++)
                    {
                        email.Attachments.AddFileAttachment(attachmentsPathList[i]);
                    }
                }

                email.Send();
            }
        }

        #region [ IDisposable ]

        public void Dispose()
        {
            service = null;
        }

        #endregion
    }
}
