using AutoMapper;
using Core;
using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Product,ProductDto>().ReverseMap();
            CreateMap<Category,CategoryDto>().ReverseMap();
            CreateMap<ProductFeature, ProductFeature>().ReverseMap();
            CreateMap<ProductUpdateDto, Product>();
            CreateMap<Product,ProductWithCategoryDto>();
            CreateMap<Category,CategoryWithProductsDto>();
            CreateMap<User, UserDto>().ReverseMap();

        }
    }
}
