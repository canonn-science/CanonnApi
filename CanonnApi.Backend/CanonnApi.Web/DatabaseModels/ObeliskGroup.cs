using System;
using System.Collections.Generic;

namespace CanonnApi.Web.DatabaseModels
{
    public partial class ObeliskGroup
    {
        public ObeliskGroup()
        {
            Obelisk = new HashSet<Obelisk>();
            RuinsiteObeliskgroups = new HashSet<RuinsiteObeliskgroups>();
        }

        public int Id { get; set; }
        public int Count { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public int RuintypeId { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<Obelisk> Obelisk { get; set; }
        public virtual ICollection<RuinsiteObeliskgroups> RuinsiteObeliskgroups { get; set; }
        public virtual RuinType Ruintype { get; set; }
    }
}
