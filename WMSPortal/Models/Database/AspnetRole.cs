using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class AspnetRole
    {
        public AspnetRole()
        {
            AspnetUsersInRoles = new HashSet<AspnetUsersInRole>();
        }

        public Guid ApplicationId { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public string LoweredRoleName { get; set; }
        public string Description { get; set; }

        public virtual AspnetApplication Application { get; set; }
        public virtual ICollection<AspnetUsersInRole> AspnetUsersInRoles { get; set; }
    }
}
