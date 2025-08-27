using System;
using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class AspnetUser
    {
        public AspnetUser()
        {
            AspnetUsersInRoles = new HashSet<AspnetUsersInRole>();
        }

        public Guid ApplicationId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string LoweredUserName { get; set; }
        public string MobileAlias { get; set; }
        public bool IsAnonymous { get; set; }
        public DateTime LastActivityDate { get; set; }

        public virtual AspnetApplication Application { get; set; }
        public virtual AspnetMembership AspnetMembership { get; set; }
        public virtual ICollection<AspnetUsersInRole> AspnetUsersInRoles { get; set; }
    }
}
