using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using CDP.DAL;
using CDP.Models;
using HashMe;

namespace ChechDatPlace.Controllers
{
    public class UserController : ApiControllerBase
    {

        #region create
        [ActionName("CreateOneUser")]
        [HttpPost]
        public HttpResponseMessage CreateOneUser(User newUser)
        {
            HttpResponseMessage response;
            var result = Service.UserServices.CreateOneUser(newUser);

            response = this.Request.CreateResponse(result.Status);

            response.Content = new StringContent(result.Message);

            return response;
        }
        #endregion

        #region read
        [ActionName("GetAllUsers")]
        [HttpGet]
        public HttpResponseMessage GetAllUsers()
        {
            HttpResponseMessage response;
            var creds = this.Request.Headers.Where(i => i.Key == "Credentials").FirstOrDefault().Value;
            if (Service.UserServices.AuthorizeUser(creds, CDPEnum.UserLevel.normal))
            {
                var users = Service.UserServices.GetAllUsers();
                response = this.Request.CreateResponse<IEnumerable<User>>(HttpStatusCode.OK, users.Value);
            }
            else
            {
                response = this.Request.CreateResponse(HttpStatusCode.Forbidden);
                response.Content = new StringContent("You are not allowed to perform this action.");
            }

            return response;
        }
        #endregion

        #region update
        [ActionName("UpdateOneUser")]
        [HttpPost]
        public HttpResponseMessage UpdateOneUser(User[] UserData)
        {

            HttpResponseMessage response;
            var creds = this.Request.Headers.Where(i => i.Key == "Credentials").FirstOrDefault().Value;
            if (Service.UserServices.AuthorizeUser(creds, CDPEnum.UserLevel.normal))
            {
                var result = Service.UserServices.UpdateOneUser(UserData);

                response = this.Request.CreateResponse(result.Status);

                response.Content = new StringContent(result.Message);
            }
            else
            {
                response = this.Request.CreateResponse(HttpStatusCode.Forbidden);
                response.Content = new StringContent("You are not allowed to perform this action.");
            }
            return response;
        }
        #endregion

        #region Delete
        [ActionName("DeleteAllUsers")]
        [HttpGet]
        public HttpResponseMessage DeleteAllUsers()
        {
            HttpResponseMessage response;
            var creds = this.Request.Headers.Where(i => i.Key == "Credentials").FirstOrDefault().Value;
            if (Service.UserServices.AuthorizeUser(creds, CDPEnum.UserLevel.normal))
            {
                var result = Service.UserServices.DeleteAllUsers();

                response = this.Request.CreateResponse(result.Status);

                response.Content = new StringContent(result.Message);
            }
            else
            {
                response = this.Request.CreateResponse(HttpStatusCode.Forbidden);
                response.Content = new StringContent("You are not allowed to perform this action.");
            }
            return response;
        }
        #endregion

        #region other
        [ActionName("ConnectUser")]
        [HttpPost]
        public HttpResponseMessage ConnectUser(string[] credentials)
        {
            HttpResponseMessage response;
            var creds = this.Request.Headers.Where(i => i.Key == "Credentials").FirstOrDefault().Value;
            if (Service.UserServices.AuthorizeUser(creds, CDPEnum.UserLevel.normal))
            {
                var result = Service.UserServices.ConnectUser(credentials);

                response = this.Request.CreateResponse(result.Status);

                response.Content = new StringContent(result.Message);
            }
            else
            {
                response = this.Request.CreateResponse(HttpStatusCode.Forbidden);
                response.Content = new StringContent("You are not allowed to perform this action.");
            }
            return response;
        }


        [ActionName("getJson")]
        [HttpGet]
        public string getJson()
        {
            var user = new User() { FirstName = "pascal", LastName = "Le Ster", Login = "plester", Password = "hello", BirthDate = DateTime.Now };
            var users = new User[] 
            { 
                new User() { FirstName = "simon", LastName = "Le Ster", Login = "slester", Password = "hello", BirthDate = DateTime.Now },
                new User() { FirstName = "morgane", LastName = "Le Ster", Login = "slester", Password = "hello", BirthDate = DateTime.Now }
            };
            var cred = new string[] { "9121daf07958b736e781efe16a11eb3c", "5d41402abc4b2a76b9719d911017c592" };
            return Json.Encode(user);

        }
        #endregion
    }
}
