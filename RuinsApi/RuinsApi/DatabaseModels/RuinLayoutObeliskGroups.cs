using System;
using System.Collections.Generic;

namespace RuinsApi.DatabaseModels
{
    public partial class RuinlayoutObeliskgroups
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public int ObeliskgroupId { get; set; }
        public int RuinlayoutId { get; set; }
        public DateTime Updated { get; set; }

        public virtual ObeliskGroup Obeliskgroup { get; set; }
        public virtual RuinLayout Ruinlayout { get; set; }
    }
}
