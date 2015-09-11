using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using Hulcher.OneSource.CustomerService.DataContext.EntityExtensions;

namespace Hulcher.OneSource.CustomerService.DataContext
{
    /// <summary>
    /// Entity object for Contact
    /// </summary>
    public partial class CS_Contact : EntityObject, IInitialAdvisePerson
    {
        /// <summary>
        /// Returns Attn, State and Phone concatenated
        /// </summary>
        public string FullContactInformation
        {
            get
            {
                StringBuilder fullContactBuilder = new StringBuilder();

                if (DynamicsContact && !string.IsNullOrEmpty(Attn.Trim()))
                    fullContactBuilder.AppendFormat("{0}", Attn.Trim());
                else if (DynamicsContact && string.IsNullOrEmpty(Attn.Trim()))
                    fullContactBuilder.Append("No Contact Name");
                else if (string.IsNullOrEmpty(LastName.Trim()) && !string.IsNullOrEmpty(Name.Trim()))
                    fullContactBuilder.AppendFormat("{0}", Name.Trim());
                else if (!string.IsNullOrEmpty(LastName.Trim()) && string.IsNullOrEmpty(Name.Trim()))
                    fullContactBuilder.AppendFormat("{0}", LastName.Trim());
                else if (string.IsNullOrEmpty(LastName.Trim()) && string.IsNullOrEmpty(Name.Trim()))
                    fullContactBuilder.Append("No Contact Name");
                else
                    fullContactBuilder.AppendFormat("{0}, {1}", LastName.Trim(), Name.Trim());

                if (!string.IsNullOrEmpty(Alias))
                    fullContactBuilder.AppendFormat(" \"{0}\"", Alias.Trim());

                if (!string.IsNullOrEmpty(State) && !string.IsNullOrEmpty(State.Trim()))
                    fullContactBuilder.AppendFormat(" - {0}", State.Trim());

                if (!string.IsNullOrEmpty(Phone) && !string.IsNullOrEmpty(ContactAllPhones.Trim()))
                    fullContactBuilder.AppendFormat(" - {0}", ContactAllPhones.Trim());

                return fullContactBuilder.ToString();
            }
        }

        public string ContactEditingPhoneNumber
        {
            get
            {
                string extension = string.Empty;
                string phone = string.Empty;

                if (string.IsNullOrEmpty(Phone))
                    return string.Empty;
                if (Phone.Contains("ext"))
                {
                    phone = Phone.Replace("ext", ",");

                    string[] pPhone = phone.Split(',');

                    phone = pPhone[0].Trim();
                    extension = " ext " + pPhone[1].Trim();
                }
                else
                    phone = Phone.Trim();

                if ((Country == "US" || Country == "CA" || string.IsNullOrEmpty(Country)) && phone.Trim().Length == 10)
                    return string.Format("({0}) {1}-{2}", phone.Substring(0, 3), phone.Substring(3, 3), phone.Substring(6)) + extension;
                else if ((string.IsNullOrEmpty(Country) || Country == "MX") && phone.Trim().Length == 11)
                    return string.Format("({0}) {1}-{2}", phone.Substring(0, 3), phone.Substring(3, 4), phone.Substring(7)) + extension;
                else if ((string.IsNullOrEmpty(Country) || Country == "MX") && phone.Trim().Length == 10)
                    return string.Format("({0}) {1}-{2}", phone.Substring(0, 2), phone.Substring(2, 4), phone.Substring(6)) + extension;
                else if ((Country == "US" || Country == "CA" || string.IsNullOrEmpty(Country)) && phone.Trim().Length == 7)
                    return string.Format("{0}-{1}", phone.Substring(0, 3), phone.Substring(3)) + extension;
                else if ((string.IsNullOrEmpty(Country) || Country == "MX") && phone.Trim().Length == 8)
                    return string.Format("{0}-{1}", phone.Substring(0, 4), phone.Substring(4)) + extension;
                else
                    return phone.Trim() + extension;

            }
        }

