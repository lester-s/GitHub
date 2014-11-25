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
    }
}
