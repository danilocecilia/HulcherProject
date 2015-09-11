using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Base;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Xml
{
    [Serializable()]
    public class DynamicTextBoxXml : DynamicControls
    {
        public DynamicTextBoxXml()
        {
        }

        public DynamicTextBoxXml(string text, string name, bool isRequired, string errorMessage, string validationGroup, int maxLength, DynamicLabel label, string css, string style, bool visible)
        {
            _text = text;
            Name = name;
            _isRequired = isRequired;
            _errorMessage = errorMessage;
            _validationGroup = validationGroup;
            _maxLength = maxLength;
            Label = label;
            Css = css;
            Style = style;
            Visible = visible;
        }

        string _text;
        bool _isRequired;
        string _errorMessage;
        string _validationGroup;
        int _maxLength;

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }

        public bool IsRequired
        {
            get
            {
                return _isRequired;
            }
            set
            {
                _isRequired = value;
            }
        }

        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
            }
        }

        public string ValidationGroup
        {
            get
            {
                return _validationGroup;
            }
            set
            {
                _validationGroup = value;
            }
        }

        public int MaxLength
        {
            get { return _maxLength; }
            set { _maxLength = value; }
        }
    }
}
