using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Extender;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Xml;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.ServerControls
{
    public class DynamicCheckBox : WebControl
    {
        #region Attributes

        private CheckBox _checkBox;
        private DynamicCheckBoxXml _controlXml;

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
                _checkBox.ID = value + "_checkbBox";
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
                return _checkBox.Text;
            }

            set
            {
                _checkBox.Text = value;
            }
        }

        public bool Checked
        {
            get
            {
                return _checkBox.Checked;
            }
            set
            {
                _checkBox.Checked = value;
            }
        }

        #endregion

        #region [ Constructors ]

        public DynamicCheckBox()
        {
            _checkBox = new CheckBox();

            Controls.Add(_checkBox);

            _controlXml = new DynamicCheckBoxXml();
        }

        public DynamicCheckBox(DynamicCheckBoxXml xml)
        {
            _checkBox = new CheckBox();

            Controls.Add(_checkBox);

            Text = xml.Text;
            ID = xml.Name;
            _checkBox.Checked = xml.Check;

            _controlXml = xml;

            Visible = xml.Visible;
            
        }

        #endregion

        #region [ Methods ]

        public void addExtender(VisibleExtender extender)
        {
            _checkBox.CheckedChanged += new EventHandler(extender.DoStuff);
            _checkBox.AutoPostBack = true;
        }

        public void addExtender(ValidationExtender extender)
        {
            _checkBox.CheckedChanged += new EventHandler(extender.DoStuff);
            _checkBox.AutoPostBack = true;
        }

        public DynamicCheckBoxXml CreateObjectToSerialize()
        {
            return new DynamicCheckBoxXml(_controlXml.Name, Checked, Text, _controlXml.Css, _controlXml.Style, this._controlXml.Label, this.Visible);
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
                this._checkBox.CssClass = _controlXml.Css;
            }

            if (!string.IsNullOrEmpty(_controlXml.Style))
            {
                string[] styles = _controlXml.Style.Split(';');

                foreach (string style in styles)
                {
                    string[] attributes = style.Split(':');

                    if (attributes.Length == 2)
                        this._checkBox.Style.Add(attributes[0], attributes[1]);
                }
            }

            base.RenderContents(output);
            output.WriteBreak();
        }

        #endregion
    }
}
