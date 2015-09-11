using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Extender;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Xml;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.ServerControls
{
    public class DynamicCountableTextBox : WebControl
    {
        #region [ Attributes ]

        private CountableTextBox _textBox;
        public RequiredFieldValidator _required;
        private DynamicCountableTextBoxXml controlXML;

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
                _required.ID = _textBox.ID + "_rfv";
                _required.ControlToValidate = _textBox.ID;
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

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public int MaxChars
        {
            get
            {
                return _textBox.MaxChars;
            }

            set
            {
                _textBox.MaxChars = value;
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public int MaxCharsWarning
        {
            get
            {
                return _textBox.MaxCharsWarning;
            }

            set
            {
                _textBox.MaxCharsWarning = value;
            }
        }

        public bool IsRequired
        {
            get { return _required.Enabled; }
            set { _required.Enabled = value; }
        }

        public string ErrorMessage
        {
            get { return _required.ErrorMessage; }
            set { _required.ErrorMessage = value; }
        }

        public string ValidationGroup
        {
            get { return _required.ValidationGroup; }
            set
            {
                _required.ValidationGroup = value;
                _textBox.ValidationGroup = value;
            }
        }

        public TextBoxMode TextMode
        {
            get { return _textBox.TextMode; }
            set { _textBox.TextMode = value; }
        }

        public override Unit Width
        {
            get { return _textBox.Width; }
            set { _textBox.Width = value; }
        }

        public override Unit Height
        {
            get { return _textBox.Height; }
            set { _textBox.Height = value; }
        }

        public string InnerClientID
        {
            get { return _textBox.ClientID; }
        }

        #endregion

        #region [ Constructors ]

        public DynamicCountableTextBox()
        {
            _textBox = new CountableTextBox();
            _textBox.CausesValidation = false;

            _required = new RequiredFieldValidator();
            _required.Display = ValidatorDisplay.Dynamic;
            _required.EnableClientScript = true;
            _required.Text = "*";
            _required.Enabled = false;

            this.Controls.Add(_textBox);
            this.Controls.Add(_required);

            this.controlXML = new DynamicCountableTextBoxXml();
            this.Style[HtmlTextWriterStyle.Display] = "block";
        }

        public DynamicCountableTextBox(DynamicCountableTextBoxXml xml)
        {
            _textBox = new CountableTextBox();
            _textBox.CausesValidation = false;

            _required = new RequiredFieldValidator();
            _required.Display = ValidatorDisplay.Dynamic;
            _required.EnableClientScript = true;
            _required.Text = "*";
            _required.Enabled = false;
            _required.ClientIDMode = System.Web.UI.ClientIDMode.Static;

            this.Controls.Add(_textBox);
            this.Controls.Add(_required);

            this.ID = xml.Name;
            this.IsRequired = xml.IsRequired;
            this.ErrorMessage = xml.ErrorMessage;
            this.ValidationGroup = xml.ValidationGroup;

            this.MaxChars = xml.MaxChars;
            this.MaxCharsWarning = xml.MaxCharsWarning;
            this.TextMode = xml.TextMode;
            this.Width = new Unit(xml.Width);
            this.Height = new Unit(xml.Height);
            _textBox.Text = xml.Text;

            this.Visible = xml.Visible;

            this.controlXML = xml;

            this.Style[HtmlTextWriterStyle.Display] = "block";
        }

        #endregion

        #region [ Methods ]

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
            this._textBox.Load += new EventHandler(extender.RegisterScript);
            this._textBox.TextChanged += new EventHandler(extender.DoStuff);

            if (extender._targetControl is DynamicTextBox)
                this._textBox.Attributes.Add("onblur", "ValidationMethod('" + ((DynamicTextBox)extender._targetControl)._required.ClientID + "');");
            else if (extender._targetControl is DynamicCountableTextBox)
                this._textBox.Attributes.Add("onblur", "ValidationMethod('" + ((DynamicCountableTextBox)extender._targetControl)._required.ClientID + "');");
            else if (extender._targetControl is DynamicDropDownList)
                this._textBox.Attributes.Add("onblur", "ValidationMethod('" + ((DynamicDropDownList)extender._targetControl)._required.ClientID + "');");
        }

        public DynamicCountableTextBoxXml CreateObjectToSerialize()
        {
            return new DynamicCountableTextBoxXml(this.Text, this.ID, this.IsRequired, this.ErrorMessage, this.ValidationGroup, this.MaxChars, this.MaxCharsWarning,
                this.TextMode, this.Width.Value, this.Height.Value, this.controlXML.Label, this.controlXML.Css, this.controlXML.Style, this.Visible);
        }

        #endregion

        #region [ Overrrides ]

        protected override void RenderContents(HtmlTextWriter output)
        {
            if (!string.IsNullOrEmpty(controlXML.Label.Text))
            {
                Label lbl = new Label();
                lbl.ID = string.Format("lbl{0}", controlXML.Name);
                lbl.Text = controlXML.Label.Text;

                if (!string.IsNullOrEmpty(controlXML.Label.Css))
                    lbl.CssClass = controlXML.Label.Css;

                if (!string.IsNullOrEmpty(controlXML.Label.Style))
                {
                    string[] styles = controlXML.Label.Style.Split(';');

                    foreach (string style in styles)
                    {
                        string[] attributes = style.Split(':');

                        if (attributes.Length == 2)
                            lbl.Style.Add(attributes[0], attributes[1]);
                    }
                }

                lbl.RenderControl(output);
            }

            if (!string.IsNullOrEmpty(controlXML.Css))
            {
                this._textBox.CssClass = controlXML.Css;
            }

            if (!string.IsNullOrEmpty(controlXML.Style))
            {
                string[] styles = controlXML.Style.Split(';');

                foreach (string style in styles)
                {
                    string[] attributes = style.Split(':');

                    if (attributes.Length == 2)
                        this._textBox.Style.Add(attributes[0], attributes[1]);
                }
            }

            base.RenderContents(output);
            //output.WriteBreak();
        }
        #endregion
    }
}
