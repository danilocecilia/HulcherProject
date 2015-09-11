using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Xml;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Extender;

using AjaxControlToolkit;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.ServerControls
{
    [Serializable()]
    public enum DtFormat { Default = 1, Iso8601, Short, Medium, Full, WithText }
    [Serializable()]
    public enum ShowOnTrigger { Focus = 1, Both, Button }
    [Serializable()]
    public enum MaskType { None = 1, Date = 2, DateTime = 3, Number = 4, Time = 5 }

    public class DynamicDatePicker : WebControl
    {
        #region [ Attributes ]

        private TextBox _textBox;
        private DynamicDatePickerXml _datePickerXml;
        private MaskedEditValidator _maskEditValidator;
        private MaskedEditExtender _maskEditExtender;
        private CompareValidator _compareValidator;

        protected string DateFormat;
        protected string ShowOnProp;

        private DtFormat _dateTimeFormat;
        private ShowOnTrigger _showOn;

        #endregion

        #region [ Properties ]

        public MaskedEditExtender MaskedEditExtender
        {
            get { return _maskEditExtender; }
            set { _maskEditExtender = value; }
        }

        [PersistenceMode(PersistenceMode.Attribute)]
        public string ValidationGroup
        {
            get { return _textBox.ValidationGroup; }
            set
            {
                _textBox.ValidationGroup = value;
                _compareValidator.ValidationGroup = value;
                _maskEditValidator.ValidationGroup = value;
            }
        }

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
                _maskEditExtender.ID = _textBox.ID + "_mee";
                _maskEditExtender.TargetControlID = _textBox.ID;
                _maskEditValidator.ID = _textBox.ID + "_mev";
                _maskEditValidator.ControlExtender = _maskEditExtender.ID;
                _maskEditValidator.ControlToValidate = _textBox.ID;
                _compareValidator.ID = _textBox.ID + "_cpv";
                _compareValidator.ControlToValidate = _textBox.ID;
            }
        }

        [PersistenceMode(PersistenceMode.Attribute)]
        public DtFormat DateTimeFormat
        {
            get
            {
                return _dateTimeFormat;
            }
            set
            {
                _dateTimeFormat = value;

                switch (value)
                {
                    case DtFormat.Default:
                        DateFormat = "mm/dd/yy";
                        _maskEditExtender.Mask = "99/99/9999";
                        _maskEditExtender.MaskType = MaskedEditType.Date;
                        _maskEditValidator.ValidationExpression = @"^((0[1-9])|(1[0-2]))\/((0[1-9])|(1[0-9])|(2[0-9])|(3[0-1]))\/(\d{4})$";
                        break;
                    case DtFormat.Iso8601:
                        DateFormat = "yy/mm/dd";
                        _maskEditExtender.Mask = "9999/99/99";
                        _maskEditExtender.MaskType = MaskedEditType.Date;
                        _maskEditValidator.ValidationExpression = @"^(19|20)\d\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$";
                        break;
                    case DtFormat.Short:
                        DateFormat = "d M, y";
                        break;
                    case DtFormat.Medium:
                        DateFormat = "d MM, y";
                        break;
                    case DtFormat.Full:
                        DateFormat = "DD, d MM, yy";
                        break;
                    case DtFormat.WithText:
                        DateFormat = "'day' d 'of' MM 'in the year' yy";
                        break;
                }
            }
        }

        [PersistenceMode(PersistenceMode.Attribute)]
        public ShowOnTrigger ShowOn
        {
            get
            {
                return _showOn;
            }
            set
            {
                _showOn = value;

                ShowOnProp = _showOn.ToString().ToLower();
            }
        }

        [PersistenceMode(PersistenceMode.Attribute)]
        public string EmptyValueMessage
        {
            get
            {
                return _maskEditValidator.EmptyValueMessage;
            }
            set
            {
                _maskEditValidator.EmptyValueMessage = value;
            }
        }

        [PersistenceMode(PersistenceMode.Attribute)]
        public string InvalidValueMessage
        {
            get
            {
                return _maskEditValidator.InvalidValueMessage;
            }
            set
            {
                _maskEditValidator.InvalidValueMessage = value;
            }
        }

        public DateTime? Value
        {
            get
            {
                if (_textBox.Text.Length.Equals(0))
                    return null;
                else
                    return Convert.ToDateTime(_textBox.Text);
            }
            set
            {
                if (value.HasValue)
                {
                    switch (_dateTimeFormat)
                    {
                        case DtFormat.Default:
                            _textBox.Text = value.Value.ToString("MM/dd/yyyy");
                            break;
                        case DtFormat.Iso8601:
                            _textBox.Text = value.Value.ToString("yyyy/MM/dd");
                            break;
                        case DtFormat.Short:
                            _textBox.Text = value.Value.ToString("d, M, y");
                            break;
                        case DtFormat.Medium:
                            _textBox.Text = value.Value.ToString("d MM, y");
                            break;
                        case DtFormat.Full:
                            _textBox.Text = value.Value.ToString("DD, d MM, yy");
                            break;
                        case DtFormat.WithText:
                            _textBox.Text = value.Value.ToString("'day' d 'of' MM 'in the year' yy");
                            break;
                    }
                }
                else
                    _textBox.Text = string.Empty;
            }
        }

        [PersistenceMode(PersistenceMode.Attribute)]
        public bool IsValidEmpty
        {
            get
            {
                return _maskEditValidator.IsValidEmpty;
            }
            set
            {
                _maskEditValidator.IsValidEmpty = value;
            }
        }

        [PersistenceMode(PersistenceMode.Attribute)]
        public string TextCompareValidator
        {
            get
            {
                return _compareValidator.ErrorMessage;
            }
            set
            {
                _compareValidator.ErrorMessage = value;

            }
        }

        [PersistenceMode(PersistenceMode.Attribute)]
        public ValidationCompareOperator ValidationCompareOperator
        {
            get
            {
                return _compareValidator.Operator;
            }
            set
            {
                _compareValidator.Operator = value;
            }
        }

        [PersistenceMode(PersistenceMode.Attribute)]
        public bool EnableCompareValidator
        {
            get
            {
                return _compareValidator.Visible;
            }
            set
            {
                _compareValidator.Visible = value;
            }
        }

        [PersistenceMode(PersistenceMode.Attribute)]
        public string ValueToCompare
        {
            get
            {
                return _compareValidator.ValueToCompare;
            }
            set
            {
                _compareValidator.ValueToCompare = value;
            }
        }

        [PersistenceMode(PersistenceMode.Attribute)]
        public bool EnableDatePicker
        {
            get
            {
                return _textBox.Enabled;
            }
            set
            {
                _textBox.Enabled = value;
                var script = new StringBuilder();

                if (!value)
                {
                    script.Append("$('#" + _textBox.ClientID + "').datepicker( \"disable\" );");
                }
                else
                {
                    script.Append("$('#" + _textBox.ClientID + "').datepicker( \"enable\" );");
                }

                ClientScriptManager cs = this.Page.ClientScript;
                cs.RegisterStartupScript(GetType(), string.Format("dPicker_{0}_Enable", _textBox.ClientID), script.ToString(), true);
            }
        }

        #endregion

        #region [ Constructors ]

        public DynamicDatePicker()
        {
            _textBox = new TextBox();
            _textBox.CausesValidation = false;
            _textBox.Width = Unit.Pixel(70);

            _maskEditExtender = new MaskedEditExtender();
            _maskEditExtender.ClearMaskOnLostFocus = true;

            _maskEditValidator = new MaskedEditValidator();
            _maskEditValidator.Display = ValidatorDisplay.Dynamic;
            _maskEditValidator.EnableClientScript = true;
            _maskEditValidator.IsValidEmpty = false;
            _maskEditValidator.Text = "*";
            _maskEditValidator.EmptyValueBlurredText = "*";

            _compareValidator = new CompareValidator();
            _compareValidator.Visible = false;
            _compareValidator.ValueToCompare = "01/01/1900";
            _compareValidator.Display = ValidatorDisplay.Dynamic;
            _compareValidator.Type = ValidationDataType.Date;
            _compareValidator.EnableClientScript = true;
            _compareValidator.Text = "*";

            this.Controls.Add(_textBox);
            this.Controls.Add(_maskEditExtender);
            this.Controls.Add(_maskEditValidator);
            this.Controls.Add(_compareValidator);
        }

        public DynamicDatePicker(DynamicDatePickerXml xml)
        {
            _textBox = new TextBox();
            _textBox.CausesValidation = false;

            _maskEditExtender = new MaskedEditExtender();
            _maskEditExtender.ClearMaskOnLostFocus = true;

            _maskEditValidator = new MaskedEditValidator();
            _maskEditValidator.Display = ValidatorDisplay.Dynamic;
            _maskEditValidator.EnableClientScript = true;
            _maskEditValidator.IsValidEmpty = false;
            _maskEditValidator.Text = "*";
            _maskEditValidator.EmptyValueBlurredText = "*";

            _compareValidator = new CompareValidator();
            _compareValidator.Visible = false;
            _compareValidator.ValueToCompare = "01/01/1900";
            _compareValidator.Display = ValidatorDisplay.Dynamic;
            _compareValidator.Type = ValidationDataType.Date;
            _compareValidator.EnableClientScript = true;
            _compareValidator.Text = "*";

            this.Controls.Add(_textBox);
            this.Controls.Add(_maskEditExtender);
            this.Controls.Add(_maskEditValidator);
            this.Controls.Add(_compareValidator);

            this.ID = xml.Name;
            if (xml.Text.HasValue)
                _textBox.Text = xml.Text.Value.ToString("MM/dd/yyyy");

            this.Value = xml.Text;
            this.IsValidEmpty = xml.IsValidEmpty;
            this.EmptyValueMessage = xml.EmptyValueMessage;
            this.InvalidValueMessage = xml.InvalidValueMessage;
            this.DateTimeFormat = xml.DateTimeFormat;
            this.ShowOn = xml.ShowOn;
            this.ValidationGroup = xml.ValidationGroup;

            this._datePickerXml = xml;

            this.Visible = xml.Visible;
        }

        #endregion

        #region [ Overrides ]

        protected override void RenderContents(HtmlTextWriter output)
        {
            StringBuilder script = new StringBuilder();
            script.Append("$('#" + this._textBox.ClientID + "').blur(function() {");
            script.AppendFormat("var ctrl = document.getElementById('{0}');", this._textBox.ClientID);
            script.Append("var value = ctrl.value;");
            script.Append("var oldVal = value;");
            script.Append("var splitedDate = value.split('/');");
            script.Append("if (splitedDate.length == 3) {");
            script.Append("splitedDate[2] = splitedDate[2].replace(/[^0-9]/g, '');");
            script.Append("if (splitedDate[2].length < 4)");
            script.Append("splitedDate[2] = (splitedDate[2].length == 3) ? '2' + splitedDate[2] : (splitedDate[2].length == 2) ? '20' + splitedDate[2] : '201' + splitedDate[2];");
            script.Append("else if (splitedDate[2].substring(0, 1) == '0')");
            script.Append("splitedDate[2] = '2' + splitedDate[2].substring(1);");
            script.Append("}");
            script.Append("value = splitedDate.join('/');");
            script.Append("if (oldVal != value)");
            script.Append("ctrl.value = value;");
            script.Append("});");

            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            if (null == scriptManager)
            {
                ClientScriptManager cs = this.Page.ClientScript;
                cs.RegisterStartupScript(this.Page.GetType(), string.Format("dPicker_{0}", this._textBox.ClientID), script.ToString(), true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), string.Format("dPicker_{0}", this._textBox.ClientID), script.ToString(), true);
            }

            if (!string.IsNullOrEmpty(_datePickerXml.Label.Text))
            {
                Label lbl = new Label();
                lbl.ID = string.Format("lbl{0}", _datePickerXml.Name);
                lbl.Text = _datePickerXml.Label.Text;

                if (!string.IsNullOrEmpty(_datePickerXml.Label.Css))
                    lbl.CssClass = _datePickerXml.Label.Css;

                if (!string.IsNullOrEmpty(_datePickerXml.Label.Style))
                {
                    string[] styles = _datePickerXml.Label.Style.Split(';');

                    foreach (string style in styles)
                    {
                        string[] attributes = style.Split(':');

                        if (attributes.Length == 2)
                            lbl.Style.Add(attributes[0], attributes[1]);
                    }
                }

                lbl.RenderControl(output);
            }

            if (!string.IsNullOrEmpty(_datePickerXml.Css))
            {
                this._textBox.CssClass = _datePickerXml.Css;
            }

            if (!string.IsNullOrEmpty(_datePickerXml.Style))
            {
                string[] styles = _datePickerXml.Style.Split(';');

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

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (ShowOnProp == null)
                ShowOnProp = "focus";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var script = new StringBuilder();
            script.Append(" $(document).ready(function () {");
            script.Append(" $('#" + _textBox.ClientID + "').datepicker({");
            script.Append("showOn: '" + ShowOnProp + "',");
            script.Append("buttonImage: 'Images/DatePicker/Calendar.gif',");
            script.Append("buttonImageOnly: true,");
            script.Append("dateFormat: '" + DateFormat + "'");
            script.Append("});");
            script.Append("});");

            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            if (null == scriptManager)
            {
                ClientScriptManager cs = this.Page.ClientScript;
                cs.RegisterStartupScript(GetType(), string.Format("dPicker_{0}", _textBox.ClientID), script.ToString(), true);
            }
            else
            {
                script.Append("var prm = Sys.WebForms.PageRequestManager.getInstance();\n");
                script.Append("\n");
                script.Append("    prm.add_endRequest(function () {\n");
                script.Append("     $('#" + _textBox.ClientID + "').datepicker({");
                script.Append("         showOn: '" + ShowOnProp + "',");
                script.Append("         buttonImage: 'Images/DatePicker/Calendar.gif',");
                script.Append("         buttonImageOnly: true,");
                script.Append("         dateFormat: '" + DateFormat + "'");
                script.Append("     });");
                script.Append("    })\n");
                ScriptManager.RegisterStartupScript(this, GetType(), string.Format("dPicker_{0}", _textBox.ClientID), script.ToString(), true);
            }
        }

        #endregion

        #region [ Methods ]

        public DynamicDatePickerXml CreateObjectToSerialize()
        {
            return new DynamicDatePickerXml(this.Value, this.ID, this.IsValidEmpty, this.EmptyValueMessage, this.InvalidValueMessage, this.DateTimeFormat, this.ShowOn, this.ValidationGroup, this.Visible);
        }

        public void addExtender(VisibleExtender extender)
        {
            this._textBox.TextChanged += new EventHandler(extender.DoStuff);
            this._textBox.AutoPostBack = true;
        }

        public void addExtender(AutoFillExtender extender)
        {
            this.Load += new EventHandler(extender.DoStuff);
        }

        public void addExtender(ValidationExtender extender)
        {
            this._textBox.TextChanged += new EventHandler(extender.DoStuff);
            this._textBox.AutoPostBack = true;
        }

        #endregion
    }
}
