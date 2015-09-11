using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Xml;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Extender;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.ServerControls
{
    public class DynamicDropDownList : WebControl
    {
        #region [ Attributes ]

        private DropDownList _dropdown;
        public RequiredFieldValidator _required;
        public bool _enter = true;

        private int _filterId;
        private ListExtender _listExtender;
        public CascadeListExtender _cascadeExtender;
        private AutoFillExtender _autoFillExtender;
        private DynamicDropDownListXml _controlXml;
        private string _selectedValueOnLoad;

        #endregion

        #region [ Properties ]

        public int FilterId
        {
            get { return _filterId; }
            set { _filterId = value; }
        }

        public ListExtender ListExtender
        {
            get { return _listExtender; }
            set { _listExtender = value; }
        }

        public AutoFillExtender AutoFillExtender
        {
            get { return _autoFillExtender; }
            set { _autoFillExtender = value; }
        }

        public DynamicDropDownListXml controlXML
        {
            get { return _controlXml; }
            set { _controlXml = value; }
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
                _dropdown.ValidationGroup = value;
            }
        }

        public string SelectedValue
        {
            get { return _dropdown.SelectedValue; }
            set { _dropdown.SelectedValue = value; }
        }

        public string SelectedValueOnLoad
        {
            get { return _selectedValueOnLoad; }
            set { _selectedValueOnLoad = value; }
        }

        public string DataTextField
        {
            get { return _dropdown.DataTextField; }
            set { _dropdown.DataTextField = value; }
        }

        public string DataValueField
        {
            get { return _dropdown.DataValueField; }
            set { _dropdown.DataValueField = value; }
        }

        public object DataSource
        {
            get { return _dropdown.DataSource; }
            set { _dropdown.DataSource = value; }
        }

        public ListItemCollection Items
        {
            get { return _dropdown.Items; }
        }

        public ListItem SelectedItem
        {
            get { return _dropdown.SelectedItem; }
        }

        public int SelectedIndex
        {
            get { return _dropdown.SelectedIndex; }
            set { _dropdown.SelectedIndex = value; }
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
                _dropdown.ID = value + "_drop";
                _required.ID = _dropdown.ID + "_rfv";
                _required.ControlToValidate = _dropdown.ID;
            }
        }

        public string InnerClientID
        {
            get { return _dropdown.ClientID; }
        }

        #endregion

        #region [ Constructors ]

        public DynamicDropDownList()
        {
            controlXML = new DynamicDropDownListXml();

            _dropdown = new DropDownList();
            _dropdown.CausesValidation = false;

            _required = new RequiredFieldValidator();
            _required.Display = ValidatorDisplay.Dynamic;
            _required.EnableClientScript = true;
            _required.Text = "*";
            _required.Enabled = false;

            this.Controls.Add(_dropdown);
            this.Controls.Add(_required);
        }


        public DynamicDropDownList(DynamicDropDownListXml xml)
        {
            _dropdown = new DropDownList();
            _dropdown.CausesValidation = false;

            _required = new RequiredFieldValidator();
            _required.Display = ValidatorDisplay.Dynamic;
            _required.EnableClientScript = true;
            _required.Text = "*";
            _required.Enabled = false;
            _required.ClientIDMode = System.Web.UI.ClientIDMode.Static;

            this.Controls.Add(_dropdown);
            this.Controls.Add(_required);

            this.ID = xml.Name;
            this.IsRequired = xml.IsRequired;
            this.ErrorMessage = xml.ErrorMessage;
            this.ValidationGroup = xml.ValidationGroup;
            this.FilterId = xml.FilterId;
            if (!string.IsNullOrEmpty(xml.SelectedValue))
            {
                _dropdown.SelectedValue = xml.SelectedValue;
                _selectedValueOnLoad = xml.SelectedValue;
            }

            this.controlXML = xml;

            this.Visible = xml.Visible;
        }

        #endregion

        #region [ Methods ]

        public void addExtender(ListExtender extender)
        {
            ListExtender = extender;
            if (extender._filteredLoadOnly)
            {
                this.Load += new EventHandler(extender.DoStuff);
            }
            else
            {
                extender.DoStuff(this, new EventArgs());
            }
        }

        public void addExtender(VisibleExtender extender)
        {
            _dropdown.Load += new EventHandler(extender.DoStuff);
            _dropdown.SelectedIndexChanged += new EventHandler(extender.DoStuff);
            _dropdown.AutoPostBack = true;
        }

        public void addExtender(CascadeListExtender extender)
        {
            _dropdown.SelectedIndexChanged += new EventHandler(extender.DoStuff);
            _dropdown.AutoPostBack = true;
            
            _cascadeExtender = extender;

            if (!string.IsNullOrEmpty(_selectedValueOnLoad))
            {
                extender.DoStuff(this, new System.EventArgs());
                ((DynamicDropDownList)extender._targetControl).SelectedValue = ((DynamicDropDownList)extender._targetControl).SelectedValueOnLoad;
            }
        }

        public void addExtender(AutoFillExtender extender)
        {
            _dropdown.Load += new EventHandler(extender.DoStuff);
            _autoFillExtender = extender;
        }

        public DynamicDropDownListXml CreateObjectToSerialize()
        {
            if(this.SelectedItem == null)
                return new DynamicDropDownListXml(this.FilterId, this.ID, this.SelectedValue, null, this.IsRequired, this.ErrorMessage, this.ValidationGroup, this.controlXML.Label, this.controlXML.Css, this.controlXML.Style, this.Visible);
            else
            {
                return new DynamicDropDownListXml(this.FilterId, this.ID, this.SelectedValue, this.SelectedItem.Text, this.IsRequired, this.ErrorMessage, this.ValidationGroup, this.controlXML.Label, this.controlXML.Css, this.controlXML.Style, this.Visible);
            }
        }

        public override void DataBind()
        {
            _dropdown.DataBind();
        }

        public void Clear()
        {
            _dropdown.Items.Clear();
        }

        #endregion

        #region [ Overrides ]

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
                this._dropdown.CssClass = controlXML.Css;
            }

            if (!string.IsNullOrEmpty(controlXML.Style))
            {
                string[] styles = controlXML.Style.Split(';');

                foreach (string style in styles)
                {
                    string[] attributes = style.Split(':');

                    if (attributes.Length == 2)
                        this._dropdown.Style.Add(attributes[0], attributes[1]);
                }
            }

            base.RenderContents(output);
            output.WriteBreak();
        }

        #endregion
    }
}

