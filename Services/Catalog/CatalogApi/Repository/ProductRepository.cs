using CatalogApi.Data;
using CatalogApi.Entities;
using MongoDB.Driver;
using System.Xml.Linq;

namespace CatalogApi.Repository;

public class ProductRepository : IProductRepository
{

    #region Ctor

    private readonly ICatalogContext _context;
    public ProductRepository(ICatalogContext context)
    {

        _context = context;
    }
    #endregion

    public async Task<Product> GetProductById(string Id)
    {
        return await _context.Products.Find(x => x.Id == Id).FirstOrDefaultAsync();
    }
    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _context.Products.Find(p => true).ToListAsync();
    }


    public async Task<IEnumerable<Product>> GetProductByName(string Name)
    {
        var filterDefinition = Builders<Product>.Filter.Eq(p => p.Name, Name);

        return await _context.Products.Find(filterDefinition).ToListAsync();
        //return  await _context.Products.Find(p=> p.Name == Name).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByCategory(string Category)
    {
        var filterDefinition = Builders<Product>.Filter.Eq(p => p.Category, Category);

        return await _context.Products.Find(filterDefinition).ToListAsync();
    }

    public async Task CreateProduct(Product product)
    {
        await _context.Products.InsertOneAsync(product);
    }

    public async Task<bool> DeleteProduct(string Id)
    {

        DeleteResult res = await _context.Products.DeleteOneAsync(p => p.Id == Id);
        return res.IsAcknowledged && res.DeletedCount > 0;
    }
 
    public async Task<bool> UpdateProduct(Product product)
    {
        var res = await _context.Products.ReplaceOneAsync(filter: p => p.Id == product.Id, replacement: product);
        return res.IsAcknowledged && res.ModifiedCount > 0;

    }
}
