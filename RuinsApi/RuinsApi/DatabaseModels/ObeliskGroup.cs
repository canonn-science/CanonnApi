using System;
using System.Collections.Generic;

namespace RuinsApi.DatabaseModels
{
    public partial class ObeliskGroup
    {
        public ObeliskGroup()
        {
            Obelisk = new HashSet<Obelisk>();
            RuinlayoutObeliskgroups = new HashSet<RuinlayoutObeliskgroups>();
        }

        public int Id { get; set; }
        public int Count { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public int RuintypeId { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<Obelisk> Obelisk { get; set; }
        public virtual ICollection<RuinlayoutObeliskgroups> RuinlayoutObeliskgroups { get; set; }
        public virtual RuinType Ruintype { get; set; }
    }
}
