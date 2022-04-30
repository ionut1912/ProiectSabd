using AutoMapper;
using Cities.API.Models;

namespace Cities.API.Profiles
{
    public class CityProfile:Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityForCreation>().ReverseMap();
            CreateMap<City,CityForUpdate>().ReverseMap();
        }
    }
}
