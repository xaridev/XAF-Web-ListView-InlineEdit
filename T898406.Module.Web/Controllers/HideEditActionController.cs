using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.ExpressApp.Web.SystemModule;
using DevExpress.ExpressApp.Web.Templates;
using DevExpress.ExpressApp.Web.Utils;
using DevExpress.Web;
using T898406.Module.BusinessObjects;

namespace T898406.Module.Web.Controllers
{
    public class HideEditActionController : ViewController<ListView>, IXafCallbackHandler
    {
        ListViewController listViewController;
        ASPxGridListEditor listEditor;
        readonly string handlerId;

        public HideEditActionController()
        {
            TargetViewId = "DomainObject1_Ones_ListView";
            handlerId = "HideEditActionController" + GetHashCode();
        }
        protected XafCallbackManager CallbackManager
        {
            get { return ((ICallbackManagerHolder)WebWindow.CurrentRequestPage).CallbackManager; }
        }


        protected override void OnViewControlsCreated()
        {
            
            base.OnViewControlsCreated();
           
            CallbackManager.RegisterHandler(handlerId, this);
            listEditor = ((ListView)View).Editor as ASPxGridListEditor;
            if (listEditor != null)
            {
                if (listEditor.IsBatchMode)
                {
                    
                    listEditor.Grid.Settings.ShowStatusBar = DevExpress.Web.GridViewStatusBarMode.Hidden;
                    ASPxGridView Grid = listEditor.Control as ASPxGridView;
                    Grid.CommandButtonInitialize += Grid_CommandButtonInitialize;
                    Grid.ClientInstanceName = "grid";
                    Grid.CellEditorInitialize += (s, e) => {
                        if (e.Column.FieldName == "PropertyName")
                        {
                            ASPxEditBase editor = e.Editor as ASPxEditBase;
                            editor.SetClientSideEventHandler("KeyDown", "OnKeyDown");
                        }
                    };
                    listEditor.Grid.ClientSideEvents.BatchEditStartEditing = "OnBatchEditStartEditing";
                    listEditor.Grid.ClientSideEvents.Init = "OnInit";
                    //listEditor.Grid.ClientSideEvents.BatchEditEndEditing = "OnBatchEditEndEditing";
                    listEditor.Grid.ClientSideEvents.EndCallback = "OnEndCallback";
                    string script1 = CallbackManager.GetScript(handlerId, "lastRow = s.GetTopVisibleIndex() + s.GetVisibleRowsOnPage() - 1;");
                   // ClientSideEventsHelper.AssignClientHandlerSafe(Grid, "Init", "OnInit", "HideEditActionController");
                    //listEditor.Grid.ClientSideEvents.BatchEditEndEditing = "OnBatchEditEndEditing";
                    //listEditor.Grid.SetClientSideEventHandler("KeyDown", "OnKeyDown");
                    //listEditor.Grid.ClientSideEvents.BatchEditEndEditing = "OnBatchEditEndEditing";
                    //listEditor.Grid.ClientSideEvents.Init = "function(s, e) { s.timerHandle = -1; s.AddNewRow(); s.UpdateEdit();}";
                    listEditor.CommitChanges += ListEditor_CommitChanges;
                    //listEditor.DataSourceChanged += ListEditor_DataSourceChanged;
                    //listEditor.Grid.ClientSideEvents.BatchEditStartEditing = listEditor.Grid.ClientSideEvents.BatchEditStartEditing.Replace("}", " clearTimeout(s.timerHandle); } ");
                    //listEditor.Grid.ClientSideEvents.BatchEditEndEditing = "function(s, e) { s.timerHandle = setTimeout(function() { s.UpdateEdit(); }, 1000); }";
                }


                //Grid.ClientInstanceName = "grid";
                
                //Frame.GetController<WebNewObjectViewController>().NewObjectAction
                //   .SetClientScript("grid.batchEditApi.AddNewRow();", false);
                Frame.GetController<WebNewObjectViewController>().ObjectCreated += HideEditActionController_ObjectCreated;               
                //Grid.ClientSideEvents.FocusedCellChanging "e.cellInfo.column.name + ';' + e.RowIndex"
                //Grid.ClientSideEvents.RowClick = @"function(s, e) { grid.batchEditApi.AddNewRow();}";
                //Add new row
                //Grid.ClientSideEvents.BatchEditEndEditing =
                //    @"function(s, e) {
                //        if(e.focusedColumn.fieldName == 'PropertyName'){
                //            grid.batchEditApi.UpdateEdit();
                //            grid.batchEditApi.AddNewRow();
                //        }
                //    }";
                //string script = CallbackManager.GetScript(handlerId, "grid.batchEditApi.AddNewRow();");
                string script = CallbackManager.GetScript(handlerId, "e.cellInfo.column.name + ';' + e.cellInfo.column.fieldName + ';' + e.cellInfo.rowVisibleIndex");
                // string script = CallbackManager.GetScript(handlerId, "s.UpdateEdit() + ';' s.AddNewRow() + ';'");
                //ClientSideEventsHelper.AssignClientHandlerSafe(Grid, "RowClick", "function(s, e) {" + script + "}", "HideEditActionController");
            }
                
        }

        private void HideEditActionController_ObjectCreated(object sender, DevExpress.ExpressApp.SystemModule.ObjectCreatedEventArgs e)
        {
        }

        private void HideEditActionController_ObjectCreating(object sender, DevExpress.ExpressApp.SystemModule.ObjectCreatingEventArgs e)
        {

        }

        private void ListEditor_CommitChanges(object sender, System.EventArgs e)
        {
            var list = ObjectSpace.GetObjects<DomainObject2>(CriteriaOperator.And(
                new NullOperator("Name"),
                new NullOperator("LastName"),
                new NullOperator("PropertyName")));

            ((ProxyCollection)listEditor.DataSource).Remove(list);
            ObjectSpace.CommitChanges();
        }

        private void ListEditor_DataSourceChanged(object sender, System.EventArgs e)
        {
            
        }

        private void Grid_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
           
            if (e.ButtonType == ColumnCommandButtonType.Update || e.ButtonType == ColumnCommandButtonType.Cancel 
                || e.ButtonType == ColumnCommandButtonType.New)
            {
                e.Visible = false;
            }
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            ObjectSpace.ObjectSaving += ObjectSpace_ObjectSaving;
            listViewController = Frame.GetController<ListViewController>();
            if (listViewController != null)
            {
                listViewController.EditAction.Active["123"] = false;
            }
        }

        private void ObjectSpace_ObjectSaving(object sender, ObjectManipulatingEventArgs e)
        {
            
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            if (listViewController != null)
            {
                listViewController.EditAction.Active.RemoveItem("123");
                listViewController = null;
            }
        }

        public void ProcessAction(string parameter)
        {
            
        }
    }
}