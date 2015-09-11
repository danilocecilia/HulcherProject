using System.Data.Objects.DataClasses;
using System.Text;

namespace Hulcher.OneSource.CustomerService.DataContext
{
    partial class CS_View_GetJobData : EntityObject
    {
        public string StateAcronymName
        {
            get
            {
                return string.Format("{0} - {1}", StateAcronym, StateName);
            }
        }

        /// <summary>
        /// Returns Name City, Name State
        /// </summary>
        public string CityStateInformation
        {
            get
            {
                if (CityName != null && StateID.HasValue)
                {
                    string name = CityName.Trim();

                    if (!string.IsNullOrEmpty(CityName.Trim()))
                        return string.Format("{0} - {1}", CityName.Trim(), StateName.Trim());
                }

                return string.Empty;
            }
        }

        public string JobNumberOrInternalTracking
        {
            get
            {
                // 06/23/11 - ACCORDING TO CYNTHIA, ONCE ACTIVE, KEEP JOB NUMBER
                // TODO: Find a way to use Globals in this one
                //if (this.CS_JobInfo.LastJobStatusID.Equals(1) ||
                //    this.CS_JobInfo.LastJobStatusID.Equals(8))
                //    return Number;
                //else
                //    return Internal_Tracking;
                if (string.IsNullOrEmpty(JobNumber))
                    return JobInternalTracking;
                else
                    return JobNumber;
            }
        }

        public string JobPrefixedJobNumber
        {
            get
            {
                StringBuilder prefixedJobNumberBuilder = new StringBuilder();
                if (
                    (!string.IsNullOrEmpty(this.PriceTypeAcronym) && !string.IsNullOrEmpty(this.JobTypeDescription))
                    && (!string.IsNullOrEmpty(this.JobNumberOrInternalTracking))
                    )
                {
                    prefixedJobNumberBuilder.Append(PriceTypeAcronym);
                    prefixedJobNumberBuilder.Append(JobTypeDescription);
                    prefixedJobNumberBuilder.Append(JobNumber);
                }

                return prefixedJobNumberBuilder.ToString();
            }
        }

        public string JobPrefixedNumber
        {
            get
            {
                StringBuilder prefixedJobNumberBuilder = new StringBuilder();
                if (
                    (!string.IsNullOrEmpty(this.PriceTypeAcronym) && !string.IsNullOrEmpty(this.JobTypeDescription))
                    && (!string.IsNullOrEmpty(this.JobNumberOrInternalTracking))
                    )
                {
                    prefixedJobNumberBuilder.Append(PriceTypeAcronym);
                    prefixedJobNumberBuilder.Append(JobTypeDescription);
                    prefixedJobNumberBuilder.Append(JobNumberOrInternalTracking);
                }
                else
                    prefixedJobNumberBuilder.Append(this.JobNumberOrInternalTracking);

                return prefixedJobNumberBuilder.ToString();
            }
        }


        /// <summary>
        /// Returns Attn, State and Phone concatenated
        /// </summary>
        public string BillToFullContactInformation
        {
            get
            {
                StringBuilder fullContactBuilder = new StringBuilder();

                if (BillToContactDynamicsContact.HasValue && BillToContactDynamicsContact.Value)
                    fullContactBuilder.AppendFormat("{0}", BillToContactAttn.Trim());
                else
                    fullContactBuilder.AppendFormat("{0}, {1}", BillToContactLastName.Trim(), BillToContactFirstName.Trim());

                if (!string.IsNullOrEmpty(BillToContactAlias))
                    fullContactBuilder.AppendFormat(" \"{0}\"", BillToContactAlias.Trim());

                if (!string.IsNullOrEmpty(BillToContactState) && !string.IsNullOrEmpty(BillToContactState.Trim()))
                    fullContactBuilder.AppendFormat(" - {0}", BillToContactState.Trim());

                if (!string.IsNullOrEmpty(BillToContactPhone) && !string.IsNullOrEmpty(BillToContactPhone.Trim()))
                    fullContactBuilder.AppendFormat(" - {0}", BillToContactPhone.Trim());

                return fullContactBuilder.ToString();
            }
        }

        public string BillToFullName
        {
            get
            {
                StringBuilder fullNameBuilder = new StringBuilder();

                if (null != BillToContactLastName)
                    fullNameBuilder.AppendFormat("{0}, {1}", BillToContactLastName, BillToContactFirstName);
                else
                    fullNameBuilder.Append(BillToContactFirstName);

                if (!string.IsNullOrEmpty(BillToContactAlias))
                    fullNameBuilder.AppendFormat(" \"{0}\"", BillToContactAlias.Trim());

                return fullNameBuilder.ToString();
            }
        }


        /// <summary>
        /// Returns Attn, State and Phone concatenated
        /// </summary>
        public string OnSiteFullContactInformation
        {
            get
            {
                StringBuilder fullContactBuilder = new StringBuilder();

                if (OnSiteContactDynamicsContact.HasValue && OnSiteContactDynamicsContact.Value)
                    fullContactBuilder.AppendFormat("{0}", OnSiteContactAttn.Trim());
                else
                    fullContactBuilder.AppendFormat("{0}, {1}", OnSiteContactLastName.Trim(), OnSiteContactFirstName.Trim());

                if (!string.IsNullOrEmpty(OnSiteContactAlias))
                    fullContactBuilder.AppendFormat(" \"{0}\"", OnSiteContactAlias.Trim());

                if (!string.IsNullOrEmpty(OnSiteContactState) && !string.IsNullOrEmpty(OnSiteContactState.Trim()))
                    fullContactBuilder.AppendFormat(" - {0}", OnSiteContactState.Trim());

                if (!string.IsNullOrEmpty(OnSiteContactPhone) && !string.IsNullOrEmpty(OnSiteContactPhone.Trim()))
                    fullContactBuilder.AppendFormat(" - {0}", OnSiteContactPhone.Trim());

                return fullContactBuilder.ToString();
            }
        }

