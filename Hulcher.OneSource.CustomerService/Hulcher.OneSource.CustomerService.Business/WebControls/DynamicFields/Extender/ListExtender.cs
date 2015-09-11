using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Base;
using System.Web.UI.WebControls;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Xml;
using System.Data;
using Hulcher.OneSource.CustomerService.Business.WebControls.ServerControls;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Business.Model;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Extender
{
    [Serializable()]
    public class ListExtender : Extenders
    {
        public WebControl _targetControl;
        public Globals.CallEntry.ListType _type;
        public List<ListExtenderItem> _items;
        public ListExtenderXml _extenderXml;
        public bool _filteredLoadOnly;

        public ListExtender(WebControl targetControl, Globals.CallEntry.ListType type)
        {
            _targetControl = targetControl;
            _type = type;
        }

        public ListExtender(ListExtenderXml xml, WebControl targetControl)
        {
            _targetControl = targetControl;
            Name = xml.Name;
            _type = xml.Type;
            _items = xml.Items;
            _filteredLoadOnly = xml.FilteredLoadOnly;

            _extenderXml = xml;
        }

        public void DoStuff(object sender, EventArgs e)
        {
            if (_targetControl is DynamicDropDownList)
            {
                DynamicDropDownList control = _targetControl as DynamicDropDownList;

                if ((_filteredLoadOnly && control.FilterId != 0) || !_filteredLoadOnly)
                {
                    switch (_type)
                    {
                        case Globals.CallEntry.ListType.Custom:
                            control = _targetControl as DynamicDropDownList;

                            control.DataTextField = "Name";
                            control.DataValueField = "Value";

                            control.DataSource = _items;
                            control.DataBind();

                            if (string.IsNullOrEmpty(control.SelectedValue))
                            {
                                ListExtenderItem item = _items.Find(delegate(ListExtenderItem match) { return match.Selected; });
                                control.Items.FindByValue(item.Value).Selected = true;
                            }

                            break;
                        case Globals.CallEntry.ListType.Country:

                            IList<CS_Country> countryList = null;
                            using (LocationModel model = new LocationModel())
                            {
                                countryList = model.ListAllCountries();
                            }

                            control.SelectedIndex = -1;
                            control.DataTextField = "Name";
                            control.DataValueField = "ID";
                            control.DataSource = countryList;
                            control.DataBind();
                            control.Items.Insert(0, new ListItem("- Select One -", ""));

                            break;
                        case Globals.CallEntry.ListType.State:

                            control = _targetControl as DynamicDropDownList;
                            IList<CS_State> stateList = null;
                            using (LocationModel model = new LocationModel())
                            {
                                if (control.FilterId > 0)
                                    stateList = model.GetStateByCountryId(control.FilterId);
                                else
                                    stateList = model.ListAllStatesOrderedByCountry();
                            }

                            control.SelectedIndex = -1;
                            control.DataTextField = "Name";
                            control.DataValueField = "ID";
                            control.DataSource = stateList;
                            control.DataBind();
                            control.Items.Insert(0, new ListItem("- Select One -", ""));

                            break;
                        case Globals.CallEntry.ListType.City:

                            control = _targetControl as DynamicDropDownList;
                            IList<CS_City> cityList = null;
                            using (LocationModel model = new LocationModel())
                            {
                                if (control.FilterId > 0)
                                    cityList = model.GetCityByState(control.FilterId);
                                else
                                    cityList = model.ListAllCities();
                            }

                            control.DataTextField = "Name";
                            control.DataValueField = "ID";
                            control.DataSource = cityList;
                            control.DataBind();
                            control.Items.Insert(0, new ListItem("- Select One -", ""));

                            break;
                        case Globals.CallEntry.ListType.Hotel:

                            control = _targetControl as DynamicDropDownList;
                            IList<CS_Hotel> hotelList = null;
                            using (CallLogModel model = new CallLogModel())
                            {
                                hotelList = model.ListAllHotels();
                            }

                            control.DataTextField = "Description";
                            control.DataValueField = "ID";
                            control.DataSource = hotelList;
                            control.DataBind();
                            control.Items.Insert(0, new ListItem("- Select One -", ""));

                            break;
                        case Globals.CallEntry.ListType.Subcontractor:

                            control = _targetControl as DynamicDropDownList;
                            IList<CS_Subcontractor> subcontractorList = null;
                            using (CallLogModel model = new CallLogModel())
                            {
                                subcontractorList = model.ListAllSubcontractors();
                            }

                            control.DataTextField = "Name";
                            control.DataValueField = "ID";
                            control.DataSource = subcontractorList;
                            control.DataBind();
                            control.Items.Insert(0, new ListItem("- Select One -", ""));

                            break;
                    }
                }
            }
            else if (_targetControl is DynamicRadioButtonList)
            {
                DynamicRadioButtonList control = _targetControl as DynamicRadioButtonList;

                if (!_filteredLoadOnly)
                {
                    switch (_type)
                    {
                        case Globals.CallEntry.ListType.Custom:
                            control = _targetControl as DynamicRadioButtonList;

                            control.DataTextField = "Name";
                            control.DataValueField = "Value";

                            control.DataSource = _items;
                            control.DataBind();

                            ListExtenderItem item = _items.Find(delegate(ListExtenderItem match) { return match.Selected; });
                            if (item != null)
                                control.Items.FindByValue(item.Value).Selected = true;

                            break;
                    }
                }
            }
        }

        public override Extenders CreateObjectToSerialize()
        {
            return new ListExtenderXml(this._targetControl.ID, this._type, this._items);
        }
    }
}
