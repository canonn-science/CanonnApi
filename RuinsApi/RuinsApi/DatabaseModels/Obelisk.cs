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
        public DateTime Created { get; set; }
        public int? DataId { get; set; }
        public int GroupId { get; set; }
        public int Number { get; set; }
        public int? RelictId { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<ActiveObelisk> ActiveObelisk { get; set; }
        public virtual CodexData Data { get; set; }
        public virtual ObeliskGroup Group { get; set; }
        public virtual Relict Relict { get; set; }
    }
}