        public string OnSiteContactFullName
        {
            get
            {
                StringBuilder fullNameBuilder = new StringBuilder();

                if (null != OnSiteContactLastName)
                    fullNameBuilder.AppendFormat("{0}, {1}", OnSiteContactLastName, OnSiteContactFirstName);
                else
                    fullNameBuilder.Append(OnSiteContactFirstName);

                if (!string.IsNullOrEmpty(OnSiteContactAlias))
                    fullNameBuilder.AppendFormat(" \"{0}\"", OnSiteContactAlias.Trim());

                return fullNameBuilder.ToString();
            }
        }

        /// <summary>
        /// Returns Attn, State and Phone concatenated
        /// </summary>
        public string SecondaryFullContactInformation
        {
            get
            {
                StringBuilder fullContactBuilder = new StringBuilder();

                if (SecondaryContactDynamicsContact.HasValue && SecondaryContactDynamicsContact.Value)
                    fullContactBuilder.AppendFormat("{0}", SecondaryContactAttn.Trim());
                else
                    fullContactBuilder.AppendFormat("{0}, {1}", SecondaryContactLastName.Trim(), SecondaryContactFirstName.Trim());

                if (!string.IsNullOrEmpty(SecondaryContactAlias))
                    fullContactBuilder.AppendFormat(" \"{0}\"", SecondaryContactAlias.Trim());

                if (!string.IsNullOrEmpty(SecondaryContactState) && !string.IsNullOrEmpty(SecondaryContactState.Trim()))
                    fullContactBuilder.AppendFormat(" - {0}", SecondaryContactState.Trim());

                if (!string.IsNullOrEmpty(SecondaryContactPhone) && !string.IsNullOrEmpty(SecondaryContactPhone.Trim()))
                    fullContactBuilder.AppendFormat(" - {0}", SecondaryContactPhone.Trim());

                return fullContactBuilder.ToString();
            }
        }

        public string SecondaryFullName
        {
            get
            {
                StringBuilder fullNameBuilder = new StringBuilder();

                if (null != SecondaryContactLastName)
                    fullNameBuilder.AppendFormat("{0}, {1}", SecondaryContactLastName, SecondaryContactFirstName);
                else
                    fullNameBuilder.Append(SecondaryContactFirstName);

                if (!string.IsNullOrEmpty(SecondaryContactAlias))
                    fullNameBuilder.AppendFormat(" \"{0}\"", SecondaryContactAlias.Trim());

                return fullNameBuilder.ToString();
            }
        }

        /// <summary>
        /// Returns Attn, State and Phone concatenated
        /// </summary>
        public string PrimaryFullContactInformation
        {
            get
            {
                StringBuilder fullContactBuilder = new StringBuilder();

                if (PrimaryContactDynamicsContact.HasValue && PrimaryContactDynamicsContact.Value)
                    fullContactBuilder.AppendFormat("{0}", PrimaryContactAttn.Trim());
                else
                    fullContactBuilder.AppendFormat("{0}, {1}", PrimaryContactLastName.Trim(), PrimaryContactFirstName.Trim());

                if (!string.IsNullOrEmpty(PrimaryContactAlias))
                    fullContactBuilder.AppendFormat(" \"{0}\"", PrimaryContactAlias.Trim());

                if (!string.IsNullOrEmpty(PrimaryContactState) && !string.IsNullOrEmpty(PrimaryContactState.Trim()))
                    fullContactBuilder.AppendFormat(" - {0}", PrimaryContactState.Trim());

                if (!string.IsNullOrEmpty(PrimaryContactPhone) && !string.IsNullOrEmpty(PrimaryContactPhone.Trim()))
                    fullContactBuilder.AppendFormat(" - {0}", PrimaryContactPhone.Trim());

                return fullContactBuilder.ToString();
            }
        }

        public string PrimaryFullName
        {
            get
            {
                StringBuilder fullNameBuilder = new StringBuilder();

                if (null != PrimaryContactLastName)
                    fullNameBuilder.AppendFormat("{0}, {1}", PrimaryContactLastName, PrimaryContactFirstName);
                else
                    fullNameBuilder.Append(PrimaryContactFirstName);

                if (!string.IsNullOrEmpty(PrimaryContactAlias))
                    fullNameBuilder.AppendFormat(" \"{0}\"", PrimaryContactAlias.Trim());

                return fullNameBuilder.ToString();
            }
        }

        public string POCEmployeeDivisionAndFullName
        {
            get
            {
                StringBuilder fullNameBuilder = new StringBuilder();
                if (POCEmployeeDivisionName != null)
                {
                    fullNameBuilder.AppendFormat("{0} - ", POCEmployeeDivisionName);
                }
                if (POCEmployeeLastName != null)
                {
                    fullNameBuilder.Append(POCEmployeeLastName.Trim());
                    fullNameBuilder.Append(", ");
                }
                if (POCEmployeeFirstName != null)
                {
                    fullNameBuilder.Append(POCEmployeeFirstName.Trim());
                }
                if (!string.IsNullOrEmpty(POCEmployeeNickname))
                {
                    fullNameBuilder.AppendFormat(" \"{0}\"", POCEmployeeNickname);
                }
                return fullNameBuilder.ToString();
            }
        }
    }
}
