using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace T898406.Module.BusinessObjects
{
    //[DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).

    public enum MyEnum
    {
        Dm1 = 0,
        Dm2 = 1
    }
    public abstract class DomainObject3 : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public DomainObject3(Session session)
            : base(session)
        {
        }
        MyEnum type;
        DomainObject1 domainObject1;
        string name3;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name3
        {
            get => name3;
            set => SetPropertyValue(nameof(Name3), ref name3, value);
        }

        public MyEnum Type
        {
            get => type;
            set => SetPropertyValue(nameof(Type), ref type, value);
        }

        [Association("DomainObject1-DomainObject3s")]
        public DomainObject1 DomainObject1
        {
            get => domainObject1;
            set => SetPropertyValue(nameof(DomainObject1), ref domainObject1, value);
        }
    }
}