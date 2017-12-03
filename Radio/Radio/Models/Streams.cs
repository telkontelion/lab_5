using System;
using System.Collections.Generic;

namespace Radio
{
    public partial class Streams
    {
        public Streams()
        {
            Records = new HashSet<Records>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? Time { get; set; }
        public int? StaffId { get; set; }
        public int? ArtistId { get; set; }

        public Staffs Staff { get; set; }
        public ICollection<Records> Records { get; set; }
    }
}
