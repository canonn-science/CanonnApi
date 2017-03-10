using System;
using System.Collections.Generic;

namespace RuinsApi.DatabaseModels
{
    public partial class RuinLayoutObeliskGroups
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public int GroupId { get; set; }
        public int LayoutId { get; set; }
        public DateTime Updated { get; set; }

        public virtual ObeliskGroup Group { get; set; }
        public virtual RuinLayout Layout { get; set; }
    }
}
