using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;

namespace Hulcher.OneSource.CustomerService.DataContext
{
    /// <summary>
    /// Entity object for Customer
    /// </summary>
    public partial class CS_Customer : EntityObject
    {
        /// <summary>
        /// Returns Name, Attn and Number concatenated
        /// </summary>
        public string FullCustomerInformation
        {
            get
            {
                string text = string.Empty;
                if (Attn != null)
                {
                    string attn = Attn.Trim();

                    if (!string.IsNullOrEmpty(Attn.Trim()))
                        return string.Format("{0} - {1} - {2} - {3}", Name.Trim(), (Country == null ? "" : Country.Trim()), (Attn == null ? "" : Attn.Trim()), (CustomerNumber == null ? "" : CustomerNumber.Trim())).Trim();
                    else
                    {
                        text = !string.IsNullOrEmpty(Name) ? Name.Trim() : string.Empty;
                        text += !string.IsNullOrEmpty(Country) ? " - " + Country.Trim() : string.Empty;
                        text += !string.IsNullOrEmpty(CustomerNumber) ? " - " + CustomerNumber.Trim() : string.Empty;
                        //return string.Format("{0} - {1} - {2}", Name.Trim(), (Country == null ? "" : Country.Trim()), (CustomerNumber == null ? "" : CustomerNumber.Trim())).Trim();
                        return text;
                    }
                }
                else
                {
                    return string.Format("{0} - {1} - {2}", Name.Trim(), (Country == null ? "" : Country.Trim()), (CustomerNumber == null ? "" : CustomerNumber.Trim())).Trim();
                }
            }
        }

        public string CustomerPhoneNumberEdited
        {
            get
            {
                if (string.IsNullOrEmpty(Phone))
                    return string.Empty;
                else if ((Country == "US" || Country == "CA" || string.IsNullOrEmpty(Country)) && Phone.Trim().Length == 10)
                    return string.Format("({0}) {1}-{2}", Phone.Substring(0, 3), Phone.Substring(3, 3), Phone.Substring(6));
                else if ((string.IsNullOrEmpty(Country) || Country == "MX") && Phone.Trim().Length == 11)
                    return string.Format("({0}) {1}-{2}", Phone.Substring(0, 3), Phone.Substring(3, 4), Phone.Substring(7));
                else if ((string.IsNullOrEmpty(Country) || Country == "MX") && Phone.Trim().Length == 10)
                    return string.Format("({0}) {1}-{2}", Phone.Substring(0, 2), Phone.Substring(2, 4), Phone.Substring(6));
                else if ((Country == "US" || Country == "CA" || string.IsNullOrEmpty(Country)) && Phone.Trim().Length == 7)
                    return string.Format("{0}-{1}", Phone.Substring(0, 3), Phone.Substring(3));
                else if ((string.IsNullOrEmpty(Country) || Country == "MX") && Phone.Trim().Length == 8)
                    return string.Format("{0}-{1}", Phone.Substring(0, 4), Phone.Substring(4));
                else
                    return Phone.Trim();

            }
        }

        public string CustomerFaxNumberEdited
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

        public string BillingCustomerPhoneNumberEdited
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

        public string BillingCustomerFaxNumberEdited
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
    }
}
