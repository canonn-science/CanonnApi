using System;
using System.Collections.Generic;

namespace RuinsApi.DatabaseModels
{
    public partial class RuinLayout
    {
        public RuinLayout()
        {
            LayoutVariant = new HashSet<LayoutVariant>();
            RuinlayoutObeliskgroups = new HashSet<RuinLayoutObeliskGroups>();
        }

        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<LayoutVariant> LayoutVariant { get; set; }
        public virtual ICollection<RuinLayoutObeliskGroups> RuinlayoutObeliskgroups { get; set; }
        public virtual RuinType Type { get; set; }
    }
}
