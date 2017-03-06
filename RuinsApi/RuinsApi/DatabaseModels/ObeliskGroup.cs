using System;
using System.Collections.Generic;

namespace RuinsApi.DatabaseModels
{
    public partial class ObeliskGroup
    {
        public ObeliskGroup()
        {
            Obelisk = new HashSet<Obelisk>();
            RuinlayoutObeilskgroups = new HashSet<RuinlayoutObeilskgroups>();
        }

        public int Id { get; set; }
        public int Count { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<Obelisk> Obelisk { get; set; }
        public virtual ICollection<RuinlayoutObeilskgroups> RuinlayoutObeilskgroups { get; set; }
        public virtual RuinType Type { get; set; }
    }
}
