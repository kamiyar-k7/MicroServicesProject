using CatalogApi.Entities;
using CatalogApi.Repository;

using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CatalogApi.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CatalogController : ControllerBase
{

    #region Ctor

    private readonly IProductRepository _productRepository;
    private readonly ILogger<CatalogController> _logger;

    public CatalogController(IProductRepository productRepository, ILogger<CatalogController> logger)
    {
        _productRepository = productRepository;
        _logger = logger;

    }
    #endregion

    #region Get Products
    [HttpGet("[action]")]
    [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var products = await _productRepository.GetProducts();
        return Ok(products);

    }
    #endregion

    #region Get Product by id
    [HttpGet("[action]/{id:length(24)}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Product>> GetProductById(string id)
    {
        var products = await _productRepository.GetProductById(id);
        if (products == null)
        {
            _logger.LogError($"product by id {id} not found");
            return NotFound();
        }

        return Ok(products);

    }
    #endregion

    #region Get Product By category

    [HttpGet("[action]/{category}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
    {
        var products = await _productRepository.GetProductByCategory(category);


        return Ok(products);

    }

    #endregion

    #region Create Product

    [HttpPost("[action]")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateProduct(Product product)
    {
        await _productRepository.CreateProduct(product);
        return  Created();
    }

    #endregion

    #region Update Product

    [HttpPut("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateProduct(Product product)
    {
        await _productRepository.UpdateProduct(product);
        return Ok();
    }

    #endregion

    #region Delete Product

    [HttpDelete("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        await _productRepository.DeleteProduct(id);
        return Ok();
    }

    #endregion
}
