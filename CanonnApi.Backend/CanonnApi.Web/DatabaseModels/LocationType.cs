using System;
using System.Collections.Generic;

namespace CanonnApi.Web.DatabaseModels
{
    public partial class LocationType
    {
        public LocationType()
        {
            Location = new HashSet<Location>();
        }

        public int Id { get; set; }
        public DateTime Created { get; set; }
        public bool IsSurface { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<Location> Location { get; set; }
    }
}
