using System;
using System.Collections.Generic;

namespace CanonnApi.Web.DatabaseModels
{
    public partial class RuinsiteActiveobelisks
    {
        public int RuinsiteId { get; set; }
        public int ObeliskId { get; set; }

        public virtual Obelisk Obelisk { get; set; }
        public virtual RuinSite Ruinsite { get; set; }
    }
}
