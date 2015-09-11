using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Xml;
using Hulcher.OneSource.CustomerService.Business.WebControls.ServerControls;
using System.Web.UI;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Base;
using Hulcher.OneSource.CustomerService.Core;
using System.Web.UI.WebControls;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Extender;
using System.Reflection;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.Utils
{
    public static class DynamicFieldsParser
    {
        public static DynamicFieldsAggregator GetAggregatorFromXml(string Xml)
        {
            DynamicFieldsSerialize serializer = new DynamicFieldsSerialize();
            DynamicFieldsAggregator aggregator = serializer.DeserializeObject(Xml);

            return aggregator;
        }

        /// <summary>
        /// Receives an Xml String and Deserializes it creating a list of objects
        /// </summary>
        /// <param name="Xml"></param>
        /// <returns></returns>
        public static List<Control> CreateFieldFromXml(string Xml, Dictionary<string, object> parameters)
        {
            DynamicFieldsAggregator aggregator = GetAggregatorFromXml(Xml);

            return CreateDynamicsFields(aggregator, parameters);
        }

        public static string CreateXmlFromControls(List<Control> controlList, string Xml)
        {
            DynamicFieldsAggregator aggregator = GetAggregatorFromXml(Xml);
            DynamicFieldsAggregator aggregatorCopy = GetAggregatorFromXml(Xml);

            aggregator.Controls.Clear();

            for (int i = 0; i < controlList.Count; i++)
            {
                if (controlList[i] is DynamicDropDownList)
                {
                    DynamicDropDownList customControl = controlList[i] as DynamicDropDownList;
                    DynamicDropDownListXml xml = customControl.CreateObjectToSerialize();
                    xml.Label = aggregatorCopy.Controls.Find(l => l.Name == xml.Name).Label;
                    aggregator.Controls.Add(xml);
                }
                else if (controlList[i] is DynamicTextBox)
                {
                    DynamicTextBox customControl = controlList[i] as DynamicTextBox;
                    DynamicTextBoxXml xml = customControl.CreateObjectToSerialize();
                    xml.Label = aggregatorCopy.Controls.Find(l => l.Name == xml.Name).Label;
                    aggregator.Controls.Add(xml);
                }
                else if (controlList[i] is DynamicGridView)
                {
                    DynamicGridView customControl = controlList[i] as DynamicGridView;
                    DynamicGridViewXml xml = customControl.CreateObjectToSerialize();
                    xml.Label = aggregatorCopy.Controls.Find(l => l.Name == xml.Name).Label;
                    aggregator.Controls.Add(xml);
                }
                else if (controlList[i] is DynamicCountableTextBox)
                {
                    DynamicCountableTextBox customControl = controlList[i] as DynamicCountableTextBox;
                    DynamicCountableTextBoxXml xml = customControl.CreateObjectToSerialize();
                    xml.Label = aggregatorCopy.Controls.Find(l => l.Name == xml.Name).Label;
                    aggregator.Controls.Add(xml);
                }
                else if (controlList[i] is DynamicDatePicker)
                {
                    DynamicDatePicker customControl = controlList[i] as DynamicDatePicker;
                    DynamicDatePickerXml xml = customControl.CreateObjectToSerialize();
                    xml.Label = aggregatorCopy.Controls.Find(l => l.Name == xml.Name).Label;
                    aggregator.Controls.Add(xml);
                }
                else if (controlList[i] is DynamicTime)
                {
                    DynamicTime customControl = controlList[i] as DynamicTime;
                    DynamicTimeXml xml = customControl.CreateObjectToSerialize();
                    xml.Label = aggregatorCopy.Controls.Find(l => l.Name == xml.Name).Label;
                    aggregator.Controls.Add(xml);
                }
                else if (controlList[i] is DynamicFilteredTextBox)
                {
                    DynamicFilteredTextBox customControl = controlList[i] as DynamicFilteredTextBox;
                    DynamicFilteredTextBoxXml xml = customControl.CreateObjectToSerialize();
                    xml.Label = aggregatorCopy.Controls.Find(l => l.Name == xml.Name).Label;
                    aggregator.Controls.Add(xml);
                }
                else if (controlList[i] is DynamicCheckBox)
                {
                    DynamicCheckBox customControl = controlList[i] as DynamicCheckBox;
                    DynamicCheckBoxXml xml = customControl.CreateObjectToSerialize();
                    xml.Label = aggregatorCopy.Controls.Find(l => l.Name == xml.Name).Label;
                    aggregator.Controls.Add(xml);
                }
                else if (controlList[i] is DynamicRadioButtonList)
                {
                    DynamicRadioButtonList customControl = controlList[i] as DynamicRadioButtonList;
                    DynamicRadioButtonListXml xml = customControl.CreateObjectToSerialize();
                    xml.Label = aggregatorCopy.Controls.Find(l => l.Name == xml.Name).Label;
                    aggregator.Controls.Add(xml);
                }
            }

            return CreateXmlFromField(aggregator);
        }

        /// <summary>
        /// Receives an DynamicFieldsAggregator and serialize it creating a Xml String
        /// </summary>
        public static string CreateXmlFromField(DynamicFieldsAggregator aggregator)
        {
            DynamicFieldsSerialize serializer = new DynamicFieldsSerialize();

            return serializer.SerializeObject(aggregator);
        }

        /// <summary>
        /// Method that will create the ASP.net Controls based on the DynamicFieldsAggregator Class
        /// </summary>
        private static List<Control> CreateDynamicsFields(DynamicFieldsAggregator aggregator, Dictionary<string, object> parameters)
        {
            List<Control> controlList = new List<Control>();

            foreach (DynamicControls control in aggregator.Controls)
            {
                string controlValue = string.Empty;

                if (parameters.ContainsKey("DateTime"))
                {
                    if (control.Name.Equals(Globals.CallEntry.ActionDateFieldName))
                        controlValue = ((DateTime)parameters["DateTime"]).ToString("MM/dd/yyyy");
                    else if (control.Name.Equals(Globals.CallEntry.ActionTimeFieldName))
                        controlValue = ((DateTime)parameters["DateTime"]).ToString("HH:mm");
                }

                if (control is DynamicTextBoxXml)
                {
                    DynamicTextBox customControl = new DynamicTextBox(control as DynamicTextBoxXml);
                    controlList.Add(customControl);
                    if (customControl.Text.Equals(string.Empty) && !controlValue.Equals(string.Empty))
                        customControl.Text = controlValue;
                }
                else if (control is DynamicDropDownListXml)
                {
                    DynamicDropDownList customControl = new DynamicDropDownList(control as DynamicDropDownListXml);
                    controlList.Add(customControl);
                }
                else if (control is DynamicGridViewXml)
                {
                    DynamicGridView customControl = new DynamicGridView(control as DynamicGridViewXml);
                    controlList.Add(customControl);
                }
                else if (control is DynamicCountableTextBoxXml)
                {
                    DynamicCountableTextBox customControl = new DynamicCountableTextBox(control as DynamicCountableTextBoxXml);
                    controlList.Add(customControl);
                    if (customControl.Text.Equals(string.Empty) && !controlValue.Equals(string.Empty))
                        customControl.Text = controlValue;
                }
                else if (control is DynamicDatePickerXml)
                {
                    DynamicDatePicker customControl = new DynamicDatePicker(control as DynamicDatePickerXml);
                    controlList.Add(customControl);
                    if (!customControl.Value.HasValue && !controlValue.Equals(string.Empty))
                        customControl.Value = Convert.ToDateTime(controlValue, new System.Globalization.CultureInfo("en-US"));
                }
                else if (control is DynamicTimeXml)
                {
                    DynamicTime customControl = new DynamicTime(control as DynamicTimeXml);
                    controlList.Add(customControl);
                    if (customControl.Text.Equals(string.Empty) && !controlValue.Equals(string.Empty))
                        customControl.Text = controlValue;
                }
                else if (control is DynamicFilteredTextBoxXml)
                {
                    DynamicFilteredTextBox customControl = new DynamicFilteredTextBox(control as DynamicFilteredTextBoxXml);
                    controlList.Add(customControl);
                    if (customControl.Text.Equals(string.Empty) && !controlValue.Equals(string.Empty))
                        customControl.Text = controlValue;
                }
                else if (control is DynamicCheckBoxXml)
                {
                    DynamicCheckBox customControl = new DynamicCheckBox(control as DynamicCheckBoxXml);
                    controlList.Add(customControl);
                }
                else if (control is DynamicRadioButtonListXml)
                {
                    DynamicRadioButtonList customControl = new DynamicRadioButtonList(control as DynamicRadioButtonListXml);
                    controlList.Add(customControl);
                }
            }

            foreach (Extenders extender in aggregator.Extenders)
            {
                if (extender is VisibleExtenderXml)
                {
                    VisibleExtenderXml extenderXml = extender as VisibleExtenderXml;

                    WebControl caller = FindControlByName(controlList, extenderXml.CallerControlName) as WebControl;
                    WebControl target = FindControlByName(controlList, extenderXml.TargetControlName) as WebControl;

                    if (caller != null && target != null)
                    {
                        VisibleExtender vExtender = new VisibleExtender(extenderXml, caller, target);

                        if (caller is DynamicTextBox)
                        {
                            ((DynamicTextBox)caller).addExtender(vExtender);
                        }
                        else if (caller is DynamicCountableTextBox)
                        {
                            ((DynamicCountableTextBox)caller).addExtender(vExtender);
                        }
                        else if (caller is DynamicDropDownList)
                        {
                            ((DynamicDropDownList)caller).addExtender(vExtender);
                        }
                        else if (caller is DynamicRadioButtonList)
                        {
                            ((DynamicRadioButtonList)caller).addExtender(vExtender);
                        }
                        else if (caller is DynamicCheckBox)
                        {
                            ((DynamicCheckBox)caller).addExtender(vExtender);
                        }
                    }
                }
                else if (extender is AutoFillExtenderXml)
                {
                    AutoFillExtenderXml extenderXml = extender as AutoFillExtenderXml;

                    WebControl target = FindControlByName(controlList, extenderXml.TargetControlName) as WebControl;

                    if (target != null)
                    {
                        AutoFillExtender aExtender = new AutoFillExtender(extenderXml, target);
                        switch (extenderXml.Type)
                        {
                            case Hulcher.OneSource.CustomerService.Core.Globals.CallEntry.AutoFillType.JobCity:
                            case Hulcher.OneSource.CustomerService.Core.Globals.CallEntry.AutoFillType.JobState:
                            case Hulcher.OneSource.CustomerService.Core.Globals.CallEntry.AutoFillType.JobCountry:
                                if (parameters.ContainsKey("JobID"))
                                    aExtender._filterId = parameters["JobID"].ToString();
                                break;
                            case Hulcher.OneSource.CustomerService.Core.Globals.CallEntry.AutoFillType.PreviousCallType:
                                if (parameters.ContainsKey("JobID"))
                                    aExtender._filterId = parameters["JobID"].ToString();
                                break;
                            default:
                                break;
                        }

                        if (!string.IsNullOrEmpty(extenderXml.ConditionalControlName))
                            aExtender._conditionalControlName = extenderXml.ConditionalControlName;

                        if (target is DynamicTextBox)
                        {
                            ((DynamicTextBox)target).addExtender(aExtender);
                        }
                        else if (target is DynamicCountableTextBox)
                        {
                            ((DynamicCountableTextBox)target).addExtender(aExtender);
                        }
                        else if (target is DynamicDropDownList)
                        {
                            ((DynamicDropDownList)target).addExtender(aExtender);
                        }
                    }
                }
                else if (extender is ValidationExtenderXml)
                {
                    ValidationExtenderXml extenderXml = extender as ValidationExtenderXml;

                    WebControl caller = FindControlByName(controlList, extenderXml.CallerControlName) as WebControl;
                    WebControl target = FindControlByName(controlList, extenderXml.TargetControlName) as WebControl;

                    if (caller != null && target != null)
                    {
                        ValidationExtender vExtender = new ValidationExtender(extenderXml, target, caller);

                        if (caller is DynamicTextBox)
                        {
                            ((DynamicTextBox)caller).addExtender(vExtender);
                        }
                        else if (caller is DynamicCountableTextBox)
                        {
                            ((DynamicCountableTextBox)caller).addExtender(vExtender);
                        }
                    }
                }
                else if (extender is ListExtenderXml)
                {
                    ListExtenderXml extenderXml = extender as ListExtenderXml;

                    WebControl target = FindControlByName(controlList, extenderXml.TargetControlName) as WebControl;

                    if (target != null)
                    {
                        ListExtender lExtender = new ListExtender(extenderXml, target);

                        if (target is DynamicDropDownList)
                        {
                            ((DynamicDropDownList)target).addExtender(lExtender);
                        }
                        else if (target is DynamicRadioButtonList)
                        {
                            ((DynamicRadioButtonList)target).addExtender(lExtender);
                        }
                    }
                }
                else if (extender is CascadeListExtenderXml)
                {
                    CascadeListExtenderXml extenderXml = extender as CascadeListExtenderXml;

                    WebControl caller = FindControlByName(controlList, extenderXml.SourceControlName) as WebControl;
                    WebControl target = FindControlByName(controlList, extenderXml.TargetControlName) as WebControl;

                    if (caller != null && target != null)
                    {
                        CascadeListExtender cExtender = new CascadeListExtender(extenderXml, target, caller);

                        if (caller is DynamicDropDownList)
                        {
                            ((DynamicDropDownList)caller).addExtender(cExtender);
                        }
                    }
                }
                else if (extender is GridViewExtenderXml)
                {
                    GridViewExtenderXml extenderXml = extender as GridViewExtenderXml;

                    WebControl source = FindControlByName(controlList, extenderXml.SourceControlName) as WebControl;
                    WebControl target = FindControlByName(controlList, extenderXml.TargetControlName) as WebControl;

                    if (source != null && target != null)
                    {
                        GridViewExtender cExtender = new GridViewExtender(source, target, extenderXml.Label);

                        if (source is DynamicGridView)
                        {
                            ((DynamicGridView)source).AddExtender(cExtender);
                        }
                    }
                }
            }

            return controlList;
        }

        /// <summary>
        /// Find in the List of controls the one with the Correct Id
        /// </summary>
        private static Control FindControlByName(List<Control> controlList, string name)
        {
            return controlList.Find(delegate(Control match) { return match.ID == name; });
        }

        public static string FormatDynamicFieldsData(Dictionary<string, string> controls)
        {
            StringBuilder formatBuilder = new StringBuilder();
            foreach (var control in controls)
            {
                formatBuilder.Append(control.Key + "<Text>");
                formatBuilder.Append(control.Value + "<BL>");
            }

            return formatBuilder.ToString();
        }

        public static Dictionary<string, string> GetDynamicFieldControlsProperties(string xml)
        {
            Dictionary<string, string> dicResult = new Dictionary<string, string>();

            DynamicFieldsSerialize serialize = new DynamicFieldsSerialize();
            DynamicFieldsAggregator aggregator = serialize.DeserializeObject(xml);

            foreach (var control in aggregator.Controls)
            {
                PropertyInfo propertyInfo = control.GetType().GetProperty(GetPropertyNameFromControl(control));
                if (propertyInfo != null && propertyInfo.GetValue(control, null) != null)
                {
                    string value = string.Empty;

                    if (control is DynamicDatePickerXml)
                    {
                        value = DateTime.Parse(propertyInfo.GetValue(control, null).ToString()).ToShortDateString();
                    }
                    else
                    {
                        value = propertyInfo.GetValue(control, null).ToString();
                    }

                    if (!string.IsNullOrEmpty(value))
                        dicResult.Add(control.Label.Text, value);
                }
            }

            return dicResult;
        }

        private static string GetPropertyNameFromControl(DynamicControls control)
        {
            if (control is DynamicTextBoxXml)
            {
                return "Text";
            }
            else if (control is DynamicDropDownListXml)
            {
                return "SelectedText";
            }
            else if (control is DynamicGridViewXml)
            {
                return "Text";
            }
            else if (control is DynamicCountableTextBoxXml)
            {
                return "Text";
            }
            else if (control is DynamicDatePickerXml)
            {
                return "Text";
            }
            else if (control is DynamicTimeXml)
            {
                return "Text";
            }
            else if (control is DynamicFilteredTextBoxXml)
            {
                return "Text";
            }
            else if (control is DynamicCheckBoxXml)
            {
                return "Check";
            }
            else if (control is DynamicRadioButtonListXml)
            {
                return "SelectedText";
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
