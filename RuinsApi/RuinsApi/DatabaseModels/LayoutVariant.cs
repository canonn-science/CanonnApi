using System;
using System.Collections.Generic;

namespace RuinsApi.DatabaseModels
{
    public partial class LayoutVariant
    {
        public LayoutVariant()
        {
            ActiveObelisk = new HashSet<ActiveObelisk>();
        }

        public int Id { get; set; }
        public DateTime Created { get; set; }
        public int LayoutId { get; set; }
        public string Name { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<ActiveObelisk> ActiveObelisk { get; set; }
        public virtual RuinLayout Layout { get; set; }
    }
}
