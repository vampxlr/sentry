//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Freelance.AuditApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class AspectItem
    {
        public int AspectItemsID { get; set; }
        public Nullable<int> ProjectAspectID { get; set; }
        public string AspectItem1 { get; set; }
    
        public virtual ProjectAspect ProjectAspect { get; set; }
    }
}