        public string ContactEditingFaxNumber
        {
            get
            {
                if (string.IsNullOrEmpty(Fax))
                    return string.Empty;
                else if ((Country == "US" || Country == "CA" || string.IsNullOrEmpty(Country)) && Fax.Trim().Length == 10)
                    return string.Format("({0}) {1}-{2}", Fax.Substring(0, 3), Fax.Substring(3, 3), Fax.Substring(6));
                else if ((string.IsNullOrEmpty(Country) || Country == "MX") && Fax.Trim().Length == 11)
                    return string.Format("({0}) {1}-{2}", Fax.Substring(0, 3), Fax.Substring(3, 4), Fax.Substring(7));
                else if ((string.IsNullOrEmpty(Country) || Country == "MX") && Fax.Trim().Length == 10)
                    return string.Format("({0}) {1}-{2}", Fax.Substring(0, 2), Fax.Substring(2, 4), Fax.Substring(6));
                else if ((Country == "US" || Country == "CA" || string.IsNullOrEmpty(Country)) && Fax.Trim().Length == 7)
                    return string.Format("{0}-{1}", Fax.Substring(0, 3), Fax.Substring(3));
                else if ((string.IsNullOrEmpty(Country) || Country == "MX") && Fax.Trim().Length == 8)
                    return string.Format("{0}-{1}", Fax.Substring(0, 4), Fax.Substring(4));
                else
                    return Fax.Trim();
            }
        }

        public string BillingContactPhoneNumber
        {
            get
            {
                if (string.IsNullOrEmpty(BillPhone))
                    return string.Empty;
                else if ((Country == "US" || Country == "CA" || string.IsNullOrEmpty(Country)) && BillPhone.Trim().Length == 10)
                    return string.Format("({0}) {1}-{2}", BillPhone.Substring(0, 3), BillPhone.Substring(3, 3), BillPhone.Substring(6));
                else if ((string.IsNullOrEmpty(Country) || Country == "MX") && BillPhone.Trim().Length == 11)
                    return string.Format("({0}) {1}-{2}", BillPhone.Substring(0, 3), BillPhone.Substring(3, 4), BillPhone.Substring(7));
                else if ((string.IsNullOrEmpty(Country) || Country == "MX") && BillPhone.Trim().Length == 10)
                    return string.Format("({0}) {1}-{2}", BillPhone.Substring(0, 2), BillPhone.Substring(2, 4), BillPhone.Substring(6));
                else if ((Country == "US" || Country == "CA" || string.IsNullOrEmpty(Country)) && BillPhone.Trim().Length == 7)
                    return string.Format("{0}-{1}", BillPhone.Substring(0, 3), BillPhone.Substring(3));
                else if ((string.IsNullOrEmpty(Country) || Country == "MX") && BillPhone.Trim().Length == 8)
                    return string.Format("{0}-{1}", BillPhone.Substring(0, 4), BillPhone.Substring(4));
                else
                    return BillPhone.Trim();
            }
        }

        public string BillingContactFaxPhone
        {
            get
            {
                if (string.IsNullOrEmpty(BillFax))
                    return string.Empty;
                else if ((Country == "US" || Country == "CA" || string.IsNullOrEmpty(Country)) && BillFax.Trim().Length == 10)
                    return string.Format("({0}) {1}-{2}", BillFax.Substring(0, 3), BillFax.Substring(3, 3), BillFax.Substring(6));
                else if ((string.IsNullOrEmpty(Country) || Country == "MX") && BillFax.Trim().Length == 11)
                    return string.Format("({0}) {1}-{2}", BillFax.Substring(0, 3), BillFax.Substring(3, 4), BillFax.Substring(7));
                else if ((string.IsNullOrEmpty(Country) || Country == "MX") && BillFax.Trim().Length == 10)
                    return string.Format("({0}) {1}-{2}", BillFax.Substring(0, 2), BillFax.Substring(2, 4), BillFax.Substring(6));
                else if ((Country == "US" || Country == "CA" || string.IsNullOrEmpty(Country)) && BillFax.Trim().Length == 7)
                    return string.Format("{0}-{1}", BillFax.Substring(0, 3), BillFax.Substring(3));
                else if ((string.IsNullOrEmpty(Country) || Country == "MX") && BillFax.Trim().Length == 8)
                    return string.Format("{0}-{1}", BillFax.Substring(0, 4), BillFax.Substring(4));
                else
                    return BillFax.Trim();
            }
        }

