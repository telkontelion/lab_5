using System;
using System.Collections.Generic;

namespace Radio
{
    public partial class Records
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? MusicId { get; set; }
        public int? ArtistId { get; set; }
        public string Album { get; set; }
        public int? Year { get; set; }
        public int? GanrId { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? Longtime { get; set; }
        public int? Raiting { get; set; }

        public Artists Artist { get; set; }
        public Ganrs Ganr { get; set; }
        public Streams Music { get; set; }
    }
}
