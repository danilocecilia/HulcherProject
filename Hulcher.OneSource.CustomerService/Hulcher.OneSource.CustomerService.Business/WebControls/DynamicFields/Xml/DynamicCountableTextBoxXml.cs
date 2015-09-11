using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Base;
using System.Web.UI.WebControls;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Xml
{
    [Serializable()]
    public class DynamicCountableTextBoxXml : DynamicControls
    {
        public DynamicCountableTextBoxXml()
        {
        }

        public DynamicCountableTextBoxXml(string text, string name, bool isRequired, string errorMessage, string validationGroup, int maxChars, int maxCharsWarning, TextBoxMode textMode, double width, double height, DynamicLabel label, string css, string style, bool visible)
        {
            _text = text;
            Name = name;
            _isRequired = isRequired;
            _errorMessage = errorMessage;
            _validationGroup = validationGroup;
            _MaxChars = maxChars;
            _MaxCharsWarning = maxCharsWarning;
            _textMode = textMode;
            _width = width;
            _height = height;
            Label = label;
            Css = css;
            Style = style;
            Visible = visible;
        }

        string _text;
        bool _isRequired;
        string _errorMessage;
        string _validationGroup;
        int _MaxChars;
        int _MaxCharsWarning;
        TextBoxMode _textMode;
        double _width;
        double _height;
        
        public int MaxChars
        {
            get
            {
                return _MaxChars;
            }
            set
            {
                _MaxChars = value;
            }
        }

        public int MaxCharsWarning
        {
            get
            {
                return _MaxCharsWarning;
            }
            set
            {
                _MaxCharsWarning = value;
            }
        }

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

        public TextBoxMode TextMode
        {
            get { return _textMode; }
            set { _textMode = value; }
        }

        public double Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public double Height
        {
            get { return _height; }
            set { _height = value; }
        }
    }
}
