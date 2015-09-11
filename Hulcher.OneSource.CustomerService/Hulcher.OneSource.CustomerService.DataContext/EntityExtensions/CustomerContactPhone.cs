using System.Data.Objects.DataClasses;
using System.Text;

namespace Hulcher.OneSource.CustomerService.DataContext
{
    partial class CS_View_Customer_Contact_Phone : EntityObject
    {
        public string FormatedCustomerHomePhone
        {
            get
            {
                if (string.IsNullOrEmpty(CustomerHomePhone))
                    return string.Empty;
                else if ((CustomerCountry == "US" || CustomerCountry == "CA" || string.IsNullOrEmpty(CustomerCountry)) && CustomerHomePhone.Trim().Length == 10)
                    return string.Format("({0}) {1}-{2}", CustomerHomePhone.Substring(0, 3), CustomerHomePhone.Substring(3, 3), CustomerHomePhone.Substring(6));
                else if ((string.IsNullOrEmpty(CustomerCountry) || CustomerCountry == "MX") && CustomerHomePhone.Trim().Length == 11)
                    return string.Format("({0}) {1}-{2}", CustomerHomePhone.Substring(0, 3), CustomerHomePhone.Substring(3, 4), CustomerHomePhone.Substring(7));
                else if ((string.IsNullOrEmpty(CustomerCountry) || CustomerCountry == "MX") && CustomerHomePhone.Trim().Length == 10)
                    return string.Format("({0}) {1}-{2}", CustomerHomePhone.Substring(0, 2), CustomerHomePhone.Substring(2, 4), CustomerHomePhone.Substring(6));
                else if ((CustomerCountry == "US" || CustomerCountry == "CA" || string.IsNullOrEmpty(CustomerCountry)) && CustomerHomePhone.Trim().Length == 7)
                    return string.Format("{0}-{1}", CustomerHomePhone.Substring(0, 3), CustomerHomePhone.Substring(3));
                else if ((string.IsNullOrEmpty(CustomerCountry) || CustomerCountry == "MX") && CustomerHomePhone.Trim().Length == 8)
                    return string.Format("{0}-{1}", CustomerHomePhone.Substring(0, 4), CustomerHomePhone.Substring(4));
                else
                    return CustomerHomePhone.Trim();
            }
        }

        public string FormatedCustomerBillPhone
        {
            get
            {
                if (string.IsNullOrEmpty(CustomerBillPhone))
                    return string.Empty;
                else if ((CustomerCountry == "US" || CustomerCountry == "CA" || string.IsNullOrEmpty(CustomerCountry)) && CustomerBillPhone.Trim().Length == 10)
                    return string.Format("({0}) {1}-{2}", CustomerBillPhone.Substring(0, 3), CustomerBillPhone.Substring(3, 3), CustomerBillPhone.Substring(6));
                else if ((string.IsNullOrEmpty(CustomerCountry) || CustomerCountry == "MX") && CustomerBillPhone.Trim().Length == 11)
                    return string.Format("({0}) {1}-{2}", CustomerBillPhone.Substring(0, 3), CustomerBillPhone.Substring(3, 4), CustomerBillPhone.Substring(7));
                else if ((string.IsNullOrEmpty(CustomerCountry) || CustomerCountry == "MX") && CustomerBillPhone.Trim().Length == 10)
                    return string.Format("({0}) {1}-{2}", CustomerBillPhone.Substring(0, 2), CustomerBillPhone.Substring(2, 4), CustomerBillPhone.Substring(6));
                else if ((CustomerCountry == "US" || CustomerCountry == "CA" || string.IsNullOrEmpty(CustomerCountry)) && CustomerBillPhone.Trim().Length == 7)
                    return string.Format("{0}-{1}", CustomerBillPhone.Substring(0, 3), CustomerBillPhone.Substring(3));
                else if ((string.IsNullOrEmpty(CustomerCountry) || CustomerCountry == "MX") && CustomerBillPhone.Trim().Length == 8)
                    return string.Format("{0}-{1}", CustomerBillPhone.Substring(0, 4), CustomerBillPhone.Substring(4));
                else
                    return CustomerBillPhone.Trim();
            }
        }

