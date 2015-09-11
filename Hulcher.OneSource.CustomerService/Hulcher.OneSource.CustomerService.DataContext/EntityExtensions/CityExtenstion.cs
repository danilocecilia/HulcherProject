using System.Data.Objects.DataClasses;

namespace Hulcher.OneSource.CustomerService.DataContext
{
    public partial class CS_City : EntityObject
    {

        public string ExtendedName
        {
            get
            {
                string name = Name.Trim();

                if (!string.IsNullOrEmpty(name.Trim()) && CSRecord)
                    return "* " + Name.Trim();
                else
                    return Name.Trim();
            }
        }

        /// <summary>
        /// Returns Name City, Name State
        /// </summary>
        public string CityStateInformation
        {
            get
            {
                if (ExtendedName != null && CS_State != null)
                {
                    string name = ExtendedName.Trim();

                    if (!string.IsNullOrEmpty(name.Trim()))
                        return string.Format("{0} - {1}", ExtendedName.Trim(), CS_State.Name.Trim());
                }

                return string.Empty;
            }
        }
    }
}
