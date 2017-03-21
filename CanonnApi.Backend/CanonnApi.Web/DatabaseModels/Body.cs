using System;
using System.Collections.Generic;

namespace CanonnApi.Web.DatabaseModels
{
    public partial class Body
    {
        public Body()
        {
            RuinSites = new HashSet<RuinSite>();
        }

        public int Id { get; set; }
        public DateTime Created { get; set; }
        public int? EddbExtId { get; set; }
        public int? EdsmExtId { get; set; }
        public string Name { get; set; }
        public int SystemId { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<RuinSite> RuinSites { get; set; }
        public virtual System System { get; set; }
    }
}
