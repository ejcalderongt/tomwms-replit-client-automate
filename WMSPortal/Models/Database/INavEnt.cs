using System.Collections.Generic;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class INavEnt
    {
        public INavEnt()
        {
            INavConfigDets = new HashSet<INavConfigDet>();
            INavConfigEnts = new HashSet<INavConfigEnt>();
        }

        public int Idnavent { get; set; }
        public string Nombre { get; set; }
        public string Endpoint { get; set; }

        public virtual ICollection<INavConfigDet> INavConfigDets { get; set; }
        public virtual ICollection<INavConfigEnt> INavConfigEnts { get; set; }
    }
}
