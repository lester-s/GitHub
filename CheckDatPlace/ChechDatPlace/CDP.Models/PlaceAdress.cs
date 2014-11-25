﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDP.Models
{
    public class PlaceAddress
    {
        public PlaceAddress(int number, string streetName, string cityName, int codePostal)
        {
            this.Number = number;
            this.StreetName = streetName;
            this.CodePostal = codePostal;
            this.CityName = cityName;
        }

        public PlaceAddress()
        {

        }
        public int? Number { get; set; }
        public string StreetName { get; set; }
        public string CityName { get; set; }
        public int CodePostal { get; set; }
    }
}
