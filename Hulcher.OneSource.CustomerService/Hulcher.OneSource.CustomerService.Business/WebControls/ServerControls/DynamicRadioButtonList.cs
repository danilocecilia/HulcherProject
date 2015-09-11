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
    public class DynamicRadioButtonList : WebControl
    {
        #region [ Attributes ]

        private RadioButtonList _radioButtonList;
        private RequiredFieldValidator _required;

        private ListExtender _listExtender;
        private DynamicRadioButtonListXml _controlXml;

        private Table _tbControls;
        private TableRow _trControls;
        private TableCell _tdLabel;
        private TableCell _tdControl;
        private TableCell _tdRequired;

        #endregion

        #region [ Properties ]

        public ListExtender ListExtender
        {
            get { return _listExtender; }
            set { _listExtender = value; }
        }

        public DynamicRadioButtonListXml controlXML
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
                _radioButtonList.ValidationGroup = value;
            }
        }

        public string SelectedValue
        {
            get { return _radioButtonList.SelectedValue; }
            set { _radioButtonList.SelectedValue = value; }
        }

        public string DataTextField
        {
            get { return _radioButtonList.DataTextField; }
            set { _radioButtonList.DataTextField = value; }
        }

        public string DataValueField
        {
            get { return _radioButtonList.DataValueField; }
            set { _radioButtonList.DataValueField = value; }
        }

        public object DataSource
        {
            get { return _radioButtonList.DataSource; }
            set { _radioButtonList.DataSource = value; }
        }

        public ListItemCollection Items
        {
            get { return _radioButtonList.Items; }
        }

        public ListItem SelectedItem
        {
            get { return _radioButtonList.SelectedItem; }
        }

        public int SelectedIndex
        {
            get { return _radioButtonList.SelectedIndex; }
            set { _radioButtonList.SelectedIndex = value; }
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
                _radioButtonList.ID = value + "_drop";
                _required.ID = _radioButtonList.ID + "_rfv";
                _required.ControlToValidate = _radioButtonList.ID;
            }
        }

        #endregion

        #region [ Constructors ]

        public DynamicRadioButtonList()
        {
            controlXML = new DynamicRadioButtonListXml();

            _tbControls = new Table();
            _trControls = new TableRow();
            _tdLabel = new TableCell();
            _tdControl = new TableCell();
            _tdRequired = new TableCell();
            _trControls.Cells.Add(_tdLabel);
            _trControls.Cells.Add(_tdControl);
            _trControls.Cells.Add(_tdRequired);
            _tbControls.Rows.Add(_trControls);

            _radioButtonList = new RadioButtonList();
            _radioButtonList.CausesValidation = false;
            _radioButtonList.RepeatDirection = RepeatDirection.Horizontal;

            _required = new RequiredFieldValidator();
            _required.Display = ValidatorDisplay.Dynamic;
            _required.EnableClientScript = true;
            _required.Text = "*";
            _required.Enabled = false;

            _tdControl.Controls.Add(_radioButtonList);
            _tdRequired.Controls.Add(_required);
            this.Controls.Add(_tbControls);
        }


        public DynamicRadioButtonList(DynamicRadioButtonListXml xml)
        {
            _tbControls = new Table();
            _trControls = new TableRow();
            _tdLabel = new TableCell();
            _tdControl = new TableCell();
            _tdRequired = new TableCell();
            _trControls.Cells.Add(_tdLabel);
            _trControls.Cells.Add(_tdControl);
            _trControls.Cells.Add(_tdRequired);
            _tbControls.Rows.Add(_trControls);

            _radioButtonList = new RadioButtonList();
            _radioButtonList.CausesValidation = false;
            _radioButtonList.RepeatDirection = RepeatDirection.Horizontal;

            _required = new RequiredFieldValidator();
            _required.Display = ValidatorDisplay.Dynamic;
            _required.EnableClientScript = true;
            _required.Text = "*";
            _required.Enabled = false;

            _tdControl.Controls.Add(_radioButtonList);
            _tdRequired.Controls.Add(_required);
            this.Controls.Add(_tbControls);

            this.ID = xml.Name;
            this.IsRequired = xml.IsRequired;
            this.ErrorMessage = xml.ErrorMessage;
            this.ValidationGroup = xml.ValidationGroup;
            this.ErrorMessage = xml.ErrorMessage;
            this.ValidationGroup = xml.ValidationGroup;

            this.controlXML = xml;

            this.Visible = xml.Visible;
        }

        #endregion

        #region [ Methods ]

        public void addExtender(ListExtender extender)
        {
            ListExtender = extender;
            //this.Load += new EventHandler(extender.DoStuff);
            extender.DoStuff(this, new EventArgs());
        }

        public void addExtender(VisibleExtender extender)
        {
            _radioButtonList.SelectedIndexChanged += new EventHandler(extender.DoStuff);
            _radioButtonList.AutoPostBack = true;
        }

        public DynamicRadioButtonListXml CreateObjectToSerialize()
        {
            return new DynamicRadioButtonListXml(this.ID, this.SelectedValue,this.SelectedItem.Text, this.IsRequired, this.ErrorMessage, this.ValidationGroup, this.controlXML.Label, this.controlXML.Css, this.controlXML.Style, this.Visible);
        }

        public override void DataBind()
        {
            _radioButtonList.DataBind();
        }

        #endregion

        #region [ Overrides ]

        protected override void RenderContents(HtmlTextWriter output)
        {
            //output.WriteLine("<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\" ><tr><td>");
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

                //lbl.RenderControl(output);
                _tdLabel.Controls.Add(lbl);
            }

            //output.WriteLine("</td><td>");

            if (!string.IsNullOrEmpty(controlXML.Css))
            {
                this._radioButtonList.CssClass = controlXML.Css;
            }

            if (!string.IsNullOrEmpty(controlXML.Style))
            {
                string[] styles = controlXML.Style.Split(';');

                foreach (string style in styles)
                {
                    string[] attributes = style.Split(':');

                    if (attributes.Length == 2)
                        this._radioButtonList.Style.Add(attributes[0], attributes[1]);
                }
            }

            base.RenderContents(output);

            //output.WriteLine("</td></tr></table>");
            //output.WriteBreak();
        }

        #endregion
    }
}