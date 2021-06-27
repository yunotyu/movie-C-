using System;
using System.Collections.Generic;

#nullable disable

namespace movies.Models
{
    public partial class Recommend
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Pic { get; set; }
        public string Price { get; set; }
    }
}
