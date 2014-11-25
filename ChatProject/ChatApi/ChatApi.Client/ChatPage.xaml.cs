using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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
    /// Interaction logic for ChatPage.xaml
    /// </summary>
    public partial class ChatPage : Page
    {

        private List<string> users;

        public List<string> Users
        {
            get { return users; }
            set { users = value; }
        }

        private string _receiver { get; set; }

        public ChatPage()
        {
            InitializeComponent();
            CheckClient();
        }

        private async void CheckClient()
        {
            GetConnectedUsers();
            GetNewMessage();
            await Task.Delay(2000);
            CheckClient();
        }


        private void GetNewMessage()
        {
            HttpWebRequest request = WebRequest.Create(App.ApiBaseUri + "/api/chat/getnewmessages?pseudo=" + App.ConnectedAs.Trim()) as HttpWebRequest;
            request.ContentType = "application/json";
            request.Method = "GET";
            request.ContentLength = 0;

            string responseData = string.Empty;
            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var data = reader.ReadToEnd();
                            var newMessages = JsonConvert.DeserializeObject<List<Message>>(data);

                            if (newMessages != null)
                            {
                                foreach (var message in newMessages)
                                {
                                    ChatContainer.Text += "\r\n" + message.Emitter + " :" + message.Content;
                                }

                            }
                        }
                        response.Close();
                    }
                }
            }
            catch (WebException err)
            {
                if (err.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = err.Response as HttpWebResponse;
                    if (response != null && (int)response.StatusCode == 400)
                    {
                        ChatContainer.Text += "\r\n Can't get new messages";
                    }
                    else
                    {
                        ChatContainer.Text += "\r\n Error:" + err.Message;
                    }
                }
            }
        }

        private void GetConnectedUsers()
        {
            HttpWebRequest request = WebRequest.Create(App.ApiBaseUri + "/api/chat/getconnectedusers") as HttpWebRequest;
            request.ContentType = "application/json";
            request.Method = "GET";
            request.ContentLength = 0;

            string responseData = string.Empty;
            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        var data = reader.ReadToEnd();
                        users = JsonConvert.DeserializeObject<List<string>>(data);
                        users.Remove(App.ConnectedAs);
                        UsersList.ItemsSource = Users;
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            // ResponseStatut.Text = "connexion réussi";
                        }
                        response.Close();
                    }
                }
            }
            catch (WebException err)
            {
                if (err.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = err.Response as HttpWebResponse;
                    if (response != null && (int)response.StatusCode == 400)
                    {
                        //ResponseStatut.Text = "Erreur lors de la connexion";
                    }
                    else
                    {
                        //ResponseStatut.Text = err.Message;
                    }
                }
            }
        }

        protected void HandleDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _receiver = ((ListViewItem)sender).Content as string;
        }

        private void SendMessage(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(TypingBox.Text))
                return;
            var newMessage = new Message() { Content = TypingBox.Text.Trim(), Emitter = App.ConnectedAs, Receiver = _receiver };
            var data = JsonConvert.SerializeObject(newMessage);
            HttpWebRequest request = WebRequest.Create(App.ApiBaseUri + "/api/chat/sendmessage") as HttpWebRequest;
            request.ContentType = "application/json";
            request.Method = "POST";
            request.Credentials = CredentialCache.DefaultCredentials;
            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(data);
                writer.Close();
            }

            string responseData = string.Empty;
            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        
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
                        ChatContainer.Text += "\r\n Error: Message not send to " + _receiver;
                    }
                    else
                    {
                        ChatContainer.Text += "\r\n Error: Message not send to " + _receiver + "\r\n" + err.Message;
                    }
                }
            }
            ChatContainer.Text += "\r\n me to " + _receiver + " :" + TypingBox.Text.Trim();
            TypingBox.Clear();
        }
    }
}
