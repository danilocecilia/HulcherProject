using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Base;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Xml
{
    [Serializable()]
    public class DynamicDropDownListXml : DynamicControls
    {
        #region [ Attributes ]

        private int _filterId;
        private string _selectedValue;
        private string _selectedText;
        private bool _isRequired;
        private string _validationGroup;
        private string _errorMessage;

        #endregion

        #region [ Properties ]

        public int FilterId
        {
            get { return _filterId; }
            set { _filterId = value; }
        }

        public string SelectedValue
        {
            get { return _selectedValue; }
            set { _selectedValue = value; }
        }

        public string SelectedText
        {
            get { return _selectedText; }
            set { _selectedText = value; }
        }

        public bool IsRequired
        {
            get { return _isRequired; }
            set { _isRequired = value; }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        public string ValidationGroup
        {
            get { return _validationGroup; }
            set { _validationGroup = value; }
        }

        #endregion

        #region [ Constructors ]

        public DynamicDropDownListXml()
        {
        }

        public DynamicDropDownListXml(int filterId, string name, string selectedValue,string selectedText, bool isRequired, string errorMessage, string validationGroup, DynamicLabel label, string css, string style,bool visible)
        {
            _filterId = filterId;
            Name = name;
            _selectedValue = selectedValue;
            _selectedText = selectedText;
            _isRequired = isRequired;
            _errorMessage = errorMessage;
            _validationGroup = validationGroup;
            Label = label;
            Css = css;
            Style = style;
            this.Visible = visible;
        }

        #endregion
    }
}
