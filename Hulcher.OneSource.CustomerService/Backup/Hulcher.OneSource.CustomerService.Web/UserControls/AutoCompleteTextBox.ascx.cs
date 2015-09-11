using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.DataContext;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Hulcher.OneSource.CustomerService.Web.UserControls
{
    public partial class AutoCompleteTextBox : System.Web.UI.UserControl
    {
        string _ColumnHeaderList;
        string _ColumnValueList;
        string _GridViewButtonImageUrl;
        string _GridViewIdName;
        string _DisplayField;
        string _windowTitle;
        string _ControlsToUpdate;
        bool _AutoPostBack;
        string _scriptToExecute;

        Globals.AutoComplete.AutoCompleteSource _AutoCompleteSource;

        #region [ Events ]

        protected void Page_Load(object sender, EventArgs e)
        {
            loadFunctions();
            loadControlsId();
            addOnClickEvent();

            if (string.IsNullOrEmpty(aceCustomer.ContextKey))
                aceCustomer.ContextKey = "0";

            //textControl.ToolTip = hfTitle.Value;
            textControl.Attributes.Add("class", "masterTooltip");
           

            if (!string.IsNullOrEmpty(TextControlOnFocusScript))
                textControl.Attributes["onfocus"] = string.Format("setFocusTimeout('{0}'); document.getElementById('{1}').value = document.getElementById('{2}').value; {3}", textControl.ClientID, hfLastText.ClientID, textControl.ClientID, TextControlOnFocusScript);
            else
                textControl.Attributes["onfocus"] = string.Format("setFocusTimeout('{0}'); document.getElementById('{1}').value = document.getElementById('{2}').value;", textControl.ClientID, hfLastText.ClientID, textControl.ClientID);

            if (!string.IsNullOrEmpty(TextControlOnClientClickScript))
                textControl.Attributes["onclick"] = string.Format("{0}", TextControlOnClientClickScript);

            hfBlur.Value = "";
        }

        protected void imgButton_Click(object sender, EventArgs e)
        {
            //StringBuilder script = new StringBuilder();
            //string parentField = null;
            //if (_AutoPostBack)
            //    parentField = textControl.ClientID;
            //script.AppendFormat("var myWindow = window.open('AutoCompletePopUp.aspx?HeaderList={0}&ValueList={1}&ReturnValueId={2}&ReturnTextId={3}&ReturnHfId={4}&AutoCompleteSource={5}&GridViewIdName={6}&DisplayField={7}&ImageUrl={8}&FilterId={9}&ParentField={10}&WindowTitle={11}','', 'width=800, height=600, scrollbars=1, resizable=yes');return false;",
            //    _ColumnHeaderList, _ColumnValueList, hfValue.ClientID, BehaviorId, hfTitle.ClientID, _AutoCompleteSource.ToString(), _GridViewIdName, _DisplayField, _GridViewButtonImageUrl, "' + $find('" + aceCustomer.BehaviorID + "').get_contextKey() + '", parentField, _windowTitle);
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AutoCompletePopUp", script.ToString(), true);
            //DataBindGridView();
        }

        //protected override void OnPreRender(EventArgs e)
        //{
        //    StringBuilder sbToolTip = new StringBuilder();

        //    sbToolTip.Append("$(document).ready(function() {debugger;");
        //    sbToolTip.Append("      $(' #" + textControl.ClientID + "').hover(function(){");
        //    sbToolTip.Append("          var title = $(this).attr('title');");
        //    sbToolTip.Append("          $(this).data('tipText', title).removeAttr('title');");
        //    sbToolTip.Append("          $('<p class='tooltip'></p>')");
        //    sbToolTip.Append("          .text(title)");
        //    sbToolTip.Append("          .appendTo('body')");
        //    sbToolTip.Append("          .fadeIn('slow');");
        //    sbToolTip.Append(" }, function() {");
        //    sbToolTip.Append("      $(this).attr('title', $(this).data('tipText'));");
        //    sbToolTip.Append("      $('.tooltip').remove();");
        //    sbToolTip.Append("}).mousemove(function(e) {");
        //    sbToolTip.Append("      var mousex = e.pageX + 20;");
        //    sbToolTip.Append("      var mousey = e.pageY + 10;");
        //    sbToolTip.Append("      $('.tooltip')");
        //    sbToolTip.Append("      .css({ top: mousey, left: mousex })");
        //    sbToolTip.Append(" });");
        //    sbToolTip.Append(" });");

        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ToolTip" + this.ID, sbToolTip.ToString(), true);

        //    base.OnPreRender(e);
        //}

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Add all Controls Id
        /// </summary>
        private void loadControlsId()
        {
            //Modal Popup
            //mpePopUp.PopupControlID = pnlPopUp.ID;
            //mpePopUp.TargetControlID = btnFake.ID;
            //mpePopUp.OkControlID = btnClose.ID;

            //Requied Field Validator
            rfvSelectedValue.ControlToValidate = hfValue.ID;

            ///Auto Complete Extender
            aceCustomer.TargetControlID = textControl.ID;
        }

        /// <summary>
        /// Add all js function
        /// </summary>
        private void loadFunctions()
        {
            aceCustomer.OnClientPopulating = "function(){ document.getElementById('" + textControl.ClientID + "').style.backgroundImage = 'url(images/uploading.gif)'; document.getElementById('" + textControl.ClientID + "').style.backgroundRepeat = 'no-repeat'; document.getElementById('" + textControl.ClientID + "').style.backgroundPosition = 'right';}";
            aceCustomer.OnClientPopulated = "function(){ document.getElementById('" + textControl.ClientID + "').style.backgroundImage = 'none';}";
            //textControl.Attributes.Add("onblur", "function () { var textbox = document.getElementByID('" + textControl.ClientID + "'); var hiddenField = document.getElementById('" + hfValue.ClientID + "'); if (textbox.value == '') { hiddenField.value = '0'; } __DoPostBack('" + textControl.ClientID + "',''); Alert('test' }");

            textControl.Attributes.Add("onkeypress", "document.getElementById('" + hfItemSelected.ClientID + "').value = '0';");

            #region Item Selected Script

            StringBuilder itemSelectedScript = new StringBuilder();
            itemSelectedScript.Append("function(source, eventArgs){ document.getElementById('" + hfItemSelected.ClientID + "').value = '1';var textControl = document.getElementById('" + textControl.ClientID + "'); document.getElementById('" + hfValue.ClientID + "').value = eventArgs.get_value(); ");
            itemSelectedScript.Append(" document.getElementById('" + hfTitle.ClientID + "').value = textControl.value;");
            itemSelectedScript.Append(" textControl.value = eventArgs.get_text();");
            itemSelectedScript.Append(" textControl.title = textControl.value;");

            itemSelectedScript.Append("     var item = eventArgs.get_text();");
            itemSelectedScript.Append("     if(item.toLowerCase() == 'not found'){");
            itemSelectedScript.Append("         textControl.value = '';");
            itemSelectedScript.Append("         textControl.title ='';}");

            if (null != _ControlsToUpdate)
            {
                string[] controlsToUpdate = _ControlsToUpdate.Split(',');

                for (int i = 0; i < controlsToUpdate.Length; i++)
                {
                    itemSelectedScript.Append(" if ($find('" + controlsToUpdate[i] + "').get_contextKey() != '0' && $find('" + controlsToUpdate[i] + "').get_contextKey() != eventArgs.get_value())");
                    itemSelectedScript.Append(" $find('" + controlsToUpdate[i] + "').raiseItemSelected(new Sys.Extended.UI.AutoCompleteItemEventArgs(null, '', '0'));");

                    itemSelectedScript.Append(" $find('" + controlsToUpdate[i] + "').set_contextKey(eventArgs.get_value());");
                }
            }

            if (!string.IsNullOrEmpty(ScriptToExecute))
            {
                itemSelectedScript.AppendFormat(ScriptToExecute + "(eventArgs.get_value());");
            }

            if (AutoPostBack)
                itemSelectedScript.Append(string.Format(" __doPostBack('{0}','');", textControl.UniqueID));

            itemSelectedScript.Append("document.getElementById('" + textControl.ClientID + "').style.backgroundImage = 'none';");

            itemSelectedScript.Append("}");

            aceCustomer.OnClientItemSelected = itemSelectedScript.ToString();

            #endregion

            StringBuilder blurScript = new StringBuilder();

            if (!string.IsNullOrEmpty(TextControlOnBlurScript))
                blurScript.AppendFormat("    {0}", TextControlOnBlurScript);

            blurScript.AppendFormat("    var textControl = document.getElementById('{0}');", textControl.ClientID);
            blurScript.AppendFormat("    var valueControl = document.getElementById('{0}');", hfValue.ClientID);
            blurScript.AppendFormat("    var acExtender = $find('{0}');", aceCustomer.BehaviorID);
            blurScript.AppendFormat("    var hfBlur = document.getElementById('{0}');", hfBlur.ClientID);
            blurScript.Append("          if (hfBlur.value != '1') {");
            blurScript.Append("             if (textControl.value == '' && valueControl.value != '0') { ");
            blurScript.Append("                 acExtender.raiseItemSelected(new Sys.Extended.UI.AutoCompleteItemEventArgs(null, '', '0'));");
            blurScript.Append("                 document.getElementById('" + hfItemSelected.ClientID + "').value = '0';");
            blurScript.Append("             }");
            blurScript.Append("             else if (textControl.value != '') {");
            blurScript.AppendFormat("           setSelectedValue{0}(valueControl.value);", this.ID);
            blurScript.Append("             }");
            blurScript.Append("          }");

            textControl.Attributes.Add("onblur", blurScript.ToString());

            aceCustomer.OnClientShown = "function(handler) { document.getElementById('" + hfBlur.ClientID + "').value = '1'; }";
            aceCustomer.OnClientHidden = "function(handler) { document.getElementById('" + hfBlur.ClientID + "').value = ''; }";

            StringBuilder webserviceScript = new StringBuilder();

            webserviceScript.Append("function setSelectedValue" + this.ID + "(lastSelectedValue) { ");
            webserviceScript.AppendFormat("var hfValue = document.getElementById('{0}');", hfValue.ClientID);
            webserviceScript.Append("      if (document.getElementById('" + hfItemSelected.ClientID + "').value != '1' && document.getElementById('" + hfLastText.ClientID + "').value != document.getElementById('" + textControl.ClientID + "').value) {");
            webserviceScript.Append("         document.getElementById('" + hfLastValue.ClientID + "').value = '0'; ");
            webserviceScript.Append("         hfValue.value = '0';");
            webserviceScript.AppendFormat("   var acExtender = $find('{0}');", aceCustomer.BehaviorID);
            webserviceScript.AppendFormat("   var textControl = document.getElementById('{0}');", textControl.ClientID);
            webserviceScript.AppendFormat("   tempuri.org.IJSONService." + aceCustomer.ServiceMethod + "JSON(textControl.value, acExtender.get_contextKey(), onSetSelectedValueCompleted{0});", this.ID);
            webserviceScript.Append("      }");
            webserviceScript.Append("      else if (document.getElementById('" + hfItemSelected.ClientID + "').value == '1') {");
            webserviceScript.Append("         document.getElementById('" + hfItemSelected.ClientID + "').value = '0';");
            webserviceScript.Append("      }");
            webserviceScript.Append(" }");

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "setSelectedValue" + this.ID, webserviceScript.ToString(), true);

            StringBuilder resultWebServiceScript = new StringBuilder();

            resultWebServiceScript.Append("function onSetSelectedValueCompleted" + this.ID + "(serviceResult) { ");
            resultWebServiceScript.AppendFormat("var acExtender = $find('{0}'); ", aceCustomer.BehaviorID);
            resultWebServiceScript.Append("      if (document.getElementById('" + hfLastValue.ClientID + "').value != serviceResult.Id) { ");
            resultWebServiceScript.Append("         acExtender.raiseItemSelected(new Sys.Extended.UI.AutoCompleteItemEventArgs(null, serviceResult.Name, serviceResult.Id));");
            resultWebServiceScript.Append("      }");
            resultWebServiceScript.Append("      else { ");
            resultWebServiceScript.Append("         document.getElementById('" + hfValue.ClientID + "').value = serviceResult.Id;");
            resultWebServiceScript.Append("      }");
            resultWebServiceScript.Append("      document.getElementById('" + hfItemSelected.ClientID + "').value = '0';");
            resultWebServiceScript.Append(" }");

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "onSetSelectedValueCompleted" + this.ID, resultWebServiceScript.ToString(), true);

            
        }

        /// <summary>
        /// clear item selection
        /// </summary>
        public void ClearSelection()
        {
            textControl.Text = string.Empty;
            hfValue.Text = rfvSelectedValue.InitialValue;
            //textControl.ToolTip = "-";
            textControl.Attributes.Add("class", "masterTooltip");
            
        }

        /// <summary>
        /// Add the js function to open the AutoCompletePopUp w/o postback
        /// </summary>
        private void addOnClickEvent()
        {
            StringBuilder script = new StringBuilder();
            string parentField = null;
            if (_AutoPostBack)
                parentField = textControl.ClientID;
            script.AppendFormat("var myWindow = window.open('AutoCompletePopUp.aspx?HeaderList={0}&ValueList={1}&ReturnValueId={2}&ReturnTextId={3}&ReturnHfId={4}&AutoCompleteSource={5}&GridViewIdName={6}&DisplayField={7}&ImageUrl={8}&FilterId={9}&ParentField={10}&WindowTitle={11}','', 'width=800, height=600, scrollbars=1, resizable=yes');return false;",
                _ColumnHeaderList, _ColumnValueList, hfValue.ClientID, BehaviorId, hfTitle.ClientID, _AutoCompleteSource.ToString(), _GridViewIdName, _DisplayField, _GridViewButtonImageUrl, "' + $find('" + aceCustomer.BehaviorID + "').get_contextKey() + '", parentField, _windowTitle);

            imgButton.Attributes.Add("onclick", script.ToString());
        }

        #endregion

        #region [ Proprieties ]

        #region [ Override ]

        public override string ID
        {
            set
            {
                base.ID = value;
                hfValue.ID = value + "_value";
                imgButton.ID = value + "_button";
                textControl.ID = value + "_text";
                aceCustomer.ID = value + "_autoComplete";
                aceCustomer.BehaviorID = value;
                rfvSelectedValue.ID = value + "_required";
                btnFake.ID = value + "_Fake";
            }
        }

        #endregion

        #region [ Window Title ]

        public string WindowTitle
        {
            get { return _windowTitle; }
            set { _windowTitle = value; }
        }

        #endregion

        #region [ Textbox Control ]

        public bool ReadOnly
        {
            get
            {
                return textControl.ReadOnly;
            }
            set
            {
                textControl.ReadOnly = value;
                imgButton.Visible = !value;
            }
        }

        /// <summary>
        /// CssClass to be used on the TextBox Control
        /// </summary>
        public string TextBoxCssClass
        {
            get
            {
                return textControl.CssClass;
            }
            set
            {
                textControl.CssClass = value;
            }
        }

        /// <summary>
        /// Get the text inside the Text Control
        /// </summary>
        public string SelectedText
        {
            get
            {
                return textControl.Text;
            }
            set
            {
                textControl.Text = value;
                //textControl.ToolTip = value;
                textControl.Attributes.Add("class", "masterTooltip");
             
            }

        }

        /// <summary>
        /// Sets the AutoPostBack propriety of the TextControl
        /// </summary>
        public bool AutoPostBack
        {
            get
            {
                return _AutoPostBack;
            }
            set
            {
                _AutoPostBack = value;
            }
        }

        public event EventHandler TextChanged
        {
            add
            {
                textControl.TextChanged += value;
            }
            remove
            {
                textControl.TextChanged -= value;
            }
        }

        public string TextControlClientID
        {
            get { return textControl.ClientID; }
        }

        public Unit TextBoxWidth
        {
            get { return textControl.Width; }
            set { textControl.Width = value; }
        }

        public string TextControlOnBlurScript
        {
            get;
            set;
        }

        public string TextControlOnFocusScript
        {
            get;
            set;
        }

        public string TextControlOnClientClickScript
        {
            get;
            set;
        }

        #endregion

        #region [ ImageButton Control ]

        /// <summary>
        /// Get and Set the Css Class to be used on the ImageButton control
        /// </summary>
        public string ImageButtonCssClass
        {
            get
            {
                return imgButton.CssClass;
            }
            set
            {
                imgButton.CssClass = value;
            }
        }

        /// <summary>
        /// Show or hide the Image Button
        /// </summary>
        public bool HasSearchButton
        {
            get
            {
                return imgButton.Visible;
            }
            set
            {
                imgButton.Visible = value;
            }
        }

        public string ImageButtonClientID
        {
            get { return imgButton.ClientID; }
        }

        /// <summary>
        /// Set the ImageUrl on the ImageButton control
        /// </summary>
        //public string ButtonImageUrl
        //{
        //    set
        //    {
        //        imgButton.ImageUrl = value;
        //    }
        //}

        #endregion

        #region [ Auto Complete Extender ]

        public string ServiceMethod
        {
            get
            {
                return aceCustomer.ServiceMethod;
            }
            set
            {
                aceCustomer.ServiceMethod = value;
            }
        }

        [DefaultValue("0")]
        public string ContextKey
        {
            get
            {
                if (string.IsNullOrEmpty(aceCustomer.ContextKey))
                    aceCustomer.ContextKey = "0";

                return aceCustomer.ContextKey;
            }
            set
            {
                aceCustomer.ContextKey = value;
                aceCustomer.UseContextKey = true;
                hfFilter.Value = value;
            }
        }

        public string ControlsToUpdate
        {
            get
            {
                return _ControlsToUpdate;
            }
            set
            {
                _ControlsToUpdate = value;
            }
        }

        public string BehaviorId
        {
            get
            {
                return aceCustomer.BehaviorID;
            }
            set
            {
                aceCustomer.BehaviorID = value;
            }
        }

        public string BodyID
        {
            get 
            {
                return dvBody.ClientID;
            }
        }

        public string ScriptToExecute
        {
            get
            {
                return _scriptToExecute;
            }
            set
            {
                _scriptToExecute = value;
            }
        }

        public int MinimumPrefixLength
        {
            get { return aceCustomer.MinimumPrefixLength; }
            set { aceCustomer.MinimumPrefixLength = value; }
        }

        #endregion

        #region [ Hidden Field ]

        public string SetValue
        {
            set
            {           
                hfValue.Text = value;
            }

        }

        /// <summary>
        /// Gets the ID of the Selected Item on the TextBox
        /// </summary>
        public string SelectedValue
        {
            set
            {
                if (value.Equals("0"))
                {
                    hfValue.Text = value;
                }
                else
                {
                    CustomerModel customerModel = new CustomerModel();
                    EmployeeModel employeeModel = new EmployeeModel();
                    JobModel jobModel = new JobModel();
                    DivisionModel divisionModel = new DivisionModel();
                    EquipmentModel equipmentModel = new EquipmentModel();

                    switch (_AutoCompleteSource)
                    {
                        case Globals.AutoComplete.AutoCompleteSource.Customer:
                            if (!value.Equals("0") && !string.IsNullOrEmpty(value))
                            {
                                CS_Customer customer = customerModel.GetCustomerById(int.Parse(value));
                                if (customer != null)
                                {
                                    hfValue.Text = customer.ID.ToString();
                                    textControl.Text = customer.FullCustomerInformation;
                                }
                            }
                            break;
                        case Globals.AutoComplete.AutoCompleteSource.DynamicsContact:
                            if (!value.Equals("0") && !string.IsNullOrEmpty(value))
                            {
                                CS_Contact dynamicsContact = customerModel.GetContactById(int.Parse(value));
                                hfValue.Text = dynamicsContact.ID.ToString();
                                textControl.Text = dynamicsContact.FullContactInformation;
                            }

                            break;
                        case Globals.AutoComplete.AutoCompleteSource.CustomerServiceContact:
                            if (!value.Equals("0") && !string.IsNullOrEmpty(value))
                            {
                                CS_Contact csContact = customerModel.GetContactById(int.Parse(value));
                                hfValue.Text = csContact.ID.ToString();
                                textControl.Text = csContact.FullContactInformation;
                            }

                            break;
                        case Globals.AutoComplete.AutoCompleteSource.Employee:
                            if (!value.Equals("0") && !string.IsNullOrEmpty(value))
                            {
                                CS_Employee employee = employeeModel.GetEmployee(int.Parse(value));
                                hfValue.Text = employee.ID.ToString();
                                textControl.Text = employee.DivisionAndFullName;
                            }
                            break;
                        case Globals.AutoComplete.AutoCompleteSource.EmployeeWithDivision:
                            if (!value.Equals("0") && !string.IsNullOrEmpty(value))
                            {
                                CS_Employee employee = employeeModel.GetEmployee(int.Parse(value));
                                if (null == employee.CS_Division)
                                {
                                    hfValue.Text = employee.ID.ToString() + " | 0";
                                    textControl.Text = employee.FullName;
                                }
                                else
                                {
                                    hfValue.Text = employee.ID.ToString() + " | " + employee.CS_Division.Name;
                                    textControl.Text = employee.DivisionAndFullName;
                                }
                            }
                            break;
                        case Globals.AutoComplete.AutoCompleteSource.JobAction:
                            if (!value.Equals("0") && !string.IsNullOrEmpty(value))
                            {
                                CS_JobAction jobAction = jobModel.GetJobAction(int.Parse(value));
                                hfValue.Text = jobAction.ID.ToString();
                                textControl.Text = jobAction.Description;
                            }
                            break;
                        case Globals.AutoComplete.AutoCompleteSource.JobStatus:
                            if (!value.Equals("0") && !string.IsNullOrEmpty(value))
                            {
                                CS_JobStatus jobAction = jobModel.GetJobStatus(int.Parse(value));
                                hfValue.Text = jobAction.ID.ToString();
                                textControl.Text = jobAction.Description;
                            }
                            break;
                        case Globals.AutoComplete.AutoCompleteSource.JobNumber:
                        case Globals.AutoComplete.AutoCompleteSource.JobNumberByStatus:
                        case Globals.AutoComplete.AutoCompleteSource.BillableJobNumber:
                            if (!value.Equals("0") && !string.IsNullOrEmpty(value))
                            {
                                CS_Job job = jobModel.GetJobById(int.Parse(value));
                                hfValue.Text = job.ID.ToString();
                                textControl.Text = job.PrefixedNumber;
                            }
                            break;
                        case Globals.AutoComplete.AutoCompleteSource.Division:
                            if (!value.Equals("0") && !string.IsNullOrEmpty(value))
                            {
                                CS_Division division = divisionModel.GetDivision(int.Parse(value));
                                hfValue.Text = division.ID.ToString();
                                textControl.Text = division.ExtendedDivisionName;
                            }
                            break;
                        case Globals.AutoComplete.AutoCompleteSource.EquipmentType:
                            if (!value.Equals("0") && !string.IsNullOrEmpty(value))
                            {
                                CS_EquipmentType equipmentType = equipmentModel.GetEquipmentType(int.Parse(value));
                                hfValue.Text = equipmentType.ID.ToString();
                                textControl.Text = equipmentType.CompleteName;
                            }
                            break;
                        case Globals.AutoComplete.AutoCompleteSource.Equipment:
                            if (!value.Equals("0") && !string.IsNullOrEmpty(value))
                            {
                                CS_Equipment equipment = equipmentModel.GetEquipment(int.Parse(value));
                                hfValue.Text = equipment.ID.ToString();
                                textControl.Text = equipment.CompleteName;
                            }
                            break;
                        case Globals.AutoComplete.AutoCompleteSource.LocalEquipmentType:
                            if (!value.Equals("0") && !string.IsNullOrEmpty(value))
                            {
                                CS_LocalEquipmentType equipmentType = equipmentModel.GetLocalEquipmentTypeByID(int.Parse(value));
                                hfValue.Text = equipmentType.ID.ToString();
                                textControl.Text = equipmentType.Name;
                            }
                            break;
                        case Globals.AutoComplete.AutoCompleteSource.ProjectManager:
                            if (!value.Equals("0") && !string.IsNullOrEmpty(value))
                            {
                                CS_Employee employee = employeeModel.GetEmployee(int.Parse(value));
                                hfValue.Text = employee.ID.ToString();
                                textControl.Text = employee.DivisionAndFullName;
                            }
                            break;
                    }
                }
            }
            get
            {
                return hfValue.Text;
            }
        }

        public string HiddenFieldValueClientID
        {
            get { return hfValue.ClientID; }
        }

        #endregion

        #region [ Required Field Validator ]

        public RequiredFieldValidator RequiredFieldControl
        {
            get
            {
                return rfvSelectedValue;
            }
        }

        /// <summary>
        /// Returns the name of the Validator Control
        /// </summary>
        public string RequiredFieldClientId
        {
            get
            {
                return rfvSelectedValue.ClientID;
            }
        }

        /// <summary>
        /// Set if the Field is required
        /// </summary>
        public bool RequiredField
        {
            get
            {
                return rfvSelectedValue.Enabled;
            }
            set
            {
                rfvSelectedValue.Enabled = value;
            }
        }

        /// <summary>
        /// Sets the validation group of the Required Field Validator
        /// </summary>
        public string ValidationGroup
        {
            get
            {
                return rfvSelectedValue.ValidationGroup;
            }
            set
            {
                rfvSelectedValue.ValidationGroup = value;
            }
        }

        /// <summary>
        /// Sets the message of the Required Field Validator
        /// </summary>
        public string ErrorMessage
        {
            set
            {
                rfvSelectedValue.ErrorMessage = value;
            }
        }


        #endregion

        #region [ GridView ]

        public string ColumnHeaderList
        {
            get
            {
                return _ColumnHeaderList;
            }
            set
            {
                _ColumnHeaderList = value;
            }
        }

        public string ColumnValueList
        {
            get
            {
                return _ColumnValueList;
            }
            set
            {
                _ColumnValueList = value;
            }
        }

        public string GridViewButtonImageUrl
        {
            get
            {
                return _GridViewButtonImageUrl;
            }
            set
            {
                _GridViewButtonImageUrl = value;
            }
        }

        public string GridViewIdName
        {
            get
            {
                return _GridViewIdName;
            }
            set
            {
                _GridViewIdName = value;
            }
        }

        public string DisplayField
        {
            set
            {
                _DisplayField = value;
            }
        }

        public Globals.AutoComplete.AutoCompleteSource AutoCompleteSource
        {
            get
            {
                return _AutoCompleteSource;
            }
            set
            {
                _AutoCompleteSource = value;
            }
        }

        #endregion

        #region [ Filter ]

        public string FilterId
        {
            get
            {
                return hfFilter.Value;
            }
            set
            {
                hfFilter.Value = value;
            }
        }

        #endregion

        #region [ All control ]

        public bool Enabled
        {
            get { return aceCustomer.Enabled; }
            set
            {
                textControl.Enabled = value;
                //aceCustomer.Enabled = value;
                rfvSelectedValue.Enabled = value;
                imgButton.Enabled = value;

                /*if (!value)
                    textControl.Attributes.Remove("onblur");
                 * */
            }
        }

        #endregion



        #endregion
    }
}
