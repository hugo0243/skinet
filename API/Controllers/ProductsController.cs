using System.Collections.Generic;
using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.specifications;
using API.Dtos;
using AutoMapper;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _genericProductRepo;
        private readonly IGenericRepository<ProductBrand> _genericProductBrand;
        private readonly IGenericRepository<ProductType> _genericTypeRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> genericProductRepo,
                                IGenericRepository<ProductBrand> genericBrandRepo,
                                IGenericRepository<ProductType> genericTypeRepo,
                                IMapper mapper)
        {
            _genericProductRepo = genericProductRepo;
            _genericProductBrand = genericBrandRepo;
            _genericTypeRepo = genericTypeRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();

            IReadOnlyList<Product> productsResult = await _genericProductRepo.ListAsync(spec); 

            return Ok(_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(productsResult));
          /*   return productsResult.Select(product => new ProductToReturnDto()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                PictureUrl = product.PictureUrl,
                ProductBrand = product.ProductBrand.Name,
                ProductType = product.ProductType.Name
            }).ToList(); */
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec =  new ProductsWithTypesAndBrandsSpecification(id);
            Product product = await _genericProductRepo.GetEntityWithSpec(spec);

            return _mapper.Map<Product, ProductToReturnDto>(product);
       /*      return new ProductToReturnDto()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                PictureUrl = product.PictureUrl,
                ProductBrand = product.ProductBrand.Name,
                ProductType = product.ProductType.Name
            }; */
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductsBrand()
        {
            var result = await _genericProductBrand.ListAllAsync();

            return Ok(result);
        }

    [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            return Ok(await _genericTypeRepo.ListAllAsync());
        }
    }
}