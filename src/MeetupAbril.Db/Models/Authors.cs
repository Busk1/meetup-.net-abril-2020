using System;
using System.Collections.Generic;

namespace MeetupAbril.Db.Models
{
    public partial class Authors
    {
        public Authors()
        {
            Books = new HashSet<Books>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }

        public virtual ICollection<Books> Books { get; set; }
    }
}
