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
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public int PrimaryRelict { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<CodexData> CodexData { get; set; }
        public virtual Relict PrimaryRelictNavigation { get; set; }
    }
}
