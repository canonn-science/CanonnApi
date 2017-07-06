using System;
using System.Collections.Generic;

namespace CanonnApi.Web.DatabaseModels
{
    public partial class Body
    {
        public Body()
        {
            Location = new HashSet<Location>();
        }

        public int Id { get; set; }
        public DateTime Created { get; set; }
        public int? Distance { get; set; }
        public double? EarthMasses { get; set; }
        public int? EddbExtId { get; set; }
        public int? EdsmExtId { get; set; }
        public DateTime? EdsmLastUpdate { get; set; }
        public double? Gravity { get; set; }
        public bool? IsLandable { get; set; }
        public string Name { get; set; }
        public double? Radius { get; set; }
        public int SystemId { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<Location> Location { get; set; }
        public virtual System System { get; set; }
    }
}
