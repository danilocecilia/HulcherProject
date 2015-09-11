<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testPage.aspx.cs" Inherits="Hulcher.OneSource.CustomerService.Web.testPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager" AsyncPostBackTimeout="36000" runat="server">
            <Services>
                <asp:ServiceReference Path="AjaxService.svc" />
            </Services>
        </asp:ScriptManager>
        <asp:TextBox ID="txtTest" runat="server" ClientIDMode="Static"></asp:TextBox>
        <asp:HiddenField ID="hfValue" runat="server" ClientIDMode="Static" />
        <input type="hidden" id="hfSelectedFired" />
        <input type="hidden" id="hfFiltro" value="1" />
        <br />
        <br />
        Selected Value : <span id="lblID"></span>
        <br />
        <br />
        <asp:Button ID="btnTeste" runat="server" UseSubmitBehavior="false" OnClientClick="test2(); return false;" />
        <script src="Scripts/jquery-1.6.2.min.js" type="text/javascript"></script>
        <script src="Scripts/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
        <script type="text/javascript">
            $("#txtTest").autocomplete({
                source: function (term, data) {
                    $.ajax({
                        type: "GET",
                        contentType: "application/json; charset=utf-8",
                        url: "WebService/AutoCompleteWebService.svc/GetTest",
                        dataType: "json",
                        data: { "prefixText": term.term,
                                "Test": test(),
                                "blabla": "teste" },
                        success: function (response) {
                            data(response.d);
                        },
                    });
                },
                select: function (event,ui) {
//                              debugger;
//                              alert("Select");
                            document.getElementById("btnTeste").focus();
                            document.getElementById("hfValue").value = GetValueByKey(ui.item.Parameters, "ID");
                            setLabel(ui.item);
                        },
                change: function (event,ui){
                            if ($('#txtTest').val() == "")
                                ClearValue("hfValue");
                            else if (ui.item == null) {
                                tempuri.org.AutoCompleteWebService.GetTest($('#txtTest').val(),1,"testBla", function (callbackObject) { if (callbackObject.length == 0) $('#txtTest').val('Not Found'); else if (callbackObject.length == 1) { var returnObject = callbackObject[0]; $('#txtTest').val(callbackObject[0].value); document.getElementById("hfValue").value = GetValueByKey(returnObject.Parameters, "ID"); setLabel(returnObject) } else $('#txtTest').val('More then 1 register found!'); }); 
                            }
                        },
                minLength: 3
            });

            $('#txtTest').bind("keydown", function () { document.getElementById("hfValue").value = "0"; setLabel(); });

            function test()
            {
                return document.getElementById("hfFiltro").value;
            }

            function ClearValue(hfID)
            {
            debugger;
                document.getElementById(hfID).value = "0";
                lblID.innerHTML = "0";
            }

            function test2()
            {
            debugger;
                alert(document.getElementById("hfValue").value);
                document.getElementById("hfFiltro").value = "2";
            }

            function setLabel(valor)
            {
                lblID.innerHTML = document.getElementById("hfValue").value;
            }

            function GetValueByKey(Parameters, Key)
            {
                for(var i in Parameters)
                {
                    if (Parameters[i].Key = Key)
                    {
                        return Parameters[i].Value;
                    }
                }

                return "";
            }
        </script>
    </div>
    </form>
</body>
</html>
