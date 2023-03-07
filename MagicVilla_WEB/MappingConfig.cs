using AutoMapper;
using MagicVilla_WEB.Models.Villa.Dto;
using MagicVilla_WEB.Models.VillaNumber.Dto;


namespace MagicVilla_WEB
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<VillaDTO, VillaCreateDTO>().ReverseMap();
            CreateMap<VillaDTO, VillaUpdateDTO>().ReverseMap();

            CreateMap<VillaNumberDTO, VillaNumberCreateDTO>().ReverseMap();
            CreateMap<VillaNumberDTO, VillaNumberUpdateDTO>().ReverseMap();
                    
        }
    }
}
