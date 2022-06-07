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
    public class CategoryService : Service<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(IGenericRepository<Category> genericRepository, IUnitOfWork unıtOfWork, IMapper mapper, ICategoryRepository categoryRepository) : base(genericRepository, unıtOfWork)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<CustomResponseDto<CategoryWithProductsDto>> GetSingleCategoryByIdWithProductAsync(int id)
        {
            var category = await _categoryRepository.GetSingleCategoryByIdWithProductAsync(id);
            var categoryDto= _mapper.Map<CategoryWithProductsDto>(category);
            return CustomResponseDto<CategoryWithProductsDto>.Success(200,categoryDto);
            

        }
    }
}
