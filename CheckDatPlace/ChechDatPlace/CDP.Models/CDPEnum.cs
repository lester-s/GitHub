using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDP.Models
{
    public class CDPEnum
    {
        public enum PlaceType
        {
            Bar,
            Restaurant,
            Club,
            PublicGarden,
            Hotel,
            Monument,
            Sport
        }

        public enum DBOpStatus
        {
            created,
            forbidden,
            internalServerError,
            ok
        }

        public enum UserLevel:int
        {
            normal = 1,
            admin
        }

        public static PlaceType GetTypeFomString(string type)
        {
            type = type.ToUpper();
            switch (type)
            {
                case "BAR":
                    return PlaceType.Bar;
                case "CLUB":
                    return PlaceType.Club;
                case "HOTEL":
                    return PlaceType.Hotel;
                case "MONUMENT":
                    return PlaceType.Monument;
                case "PUBLICGARDEN":
                    return PlaceType.PublicGarden;
                case "RESTAURANT":
                    return PlaceType.Restaurant;
                case "SPORT":
                    return PlaceType.Sport;
                default:
                    return PlaceType.Bar;
            }
        }
    }
}
