using System;
using System.Linq;

namespace CustomORMExample
{
    class Program
    {
        static void Main()
        {
            string connectionString = "DefaultConnection";
            var dbContext = new DbContext(connectionString);

            // Create a repository for the Product entity
            var productRepository = new Repository<Product>(dbContext);

            // Adding a new product
            var newProduct = new Product { Name = "Laptop", Price = 1200.50m };
            productRepository.Add(newProduct);

            // Get all products
            var products = productRepository.GetAll();
            foreach (var product in products)
            {
                Console.WriteLine($"{product.ProductId} - {product.Name} - {product.Price}");
            }

            // Update a product
            var productToUpdate = products.First();
            productToUpdate.Price = 1500;
            productRepository.Update(productToUpdate);

            // Delete a product
            productRepository.Delete(productToUpdate.ProductId);
        }
    }
}
