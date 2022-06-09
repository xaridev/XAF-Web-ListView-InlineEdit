<%@ Page Language="C#" AutoEventWireup="true" Inherits="Default" EnableViewState="false"
    ValidateRequest="false" CodeBehind="Default.aspx.cs" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v21.2, Version=21.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" 
    Namespace="DevExpress.ExpressApp.Web.Templates" TagPrefix="cc3" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v21.2, Version=21.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.ExpressApp.Web.Controls" TagPrefix="cc4" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Main Page</title>
    <meta http-equiv="Expires" content="0" />
     <script language="javascript" type="text/javascript">
         var timerHandle = -1;
         var currentRow;
         var lastRow;
         var init = 0;
        function OnBatchEditStartEditing(s, e) {
            /*clearTimeout(timerHandle);*/
            console.log(e.visibleIndex);
            currentRow = e.visibleIndex;
            
            console.log("Start Editing: " + init);
           /* grid.UpdateEdit();*/
            if (e.visibleIndex < 0 && !grid.batchEditApi.HasChanges(e.visibleIndex - 1)) {
                lastRow = e.visibleIndex;
            }
    	}	
        function OnBatchEditEndEditing(s, e) {
            //timerHandle = setTimeout(function () {
            //    s.UpdateEdit();
            //}, 1000);
            //init = 0;
            //console.log(currentRow);
            //console.log(lastRow);
            if (currentRow == lastRow) {
                grid.AddNewRow();
                grid.UpdateEdit();
            }
         }
         
         function OnKeyDown(s, e) {
             if ((e.htmlEvent.keyCode == 9 || e.htmlEvent.keyCode == 13) && (currentRow == lastRow)) { //tab and last row
                 //console.log(currentRow);
                 //console.log(lastRow);
                 grid.AddNewRow();
                // grid.UpdateEdit();
                 //ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
                 
             }
         }
         //function OnBatchStartEditing(s, e) {
         //    console.log(e.visibleIndex);
         //    currentRow = e.visibleIndex;
         //    if (e.visibleIndex < 0 && !grid.batchEditApi.HasChanges(e.visibleIndex - 1)) {
         //        lastRow = e.visibleIndex;
         //    }
         //}
         function OnInit(s, e) {
             //console.log("OnInit: " + init);
            /* if (init == 0) {*/
                 s.AddNewRow();
                 //s.UpdateEdit();
                 
             //    init = 1;
             //}
             lastRow = s.GetTopVisibleIndex() + s.GetVisibleRowsOnPage() - 1;
         }
         function OnEndCallback(s, e) {
             init = 0;
             lastRow = s.GetTopVisibleIndex() + s.GetVisibleRowsOnPage() - 1;
         }
     </script>

</head>
<body class="VerticalTemplate">
    <form id="form2" runat="server">
    <cc4:ASPxProgressControl ID="ProgressControl" runat="server" />
    <div runat="server" id="Content" />
    </form>
</body>
</html>