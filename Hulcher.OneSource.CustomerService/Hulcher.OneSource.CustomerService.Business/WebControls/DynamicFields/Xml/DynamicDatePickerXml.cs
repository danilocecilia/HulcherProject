using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Base;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Xml
{
    [Serializable()]
    public class DynamicDatePickerXml : DynamicControls
    {
        #region [ Attributes ]

        private DateTime? _text;
        private bool _isValidEmpty;
        private string _emptyValueMessage;
        private string _invalidValueMessage;
        private ServerControls.DtFormat _dateTimeFormat;
        private ServerControls.ShowOnTrigger _showOn;
        private string _validationGroup;

        #endregion

        #region [ Properties ]

        public DateTime? Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public bool IsValidEmpty
        {
            get { return _isValidEmpty; }
            set { _isValidEmpty = value; }
        }

        public string EmptyValueMessage
        {
            get { return _emptyValueMessage; }
            set { _emptyValueMessage = value; }
        }

        public string InvalidValueMessage
        {
            get { return _invalidValueMessage; }
            set { _invalidValueMessage = value; }
        }

        public ServerControls.DtFormat DateTimeFormat
        {
            get { return _dateTimeFormat; }
            set { _dateTimeFormat = value; }
        }

        public ServerControls.ShowOnTrigger ShowOn
        {
            get { return _showOn; }
            set { _showOn = value; }
        }

        public string ValidationGroup
        {
            get { return _validationGroup; }
            set { _validationGroup = value; }
        }

        #endregion

        #region [ Constructors ]

        public DynamicDatePickerXml()
        {
        }

        public DynamicDatePickerXml(DateTime? text, string name, bool isValidEmpty, string emptyValueMessage, string invalidValueMessage, ServerControls.DtFormat dateTimeFormat, ServerControls.ShowOnTrigger showOn, string validationGroup, bool visible)
        {
            _text = text;
            Name = name;
            _isValidEmpty = isValidEmpty;
            _emptyValueMessage = emptyValueMessage;
            _invalidValueMessage = invalidValueMessage;
            _dateTimeFormat = dateTimeFormat;
            _showOn = showOn;
            _validationGroup = validationGroup;
            Visible = visible;
        }

        #endregion
    }
}
