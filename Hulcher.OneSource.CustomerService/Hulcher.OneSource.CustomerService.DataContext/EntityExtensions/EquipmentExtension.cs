using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;

namespace Hulcher.OneSource.CustomerService.DataContext
{
    public partial class CS_Equipment : EntityObject
    {
        public string CompleteName
        {
            get
            {
                string completeName = this._Name;

                if (this._ComboID.HasValue)
                {
                    completeName = string.Format("{0}, {1}", this.CS_EquipmentCombo.Name, completeName);

                    if (this.CS_EquipmentCombo.PrimaryEquipmentID == this._ID)
                        completeName = string.Format("{0}(P)", completeName);
                }

                return completeName;
            }
        }

        public string DivisionName
        {
            get
            {
                string divisionName = string.Empty;
                
                if (null != this.CS_Division)
                    divisionName = this.CS_Division.ExtendedDivisionName;

                if (null != this.CS_EquipmentCoverage && this.CS_EquipmentCoverage.Count > 0)
                {
                    CS_EquipmentCoverage coverage = this.CS_EquipmentCoverage.Where(e => e.Active).FirstOrDefault();

                    if (null != coverage && null != coverage.CS_Division)
                        divisionName = string.Format("C {0}/{1}", coverage.CS_Division.ExtendedDivisionName, divisionName);
                }

                return divisionName;
            }
        }

        public bool WhiteLight
        {
            get
            {
                bool whiteLight = false;

                if (null != this.CS_EquipmentWhiteLight && this.CS_EquipmentWhiteLight.Count > 0)
                {
                    CS_EquipmentWhiteLight equipmentWhiteLight = this.CS_EquipmentWhiteLight.Where(e => e.Active).FirstOrDefault();

                    if (null != equipmentWhiteLight)
                        whiteLight = true;
                }

                return whiteLight;
            }
        }
    }
}
