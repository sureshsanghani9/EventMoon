//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MusicEventDataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserGroup
    {
        public int UserGroupId { get; set; }
        public Nullable<int> AdminId { get; set; }
        public string GroupName { get; set; }
        public string GroupImage { get; set; }
        public string GroupType { get; set; }
        public string TagName { get; set; }
        public Nullable<bool> ISActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public System.DateTime LastSeen { get; set; }
    }
}
