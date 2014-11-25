using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CDP.BLL;

namespace ChechDatPlace.Controllers
{
    public class ApiControllerBase : ApiController
    {
        public ServiceFactory Service { get { return ServiceFactory.Instance; } }
    }
}