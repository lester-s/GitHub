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
    /// Interaction logic for CreateUserPage.xaml
    /// </summary>
    public partial class CreateUserPage : Page
    {
        public CreateUserPage()
        {
            InitializeComponent();
        }

        private void CreateUserCall(object sender, RoutedEventArgs e)
        {
            if (Pseudo.Text != string.Empty && Name.Text != string.Empty && FirstName.Text != string.Empty)
            {
                var user = new User() { Pseudo = Pseudo.Text, Name = Name.Text, FirstName = FirstName.Text };
                var userData = JsonConvert.SerializeObject(user);
                HttpWebRequest request = WebRequest.Create(App.ApiBaseUri + "/api/chat/createuser") as HttpWebRequest;
                request.ContentType = "application/json";
                request.Method = "POST";
                request.Credentials = CredentialCache.DefaultCredentials;
                try
                {
                    using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                    {
                        writer.Write(userData);
                        writer.Close();
                    }

                    string responseData = string.Empty;

                    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            ResponseStatut.Text = "Création du compte réussie";
                            var page = new ConnectionPage();
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
                            ResponseStatut.Text = "Erreur lors de la création du profil";
                        }
                        else
                        {
                            ResponseStatut.Text = err.Message;
                        }
                    }
                    
                    Name.Clear();
                    FirstName.Clear();
                    Pseudo.Clear();
                }
            }
            else
            {
                ResponseStatut.Text = "Veuillez remplir tout les champs";
            }
        }
    }
}
