using BlazorApp.Models;

namespace BlazorApp.Services;

public interface IProductsService
{
    void AddProduct(Product product);
    List<Product> GetProducts();
}
