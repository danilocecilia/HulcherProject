using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Base;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Xml;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields
{
    [XmlInclude(typeof(DynamicTextBoxXml))]
    [XmlInclude(typeof(DynamicCountableTextBoxXml))]
    [XmlInclude(typeof(DynamicDatePickerXml))]
    [XmlInclude(typeof(DynamicDropDownListXml))]
    [XmlInclude(typeof(VisibleExtenderXml))]
    [XmlInclude(typeof(AutoFillExtenderXml))]
    [XmlInclude(typeof(ListExtenderXml))]
    [XmlInclude(typeof(ValidationExtenderXml))]
    [XmlInclude(typeof(CascadeListExtenderXml))]
    [XmlInclude(typeof(DynamicGridViewXml))]
    [XmlInclude(typeof(GridViewExtenderXml))]
    [XmlInclude(typeof(DynamicFilteredTextBoxXml))]
    [XmlInclude(typeof(DynamicCheckBoxXml))]
    [XmlInclude(typeof(DynamicRadioButtonListXml))]
    [XmlInclude(typeof(DynamicTimeXml))]    
    public class DynamicFieldsAggregator
    {
        public DynamicFieldsAggregator()
        {
            _controls = new List<DynamicControls>();
            _extenders = new List<Extenders>();
        }

        List<DynamicControls> _controls;
        List<Extenders> _extenders;

        public List<DynamicControls> Controls
        {
            get
            {
                return _controls;
            }
        }

        public List<Extenders> Extenders
        {
            get
            {
                return _extenders;
            }
        }
    }
}
