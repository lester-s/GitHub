using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDP.Models
{
    public class Place
    {
        public int PlaceID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public CDPEnum.PlaceType Type { get; set; }
        public PlaceAddress Address { get; set; }
        public int? Rate { get; set; }
        public int? AvgPrice { get; set; }
    }
}
