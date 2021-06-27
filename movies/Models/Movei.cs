using System;
using System.Collections.Generic;

#nullable disable

namespace movies.Models
{
    public partial class Movei
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PriviewImgPath { get; set; }
        public string SwiperImgPath { get; set; }
        public DateTime? PlayTime { get; set; }
        public int? Star { get; set; }
        public string Score { get; set; }
        public string VedioPath { get; set; }
    }
}
