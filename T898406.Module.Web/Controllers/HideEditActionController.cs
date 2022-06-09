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
    public class HideEditActionController : ViewController<ListView>
    {
        ListViewController listViewController;
        ASPxGridListEditor listEditor;
        bool flag = false;
        public HideEditActionController()
        {
            TargetViewId = "DomainObject1_Ones_ListView";
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
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
                    if (!flag)
                    {
                        listEditor.Grid.ClientSideEvents.Init = "OnInit";
                        flag = true;
                    }
                    listEditor.Grid.ClientSideEvents.EndCallback = "OnEndCallback";
                }
            }
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
            listViewController = Frame.GetController<ListViewController>();
            if (listViewController != null)
            {
                listViewController.EditAction.Active["123"] = false;
            }
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            flag = false;
            if (listViewController != null)
            {
                listViewController.EditAction.Active.RemoveItem("123");
                listViewController = null;
            }
        }
    }
}