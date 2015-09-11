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

using AjaxControlToolkit;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.ServerControls
{
    public class DynamicFilteredTextBox : WebControl
    {
        #region [ Attributes ]

        private TextBox _textBox;
        private RequiredFieldValidator _required;
        private FilteredTextBoxExtender _filteredExtender;
        private DynamicFilteredTextBoxXml _controlXml;

        #endregion

        #region [ Properties ]

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

        public int MaxLength
        {
            get { return _textBox.MaxLength; }
            set { _textBox.MaxLength = value; }
        }

        public FilterTypes FilterType
        {
            get { return _filteredExtender.FilterType; }
            set { _filteredExtender.FilterType = value; }
        }

        public FilterModes FilterMode
        {
            get { return _filteredExtender.FilterMode; }
            set { _filteredExtender.FilterMode = value; }
        }

        public int FilterInterval
        {
            get { return _filteredExtender.FilterInterval; }
            set { _filteredExtender.FilterInterval = value; }
        }

        public string InvalidChars
        {
            get { return _filteredExtender.InvalidChars; }
            set { _filteredExtender.InvalidChars = value; }
        }

        public string ValidChars
        {
            get { return _filteredExtender.ValidChars; }
            set { _filteredExtender.ValidChars = value; }
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
                _required.ID = _textBox.ID + "_rfv";
                _required.ControlToValidate = _textBox.ID;
                _filteredExtender.ID = _textBox.ID + "_fte";
                _filteredExtender.TargetControlID = _textBox.ID;
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

        #endregion

        #region [ Constructors ]

        public DynamicFilteredTextBox()
        {
            _textBox = new TextBox();
            _textBox.CausesValidation = false;

            _required = new RequiredFieldValidator();
            _required.Display = ValidatorDisplay.Dynamic;
            _required.ErrorMessage = "erro, idiota";
            _required.ValidationGroup = "teste";
            _required.EnableClientScript = true;
            _required.Text = "*";
            _required.Enabled = false;

            _filteredExtender = new FilteredTextBoxExtender();

            this.Controls.Add(_textBox);
            this.Controls.Add(_required);
            this.Controls.Add(_filteredExtender);

            this._controlXml = new DynamicFilteredTextBoxXml();
        }

        public DynamicFilteredTextBox(DynamicFilteredTextBoxXml xml)
        {
            _textBox = new TextBox();
            _textBox.CausesValidation = false;

            _required = new RequiredFieldValidator();
            _required.Display = ValidatorDisplay.Dynamic;
            _required.EnableClientScript = true;
            _required.Text = "*";
            _required.Enabled = false;

            _filteredExtender = new FilteredTextBoxExtender();

            this.Controls.Add(_textBox);
            this.Controls.Add(_required);
            this.Controls.Add(_filteredExtender);

            this.ID = xml.Name;
            this.IsRequired = xml.IsRequired;
            this.ErrorMessage = xml.ErrorMessage;
            this.ValidationGroup = xml.ValidationGroup;
            if (xml.Width > 0)
                this.Width = new Unit(xml.Width);
            if (xml.Height > 0)
                this.Height = new Unit(xml.Height);
            if (xml.MaxLength > 0)
                this.MaxLength = xml.MaxLength;
            this.FilterMode = xml.FilterMode;
            this.FilterType = xml.FilterType;
            this.FilterInterval = xml.FilterInterval;
            this.ValidChars = xml.ValidChars;
            this.InvalidChars = xml.InvalidChars;
            _textBox.Text = xml.Text;

            this.Visible = xml.Visible;

            this._controlXml = xml;
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
            this._textBox.TextChanged += new EventHandler(extender.DoStuff);
            this._textBox.AutoPostBack = true;
        }

        public DynamicFilteredTextBoxXml CreateObjectToSerialize()
        {
            return new DynamicFilteredTextBoxXml(this.Text, this.ID, this.IsRequired, this.ErrorMessage, this.ValidationGroup,
                this.Width.Value, this.Height.Value, this.MaxLength, this.FilterType, this.FilterMode, this.FilterInterval, this.ValidChars, this.InvalidChars,
                this._controlXml.Label, this._controlXml.Css, this._controlXml.Style, this.Visible);
        }

        #endregion

        #region [ Overrides ]

        protected override void RenderContents(HtmlTextWriter output)
        {
            if (!string.IsNullOrEmpty(_controlXml.Label.Text))
            {
                Label lbl = new Label();
                lbl.ID = string.Format("lbl{0}", _controlXml.Name);
                lbl.Text = _controlXml.Label.Text;

                if (!string.IsNullOrEmpty(_controlXml.Label.Css))
                    lbl.CssClass = _controlXml.Label.Css;

                if (!string.IsNullOrEmpty(_controlXml.Label.Style))
                {
                    string[] styles = _controlXml.Label.Style.Split(';');

                    foreach (string style in styles)
                    {
                        string[] attributes = style.Split(':');

                        if (attributes.Length == 2)
                            lbl.Style.Add(attributes[0], attributes[1]);
                    }
                }

                lbl.RenderControl(output);
            }

            if (!string.IsNullOrEmpty(_controlXml.Css))
            {
                this._textBox.CssClass = _controlXml.Css;
            }

            if (!string.IsNullOrEmpty(_controlXml.Style))
            {
                string[] styles = _controlXml.Style.Split(';');

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
