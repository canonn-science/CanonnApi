using System;
using System.Collections.Generic;

namespace CanonnApi.Web.DatabaseModels
{
    public partial class RuinSite
    {
        public RuinSite()
        {
            RuinsiteActiveobelisks = new HashSet<RuinsiteActiveobelisks>();
            RuinsiteObeliskgroups = new HashSet<RuinsiteObeliskgroups>();
        }

        public int Id { get; set; }
        public DateTime Created { get; set; }
        public int LocationId { get; set; }
        public int RuintypeId { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<RuinsiteActiveobelisks> RuinsiteActiveobelisks { get; set; }
        public virtual ICollection<RuinsiteObeliskgroups> RuinsiteObeliskgroups { get; set; }
        public virtual Location Location { get; set; }
        public virtual RuinType Ruintype { get; set; }
    }
}
