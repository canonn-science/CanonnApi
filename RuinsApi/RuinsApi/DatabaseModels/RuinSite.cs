using System;
using System.Collections.Generic;

namespace RuinsApi.DatabaseModels
{
    public partial class RuinSite
    {
        public RuinSite()
        {
            RuinsiteActiveobelisks = new HashSet<RuinsiteActiveobelisks>();
            RuinsiteObeliskgroups = new HashSet<RuinsiteObeliskgroups>();
        }

        public int Id { get; set; }
        public int BodyId { get; set; }
        public DateTime Created { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int RuintypeId { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<RuinsiteActiveobelisks> RuinsiteActiveobelisks { get; set; }
        public virtual ICollection<RuinsiteObeliskgroups> RuinsiteObeliskgroups { get; set; }
        public virtual Body Body { get; set; }
        public virtual RuinType Ruintype { get; set; }
    }
}
