using System;
using System.Collections.Generic;

namespace CanonnApi.Web.DatabaseModels
{
    public partial class RuinType
    {
        public RuinType()
        {
            ObeliskGroup = new HashSet<ObeliskGroup>();
            RuinSite = new HashSet<RuinSite>();
        }

        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<ObeliskGroup> ObeliskGroup { get; set; }
        public virtual ICollection<RuinSite> RuinSite { get; set; }
    }
}
