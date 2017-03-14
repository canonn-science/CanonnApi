using System;
using System.Collections.Generic;

namespace RuinsApi.DatabaseModels
{
    public partial class CodexCategory
    {
        public CodexCategory()
        {
            CodexData = new HashSet<CodexData>();
        }

        public int Id { get; set; }
        public int ArtifactId { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<CodexData> CodexData { get; set; }
        public virtual Artifact Artifact { get; set; }
    }
}
