using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.BLL.Specifications.Products;
using Talabat.DAL.Entities;

namespace Talabat.BLL.Specifications.Product_Specification
{
    public class ProductWithFiltersForCountSpecification:BaseSepcification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecParams productParams)
            : base(P =>
            (string.IsNullOrEmpty(productParams.Search) || P.Name.ToLower().Contains(productParams.Search)) &&
            (!productParams.TypedId.HasValue || P.ProductTypeId == productParams.TypedId) &&
            (!productParams.TypedId.HasValue || P.ProductBrandId == productParams.TypedId)
            )
        {
            
        }
    }
}
