using Core.Entities;

namespace Core.specifications
{
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        // No importan las demas expresions ya que acá se aplica el mismo filtro que enl spec
        // principal solo interesa contar los elementos que retorna el filtro, no la paginación etc.
        public ProductWithFiltersForCountSpecification(ProductSpecParams productParams)
            : base(prod => 
                    (string.IsNullOrEmpty(productParams.Search) || prod.Name.ToLower().Contains(productParams.Search)) &&
                    (!productParams.BrandId.HasValue || prod.ProductBrandId == productParams.BrandId) && (!productParams.TypeId.HasValue || prod.ProductTypeId == productParams.TypeId)
                )
        {

        }
    }
}