using System;
using System.Collections.Generic;

namespace Radio
{
    public partial class Ganrs
    {
        public Ganrs()
        {
            Records = new HashSet<Records>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Records> Records { get; set; }
    }
}
