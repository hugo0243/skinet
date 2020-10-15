using Core.Entities;

namespace Core.specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        // brandId or typeId son números que me dicen el tipo de marca o de tipo
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams)
        : base(prod => 
        (string.IsNullOrEmpty(productParams.Search) || prod.Name.ToLower().Contains(productParams.Search)) &&
        (!productParams.BrandId.HasValue || prod.ProductBrandId == productParams.BrandId) && (!productParams.TypeId.HasValue || prod.ProductTypeId == productParams.TypeId)
        )
        {
            AddInclude(product => product.ProductBrand);
            AddInclude(product => product.ProductType);
            AddOrderBy(product => product.Name);
            // Digo que skip según el pageIndex y -1 para que si escojo la primera no skip,
            // y luego cuantos items quiero retornar para mostrar.
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(product => product.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(product => product.Price);
                        break;
                    default:
                        AddOrderBy(product => product.Name);
                        break;
                }
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(prod => prod.Id == id)
        {
            AddInclude(product => product.ProductBrand);
            AddInclude(product => product.ProductType);
        }
    }
}