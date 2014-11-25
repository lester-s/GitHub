using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApi.Bll
{
    public class ServiceFactory
    {

        public ChatServices ChatServices { get { return ChatServices.Instance; }}

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
