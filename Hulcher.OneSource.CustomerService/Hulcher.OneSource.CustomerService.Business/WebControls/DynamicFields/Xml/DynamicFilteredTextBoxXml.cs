using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Base;

using AjaxControlToolkit;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Xml
{
    [Serializable()]
    public class DynamicFilteredTextBoxXml : DynamicControls
    {
        public DynamicFilteredTextBoxXml()
        {
        }

        public DynamicFilteredTextBoxXml(string text, string name, bool isRequired, string errorMessage, string validationGroup, double width, double height, int maxLength,
            FilterTypes filterType, FilterModes filterMode, int filterInterval, string validChars, string invalidChars,
            DynamicLabel label, string css, string style, bool visible)
        {
            _text = text;
            Name = name;
            _isRequired = isRequired;
            _errorMessage = errorMessage;
            _validationGroup = validationGroup;
            _width = width;
            _height = height;
            _maxLength = maxLength;
            _filterType = filterType;
            _filterMode = filterMode;
            _filterInterval = filterInterval;
            _validChars = validChars;
            _invalidChars = invalidChars;
            Label = label;
            Css = css;
            Style = style;
            Visible = visible;
        }

        string _text;
        bool _isRequired;
        string _errorMessage;
        string _validationGroup;
        double _width;
        double _height;
        int _maxLength;
        FilterTypes _filterType;
        FilterModes _filterMode;
        int _filterInterval;
        string _validChars;
        string _invalidChars;
        
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

        public FilterTypes FilterType
        {
            get { return _filterType; }
            set { _filterType = value; }
        }

        public FilterModes FilterMode
        {
            get { return _filterMode; }
            set { _filterMode = value; }
        }

        public int FilterInterval
        {
            get { return _filterInterval; }
            set { _filterInterval = value; }
        }

        public string ValidChars
        {
            get { return _validChars; }
            set { _validChars = value; }
        }

        public string InvalidChars
        {
            get { return _invalidChars; }
            set { _invalidChars = value; }
        }

        public int MaxLength
        {
            get { return _maxLength; }
            set { _maxLength = value; }
        }
    }
}