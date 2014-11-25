using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ChatApi.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string ConnectedAs { get; set; }
        public static string ApiBaseUri { get; set; }

        public App()
        {
            //ApiBaseUri = "http://localhost:55258";
            ApiBaseUri = "http://localhost/ChatApi";

            
        }
    }
}
