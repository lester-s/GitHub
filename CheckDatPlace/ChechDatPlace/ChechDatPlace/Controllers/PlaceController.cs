using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using CDP.Models;

namespace ChechDatPlace.Controllers
{
    public class PlaceController : ApiControllerBase
    {
        #region create
        [ActionName("CreateOnePlace")]
        [HttpPost]
        public HttpResponseMessage CreateOnePlace(Place newPlace)
        {
            HttpResponseMessage response;
            var creds = this.Request.Headers.Where(i => i.Key == "Credentials").FirstOrDefault().Value;
            if (Service.UserServices.AuthorizeUser(creds, CDPEnum.UserLevel.normal))
            {
                var result = Service.PlaceServices.CreateOnePlace(newPlace);

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

        #region read
        [ActionName("GetPlaceByUser")]
        [HttpGet]
        public List<Place> GetPlaceByUser(int userId)
        {
            //TODO return a HttpMessageResponse
            return Service.PlaceServices.GetPlaceByUser(userId);
        }
        #endregion

        #region update
        [ActionName("UpdateOnePlace")]
        [HttpPost]
        public HttpResponseMessage UpdateOnePlace(Place[] placeData)
        {
            HttpResponseMessage response;

            var result = Service.PlaceServices.UpdateOnePlace(placeData);

            response = this.Request.CreateResponse(result.Status);

            response.Content = new StringContent(result.Message);

            return response;
        }
        #endregion

        #region delete
        [ActionName("DeleteOnePlace")]
        [HttpPost]
        public HttpResponseMessage DeleteOnePlace(Place place)
        {
            HttpResponseMessage response;

            var result = Service.PlaceServices.DeleteOnePlace(place);

            response = this.Request.CreateResponse(result.Status);

            response.Content = new StringContent(result.Message);

            return response;
        }
        #endregion

        #region other

        [ActionName("getJson")]
        [HttpGet]
        public string getJson()
        {

            var place = new Place()
            {
                Address = new PlaceAddress(12, "general leclerc", "Paris", 75000),
                Name = "MYUPDATEBAR",
                Rate = 0,
                Type = CDPEnum.PlaceType.Bar,
                UserID = 1
            };

            var places = new Place[] 
            { 
                new Place() 
                {
                Address = new PlaceAddress(12,"general leclerc","Paris", 75000),
                Name = "MyBar",
                Rate = 0,
                Type = CDPEnum.PlaceType.Bar,
                UserID = 1
            },
            new Place()
            {
                Address = new PlaceAddress(12,"general leclerc","Paris", 75000),
                Name = "MyUpdateBar",
                Rate = 0,
                Type = CDPEnum.PlaceType.Bar,
                UserID = 1
            }};
            return Json.Encode(place);

        }
        #endregion
    }
}
