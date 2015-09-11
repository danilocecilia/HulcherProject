using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Xml;
using System.ComponentModel;
using System.Web.UI;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.ServerControls
{
    public class DynamicTime : WebControl
    {
        #region [ Attributes ]

        private TextBox _textBox;
        private MaskedEditExtender _mskEditExtender;
        private MaskedEditValidator _mskEditValidator;
        private DynamicTimeXml _timePickerXml;

        #endregion

        #region [ Properties ]

        public override string ID
        {
            get
            {
                return base.ID;
            }
            set
            {
                base.ID = value;
                _textBox.ID = value + "_text";
                _mskEditExtender.ID = _textBox.ID + "_mee";
                _mskEditValidator.ID = _textBox.ID + "_mev";
                _mskEditExtender.TargetControlID = _textBox.ID;
                _mskEditValidator.ControlToValidate = _textBox.ID;
                _mskEditValidator.ControlExtender = _mskEditExtender.ID;

            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Text
        {
            get
            {
                //String s = (String)ViewState["Text"];
                //return ((s == null) ? String.Empty : s);
                return _textBox.Text;
            }

            set
            {
                //ViewState["Text"] = value;
                _textBox.Text = value;
            }
        }

        public string Mask
        {
            get { return _mskEditExtender.Mask; }
            set { _mskEditExtender.Mask = value; }
        }

        public MaskedEditType MaskedType
        {
            get { return _mskEditExtender.MaskType; }
            set 
            {
                _mskEditExtender.MaskType = value;  
            }            
        }

        public bool IsValidEmpty
        {
            get { return _mskEditValidator.IsValidEmpty; }
            set { _mskEditValidator.IsValidEmpty = value; }
        }

        public string ValidationGroup
        {
            get { return _mskEditValidator.ValidationGroup; }
            set
            {
                _mskEditValidator.ValidationGroup = value;
                _textBox.ValidationGroup = value;
            }
        }

        public string InvalidValueMessage
        {
            get { return _mskEditValidator.InvalidValueMessage; }
            set { _mskEditValidator.InvalidValueMessage = value; }
        }

        public string EmptyValueMessage
        {
            get { return _mskEditValidator.EmptyValueMessage; }
            set { _mskEditValidator.EmptyValueMessage = value;}
        }

        #endregion

        #region [ Constructors ]

        public DynamicTime()
        {
            _textBox = new TextBox();
            _textBox.CausesValidation = false;

            _mskEditExtender = new MaskedEditExtender();
            _mskEditExtender.TargetControlID = _textBox.ID;
            _mskEditExtender.Mask = "99:99";
            _mskEditExtender.MaskType = MaskedEditType.Time;
            _mskEditExtender.AcceptAMPM = false;
            _mskEditExtender.UserTimeFormat = MaskedEditUserTimeFormat.TwentyFourHour;
            _mskEditExtender.AutoComplete = true;
            

            _mskEditValidator = new MaskedEditValidator();
            _mskEditValidator.ControlExtender = _mskEditExtender.ID;
            _mskEditValidator.ControlToValidate = _textBox.ID;
            _mskEditValidator.EnableClientScript = true;
            _mskEditValidator.Display = ValidatorDisplay.Dynamic;
            _mskEditValidator.InvalidValueBlurredMessage = "*";
            _mskEditValidator.InvalidValueMessage = "invalid value message";
            _mskEditValidator.EmptyValueMessage = "empty value message";
            _mskEditValidator.EmptyValueBlurredText = "*";

            this.Controls.Add(_textBox);
            this.Controls.Add(_mskEditExtender);
            this.Controls.Add(_mskEditValidator);

            this._timePickerXml = new DynamicTimeXml();
        }

        public DynamicTime(DynamicTimeXml xml)
        {
            _textBox = new TextBox();
            _textBox.CausesValidation = false;
            _mskEditExtender = new MaskedEditExtender();
            _mskEditExtender.Mask = xml.Mask;
            _mskEditExtender.MaskType = xml.MaskedType;
            _mskEditExtender.InputDirection = xml.InputDirection;
            //_mskEditExtender.AcceptAMPM = false;
            //_mskEditExtender.UserTimeFormat = MaskedEditUserTimeFormat.TwentyFourHour;
            //_mskEditExtender.AutoComplete = true;

            _mskEditValidator = new MaskedEditValidator();
            _mskEditValidator.IsValidEmpty = xml.IsValidEmpty;
            _mskEditValidator.EnableClientScript = true;
            _mskEditValidator.Display = ValidatorDisplay.Dynamic;
            _mskEditValidator.InvalidValueBlurredMessage = "*";
            _mskEditValidator.InvalidValueMessage = xml.InvalidValueMessage;
            _mskEditValidator.EmptyValueMessage = xml.EmptyValueMessage;
            _mskEditValidator.EmptyValueBlurredText = "*";
            this.ValidationGroup = xml.ValidationGroup;
            this.Visible = xml.Visible;
            this.ID = xml.Name;

            this.Controls.Add(_textBox);
            this.Controls.Add(_mskEditExtender);
            this.Controls.Add(_mskEditValidator);

            this._timePickerXml = xml;

            _textBox.Text = xml.Text;
        }

        #endregion

        #region [ Methods ]

        public DynamicTimeXml CreateObjectToSerialize()
        {
            return new DynamicTimeXml(this.Text, this._timePickerXml.Name,this._timePickerXml.Mask, this._timePickerXml.MaskedType,this._timePickerXml.IsValidEmpty,
                this._timePickerXml.ValidationGroup, _timePickerXml.InvalidValueMessage, this._timePickerXml.EmptyValueMessage, this._timePickerXml.Label, this._timePickerXml.Css, this._timePickerXml.Style, this._timePickerXml.InputDirection, this.Visible);
        }

        #endregion

        #region [ Overrides ]

        protected override void RenderContents(HtmlTextWriter output)
        {
            if (!string.IsNullOrEmpty(_timePickerXml.Label.Text))
            {
                Label lbl = new Label();
                lbl.ID = string.Format("lbl{0}", _timePickerXml.Name);
                lbl.Text = _timePickerXml.Label.Text;

                if (!string.IsNullOrEmpty(_timePickerXml.Label.Css))
                    lbl.CssClass = _timePickerXml.Label.Css;

                if (!string.IsNullOrEmpty(_timePickerXml.Label.Style))
                {
                    string[] styles = _timePickerXml.Label.Style.Split(';');

                    foreach (string style in styles)
                    {
                        string[] attributes = style.Split(':');

                        if (attributes.Length == 2)
                            lbl.Style.Add(attributes[0], attributes[1]);
                    }
                }

                lbl.RenderControl(output);
            }

            if (!string.IsNullOrEmpty(_timePickerXml.Css))
            {
                this._textBox.CssClass = _timePickerXml.Css;
            }

            if (!string.IsNullOrEmpty(_timePickerXml.Style))
            {
                string[] styles = _timePickerXml.Style.Split(';');

                foreach (string style in styles)
                {
                    string[] attributes = style.Split(':');

                    if (attributes.Length == 2)
                        this._textBox.Style.Add(attributes[0], attributes[1]);
                }
            }

            base.RenderContents(output);
            output.WriteBreak();
        }

        #endregion
    }
}
