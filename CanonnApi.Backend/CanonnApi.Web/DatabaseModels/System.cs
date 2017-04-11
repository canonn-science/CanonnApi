using System;
using System.Collections.Generic;

namespace CanonnApi.Web.DatabaseModels
{
    public partial class System
    {
        public System()
        {
            Bodies = new HashSet<Body>();
        }

        public int Id { get; set; }
        public DateTime Created { get; set; }
        public int? EddbExtId { get; set; }
        public int? EdsmExtId { get; set; }
        public string Name { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<Body> Bodies { get; set; }
    }
}
