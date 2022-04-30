using AutoMapper;
using MenuItems.API.Models;

namespace MenuItems.API.Profiles
{
    public class MenuItemProfile:Profile
    {
        public MenuItemProfile()
        {
            CreateMap<MenuItem,MenuItemForCreation>().ReverseMap();
            CreateMap<MenuItem,MenuItemForUpdate>().ReverseMap();
        }
    }
}
