using System;
using System.Collections.Generic;

namespace RuinsApi.DatabaseModels
{
    public partial class RuinLayout
    {
        public RuinLayout()
        {
            RuinlayoutObeliskgroups = new HashSet<RuinlayoutObeliskgroups>();
            RuinlayoutVariant = new HashSet<RuinlayoutVariant>();
        }

        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public int RuintypeId { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<RuinlayoutObeliskgroups> RuinlayoutObeliskgroups { get; set; }
        public virtual ICollection<RuinlayoutVariant> RuinlayoutVariant { get; set; }
        public virtual RuinType Ruintype { get; set; }
    }
}
