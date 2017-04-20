using System;
using System.Collections.Generic;

namespace CanonnApi.Web.DatabaseModels
{
    public partial class Obelisk
    {
        public Obelisk()
        {
            RuinsiteActiveobelisks = new HashSet<RuinsiteActiveobelisks>();
        }

        public int Id { get; set; }
        public int? CodexdataId { get; set; }
        public DateTime Created { get; set; }
        public bool IsBroken { get; set; }
        public bool IsVerified { get; set; }
        public int Number { get; set; }
        public int ObeliskgroupId { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<RuinsiteActiveobelisks> RuinsiteActiveobelisks { get; set; }
        public virtual CodexData Codexdata { get; set; }
        public virtual ObeliskGroup Obeliskgroup { get; set; }
    }
}
