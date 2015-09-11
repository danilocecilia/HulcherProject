using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Base;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Xml
{
    [Serializable()]
    public class DynamicRadioButtonListXml : DynamicControls
    {
        #region [ Attributes ]

        private string _selectedValue;
        private string _selectedText;
        private bool _isRequired;
        private string _validationGroup;
        private string _errorMessage;

        #endregion

        #region [ Properties ]

        public string SelectedValue
        {
            get { return _selectedValue; }
            set { _selectedValue = value; }
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

        public string SelectedText
        {
            get { return _selectedText; }
            set { _selectedText = value; }
        }

        #endregion

        #region [ Constructors ]

        public DynamicRadioButtonListXml()
        {
        }

        public DynamicRadioButtonListXml(string name, string selectedValue,string selectedText, bool isRequired, string errorMessage, string validationGroup, DynamicLabel label, string css, string style, bool visible)
        {
            Name = name;
            _selectedValue = selectedValue;
            _selectedText = selectedText;
            _isRequired = isRequired;
            _errorMessage = errorMessage;
            _validationGroup = validationGroup;
            Label = label;
            Css = css;
            Style = style;
            Visible = visible;
        }

        #endregion
    }
}
