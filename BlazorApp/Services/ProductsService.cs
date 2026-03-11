using BlazorApp.Models;

namespace BlazorApp.Services;

public class ProductsService : IProductsService
{
    private List<Product> _products = new List<Product>
    {
        new Product { 
            Id = 1, Name = "Laptop", 
            Description = "A high-performance laptop.", Price = 999.99m },
        new Product { 
            Id = 2, Name = "Smartphone", 
            Description = "A latest model smartphone.", Price = 499.99m },
        new Product { 
            Id = 3, Name = "Headphones", 
            Description = "Noise-cancelling headphones.", Price = 199.99m }
    };

    public ProductsService()
    {
    }

    public List<Product> GetProducts()
    {
        return _products;
    }

    public void AddProduct(Product product)
    {
        product.Id = _products.Count + 1;
        _products.Add(product);
    }
}
