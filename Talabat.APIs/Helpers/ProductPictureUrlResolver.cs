using AutoMapper;
using AutoMapper.Execution;
using Microsoft.Extensions.Configuration;
using Talabat.APIs.DTOs;
using Talabat.DAL.Entities;

namespace Talabat.APIs.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDTO, string>
    {
        private readonly IConfiguration Configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{Configuration["ApiUrl"]}{source.PictureUrl}";
            return null;
        }
    }
}
