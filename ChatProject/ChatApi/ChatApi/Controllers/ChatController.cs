using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using ChatApi.Models;

namespace ChatApi.Controllers
{
    public class ChatController: ApiControllerBase
    {

        [ActionName("SendMessage")]
        [HttpPost]
        public void SendMessage(Message message)
        {
            Service.ChatServices.SendMessage(message);
        }

        [ActionName("CreateUser")]
        [HttpPost]
        public HttpResponseMessage CreateUser(User user)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            var result = Service.ChatServices.CreateUser(user);
            if (result)
            {
                return response;
            }
            response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            return response;
        }

        [ActionName("ConnectUser")]
        [HttpPost]
        public HttpResponseMessage ConnectUser(User user)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK); //Request.CreateResponse<User>(HttpStatusCode.OK, user);
            var result = Service.ChatServices.ConnectUser(user.Pseudo);
            if(result)
            {
                return response;
            }
            response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            return response;
        }

        [ActionName("GetNewMessages")]
        [HttpGet]
        public List<Message> GetNewMessages(string pseudo)
        {
            var messages = new List<Message>();
            messages = Service.ChatServices.GetNewMessages(pseudo);
            return messages;
        }

        [ActionName("GetConnectedUsers")]
        [HttpGet]
        public List<string> GetConnectedUsers()
        {
            var users = Service.ChatServices.GetConnectedUsers();
            return users;
        }


        [ActionName("testjson")]
        [HttpGet]
        public List<Message> testjson()
        {
            var users = new List<User>() { new User() { Pseudo = "p1" }, new User() { Pseudo = "p2" } };
            var user = new User() { FirstName = "simon", Name = "le_ster", Pseudo = "moha" };
            var message = new Message() { Content = "Message content", Emitter = "moha", Receiver = "moha2" };
            var js = Json.Encode(message);
            var messages = new List<Message>() { message, message, message };
            return messages;
            //[{"Id":0,"Name":null,"FirstName":null,"Pseudo":"p1","Chats":null},{"Id":0,"Name":null,"FirstName":null,"Pseudo":"p2","Chats":null}]
            //{"Id":0,"Name":"le_ster","FirstName":"simon","Pseudo":"moha","Chats":null}
            //{"Id":0,"Receiver":"moha2","Emitter":"moha","Content":"Message content"}
        }
    }
}