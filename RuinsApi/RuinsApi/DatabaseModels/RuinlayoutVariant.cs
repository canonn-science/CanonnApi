using System;
using System.Collections.Generic;

namespace RuinsApi.DatabaseModels
{
    public partial class RuinlayoutVariant
    {
        public RuinlayoutVariant()
        {
            ActiveObelisk = new HashSet<ActiveObelisk>();
        }

        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public int RuinlayoutId { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<ActiveObelisk> ActiveObelisk { get; set; }
        public virtual RuinLayout Ruinlayout { get; set; }
    }
}
