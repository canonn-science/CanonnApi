using System;
using System.Collections.Generic;

namespace CanonnApi.Web.DatabaseModels
{
    public partial class Artifact
    {
        public Artifact()
        {
            CodexCategory = new HashSet<CodexCategory>();
            CodexData = new HashSet<CodexData>();
        }

        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<CodexCategory> CodexCategory { get; set; }
        public virtual ICollection<CodexData> CodexData { get; set; }
    }
}
