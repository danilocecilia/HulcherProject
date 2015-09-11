using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.ServerControls
{
    [ToolboxData("<{0}:ScrollableGridView runat=server CssClass=ScrollableGridView AutoGenerateColumns=false></{0}:ScrollableGridView>")]
    public class ScrollableGridView : GridView
    {
        #region [ Properties ]

        /// <summary>
        /// Cells that must not be sortable separated by comma
        /// </summary>
        public string NonSorteableCells
        {
            get
            {
                if (null == ViewState[string.Format("{0}_NonSorteableCells", this.ID)])
                    ViewState[string.Format("{0}_NonSorteableCells", this.ID)] = "";

                return ViewState[string.Format("{0}_NonSorteableCells", this.ID)].ToString();
            }
            set
            {
                ViewState[string.Format("{0}_NonSorteableCells", this.ID)] = value;
            }
        }
        /// <summary>
        /// Override Grid Width
        /// </summary>
        public override Unit Width
        {
            get
            {
                if (null == ViewState[string.Format("{0}_Width", this.ID)])
                    Width = new Unit("100%");

                return (Unit)ViewState[string.Format("{0}_Width", this.ID)];
            }
            set
            {
                ViewState[string.Format("{0}_Width", this.ID)] = value;
            }
        }

        /// <summary>
        /// Scrolable Grid Max Height
        /// </summary>
        [DefaultValue(200)]
        public Unit MaxHeight
        {
            get
            {
                if (null == ViewState[string.Format("{0}_MaxHeight", this.ID)])
                    MaxHeight = new Unit("200px");

                return (Unit)ViewState[string.Format("{0}_MaxHeight", this.ID)];
            }
            set
            {
                ViewState[string.Format("{0}_MaxHeight", this.ID)] = value;
            }
        }

        /// <summary>
        /// Scrolable Grid Max Width
        /// </summary>
        [DefaultValue(500)]
        public Unit MinWidth
        {
            get
            {
                if (null == ViewState[string.Format("{0}_MinWidth", this.ID)])
                    MinWidth = new Unit("500px");

                return (Unit)ViewState[string.Format("{0}_MinWidth", this.ID)];
            }
            set
            {
                ViewState[string.Format("{0}_MinWidth", this.ID)] = value;
            }
        }

        /// <summary>
        /// Enables the Saving of the Scroll Position
        /// </summary>
        [DefaultValue(false)]
        public bool SaveScrollPosition
        {
            get
            {
                if (null == ViewState[string.Format("{0}_SaveScrollPosition", this.ID)])
                    SaveScrollPosition = false;

                return (bool)ViewState[string.Format("{0}_SaveScrollPosition", this.ID)];
            }
            set
            {
                ViewState[string.Format("{0}_SaveScrollPosition", this.ID)] = value;
            }
        }

        /// <summary>
        /// Enables the navigating rows by the keyboard
        /// </summary>
        [DefaultValue(false)]
        public bool AllowKeyBoardNavigation
        {
            get
            {
                if (null == ViewState[string.Format("{0}_AllowKeyBoardNavigation", this.ID)])
                    AllowKeyBoardNavigation = false;

                return (bool)ViewState[string.Format("{0}_AllowKeyBoardNavigation", this.ID)];
            }
            set
            {
                ViewState[string.Format("{0}_AllowKeyBoardNavigation", this.ID)] = value;
            }
        }

        #endregion

        #region [ Overrides ]

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            //This code snipet makes the GridView render a <thead> and <tbody> element, which is not default behavior
            this.UseAccessibleHeader = true;

            if (null != this.HeaderRow)
            {
                this.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

            StringBuilder script = new StringBuilder();

            StringBuilder nonSort = new StringBuilder();
            string[] nonSortCells = NonSorteableCells.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < nonSortCells.Length; i++)
            {
                if (nonSort.Length == 0)
                    nonSort.Append("{ headers: {");
                else
                     nonSort.Append(",");

                nonSort.Append(nonSortCells[i].Trim() + ": { sorter:false }");
            }

            if (nonSort.Length > 0)
                nonSort.Append("}}");


            //setSortable: prepares the grid to sort.
            script.Append("function setSortable" + this.ID + "(clientID) {");

            if (this.AllowSorting)
            {
                script.Append("$(clientID).tablesorter(" + nonSort.ToString() + ");");

            }
            script.Append("}");

            script.Append("$(document).ready(function () {");
            script.AppendFormat("setSortable" + this.ID + "(\"#{1}\");", this.ID, this.ClientID);
            script.Append("});");


            //resizeHeader: resizes the grid header on every onresize event.
            //script.Append("function resizeHeader" + this.ID + "() {");
            //script.Append("var colgroup = $('<colgroup>');");
            //script.Append("$(\"#" + this.ClientID + "\").find(\"tr:last td\").each(function () {");
            //script.Append("colgroup.append($('<col>').css('width', ((isNaN(parseInt($(this)[0].style.width.replace(\"px\", \"\"))))? $(this).width() : parseInt($(this)[0].style.width.replace(\"px\", \"\")) ) ));");
            //script.Append("});");
            //script.Append("if (colgroup.length > 0 && colgroup[0].childNodes.length > 0 && colgroup[0].outerHTML.indexOf(\"WIDTH: 0px\") == -1){");
            //script.AppendFormat("$(\"#{0}_Header\").prepend(colgroup);", this.ClientID);
            //script.Append("var rowscolgroup = $('<colgroup>');");
            //script.Append("$(\"#" + this.ClientID + "_Header\").find(\"tr:first th\").each(function () {");
            //script.Append("rowscolgroup.append($('<col>').css('width', ((isNaN(parseInt($(this)[0].style.width.replace(\"px\", \"\"))))? $(this).width() : parseInt($(this)[0].style.width.replace(\"px\", \"\")) ) ));");
            //script.Append("});");
            //script.Append("if (rowscolgroup.length > 0 && rowscolgroup[0].childNodes.length > 0 && rowscolgroup[0].outerHTML.indexOf(\"WIDTH: 0px\") == -1){");
            //script.Append("var i = 0;");
            //script.Append("var setRowsWidth = false;");
            //script.Append("for (i = 0; i < colgroup[0].childNodes.length; i++)");
            //script.Append("{");
            //script.Append("if (parseInt(colgroup[0].childNodes[i].style.width.replace(\"px\", \"\")) < parseInt(rowscolgroup[0].childNodes[i].style.width.replace(\"px\", \"\")))");
            //script.Append("setRowsWidth = true;");
            //script.Append("}");
            //script.Append("if (setRowsWidth)");
            //script.AppendFormat("$(\"#{0}\").prepend(rowscolgroup);", this.ClientID);
            //script.Append(" else ");
            //script.AppendFormat("$(\"#{0}\").prepend(rowscolgroup);", this.ClientID);
            //script.Append("}");
            //script.Append("}");
            //script.Append("clearTimeout(resizeTimer" + this.ID + ");");
            //script.Append("};");

            //script.Append("var resizeTimer" + this.ID + " = null;");

            //script.Append("$(window).bind('resize', function () {");
            //script.Append("if (resizeTimer" + this.ID + ") clearTimeout(resizeTimer" + this.ID + ");");
            //script.Append("resizeTimer" + this.ID + " = setTimeout(resizeHeader" + this.ID + ", 100);");
            //script.Append("});");

            StringBuilder savePositionScript = new StringBuilder();
            //javaScript variables
            savePositionScript.AppendFormat("var control_{0}, yPos_{0};", this.ID);

            //Instance of ScripManager
            savePositionScript.AppendFormat("var prm_{0} = Sys.WebForms.PageRequestManager.getInstance();", this.ID);

            //Appends Methods to the ScriptManager Handlers
            savePositionScript.AppendFormat("prm_{0}.add_beginRequest(BeginRequestHandler_{0});", this.ID);
            savePositionScript.AppendFormat("prm_{0}.add_endRequest(EndRequestHandler_{0});", this.ID);

            //Methods
            savePositionScript.AppendFormat("function BeginRequestHandler_{0}(sender, args)", this.ID);
            savePositionScript.Append("{");
            savePositionScript.AppendFormat("control_{0} = $get('{0}_ScrollDiv');", this.ID);
            savePositionScript.AppendFormat("if (control_{0})", this.ID);
            savePositionScript.AppendFormat(" yPos_{0} = control_{0}.scrollTop;", this.ID);
            savePositionScript.Append("}");

            savePositionScript.AppendFormat("function EndRequestHandler_{0}(sender, args)", this.ID);
            savePositionScript.Append("{");
            savePositionScript.AppendFormat("control_{0} = $get('{0}_ScrollDiv');", this.ID);
            savePositionScript.AppendFormat("if (control_{0})", this.ID);
            savePositionScript.AppendFormat(" control_{0}.scrollTop = yPos_{0};", this.ID);
            savePositionScript.Append("}");

            if (this.AllowKeyBoardNavigation)
            {
                if (SaveScrollPosition)
                {
                    //Instance of ScripManager
                    script.AppendFormat("var prmkey_{0} = Sys.WebForms.PageRequestManager.getInstance();", this.ID);

                    //Appends Methods to the ScriptManager Handlers
                    script.AppendFormat("prmkey_{0}.add_endRequest(EndRequestHandlerKey_{0});", this.ID);

                    script.AppendFormat("function EndRequestHandlerKey_{0}(sender, args)", this.ID);
                    script.Append("{");
                    script.AppendFormat("$('#{0}_Group').keydown(function (event) ", this.ID);
                    script.Append("{ KeyCheck(); });");
                    script.Append("}");
                }
                else
                {
                    script.Append("$(document).ready( function () ");
                    script.Append("{");
                    script.AppendFormat(" $('#{0}_Group').keydown(function (event) ", this.ID);
                    script.Append("{ KeyCheck(); });");
                    script.Append(" });");
                }

                script.Append("var currentId = null;");

                script.Append("function deselectLastRow(odd) {");
                script.Append("var row = document.getElementById(currentId);");
                script.Append("var cells;");
                script.Append("cells = row.getElementsByTagName('td');");
                script.Append("cells[0].setAttribute('id', '');");
                script.Append("if (odd)");
                script.Append("row.setAttribute('className', 'odd');");
                script.Append(" else ");
                script.Append("row.setAttribute('className', 'even');");
                script.Append("}");

                script.Append("function selectCurrentRow() {");
                script.Append("var row = document.getElementById(currentId);");
                script.AppendFormat("var t = document.getElementById('{0}');", this.ClientID);
                script.Append("var cells;");
                script.Append("row.setAttribute('className', 'focus');");
                script.Append("cells = row.getElementsByTagName('td');");
                script.Append("cells[0].setAttribute('id', 'focus');");
                script.Append("var foc = document.getElementById('focus');");
                script.Append("foc.focus();");
                script.Append("if (row.offsetTop >= t.rows[0].offsetTop && row.offsetTop <= t.rows[0].offsetTop + t.rows[0].scrollHeight)");
                script.AppendFormat("{0}_ScrollDiv.scrollTop = t.rows[0].offsetTop +(row.offsetTop - t.rows[0].offsetTop) - t.rows[0].scrollHeight;", this.ID);
                script.Append("}");

                script.Append("function rowClick(id) {");
                script.AppendFormat("var t = document.getElementById('{0}');", this.ClientID);
                script.Append("if (null != currentId)");
                script.Append("deselectLastRow(t.rows.item(currentId).rowIndex % 2 == 0);");
                script.Append("currentId = id;");
                script.Append("selectCurrentRow();");
                script.Append("}");

                script.Append("function KeyCheck() {");
                script.AppendFormat("var t = document.getElementById('{0}');", this.ClientID);
                script.Append("var KeyID = event.keyCode;");
                script.Append("var RowIndex;");
                script.Append("if (KeyID == 38 || KeyID == 9 || KeyID == 40) {");
                script.Append("if (null == currentId)");
                script.Append("currentId = t.rows[1].id;");
                script.Append(" else {");
                script.Append("RowIndex = t.rows.item(currentId).rowIndex;");
                script.Append("if (KeyID == 38) {");
                script.Append("if (RowIndex - 1 <= 0)");
                script.Append("return false;");
                script.Append(" else ");
                script.Append("deselectLastRow(RowIndex % 2 == 0);");
                script.Append("currentId = t.rows[RowIndex -1].id;");
                script.Append("}");
                script.Append(" else {");
                script.Append("if (RowIndex + 1 >= t.rows.length)");
                script.Append("return false;");
                script.Append(" else ");
                script.Append("deselectLastRow(RowIndex % 2 == 0);");
                script.Append("currentId = t.rows[RowIndex +1].id;");
                script.Append("}");
                script.Append("}");
                script.Append("selectCurrentRow();");
                script.Append("return false;");
                script.Append("}");
                script.Append("}");
            }

            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            if (null == scriptManager)
            {
                ClientScriptManager cs = this.Page.ClientScript;
                cs.RegisterStartupScript(GetType(), string.Format("ScrollabeGridView_{0}", ID), script.ToString(), true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), string.Format("ScrollabeGridView_{0}", ID), script.ToString(), true);

                if (SaveScrollPosition)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), string.Format("SaveScrollPosition_{0}", ID), savePositionScript.ToString(), true);
                }
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.WriteLine(string.Format("<div id=\"{0}_Group\" class=\"ScrollableGridView_Group\" style=\"width: {1};\" >", this.ID, this.Width));
            writer.WriteLine(string.Format("<div id=\"{0}_HeaderDiv\" class=\"ScrollableGridView_HeaderDiv\" style=\"min-width: {1};\" ></div>", this.ID, this.MinWidth));
            writer.WriteLine(string.Format("<div id=\"{0}_ScrollDiv\" class=\"ScrollableGridView_ScrollDiv\" style=\"max-height: {1}; min-width: {2};\" >", this.ID, this.MaxHeight, this.MinWidth));
            base.Render(writer);
            this.Attributes.Add("style", "table-layout:fixed");
            writer.WriteLine("</div>");
            writer.WriteLine("</div>");
        }

        protected override void OnRowDataBound(GridViewRowEventArgs e)
        {
            

            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    if (e.Row.Cells[i].Controls.Count.Equals(0))
                        e.Row.Cells[i].Text = "<span class='MarginRight'>" + e.Row.Cells[i].Text + "</span>";
                }
            }

            base.OnRowDataBound(e);

            if (e.Row.RowType == DataControlRowType.DataRow && this.AllowKeyBoardNavigation)
            {
                e.Row.Attributes.Add("id", string.Format("{0}_row_{1}", this.ID, e.Row.DataItemIndex));
                e.Row.Attributes.Add("onclick", string.Format("javascript:rowClick('{0}_row_{1}');", this.ID, e.Row.DataItemIndex));
                e.Row.Attributes.Add("onmouseover", "javascript:this.style.cursor = 'hand';");
                e.Row.Attributes.Add("onmouseout", "javascript:this.style.cursor = '';");
            }
        }

        #endregion
    }
}
