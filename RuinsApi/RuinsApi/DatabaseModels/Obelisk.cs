using System;
using System.Collections.Generic;

namespace RuinsApi.DatabaseModels
{
    public partial class Obelisk
    {
        public Obelisk()
        {
            ActiveObelisk = new HashSet<ActiveObelisk>();
        }

        public int Id { get; set; }
        public int? ArtifactId { get; set; }
        public int? CodexdataId { get; set; }
        public DateTime Created { get; set; }
        public int Number { get; set; }
        public int ObeliskgroupId { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<ActiveObelisk> ActiveObelisk { get; set; }
        public virtual Artifact Artifact { get; set; }
        public virtual CodexData Codexdata { get; set; }
        public virtual ObeliskGroup Obeliskgroup { get; set; }
    }
}
