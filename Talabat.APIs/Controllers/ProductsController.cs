using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.BLL.Interfaces;
using Talabat.BLL.Specifications;
using Talabat.BLL.Specifications.Product_Specification;
using Talabat.BLL.Specifications.Products;
using Talabat.DAL.Entities;

namespace Talabat.APIs.Controllers
{
    
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _prodBrandRepo;
        private readonly IGenericRepository<ProductType> _prodTypeRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepository, IGenericRepository<ProductBrand> prodBrandRepo, IGenericRepository<ProductType> prodTypeRepo , IMapper mapper)
        {
            _productRepository = productRepository;
            _prodBrandRepo = prodBrandRepo;
            _prodTypeRepo = prodTypeRepo;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDTO>>> GetProducts([FromQuery]ProductSpecParams productParams)
        {
            //var products = await _productRepository.GetAllAsync();

            var spec = new ProductWithTypeAndBrandSpecification(productParams);
            var products = await _productRepository.GetAllWithSpecAsync(spec);
            var mappedProd = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>(products);
            var countSpec = new ProductWithFiltersForCountSpecification(productParams);
            var Count = await _productRepository.GetCountAsync(countSpec);
            if (mappedProd == null)
                return NotFound(new ApiResponse(404));
            return Ok(new Pagination<ProductToReturnDTO>(productParams.PageIndex, productParams.PageSize,Count,mappedProd));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDTO>>> GetProduct(int id)
        {
            //var products = await _productRepository.GetAllAsync();

            var spec = new ProductWithTypeAndBrandSpecification(id);
            var product = await _productRepository.GetByIdWithSpecAsync(spec);
            if(product == null)
                return NotFound(new ApiResponse(400));
            var mappedProd = _mapper.Map<Product,ProductToReturnDTO>(product);
            if(mappedProd == null)
                return NotFound(new ApiResponse(404));
            return Ok(mappedProd);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _prodBrandRepo.GetAllAsync();
            if (brands == null)
                return NotFound(new ApiResponse(404));
            return Ok(brands);
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var types = await _prodTypeRepo.GetAllAsync();
            if (types == null)
                return NotFound(new ApiResponse(404));
            return Ok(types);
        }
    }
}