        public string FormatedCustomerFax
        {
            get
            {
                if (string.IsNullOrEmpty(CustomerFax))
                    return string.Empty;
                else if ((CustomerCountry == "US" || CustomerCountry == "CA" || string.IsNullOrEmpty(CustomerCountry)) && CustomerFax.Trim().Length == 10)
                    return string.Format("({0}) {1}-{2}", CustomerFax.Substring(0, 3), CustomerFax.Substring(3, 3), CustomerFax.Substring(6));
                else if ((string.IsNullOrEmpty(CustomerCountry) || CustomerCountry == "MX") && CustomerFax.Trim().Length == 11)
                    return string.Format("({0}) {1}-{2}", CustomerFax.Substring(0, 3), CustomerFax.Substring(3, 4), CustomerFax.Substring(7));
                else if ((string.IsNullOrEmpty(CustomerCountry) || CustomerCountry == "MX") && CustomerFax.Trim().Length == 10)
                    return string.Format("({0}) {1}-{2}", CustomerFax.Substring(0, 2), CustomerFax.Substring(2, 4), CustomerFax.Substring(6));
                else if ((CustomerCountry == "US" || CustomerCountry == "CA" || string.IsNullOrEmpty(CustomerCountry)) && CustomerFax.Trim().Length == 7)
                    return string.Format("{0}-{1}", CustomerFax.Substring(0, 3), CustomerFax.Substring(3));
                else if ((string.IsNullOrEmpty(CustomerCountry) || CustomerCountry == "MX") && CustomerFax.Trim().Length == 8)
                    return string.Format("{0}-{1}", CustomerFax.Substring(0, 4), CustomerFax.Substring(4));
                else
                    return CustomerFax.Trim();
            }
        }

        public string FormatedCustomerBillFax
        {
            get
            {
                if (string.IsNullOrEmpty(CustomerBillFax))
                    return string.Empty;
                else if ((CustomerCountry == "US" || CustomerCountry == "CA" || string.IsNullOrEmpty(CustomerCountry)) && CustomerBillFax.Trim().Length == 10)
                    return string.Format("({0}) {1}-{2}", CustomerBillFax.Substring(0, 3), CustomerBillFax.Substring(3, 3), CustomerBillFax.Substring(6));
                else if ((string.IsNullOrEmpty(CustomerCountry) || CustomerCountry == "MX") && CustomerBillFax.Trim().Length == 11)
                    return string.Format("({0}) {1}-{2}", CustomerBillFax.Substring(0, 3), CustomerBillFax.Substring(3, 4), CustomerBillFax.Substring(7));
                else if ((string.IsNullOrEmpty(CustomerCountry) || CustomerCountry == "MX") && CustomerBillFax.Trim().Length == 10)
                    return string.Format("({0}) {1}-{2}", CustomerBillFax.Substring(0, 2), CustomerBillFax.Substring(2, 4), CustomerBillFax.Substring(6));
                else if ((CustomerCountry == "US" || CustomerCountry == "CA" || string.IsNullOrEmpty(CustomerCountry)) && CustomerBillFax.Trim().Length == 7)
                    return string.Format("{0}-{1}", CustomerBillFax.Substring(0, 3), CustomerBillFax.Substring(3));
                else if ((string.IsNullOrEmpty(CustomerCountry) || CustomerCountry == "MX") && CustomerBillFax.Trim().Length == 8)
                    return string.Format("{0}-{1}", CustomerBillFax.Substring(0, 4), CustomerBillFax.Substring(4));
                else
                    return CustomerBillFax.Trim();
            }
        }
    }
}
