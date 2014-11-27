using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CDP.Models;

namespace CDP.DAL
{
    public class PlaceDAL
    {
        private CDP_DBContext db;

        public PlaceDAL()
        {
            db = CDP_DBContext.Instance;
        }
        #region create
        public DBopMessage CreateOnePlace(Place newPlace)
        {
            try
            {
                var places = db.Places.Where(p => p.Name == newPlace.Name);
                var test = places.ToList();
                if (places.Count() == 0)
                {
                    return AddPlaceToDB(newPlace);
                }
                else
                {
                    foreach (var place in places)
                    {
                        if (place.Address.CityName == newPlace.Address.CityName &&
                            place.Address.CodePostal == newPlace.Address.CodePostal &&
                            place.Address.Number == newPlace.Address.Number &&
                            place.Address.StreetName == newPlace.Address.StreetName)
                        {
                            return new DBopMessage(HttpStatusCode.Forbidden, "This place already exist");
                        }
                    }

                    return AddPlaceToDB(newPlace);
                }
            }
            catch (Exception)
            {
                return new DBopMessage(HttpStatusCode.InternalServerError, "Something went wrong on Place creation");
            }
        }

        private DBopMessage AddPlaceToDB(Place newPlace)
        {
            db.Places.Add(newPlace);
            if (db.SaveChanges() == 1)
            {
                return new DBopMessage(HttpStatusCode.Created, "new place created");
            }
            else
            {
                return new DBopMessage(HttpStatusCode.InternalServerError, "Something went wrong on place creation");
            }
        }
        #endregion

        public DBopMessage<List<Place>> GetPlaceByUser(int userId)
        {
            try
            {
                var places = db.Places.Where(p => p.UserID == userId).ToList();

                if (places != null)
                {
                    return new DBopMessage<List<Place>>(HttpStatusCode.OK, "request to get places by user succeed !!!", places);
                }
                else
                {
                    return new DBopMessage<List<Place>>(HttpStatusCode.InternalServerError, "No places to get", null);
                }
            }
            catch (Exception)
            {
                return new DBopMessage<List<Place>>(HttpStatusCode.InternalServerError, "Something went wrong on getting places", null);
            }
        }

        public DBopMessage UpdateOnePlace(Place beforeData, Place afterData)
        {
            try
            {
                var place = db.Places.Find(beforeData.PlaceID);

                if (place != null)
                {
                    place.Address = afterData.Address;
                    place.AvgPrice = afterData.AvgPrice;
                    place.Name = afterData.Name;
                    place.Rate = afterData.Rate;
                    place.Type = afterData.Type;

                    if (db.SaveChanges() == 1)
                    {
                        return new DBopMessage(HttpStatusCode.OK, "Update place succeed !!!");
                    }
                    else
                    {
                        return new DBopMessage(HttpStatusCode.InternalServerError, "Something went wrong on updating the place");
                    }
                }
                else
                {
                    return new DBopMessage(HttpStatusCode.Forbidden, "place not found. Cannot update");
                }
            }
            catch (Exception)
            {
                return new DBopMessage(HttpStatusCode.InternalServerError, "Something went wrong on updating the place");
            }
        }

        public DBopMessage DeleteOnPlace(Place place)
        {
            try
            {
                var delPlace = db.Places.Find(place.PlaceID);

                if (delPlace != null)
                {
                    db.Places.Remove(delPlace);

                    if (db.SaveChanges() == 1)
                    {
                        return new DBopMessage(HttpStatusCode.OK, "Delete place succeed !!!");
                    }
                    else
                    {
                        return new DBopMessage(HttpStatusCode.InternalServerError, "Something went wrong on deleting the place");
                    }
                }
                else
                {
                    return new DBopMessage(HttpStatusCode.Forbidden, "place not found. Cannot delete");
                }
            }
            catch (Exception)
            {
                return new DBopMessage(HttpStatusCode.InternalServerError, "Something went wrong on deleting the place");
            }
        }

        public DBopMessage<List<Place>> SearchOnePlace(Place place)
        {
            try
            {
                var places = db.Places.Where(p => (p.Name == place.Name) 
                    && (p.Type == place.Type) 
                    && (p.Address.CityName == place.Address.CityName)
                    && (p.Address.StreetName == place.Address.StreetName)).ToList();

                if (places != null)
                {
                    return new DBopMessage<List<Place>>(HttpStatusCode.OK, "request to search one place succeed !!!", places);
                }
                else
                {
                    return new DBopMessage<List<Place>>(HttpStatusCode.InternalServerError, "No places found", null);
                }
            }
            catch (Exception)
            {
                return new DBopMessage<List<Place>>(HttpStatusCode.InternalServerError, "Something went wrong on finding places", null);
            }
        }
    }
}
