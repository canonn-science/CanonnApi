using System;
using System.Collections.Generic;

namespace CanonnApi.Web.DatabaseModels
{
    public partial class CodexData
    {
        public CodexData()
        {
            Obelisk = new HashSet<Obelisk>();
        }

        public int Id { get; set; }
        public int? ArtifactId { get; set; }
        public int CategoryId { get; set; }
        public DateTime Created { get; set; }
        public int EntryNumber { get; set; }
        public string Text { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<Obelisk> Obelisk { get; set; }
        public virtual Artifact Artifact { get; set; }
        public virtual CodexCategory Category { get; set; }
    }
}
