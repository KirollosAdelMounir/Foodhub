using AutoMapper;
using Eatable.Areas.Identity.Data;
using Eatable.ViewModels;

namespace Eatable
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
