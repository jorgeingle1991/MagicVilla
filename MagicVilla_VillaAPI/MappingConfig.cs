﻿using AutoMapper;
using MagicVilla_VillaAPI.Models.Authentication;
using MagicVilla_VillaAPI.Models.Authentication.Dto;
using MagicVilla_VillaAPI.Models.Villa;
using MagicVilla_VillaAPI.Models.Villa.Dto;
using MagicVilla_VillaAPI.Models.VillaNumber;
using MagicVilla_VillaAPI.Models.VillaNumber.Dto;

namespace MagicVilla_VillaAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<VillaModel, VillaDTO>();
            CreateMap<VillaDTO, VillaModel>();

            CreateMap<VillaModel, VillaCreateDTO>().ReverseMap();
            CreateMap<VillaModel, VillaUpdateDTO>().ReverseMap();

            CreateMap<VillaNumberModel, VillaNumberDTO>().ReverseMap();
            CreateMap<VillaNumberModel, VillaNumberCreateDTO>().ReverseMap();
            CreateMap<VillaNumberModel,VillaNumberUpdateDTO>().ReverseMap();
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();

        }
    }
}
