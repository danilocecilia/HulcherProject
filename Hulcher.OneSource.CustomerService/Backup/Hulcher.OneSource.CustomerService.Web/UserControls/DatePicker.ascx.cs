using System;
using System.Web.UI;
using AjaxControlToolkit;
using System.Text;
using System.Web.UI.WebControls;

namespace Hulcher.OneSource.CustomerService.Web.UserControls
{
    public partial class DatePicker : System.Web.UI.UserControl
    {
        private string _replicationScript;

        protected void Page_Load(object sender, EventArgs e)
        {
            var script = new StringBuilder();
            script.Append(" $(document).ready(function () {");
            script.Append(" $('#" + txtDatePicker.ClientID + "').datepicker({");
            script.Append("showOn: 'button',");
            script.Append("buttonImage: 'Images/DatePicker/Calendar.gif',");
            script.Append("buttonImageOnly: true,");
            script.Append("dateFormat: '" + DateFormat + "'");
            script.Append(" });");
            script.Append("});");

            script.Append("$('#" + txtDatePicker.ClientID + "').blur(function() {");
            script.AppendFormat("var ctrl = document.getElementById('{0}');", txtDatePicker.ClientID);
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
            if (!string.IsNullOrEmpty(TextControlOnBlurScript))
                script.Append(TextControlOnBlurScript);
            script.Append("});");

            if (!string.IsNullOrEmpty(TextControlOnClientClickScript))
                txtDatePicker.Attributes.Add("onclick", TextControlOnClientClickScript);
            if (!string.IsNullOrEmpty(TextControlOnFocusScript))
                txtDatePicker.Attributes.Add("onfocus", TextControlOnFocusScript);

            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            if (null == scriptManager)
            {
                ClientScriptManager cs = this.Page.ClientScript;
                cs.RegisterStartupScript(GetType(), string.Format("dPicker_{0}", txtDatePicker.ClientID), script.ToString(), true);
            }
            else
            {
                script.Append("var prm = Sys.WebForms.PageRequestManager.getInstance();\n");
                script.Append("\n");
                script.Append("    prm.add_endRequest(function () {\n");
                script.Append("     $('#" + txtDatePicker.ClientID + "').datepicker({");
                script.Append("         showOn: '" + ShowOnProp + "',");
                script.Append("         buttonImage: 'Images/DatePicker/Calendar.gif',");
                script.Append("         buttonImageOnly: true,");
                script.Append("         dateFormat: '" + DateFormat + "'");
                script.Append("     });");
                script.Append("    })\n");
                ScriptManager.RegisterStartupScript(this, GetType(), string.Format("dPicker_{0}", txtDatePicker.ClientID), script.ToString(), true);
            }

            if (!string.IsNullOrEmpty(_replicationScript))
                ScriptManager.RegisterStartupScript(this, GetType(), string.Format("dPicker_{0}_Replication", txtDatePicker.ClientID), _replicationScript, true);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (ShowOnProp == null)
                ShowOnProp = "focus";
        }

        public string ReplicationScript
        {
            get
            {
                return _replicationScript;
            }
            set
            {
                _replicationScript = value;
            }


        }

        protected string DateFormat;
        protected string ShowOnProp;

        public enum DtFormat { Default = 1, Iso8601, Short, Medium, Full, WithText }
        public enum ShowOnTrigger { Focus = 1, Both, Button }
        public enum MaskType { None = 1, Date = 2, DateTime = 3, Number = 4, Time = 5 }

        private DtFormat _dateTimeFormat;
        private ShowOnTrigger _showOn;


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
                        MaskedEditExtender1.Mask = "99/99/9999";
                        MaskedEditExtender1.MaskType = MaskedEditType.Date;
                        MaskedEditValidator1.ValidationExpression = @"^((0[1-9])|(1[0-2]))\/((0[1-9])|(1[0-9])|(2[0-9])|(3[0-1]))\/(\d{4})$";
                        break;
                    case DtFormat.Iso8601:
                        DateFormat = "yy/mm/dd";
                        MaskedEditExtender1.Mask = "9999/99/99";
                        MaskedEditExtender1.MaskType = MaskedEditType.Date;
                        MaskedEditValidator1.ValidationExpression = @"^(19|20)\d\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$";
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
        public string MaskedEditValidatorID
        {
            get { return MaskedEditValidator1.ClientID; }
        }

