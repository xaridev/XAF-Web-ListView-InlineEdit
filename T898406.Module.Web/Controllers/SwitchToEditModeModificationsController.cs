using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using T898406.Module.BusinessObjects;

namespace T898406.Module.Web.Controllers
{
    public class SwitchToEditModeModificationsController : ViewController<DetailView>
    {
        public SwitchToEditModeModificationsController()
        {
            TargetObjectType = typeof(DomainObject1);
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            if (View.ViewEditMode == ViewEditMode.View)
            {
                View.ViewEditMode = ViewEditMode.Edit;
            }
        }
    }
}
