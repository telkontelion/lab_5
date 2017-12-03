using System;
using System.Collections.Generic;

namespace Radio
{
    public partial class Staffs
    {
        public Staffs()
        {
            Streams = new HashSet<Streams>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Post { get; set; }
        public string Education { get; set; }
        public int? Worktime { get; set; }

        public ICollection<Streams> Streams { get; set; }
    }
}