        [PersistenceMode(PersistenceMode.Attribute)]
        public string EmptyValueMessage
        {
            get
            {
                return MaskedEditValidator1.EmptyValueMessage;
            }
            set
            {
                MaskedEditValidator1.EmptyValueMessage = value;
            }
        }

        [PersistenceMode(PersistenceMode.Attribute)]
        public string InvalidValueMessage
        {
            get
            {
                return MaskedEditValidator1.InvalidValueMessage;
            }
            set
            {
                MaskedEditValidator1.InvalidValueMessage = value;
            }
        }

        public DateTime? Value
        {
            get
            {
                if (txtDatePicker.Text.Length.Equals(0))
                    return null;
                else
                    return Convert.ToDateTime(txtDatePicker.Text);
            }
            set
            {
                if (value.HasValue)
                {
                    switch (_dateTimeFormat)
                    {
                        case DtFormat.Default:
                            txtDatePicker.Text = value.Value.ToString("MM/dd/yyyy");
                            break;
                        case DtFormat.Iso8601:
                            txtDatePicker.Text = value.Value.ToString("yyyy/MM/dd");
                            break;
                        case DtFormat.Short:
                            txtDatePicker.Text = value.Value.ToString("d, M, y");
                            break;
                        case DtFormat.Medium:
                            txtDatePicker.Text = value.Value.ToString("d MM, y");
                            break;
                        case DtFormat.Full:
                            txtDatePicker.Text = value.Value.ToString("DD, d MM, yy");
                            break;
                        case DtFormat.WithText:
                            txtDatePicker.Text = value.Value.ToString("'day' d 'of' MM 'in the year' yy");
                            break;
                    }
                }
                else
                    txtDatePicker.Text = string.Empty;
            }
        }

        [PersistenceMode(PersistenceMode.Attribute)]
        public string ValidationGroup
        {
            get
            {
                return MaskedEditValidator1.ValidationGroup;
            }
            set
            {
                MaskedEditValidator1.ValidationGroup = value;
                cvDatePicker.ValidationGroup = value;
            }
        }

        [PersistenceMode(PersistenceMode.Attribute)]
        public bool IsValidEmpty
        {
            get
            {
                return MaskedEditValidator1.IsValidEmpty;
            }
            set
            {
                MaskedEditValidator1.IsValidEmpty = value;
            }
        }

        [PersistenceMode(PersistenceMode.Attribute)]
        public string TextCompareValidator
        {
            get
            {
                return cvDatePicker.ErrorMessage;
            }
            set
            {
                cvDatePicker.ErrorMessage = value;

            }
        }

        [PersistenceMode(PersistenceMode.Attribute)]
        public ValidationCompareOperator ValidationCompareOperator
        {
            get
            {
                return cvDatePicker.Operator;
            }
            set
            {
                cvDatePicker.Operator = value;
            }
        }

        [PersistenceMode(PersistenceMode.Attribute)]
        public bool EnableCompareValidator
        {
            get
            {
                return cvDatePicker.Visible;
            }
            set
            {
                cvDatePicker.Visible = value;
            }
        }

        [PersistenceMode(PersistenceMode.Attribute)]
        public string ValueToCompare
        {
            get
            {
                return cvDatePicker.ValueToCompare;
            }
            set
            {
                cvDatePicker.ValueToCompare = value;
            }
        }

        [PersistenceMode(PersistenceMode.Attribute)]
        public bool EnableDatePicker
        {
            get
            {
                return txtDatePicker.Enabled;
            }
            set
            {
                txtDatePicker.Enabled = value;
                var script = new StringBuilder();

                if (!value)
                {
                    script.Append("$('#" + txtDatePicker.ClientID + "').datepicker( \"disable\" );");
                }
                else
                {
                    script.Append("$('#" + txtDatePicker.ClientID + "').datepicker( \"enable\" );");
                }

                ClientScriptManager cs = this.Page.ClientScript;
                cs.RegisterStartupScript(GetType(), string.Format("dPicker_{0}_Enable", txtDatePicker.ClientID), script.ToString(), true);
            }
        }

        [PersistenceMode(PersistenceMode.Attribute)]
        public bool EnableDisableMasketEditValidator
        {
            set
            {
                MaskedEditValidator1.Enabled = value;
            }
        }

        [PersistenceMode(PersistenceMode.Attribute)]
        public string TextBoxClientID
        {
            get
            {
                return txtDatePicker.ClientID;
            }
        }

        [PersistenceMode(PersistenceMode.Attribute)]
        public string CompareValidatorClientID
        {
            get
            {
                return cvDatePicker.ClientID;
            }
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
    }
}
