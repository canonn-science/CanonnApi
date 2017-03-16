using System;
using System.Collections.Generic;

namespace RuinsApi.DatabaseModels
{
    public partial class RuinsiteObeliskgroups
    {
        public int RuinsiteId { get; set; }
        public int ObeliskgroupId { get; set; }

        public virtual ObeliskGroup Obeliskgroup { get; set; }
        public virtual RuinSite Ruinsite { get; set; }
    }
}
