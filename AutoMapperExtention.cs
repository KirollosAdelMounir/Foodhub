using AutoMapper;
using FoodHub.Areas.Identity.Data;
using FoodHub.ViewModels;

namespace FoodHub
{
    public static class AutoMapperExtention
    {
        public class AutoMapperProfile : Profile
        {
            public AutoMapperProfile()
            {
                CreateMap<UserTable, ApplicationUser>();
                CreateMap<ApplicationUser, UserTable>();

            }
        }
    }
}
