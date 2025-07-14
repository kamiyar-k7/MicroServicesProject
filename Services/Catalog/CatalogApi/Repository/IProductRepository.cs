using CatalogApi.Entities;

namespace CatalogApi.Repository;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProducts();
    Task<Product> GetProductById(string Id);

    Task<IEnumerable<Product>> GetProductByName(string Name);

    Task<IEnumerable<Product>> GetProductByCategory(string Category);

    Task CreateProduct(Product product);

    Task<bool> UpdateProduct(Product product);
    Task<bool> DeleteProduct(string Id);
    

}
