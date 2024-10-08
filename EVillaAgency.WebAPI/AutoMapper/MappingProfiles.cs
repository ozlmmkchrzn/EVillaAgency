﻿using AutoMapper;
using EVillaAgency.DtoLayer.AppUserDtos;
using EVillaAgency.DtoLayer.FavoriteDtos;
using EVillaAgency.DtoLayer.HeatingTypeDtos;
using EVillaAgency.DtoLayer.HouseDtos;
using EVillaAgency.DtoLayer.HouseImageDtos;
using EVillaAgency.DtoLayer.HouseTypeDtos;
using EVillaAgency.DtoLayer.ImageDtos;
using EVillaAgency.DtoLayer.UserDtos;
using EVillaAgency.EntityLayer.Concrete;

namespace EVillaAgency.WebAPI.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Image,ResultImageDto>().ReverseMap();
            CreateMap<Image,CreateImageDto>().ReverseMap();
            CreateMap<Image,UpdateImageDto>().ReverseMap();

            CreateMap<HouseType, ResultHouseTypeDto>().ReverseMap();
            CreateMap<HouseType, CreateHouseTypeDto>().ReverseMap();
            CreateMap<HouseType, UpdateHouseTypeDto>().ReverseMap();

            CreateMap<AppUser, ResultAppUserDto>().ReverseMap();
            CreateMap<AppUser, CreateAppUserDto>().ReverseMap();
            CreateMap<AppUser, UpdateAppUserDto>().ReverseMap();

            CreateMap<House, ResultHouseDto>().ReverseMap();
            CreateMap<House, ResultHousesWithNames>().ReverseMap();
            CreateMap<House, CreateHouseDto>().ReverseMap();
            CreateMap<House, UpdateHouseDto>().ReverseMap();

            CreateMap<HouseImage, ResultHouseImageDto>().ReverseMap();
            CreateMap<HouseImage, ResultHouseImageWithNamesDto>().ReverseMap();
            CreateMap<HouseImage, CreateHouseImageDto>().ReverseMap();
            CreateMap<HouseImage, UpdateHouseImageDto>().ReverseMap();

            CreateMap<Favorite, ResultFavoriteDto>().ReverseMap();
            CreateMap<Favorite, ResultHouseNameAndUsernameByFavoriteIdDto>().ReverseMap();
            CreateMap<Favorite, ResultAllFavoritesWithNamesDto>().ReverseMap();
            CreateMap<Favorite, ResultTop3FavoritedHousesDto>().ReverseMap();
            CreateMap<Favorite, CreateFavoriteDto>().ReverseMap();
            CreateMap<Favorite, UpdateFavoriteDto>().ReverseMap();


            CreateMap<HeatingType, ResultHeatingTypeDto>().ReverseMap();

        }
    }
}
