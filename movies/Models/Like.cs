using System;
using System.Collections.Generic;

#nullable disable

namespace movies.Models
{
    public partial class Like
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MainImg { get; set; }
        public string MovieImgs { get; set; }
        public string Zhuyan { get; set; }
        public DateTime? ShoTime { get; set; }
    }
}
