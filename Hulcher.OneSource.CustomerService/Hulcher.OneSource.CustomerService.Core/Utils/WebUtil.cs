using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.IO.Compression;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;

namespace Hulcher.OneSource.CustomerService.Core.Utils
{
    public class WebUtil
    {
        public static Table ListCustomerSpecificInfoFields(IList<CustomerSpecificInfo> listCustomerInfo)
        {
            Table tblCustomerSpecific = new Table();
            TableRow trItem = null;
            TableCell tdLabelItem = null;
            TableCell tdTextItem = null;

            Label lbl = null;
            TextBox txt = null;

            foreach (CustomerSpecificInfo item in listCustomerInfo)
            {
                trItem = new TableRow();

                tdLabelItem = new TableCell();

                lbl = new Label();
                lbl.Text = string.Format("{0}:", item.Name);
                lbl.ID = string.Format("lbl{0}", item.Name.Replace(" ", ""));
                tdLabelItem.Controls.Add(lbl);
                trItem.Controls.Add(tdLabelItem);

                tdTextItem = new TableCell();
                switch (item.Type)
                {
                    default:
                        txt = new TextBox();
                        txt.ID = string.Format("txt{0}", item.Name.Replace(" ", ""));
                        if (!string.IsNullOrEmpty(item.Value))
                            txt.Text = item.Value;
                        tdTextItem.Controls.Add(txt);
                        break;
                }
                trItem.Controls.Add(tdTextItem);

                tblCustomerSpecific.Controls.Add(trItem);
            }

            return tblCustomerSpecific;
        }

        public static string ListCustomerSpecificInfo(Table tblCustomerSpecific)
        {
            List<CustomerSpecificInfo> list = new List<CustomerSpecificInfo>();
            CustomerSpecificInfo item = null;

            Label lbl = null;
            TextBox txt = null;

            foreach (TableRow row in tblCustomerSpecific.Controls)
            {
                lbl = row.Cells[0].Controls[0] as Label;
                txt = row.Cells[1].Controls[0] as TextBox;

                item = new CustomerSpecificInfo();
                item.Name = lbl.Text;
                item.Type = "TextBox";
                item.Value = txt.Text;
                list.Add(item);
            }

            return CustomerSpecificInfo.SerializeObject(list);
        }

        public static byte[] Compress(byte[] data)
        {
            using (MemoryStream output = new MemoryStream())
            {
                using (GZipStream gzip = new GZipStream(output, CompressionMode.Compress, true))
                {
                    gzip.Write(data, 0, data.Length);
                    gzip.Close();
                    return output.ToArray();
                }
            }
        }

        public static byte[] Decompress(byte[] data)
        {
            using (MemoryStream input = new MemoryStream())
            {
                input.Write(data, 0, data.Length);
                input.Position = 0;
                using (GZipStream gzip = new GZipStream(input, CompressionMode.Decompress, true))
                {
                    using (MemoryStream output = new MemoryStream())
                    {
                        byte[] buff = new byte[64];
                        int read = -1;
                        read = gzip.Read(buff, 0, buff.Length);
                        while (read > 0)
                        {
                            output.Write(buff, 0, read);
                            read = gzip.Read(buff, 0, buff.Length);
                        }
                        gzip.Close();
                        return output.ToArray();
                    }
                }
            }
        }

        public string BuildQueryStringToPrintCallLogViewInDashBoardView(IDashboardView view)
        {
            QueryStringBuilder builder = new QueryStringBuilder();
            if (view.DashBoardViewType != Globals.Dashboard.ViewType.None)               
                builder.AppendQueryString("ViewPoint", Convert.ToInt32(view.DashBoardViewType).ToString());
            if (!view.BeginDateCallLogViewFilter.Equals(DateTime.MinValue))
                builder.AppendQueryString("StartDateFilter", view.BeginDateCallLogViewFilter.ToString("MM/dd/yyyy"));
            if (!view.EndDateCallLogViewFilter.Equals(DateTime.MinValue))
                builder.AppendQueryString("EndDateFilter", view.EndDateCallLogViewFilter.ToString("MM/dd/yyyy"));
            if(view.JobStatusCallLogViewFilter.HasValue)
                builder.AppendQueryString("JobStatusFilter", view.JobStatusCallLogViewFilter.Value.ToString());
            if(view.DivisionValueCallLogViewFilter.HasValue)
                builder.AppendQueryString("DivisionFilter", view.DivisionValueCallLogViewFilter.Value.ToString());
            if(view.CallTypeCallLogViewFilter.HasValue)
                builder.AppendQueryString("CallTypeFilter", view.CallTypeCallLogViewFilter.Value.ToString());
            if(!string.IsNullOrEmpty(view.ModifiedByCallLogViewFilter))
                builder.AppendQueryString("ModifiedByFilter",view.ModifiedByCallLogViewFilter);
            if (view.OrderBy != null && view.OrderBy.Length > 0)
                builder.AppendQueryString("OrderBy", string.Format("{0} {1}", view.OrderBy[0], view.OrderBy[1]));
            if(view.ShiftTransferLogCallLogViewFilter)
                builder.AppendQueryString("ShiftTransferLogFilter", view.ShiftTransferLogCallLogViewFilter.ToString());
            if(view.GeneralLogCallLogViewFilter)
                builder.AppendQueryString("GeneralLogFilter", view.GeneralLogCallLogViewFilter.ToString());
            if (null != view.PersonNameCallLog)
                builder.AppendQueryString("PersonName", view.PersonNameCallLog);

            return builder.ToString();
        }

