using System;
using System.Collections.Generic;

namespace Radio
{
    public partial class Groups
    {
        public Groups()
        {
            Artists = new HashSet<Artists>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Artists> Artists { get; set; }
    }
}
