using System;
using System.Collections.Generic;

namespace CanonnApi.Web.DatabaseModels
{
    public partial class Location
    {
        public Location()
        {
            RuinSite = new HashSet<RuinSite>();
        }

        public int Id { get; set; }
        public int? BodyId { get; set; }
        public DateTime Created { get; set; }
        public int? DirectionSystemId { get; set; }
        public int? Distance { get; set; }
        public bool IsVisible { get; set; }
        public decimal? Latitude { get; set; }
        public int LocationtypeId { get; set; }
        public decimal? Longitude { get; set; }
        public int SystemId { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<RuinSite> RuinSite { get; set; }
        public virtual Body Body { get; set; }
        public virtual System DirectionSystem { get; set; }
        public virtual LocationType Locationtype { get; set; }
        public virtual System System { get; set; }
    }
}
