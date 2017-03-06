using System;
using System.Collections.Generic;

namespace RuinsApi.DatabaseModels
{
    public partial class RuinType
    {
        public RuinType()
        {
            ObeliskGroup = new HashSet<ObeliskGroup>();
            RuinLayout = new HashSet<RuinLayout>();
        }

        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<ObeliskGroup> ObeliskGroup { get; set; }
        public virtual ICollection<RuinLayout> RuinLayout { get; set; }
    }
}
