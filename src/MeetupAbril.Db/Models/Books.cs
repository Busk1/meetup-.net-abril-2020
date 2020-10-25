using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeetupAbril.Db.Models
{
    public partial class Books
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? AuthorId { get; set; }

        public virtual Authors AuthorIdNavigation { get; set; }
    }
}
