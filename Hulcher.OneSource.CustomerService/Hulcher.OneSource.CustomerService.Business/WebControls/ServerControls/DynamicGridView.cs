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
    public class DynamicGridView : WebControl
    {
        #region [ Attributes ]
        
        private GridView _gridView;
        private Button _addButton;
        private Panel _controls;
        private List<KeyValuePair<string, WebControl>> _webControlList = new List<KeyValuePair<string, WebControl>>();
        private DynamicGridViewXml _controlXML;

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
                _gridView.ID = value + "_grid";
            }
        }

        public List<string[]> FillList
        {
            get
            {
                if (null == ViewState["FillList"])
                    ViewState["FillList"] = new List<string[]>();
                return (List<string[]>)ViewState["FillList"];
            }
            set
            {
                ViewState["FillList"] = value;
            }
        }

        #endregion

        #region [ Constructors ]

        public DynamicGridView()
        {
            this._controlXML = new DynamicGridViewXml();
        }

        public DynamicGridView(DynamicGridViewXml xml)
        {
            _controls = new Panel();
            this.Controls.Add(_controls);

            _addButton = new Button();
            _addButton.CssClass = "btn";
            _addButton.Text = "Add";
            _addButton.Click += new EventHandler(_addButton_Click);
            _addButton.CausesValidation = false;
            _controls.Controls.Add(_addButton);

            Literal litBr = new Literal();
            litBr.Text = "<br /><br />";
            _controls.Controls.Add(litBr);

            _gridView = new GridView();
            _gridView.AutoGenerateColumns = false;
            _gridView.EnableViewState = true;
            _gridView.Width = Unit.Percentage(100);
            _gridView.SkinID = "DynamicGridView";

            TemplateField tField = new TemplateField();
            tField.HeaderTemplate = new DynamicGridViewButtonTemplate(DataControlRowType.Header);
            tField.ItemTemplate = new DynamicGridViewButtonTemplate(DataControlRowType.DataRow);
            tField.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            _gridView.Columns.Add(tField);

            _gridView.RowCommand += new GridViewCommandEventHandler(_gridView_RowCommand);
            _gridView.RowDataBound += new GridViewRowEventHandler(_gridView_RowDataBound);
            _gridView.Load += new EventHandler(_gridView_Load);

            _controls.Controls.Add(_gridView);

            this._controlXML = xml;
            this.ID = xml.Name;
            this.Visible = xml.Visible;
        }

        #endregion

        #region [ Methods ]

        private void AddControl(string label, WebControl control)
        {
            _webControlList.Add(new KeyValuePair<string, WebControl>(label, control));            
            BoundField bField = new BoundField();
            bField.HeaderText = label;
            _gridView.Columns.Insert(_gridView.Columns.Count - 1, bField);
        }

        public void AddExtender(GridViewExtender extender)
        {
            AddControl(extender._label, extender._targetControl);
        }

        public void BindGridView()
        {
            _gridView.DataSource = FillList;
            _gridView.DataBind();
        }

        public DynamicGridViewXml CreateObjectToSerialize()
        {
            return new DynamicGridViewXml(this.ID, _controlXML.Label, _controlXML.Css, _controlXML.Style, this.FillList, this.Visible);
        }

        #endregion

        #region [ Events ]

        protected void _gridView_Load(object sender, EventArgs e)
        {
            if (null != _controlXML.DataSourceList)
                FillList.AddRange(_controlXML.DataSourceList);
            BindGridView();
        }

        protected void _gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string[] item = e.Row.DataItem as string[];
                for (int i = 0; i < item.Length; i++)
                    e.Row.Cells[i].Text = item[i];
            }
        }

        protected void _addButton_Click(object sender, EventArgs e)
        {
            string[] values = new string[_webControlList.Count];
            int i = 0;
            foreach (KeyValuePair<string, WebControl> control in _webControlList)
            {
                if (control.Value is DynamicDropDownList)
                {
                    DynamicDropDownList dropDown = control.Value as DynamicDropDownList;
                    values[i] = dropDown.SelectedItem.Text;
                    dropDown.SelectedIndex = 0;
                }
                else if (control.Value is DynamicTextBox)
                {
                    DynamicTextBox textBox = control.Value as DynamicTextBox;
                    values[i] = textBox.Text;
                    textBox.Text = string.Empty;
                }
                else if (control.Value is DynamicFilteredTextBox)
                {
                    DynamicFilteredTextBox textBox = control.Value as DynamicFilteredTextBox;
                    values[i] = textBox.Text;
                    textBox.Text = string.Empty;
                }
                else if (control.Value is DynamicTime)
                {
                    DynamicTime textBox = control.Value as DynamicTime;
                    values[i] = textBox.Text;
                    textBox.Text = string.Empty;
                }
                i++;
            }
            FillList.Add(values);
            BindGridView();
        }

        protected void _gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Remove", StringComparison.OrdinalIgnoreCase))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                FillList.RemoveAt(index);
                BindGridView();
            }
        }

        #endregion

        #region [ Overrides ]

        protected override void RenderContents(HtmlTextWriter output)
        {
            if (!string.IsNullOrEmpty(_controlXML.Css))
            {
                this._controls.CssClass = _controlXML.Css;
            }

            if (!string.IsNullOrEmpty(_controlXML.Style))
            {
                string[] styles = _controlXML.Style.Split(';');

                foreach (string style in styles)
                {
                    string[] attributes = style.Split(':');

                    if (attributes.Length == 2)
                        this._gridView.Style.Add(attributes[0], attributes[1]);
                }
            }
            base.RenderContents(output);
            output.WriteBreak();
        }

        #endregion
    }

    public class DynamicGridViewButtonTemplate : ITemplate
    {
        DataControlRowType _rowType;

        public DynamicGridViewButtonTemplate(DataControlRowType rowType)
        {
            _rowType = rowType;
        }

        public void InstantiateIn(System.Web.UI.Control container)
        {
            switch (_rowType)
            {
                case DataControlRowType.Header:
                    break;

                case DataControlRowType.DataRow:

                    LinkButton lnk = new LinkButton();
                    lnk.DataBinding += new EventHandler(this.btnDataBind);
                    lnk.ID = "lnkRemove";
                    container.Controls.Add(lnk);

                    break;

                default:
                    break;
            }
        }


        private void btnDataBind(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            GridViewRow row = lnk.NamingContainer as GridViewRow;

            lnk.Text = "Remove";
            lnk.CommandName = "Remove";
            lnk.CommandArgument = row.DataItemIndex.ToString();
            lnk.OnClientClick = "return confirm('Are you sure you want to remove this item from the list?');";

            ScriptManager scm = ScriptManager.GetCurrent(lnk.Page);
            if (null != scm)
                scm.RegisterAsyncPostBackControl(lnk);
        }
    }
}

