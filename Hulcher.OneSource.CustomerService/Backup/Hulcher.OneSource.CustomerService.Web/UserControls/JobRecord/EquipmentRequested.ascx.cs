using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.DataContext.VO;
using Hulcher.OneSource.CustomerService.Core.Security;

using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Hulcher.OneSource.CustomerService.Web.UserControls.JobRecord
{
    public partial class EquipmentRequested : System.Web.UI.UserControl, IEquipmentRequestedView
    {
        #region [ Attributes ]

        /// <summary>
        /// Presenter Class for the Equipment Requested user control
        /// </summary>
        private EquipmentRequestedPresenter _presenter;

        #endregion

        #region [ Properties ]

        public int? JobId { get; set; }

        public int? CallLogId { get; set; }

        //Remove
        //public IList<CS_LocalEquipmentType> EquipmentTypeList
        //{
        //    set
        //    {
        //        ddlEquipmentType.DataSource = value;
        //        ddlEquipmentType.DataTextField = "Name";
        //        ddlEquipmentType.DataValueField = "ID";
        //        ddlEquipmentType.DataBind();
        //    }
        //}
       
        public IList<LocalEquipmentTypeVO> SelectedEquipments
        {
            get
            {
                List<LocalEquipmentTypeVO> _selectedEquipmentsList = new List<LocalEquipmentTypeVO>();
                string[] selectedEquipmentString = hidEquipmentRequested.Value.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < selectedEquipmentString.Length; i++)
                {
                    string[] selectedEquipmentAttributes = selectedEquipmentString[i].Split(new string[] { ";" }, StringSplitOptions.None);

                    LocalEquipmentTypeVO requestedEquip = new LocalEquipmentTypeVO();

                    requestedEquip.LocalEquipmentTypeID = int.Parse(selectedEquipmentAttributes[0]);
                    requestedEquip.Name = selectedEquipmentAttributes[1];
                    requestedEquip.Quantity = int.Parse(selectedEquipmentAttributes[2]);

                    if (!selectedEquipmentAttributes[3].Equals("0"))
                    {
                        requestedEquip.ID = int.Parse(selectedEquipmentAttributes[3]);
                        requestedEquip.CreatedBy = selectedEquipmentAttributes[4];
                        requestedEquip.CreationDate = DateTime.Parse(selectedEquipmentAttributes[5]);
                    }
                    else
                    {
                        requestedEquip.ID = 0;
                        //requestedEquip.CreatedBy = ((IBaseView)Page).Username;
                        requestedEquip.CreatedBy = string.Empty;
                        requestedEquip.CreationDate = DateTime.Now;
                    }

                    _selectedEquipmentsList.Add(requestedEquip);
                }

                return _selectedEquipmentsList;
            }
            set
            {
                hidEquipmentRequested.Value = string.Empty;

                for (int i = 0; i < value.Count; i++)
                {
                    string selectedEquipmentAttributes = "|";

                    selectedEquipmentAttributes += value[i].LocalEquipmentTypeID + ";";
                    selectedEquipmentAttributes += value[i].Name + ";";
                    selectedEquipmentAttributes += value[i].Quantity + ";";
                    selectedEquipmentAttributes += value[i].ID + ";";
                    selectedEquipmentAttributes += value[i].CreatedBy + ";";
                    selectedEquipmentAttributes += value[i].CreationDate.ToString();

                    hidEquipmentRequested.Value += selectedEquipmentAttributes;
                }
            }
        }

        /// <summary>
        /// Sets the Validation Group of the Validators inside the User Control
        /// </summary>
        public string ValidationGroup
        {
            set
            {
                rfvValidateQuantity.ValidationGroup = value;
            }
        }

        #endregion

        #region [ Overrides ]

        /// <summary>
        /// Initializes the Presenter class
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _presenter = new EquipmentRequestedPresenter(this);
        }

        public bool DisplayPanel
        {
            get { return pnlDisplay.Visible; }
            set { pnlDisplay.Visible = value; }
        }

        #endregion

        #region [ Events ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RegisterScript();
            }
            txtQuatity.Attributes.Add("onkeydown", "return NumbersOnly(event);");
        }

        #endregion

        #region [ Methods ]

        public void ReadOnlyEquipmentRequested()
        {
            throw new NotImplementedException();
        }

        public void RegisterScript()
        {
            string script =
                @"function AddSelected() { 
                    var hidden = document.getElementById('" + hidEquipmentRequested.ClientID + @"');
                    var tbEquipments = document.getElementById('tbEquipments');

                    var equipmentText = document.getElementById('" + actEquipmentType.TextControlClientID + @"');
                    var equipmentValue = document.getElementById('" + actEquipmentType.HiddenFieldValueClientID + @"');
                    var equipmentSpecify = document.getElementById('" + txtSpecifyEquipmentType.ClientID + @"');
                    var equipmentQty = document.getElementById('" + txtQuatity.ClientID + @"');

                    if (null != hidden && null != equipmentText && null != equipmentValue && null != equipmentSpecify) {
                        
                        var equipmentString = '|' + equipmentValue.value + ';' + equipmentText.value + ((equipmentSpecify.value == '')? '' : ' - ' + equipmentSpecify.value) + ';' + equipmentQty.value + ';0;;';
                        var equipmentStringSearch = '|' + equipmentValue.value + ';' + equipmentText.value + ((equipmentSpecify.value == '')? '' : ' - ' + equipmentSpecify.value);

                        if (hidden.value.indexOf(equipmentStringSearch) < 0)
                            hidden.value += equipmentString;

                        $find('" + actEquipmentType.BehaviorId + @"').raiseItemSelected(new Sys.Extended.UI.AutoCompleteItemEventArgs(null, '', 0));
                        equipmentSpecify.value = '';
                        equipmentQty.value = '';
                    }

                    CreateTable();
                }
                
                function ShowSpecifyTextBox(eqId)
                {
                    if (eqId == 24 || eqId == 29) {
                        $('#divSpecify').attr('style', 'display:auto;');
                        ValidatorEnable($get('" + rfvSpecifiedEquip.ClientID + @"'), true);
                    }
                    else {
                        $('#divSpecify').attr('style', 'display:none;');
                        ValidatorEnable($get('" + rfvSpecifiedEquip.ClientID + @"'), false);
                    }
                }

                function CreateTable() {                    
                    var hidden = document.getElementById('" + hidEquipmentRequested.ClientID + @"');
                    var tbEquipments = document.getElementById('tbEquipments');
                    var txtValidateQuantity = document.getElementById('" + txtValidateQuantity.ClientID + @"');

                    if(tbEquipments != null){
                        //Clear Table
                        var rowCount = tbEquipments.rows.length;
                        for (var i = rowCount - 1; i > 0; i--) {
                            tbEquipments.deleteRow(i);
                        }

                        if ('' != hidden.value) {
                            txtValidateQuantity.value = '1';
                            var equipmentList = hidden.value.substring(1).split('|');

                            //Insert New Values
                            for (var i = 0; i < equipmentList.length; i++) {
                                var rowIndex = parseInt(i + 1);
                                var values = equipmentList[i].split(';');

                                var row = tbEquipments.insertRow(rowIndex);

                                // Equipment Type Id
                                var cell1 = row.insertCell(-1);
                                cell1.innerHTML = values[0];
                                cell1.style.display = 'none';

                                // Equipment Type name
                                var cell2 = row.insertCell(-1);
                                cell2.innerHTML = values[1];

                            // Quantity TextBox
                            var cell3 = row.insertCell(-1);
                            var textbox = document.createElement('input');
                            textbox.id = 'qty' + rowIndex;
                            textbox.type = 'text';
                            textbox.className = 'input ' + values[2];
                            textbox.style.width = '30px';
                            textbox.value = values[2];
                            textbox.onblur = function () { UpdateQuantity(this, this.value, this.className.replace('input ', ''), this.id.replace('qty', '')); };
                            textbox.onkeydown = function (event) {
                                    event = event || window.event;
                                    var charCode = (event.which) ? event.which : event.keyCode
                                    if (charCode == 46 || charCode == 37 || charCode == 39)
                                        return true;
                                    if (event.srcElement.value.length >= 4 && charCode > 31)
                                        return false;
                                    if (charCode > 31 && !((charCode > 47 && charCode < 58) || (charCode > 95 && charCode < 106)))
                                        return false;
                                    if (charCode > 31 && !((charCode > 47 && charCode < 58) || (charCode > 95 && charCode < 106)) && (charCode != 190 || (charCode == 190 && event.srcElement.value.indexOf('.') != -1)))
                                        return false;

                                    return true;
                                };
                                cell3.appendChild(textbox);

                                if (values[2] == '')
                                    txtValidateQuantity.value = '';

                                // Remove Button
                                var cell4 = row.insertCell(-1);
                                var button = document.createElement('input');
                                button.id = 'btnRemove' + rowIndex;
                                button.type = 'button';
                                button.setAttribute('className', 'linkButtonStyle');
                                button.value = 'Remove';
                                button.onclick = function () { RemoveEquipment(this.id.replace('btnRemove', '')); };
                                cell4.appendChild(button);

                                // Id Relationship
                                var cell5 = row.insertCell(-1);
                                cell5.innerHTML = values[3];
                                cell5.style.display = 'none';

                                // Created By
                                var cell6 = row.insertCell(-1);
                                cell6.innerHTML = values[4];
                                cell6.style.display = 'none';

                                // Creation Date
                                var cell7 = row.insertCell(-1);
                                cell7.innerHTML = values[5];
                                cell7.style.display = 'none';
                            }
                        }

                        if (tbEquipments.rows.length <= 1) {
                            var row = tbEquipments.insertRow(1);

                            var cell1 = row.insertCell(0);
                            cell1.innerHTML = 'The list is Empty';
                            cell1.style.textAlign = 'center';
                            cell1.colSpan = '7';

                            txtValidateQuantity.value = '1';
                            tbEquipments.rows[0].style.display = 'none';
                        }
                        else
                            tbEquipments.rows[0].style.display = '';
                    }
                }

                function UpdateQuantity(textbox, qty, oldqty, index) { 
                    var hidden = document.getElementById('" + hidEquipmentRequested.ClientID + @"');
                    var tbEquipments = document.getElementById('tbEquipments');
                    var txtValidateQuantity = document.getElementById('" + txtValidateQuantity.ClientID + @"');

                    var replaceFrom = '';
                    var replaceTo = '';
                    var row = tbEquipments.rows[index];

                    var id = row.cells[0].innerHTML;
                    var name = row.cells[1].innerHTML;
                    var idrel = row.cells[4].innerHTML;
                    var createdby = row.cells[5].innerHTML;
                    var creationdate = row.cells[6].innerHTML;

                    replaceFrom = '|' + id + ';' + name + ';' + oldqty + ';' + idrel + ';' + createdby + ';' + creationdate;
                    replaceTo = '|' + id + ';' + name + ';' + qty + ';' + idrel + ';' + createdby + ';' + creationdate;
                    hidden.value = hidden.value.replace(replaceFrom, replaceTo);

                    textbox.className = 'input ' + qty;

                    if (qty == '' || qty <= 0)
                        txtValidateQuantity.value = '';
                    else {
                        txtValidateQuantity.value = '1';
                        $('#tbEquipments tr td :input[type=text]').each(function (i) {
                            if (this.value == '' || this.value <= 0)
                                txtValidateQuantity.value = '';
                        });
                    }

                }

                function RemoveEquipment(index) { 
                    var hidden = document.getElementById('" + hidEquipmentRequested.ClientID + @"');
                    var tbEquipments = document.getElementById('tbEquipments');

                    var replace = '';
                    var row = tbEquipments.rows[index];

                    var id = row.cells[0].innerHTML;
                    var name = row.cells[1].innerHTML;
                    var qty = document.getElementById('qty' + index).value;
                    var idrel = row.cells[4].innerHTML;
                    var createdby = row.cells[5].innerHTML;
                    var creationdate = row.cells[6].innerHTML;

                    replace = '|' + id + ';' + name + ';' + qty + ';' + idrel + ';' + createdby + ';' + creationdate;
                    hidden.value = hidden.value.replace(replace, '');

                    CreateTable();
                }

                function NumbersOnly (event) {
                    event = event || window.event;
                    var charCode = (event.which) ? event.which : event.keyCode
                    if (charCode == 46 || charCode == 37 || charCode == 39)
                        return true;
                    if (event.srcElement.value.length >= 4 && charCode > 31)
                        return false;
                    if (charCode > 31 && !((charCode > 47 && charCode < 58) || (charCode > 95 && charCode < 106)))
                        return false;
                    if (charCode > 31 && !((charCode > 47 && charCode < 58) || (charCode > 95 && charCode < 106)) && (charCode != 190 || (charCode == 190 && event.srcElement.value.indexOf('.') != -1)))
                        return false;

                    return true;
                };

                var scriptManager = Sys.WebForms.PageRequestManager.getInstance();
                scriptManager.add_endRequest(function () {
                    CreateTable();
                });
                $(document).ready(function () {
                    CreateTable();
                });";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "EquipmentRequested", script, true);
            actEquipmentType.ScriptToExecute = "ShowSpecifyTextBox";
        }

        public void DisplayMessage(string message, bool closeWindow)
        {
            ((IJobRecordView)Page).DisplayMessage(message, closeWindow);
        }

        #endregion
    }
}
