using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Base;
using System.Web.UI.WebControls;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Xml;
using Hulcher.OneSource.CustomerService.Business.WebControls.ServerControls;
using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Business.WebControls.Utils;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Extender
{
    public class AutoFillExtender : Extenders
    {
        public WebControl _targetControl;
        public string _conditionalControlName;
        public Globals.CallEntry.AutoFillType _type;
        public string _filterId;

        public AutoFillExtender(WebControl targetControl, Globals.CallEntry.AutoFillType type, string filterId)
        {
            _targetControl = targetControl;
            _type = type;
            _filterId = filterId;
        }

        public AutoFillExtender(AutoFillExtenderXml xml, WebControl target)
        {
            _targetControl = target;
            _type = xml.Type;
            _filterId = xml.FilterId;
            this.Name = xml.Name;
        }

        public void DoStuff(object sender, EventArgs e)
        {
            bool mustEnter = true;
            if (_targetControl is DynamicTextBox)
            {
                if (!string.IsNullOrEmpty(((DynamicTextBox)_targetControl).Text))
                    mustEnter = false;
            }
            else if (_targetControl is DynamicCountableTextBox)
            {
                if (!string.IsNullOrEmpty(((DynamicCountableTextBox)_targetControl).Text))
                    mustEnter = false;
            }
            else if (_targetControl is DynamicDropDownList)
            {
                if (((DynamicDropDownList)_targetControl).SelectedIndex != 0)
                    mustEnter = false;
            }

            if (mustEnter)
            {
                switch (_type)
                {
                    case Globals.CallEntry.AutoFillType.JobCity:

                        using (JobModel model = new JobModel())
                        {
                            CS_LocationInfo locationInfo = model.GetLocationInfoByJobId(Convert.ToInt32(_filterId));
                            if (null != locationInfo && null != locationInfo.CS_City)
                            {
                                if (_targetControl is DynamicTextBox)
                                    ((DynamicTextBox)_targetControl).Text = locationInfo.CS_City.Name;
                                else if (_targetControl is DynamicCountableTextBox)
                                    ((DynamicCountableTextBox)_targetControl).Text = locationInfo.CS_City.Name;
                                else if (_targetControl is DynamicDropDownList)
                                {
                                    if (null != ((DynamicDropDownList)_targetControl).Items.FindByValue(locationInfo.CityID.ToString()))
                                        ((DynamicDropDownList)_targetControl).SelectedValue = locationInfo.CityID.ToString();
                                }
                            }
                        }

                        break;

                    case Globals.CallEntry.AutoFillType.JobState:

                        using (JobModel model = new JobModel())
                        {
                            CS_LocationInfo locationInfo = model.GetLocationInfoByJobId(Convert.ToInt32(_filterId));
                            if (null != locationInfo && null != locationInfo.CS_State)
                            {
                                if (_targetControl is DynamicTextBox)
                                    ((DynamicTextBox)_targetControl).Text = locationInfo.CS_State.Name;
                                else if (_targetControl is DynamicCountableTextBox)
                                    ((DynamicCountableTextBox)_targetControl).Text = locationInfo.CS_State.Name;
                                else if (_targetControl is DynamicDropDownList)
                                {
                                    if (null != ((DynamicDropDownList)_targetControl).Items.FindByValue(locationInfo.StateID.ToString()))
                                        ((DynamicDropDownList)_targetControl).SelectedValue = locationInfo.StateID.ToString();

                                    if (null != ((DynamicDropDownList)_targetControl)._cascadeExtender)
                                        ((DynamicDropDownList)_targetControl)._cascadeExtender.DoStuff(null, null);
                                }                                    
                            }
                        }

                        break;

                    case Globals.CallEntry.AutoFillType.JobCountry:

                        using (JobModel model = new JobModel())
                        {
                            CS_LocationInfo locationInfo = model.GetLocationInfoByJobId(Convert.ToInt32(_filterId));
                            if (null != locationInfo && null != locationInfo.CS_Country)
                            {
                                if (_targetControl is DynamicTextBox)
                                    ((DynamicTextBox)_targetControl).Text = locationInfo.CS_Country.Name;
                                else if (_targetControl is DynamicCountableTextBox)
                                    ((DynamicCountableTextBox)_targetControl).Text = locationInfo.CS_Country.Name;
                                else if (_targetControl is DynamicDropDownList)
                                {
                                    if (null != ((DynamicDropDownList)_targetControl).Items.FindByValue(locationInfo.CountryID.ToString()))
                                        ((DynamicDropDownList)_targetControl).SelectedValue = locationInfo.CountryID.ToString();

                                    if (null != ((DynamicDropDownList)_targetControl)._cascadeExtender)
                                        ((DynamicDropDownList)_targetControl)._cascadeExtender.DoStuff(null, null);
                                }
                            }
                        }

                        break;

                    case Globals.CallEntry.AutoFillType.PreviousCallType:

                        using (CallLogModel model = new CallLogModel())
                        {
                            CS_CallLog callLog = model.GetLastCallEntryByFilter(Convert.ToInt32(_filterId), string.Empty);

                            if (null != callLog && !string.IsNullOrEmpty(callLog.CS_CallType.Description) && !string.IsNullOrEmpty(callLog.Xml))
                            {
                                DynamicFieldsSerialize serializer = new DynamicFieldsSerialize();
                                DynamicFieldsAggregator aggregator = serializer.DeserializeObject(callLog.Xml);

                                DynamicControls control = aggregator.Controls.Find(delegate(DynamicControls match) { return match.Name == _conditionalControlName; });

                                ((DynamicTextBox)_targetControl).Text = ((DynamicTextBoxXml)control).Text;
                                
                            }
                        }

                        break;

                    default:
                        break;
                }
            }
        }

        public override Extenders CreateObjectToSerialize()
        {
            return new AutoFillExtenderXml(this._targetControl.ID, this._type, this._filterId);
        }
    }
}