        public string ContactAllPhones
        {
            get
            {
                StringBuilder phones = new StringBuilder();

                if (!string.IsNullOrEmpty(ContactEditingPhoneNumber))
                    phones.AppendFormat("Work: {0}", ContactEditingPhoneNumber);

                if (null != this.CS_PhoneNumber)
                {
                    foreach (CS_PhoneNumber phone in this.CS_PhoneNumber.Where(e => e.Active).ToList())
                    {
                        if (phone.PhoneTypeID == 2)
                            if (!string.IsNullOrEmpty(phone.Number))
                                phones.AppendFormat(" Cell: {0}", phone.Number);

                        if (phone.PhoneTypeID == 1)
                            if (!string.IsNullOrEmpty(phone.Number))
                                phones.AppendFormat(" Home: {0}", phone.Number);
                    }
                }

                if (!string.IsNullOrEmpty(ContactEditingFaxNumber))
                    phones.AppendFormat(" Fax: {0}", ContactEditingFaxNumber);

                return phones.ToString().Trim();
            }
        }

        public string FullName
        {
            get
            {
                StringBuilder fullNameBuilder = new StringBuilder();

                if (null != LastName)
                    fullNameBuilder.AppendFormat("{0}, {1}", LastName, Name);
                else
                    fullNameBuilder.Append(Name);

                if (!string.IsNullOrEmpty(Alias))
                    fullNameBuilder.AppendFormat(" \"{0}\"", Alias.Trim());

                return fullNameBuilder.ToString();
            }
        }

        public string ContactInfo
        {
            get
            {
                StringBuilder contactInfoBuilder = new StringBuilder();

                if (null != Phone)
                {
                    if (!string.IsNullOrEmpty(Phone.Trim()))
                    {
                        contactInfoBuilder.Append(string.Format("(P) {0}-{1}-{2}", Phone.Substring(0, 3), Phone.Substring(3, 3), Phone.Substring(6)));
                        contactInfoBuilder.Append(" ");
                    }
                }

                if (null != Fax)
                {
                    if (!string.IsNullOrEmpty(Fax.Trim()))
                    {
                        contactInfoBuilder.Append(string.Format("(F) {0}-{1}-{2}", Fax.Substring(0, 3), Fax.Substring(3, 3), Fax.Substring(6)));
                        contactInfoBuilder.Append(" ");
                    }
                }
                return contactInfoBuilder.ToString();
            }
        }

        public string Description
        {
            get
            {
                return this.CS_Customer_Contact.First().CS_Customer.FullCustomerInformation;
            }
        }

        public string Person
        {
            get
            {
                return this.Name;
            }
        }

        public string Note
        {
            get
            {
                return "Hazmat";
            }
        }

        string IInitialAdvisePerson.ContactInfo
        {
            get
            {
                return this.ContactInfo;
            }
        }

        public string Type
        {
            get
            {
                if (this.DynamicsContact)
                    return "Bill-To Contact";
                else
                    return "Primary Contact";
            }
        }

        public string Location
        {
            get
            {
                if (!string.IsNullOrEmpty(this.City) && !string.IsNullOrEmpty(this.State))
                    return string.Format("{0}, {1}", this.State, this.City);
                else if (!string.IsNullOrEmpty(this.City))
                    return this.City;
                else if (!string.IsNullOrEmpty(this.State))
                    return this.State;

                return "";
            }
        }
    }
}
