using System.Data.Objects.DataClasses;

namespace Hulcher.OneSource.CustomerService.DataContext
{
    public partial class CS_EquipmentType : EntityObject
    {
        /// <summary>
        /// Returns a Complete Name (Number - Name)
        /// </summary>
        public string CompleteName
        {
            get
            {
                if (Name != null && Number != null)
                {
                    string name = Name.Trim();

                    if (!string.IsNullOrEmpty(name.Trim()))
                        return string.Format("{0} - {1}", Number, name);
                }

                return string.Empty;
            }
        }
    }
}
