using System;
using System.Collections.Generic;

namespace Radio
{
    public partial class Artists
    {
        public Artists()
        {
            Records = new HashSet<Records>();
        }

        public int Id { get; set; }
        public string NameS { get; set; }
        public int? GroupId { get; set; }

        public Groups Group { get; set; }
        public ICollection<Records> Records { get; set; }
    }
}
