using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChatApi.Models;
using Newtonsoft.Json;

namespace ChatApi.Client
{
    /// <summary>
    /// Interaction logic for ConnectionPage.xaml
    /// </summary>
    public partial class ConnectionPage : Page
    {
        public ConnectionPage()
        {
            InitializeComponent();
        }
        private void ConnectUserCall(object sender, RoutedEventArgs e)
        {
            var user = new User() { Pseudo = Pseudo.Text.Trim() };
            var userData = JsonConvert.SerializeObject(user);
            HttpWebRequest request = WebRequest.Create(App.ApiBaseUri + "/api/chat/connectuser") as HttpWebRequest;
            request.ContentType = "application/json";
            request.Method = "POST";

            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(userData);
                writer.Close();
            }

            string responseData = string.Empty;
            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ResponseStatut.Text = "connexion réussi";
                        App.ConnectedAs = Pseudo.Text.Trim();
                        var page = new ChatPage();
                        NavigationService.GetNavigationService(this).Navigate(page);
                    }
                    response.Close();
                }
            }
            catch (WebException err)
            {
                if (err.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = err.Response as HttpWebResponse;
                    if (response != null && (int)response.StatusCode == 400)
                    {
                        ResponseStatut.Text = "Erreur lors de la connexion";
                    }
                    else
                    {
                        ResponseStatut.Text = err.Message;
                    }
                }
            }

        }

        private void NavigateToCreateUserPage(object sender, RoutedEventArgs e)
        {
            var page = new CreateUserPage();
            NavigationService.GetNavigationService(this).Navigate(page);
        }

    }
}
