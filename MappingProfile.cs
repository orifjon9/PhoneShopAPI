
using AutoMapper;
using AutoMapper.Configuration;
using PhoneShopAPI.Models;
using PhoneShopAPI.ViewModels;

namespace PhoneShopAPI
{
    public class MapperProfile : MapperConfigurationExpression
    {
        public MapperProfile()
        {
            CreateMap<Phone, PhoneViewModel>();
            CreateMap<PhoneViewModel, Phone>();
        }
    }
}