        public string BuildQueryStringToPrintJobSummaryInDashBoardView(IDashboardView view)
        {
            QueryStringBuilder builder = new QueryStringBuilder();
            if (view.DashBoardViewType != Globals.Dashboard.ViewType.None)
                builder.AppendQueryString("ViewPoint", Convert.ToInt32(view.DashBoardViewType).ToString());
            if (view.JobStatusFilterValue.HasValue)
                builder.AppendQueryString("JobStatusFilter", view.JobStatusFilterValue.Value.ToString());
            if (view.JobNumberFilterValue.HasValue)
                builder.AppendQueryString("JobNumberFilter", view.JobNumberFilterValue.Value.ToString());
            if (view.DivisionFilterValue.HasValue)
                builder.AppendQueryString("DivisionFilter", view.DivisionFilterValue.Value.ToString());
            if (view.CustomerFilterValue.HasValue)
                builder.AppendQueryString("CustomerFilter", view.CustomerFilterValue.Value.ToString());
            if (view.DateFilterTypeValue != Globals.Dashboard.DateFilterType.None)
                builder.AppendQueryString("DateTypeFilter", Convert.ToInt32(view.DateFilterTypeValue).ToString());
            if (view.BeginDateJobSummaryValue != DateTime.MinValue)
                builder.AppendQueryString("StartDateFilter", view.BeginDateJobSummaryValue.ToString("MM/dd/yyyy"));
            if (view.EndDateJobSummaryValue != DateTime.MinValue)
                builder.AppendQueryString("EndDateFilter", view.EndDateJobSummaryValue.ToString("MM/dd/yyyy"));
            if (view.OrderBy != null && view.OrderBy.Length > 0)
                builder.AppendQueryString("OrderBy", string.Format("{0} {1}", view.OrderBy[0], view.OrderBy[1]));
            if (null != view.PersonNameValueJobSummary)
                builder.AppendQueryString("PersonName", view.PersonNameValueJobSummary);

            return builder.ToString();
        }

        public string BuildNewWindowClientScript(string pagePath, string queryString,string name, int width, int height,bool scrollbars,bool resizable,bool includeReturnFalse)
        {
            //Append page Path parameter
            StringBuilder scriptBuilder = new StringBuilder();            
            scriptBuilder.Append("javascript: var newWindow = window.open('");
            scriptBuilder.Append(pagePath);
            scriptBuilder.Append(queryString);
            scriptBuilder.Append("',");

            //Append page name parameter
            scriptBuilder.Append("'");
            scriptBuilder.Append(name);
            scriptBuilder.Append("',");

            //Append width parameter
            scriptBuilder.Append("'");
            scriptBuilder.Append("width=");
            scriptBuilder.Append(width.ToString());

            //Append height parameter
            scriptBuilder.Append(",height=");
            scriptBuilder.Append(height);

            //Append Scrollbar parameter
            if(scrollbars)
                scriptBuilder.Append(",scrollbars=1");
            else
                scriptBuilder.Append(",scrollbars=0");
            //Append Resizable parameter
            if(resizable)
                scriptBuilder.Append(",resizable=yes");
            else
                scriptBuilder.Append(",scrollbars=no");

            scriptBuilder.Append("');");

            if (includeReturnFalse)
                scriptBuilder.Append("return false;");

            return scriptBuilder.ToString();
        }

        public string BuildQueryStringToQuickSearch(IDefaultPageView view)
        {
            QueryStringBuilder quickSearchQueryBuilder = new QueryStringBuilder();
            quickSearchQueryBuilder.AppendQueryString("JobId", view.QuickSearchJobId.ToString());
            return quickSearchQueryBuilder.ToString();
        }

        public string BuildQueryStringForQuickReference(Dictionary<string,string> parameters)
        {
            QueryStringBuilder quickReferenceQueryBuilder = new QueryStringBuilder();
            foreach (var item in parameters)
            {
                quickReferenceQueryBuilder.AppendQueryString(item.Key , item.Value);
            }            
            return  quickReferenceQueryBuilder.ToString();
        }

    }
}
