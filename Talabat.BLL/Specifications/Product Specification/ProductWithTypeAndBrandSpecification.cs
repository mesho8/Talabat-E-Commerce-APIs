using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.DAL.Entities;

namespace Talabat.BLL.Specifications.Products
{
    public class ProductWithTypeAndBrandSpecification : BaseSepcification<Product>
    {
        // This Constructor Will Be Used To Get All Products
        public ProductWithTypeAndBrandSpecification(ProductSpecParams productParams)
            : base(P =>
            (string.IsNullOrEmpty(productParams.Search) || P.Name.ToLower().Contains(productParams.Search)) &&
            (!productParams.TypedId.HasValue || P.ProductTypeId == productParams.TypedId) &&
            (!productParams.TypedId.HasValue || P.ProductBrandId == productParams.TypedId)
            )
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
            AddOrderBy(P => P.Name);
            ApplyPagination(productParams.PageSize * (productParams.PageIndex - 1) , productParams.PageSize);
            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }
        }
        // This Constructor Will Be Used To Get specific Product with id
        public ProductWithTypeAndBrandSpecification(int id) : base(P => P.Id == id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
    }
}
