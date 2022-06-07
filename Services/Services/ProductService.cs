using AutoMapper;
using Core;
using Core.DTOs;
using Core.Repositories;
using Core.Services;
using Core.UnıtOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IGenericRepository<Product> genericRepository, IUnitOfWork unıtOfWork, IProductRepository productRepository, IMapper mapper) : base(genericRepository, unıtOfWork)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
        {
            var products= await _productRepository.GetProductsWithCategory();
            var productsDto=_mapper.Map<List<ProductWithCategoryDto>>(products);
            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200,productsDto);
        }
    }
}
