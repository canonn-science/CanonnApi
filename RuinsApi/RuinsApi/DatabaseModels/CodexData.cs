using System;
using System.Collections.Generic;

namespace RuinsApi.DatabaseModels
{
    public partial class CodexData
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public DateTime Created { get; set; }
        public int EntryNumber { get; set; }
        public string Text { get; set; }
        public DateTime Updated { get; set; }

        public virtual CodexCategory Category { get; set; }
    }
}
