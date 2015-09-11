using System.Data.Objects.DataClasses;

namespace Hulcher.OneSource.CustomerService.DataContext
{
    public partial class CS_FirstAlert : EntityObject
    {
        public string Location
        {
            get
            {
                string location = string.Empty;

                if(CountryID.HasValue && CS_Country != null)
                {
                    location = CS_Country.Name; 
                }
                if(StateID.HasValue && CS_State != null)
                {
                    if(!string.IsNullOrEmpty(location))
                    {
                        location += ", " + CS_State.AcronymName;
                    }
                    else
                    {
                        location = CS_State.AcronymName;
                    }
                }
                if(CityID.HasValue && CS_City != null)
                {
                    if(!string.IsNullOrEmpty(location))
                    {
                        location += ", " + CS_City.ExtendedName;
                    }
                    else
                    {
                        location = CS_City.ExtendedName;
                    }
                }

                return location;
            }
        }
    }
}
