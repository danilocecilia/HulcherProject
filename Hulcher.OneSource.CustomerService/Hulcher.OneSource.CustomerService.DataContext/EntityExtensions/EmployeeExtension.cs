using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using Hulcher.OneSource.CustomerService.DataContext.EntityExtensions;

namespace Hulcher.OneSource.CustomerService.DataContext
{
    /// <summary>
    /// Entity object for Employee
    /// </summary>
    public partial class CS_Employee : EntityObject, IInitialAdvisePerson
    {
        /// <summary>
        /// Employee FullName
        /// </summary>
        private string fullName;

        /// <summary>
        /// Returns the first name and last name concatenated 
        /// </summary>
        public string FullName
        {
            get
            {
                StringBuilder fullNameBuilder = new StringBuilder();
                if (Name != null)
                {
                    fullNameBuilder.Append(Name.Trim());
                    fullNameBuilder.Append(", ");
                }
                if (FirstName != null)
                {
                    fullNameBuilder.Append(FirstName.Trim());
                }
                if (!string.IsNullOrEmpty(Nickname))
                {
                    fullNameBuilder.AppendFormat(" \"{0}\"", Nickname);
                }
                return fullNameBuilder.ToString();
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    fullName = value;
            }
        }

        /// <summary>
        /// Returns the Division, first name and last name concatenated 
        /// </summary>
        public string DivisionAndFullName
        {
            get
            {
                StringBuilder fullNameBuilder = new StringBuilder();
                if (CS_Division != null)
                {
                    fullNameBuilder.AppendFormat("{0} - ", DivisionName);
                }
                if (Name != null)
                {
                    fullNameBuilder.Append(Name.Trim());
                    fullNameBuilder.Append(", ");
                }
                if (FirstName != null)
                {
                    fullNameBuilder.Append(FirstName.Trim());
                }
                if (!string.IsNullOrEmpty(Nickname))
                {
                    fullNameBuilder.AppendFormat(" \"{0}\"", Nickname);
                }
                return fullNameBuilder.ToString();
            }
        }

        /// <summary>
        /// Returns the Employee's complete name (First Name + Last Name)
        /// </summary>
        public string CompleteName
        {
            get
            {
                StringBuilder completeNameBuilder = new StringBuilder();
                if (FirstName != null)
                {
                    completeNameBuilder.Append(FirstName.Trim());
                    completeNameBuilder.Append(" ");
                }
                if (Name != null)
                {
                    completeNameBuilder.Append(Name.Trim());
                }
                return completeNameBuilder.ToString();
            }
        }

        public string ContactInfo
        {
            get
            {
                StringBuilder contactInfoBuilder = new StringBuilder();
                if (!string.IsNullOrEmpty(DayAreaCode))
                {
                    contactInfoBuilder.Append(string.Format("(W) {0}-{1}", DayAreaCode, DayPhone));
                    contactInfoBuilder.Append(" \n");
                }
                if (!string.IsNullOrEmpty(FaxAreaCode))
                {
                    contactInfoBuilder.Append(string.Format("(F) {0}-{1}", FaxAreaCode, FaxPhone));
                    contactInfoBuilder.Append(" \n");
                }
                if (!string.IsNullOrEmpty(HomeAreaCode))
                {
                    contactInfoBuilder.Append(string.Format("(H) {0}-{1}", HomeAreaCode, HomePhone));
                    contactInfoBuilder.Append(" \n");
                }
                if (!string.IsNullOrEmpty(MobileAreaCode))
                {
                    contactInfoBuilder.Append(string.Format("(M) {0}-{1}", MobileAreaCode, MobilePhone));
                    contactInfoBuilder.Append(" \n");
                }
                if (!string.IsNullOrEmpty(OtherPhoneAreaCode))
                {
                    contactInfoBuilder.Append(string.Format("(O) {0}-{1}", OtherPhoneAreaCode, OtherPhone));
                    contactInfoBuilder.Append(" \n");
                }

                return contactInfoBuilder.ToString();
            }
        }

        public string Description
        {
            get { return this.CS_Division.ExtendedDivisionName; }
        }

        public string Person
        {
            get { return FullName; }
        }

        public string Note
        {
            get { return "Hazmat"; }
        }

        public string GetEmployeeEmail(string domain)
        {
            if (!string.IsNullOrEmpty(this.UserLogin))
            {
                string userLogin = this.UserLogin.Substring(this.UserLogin.IndexOf('\\') + 1);
                return string.Format("{0}@{1}", userLogin, domain);
            }
            return string.Empty;
        }

        public string FullDivisionName
        {
            get
            {
                string divisionName = string.Empty;

                if (null != this.CS_Division)
                    divisionName = this.CS_Division.ExtendedDivisionName;

                if (null != this.CS_EmployeeCoverage && this.CS_EmployeeCoverage.Count > 0)
                {
                    CS_EmployeeCoverage coverage = this.CS_EmployeeCoverage.Where(e => e.Active).FirstOrDefault();

                    if (null != coverage && null != coverage.CS_Division)
                        divisionName = string.Format("C {0}/{1}", coverage.CS_Division.ExtendedDivisionName, divisionName);
                }

                return divisionName;
            }
        }

        public string DivisionName
        {
            get
            {
                string divisionName = string.Empty;

                if (null != this.CS_Division)
                    divisionName = this.CS_Division.Name;

                if (null != this.CS_EmployeeCoverage && this.CS_EmployeeCoverage.Count > 0)
                {
                    CS_EmployeeCoverage coverage = this.CS_EmployeeCoverage.Where(e => e.Active).FirstOrDefault();

                    if (null != coverage && null != coverage.CS_Division)
                        divisionName = string.Format("C {0}/{1}", coverage.CS_Division.Name, divisionName);
                }

                return divisionName;
            }
        }


        public string FullAddress
        {
            get
            {
                return string.Format("{0}, {1}", this.Address, this.Address2);
            }
        }
    }
}
