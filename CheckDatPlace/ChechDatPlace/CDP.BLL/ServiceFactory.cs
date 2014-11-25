using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDP.BLL
{
    public class ServiceFactory
    {

        public UserService UserServices { get { return UserService.Instance; } }
        public PlaceService PlaceServices { get { return PlaceService.Instance; } }

        private static ServiceFactory _instance;
        public static ServiceFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ServiceFactory();
                }
                return _instance;
            }
        }

    }
}
