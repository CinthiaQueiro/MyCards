using AutoMapper;
using CoreApiClient.Entities;
using UI.ViewModel;

namespace UI.Mapper
{
    public class ViewModelProfile : Profile
    {
        public ViewModelProfile()
        {
            CreateMap<UserViewModel, User>().ReverseMap();
        }
    }
}
