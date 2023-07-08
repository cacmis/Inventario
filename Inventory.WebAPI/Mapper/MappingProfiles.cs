using AutoMapper;
using Inventory.DTOs;
using Inventory.DTOs.Category;
using Inventory.Entities;

namespace Inventory.WebAPI.Mapper
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<CategoryToCreateDto,Category>();
            CreateMap<CategoryToEditDto,Category>();
            CreateMap<Category, CategoryToListDto>();
        }

        
    }
}