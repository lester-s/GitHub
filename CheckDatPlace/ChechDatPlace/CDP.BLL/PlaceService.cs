using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CDP.DAL;
using CDP.Models;

namespace CDP.BLL
{
    public class PlaceService
    {
        private static PlaceService _instance;
        public static PlaceService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PlaceService();
                }
                return _instance;
            }
        }

        public DBopMessage CreateOnePlace(Place newPlace)
        {
            newPlace.Address.CityName = newPlace.Address.CityName.ToUpper();
            newPlace.Address.StreetName = newPlace.Address.StreetName.ToLower();
            newPlace.Name = newPlace.Name.ToUpper();
            var opStatus = new PlaceDAL().CreateOnePlace(newPlace);
            return opStatus;
        }

        public List<Place> GetPlaceByUser(int userId)
        {
            return new PlaceDAL().GetPlaceByUser(userId);
        }

        public DBopMessage UpdateOnePlace(Place[] placeData)
        {
            for (int i = 0; i < 2; i++)
            {
                placeData[i].Address.CityName = placeData[i].Address.CityName.ToUpper();
                placeData[i].Address.StreetName = placeData[i].Address.StreetName.ToLower();
                placeData[i].Name = placeData[i].Name.ToUpper();
            }

            var opStatus = new PlaceDAL().UpdateOnePlace(placeData[0], placeData[1]);
            return opStatus;
        }

        public DBopMessage DeleteOnePlace(Place place)
        {
            var opStatus = new PlaceDAL().DeleteOnPlace(place);
            return opStatus;
        }
    }
}
