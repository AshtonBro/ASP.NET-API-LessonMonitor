using System;
using System.Linq;
using System.Threading.Tasks;
using EFCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore
{
    internal class Program
    {
        private const string connectionString = @"Data Source=ASHTON\ASHTON;Initial Catalog=LessonSql;Integrated Security=True;";
        private static async Task Main(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MyDBContext>();

            builder.UseSqlServer(connectionString);

            var contextFactoty = new MyContextFactory();

            await using (var context = contextFactoty.CreateDbContext(args))
            {

                {//Create new product
                    //for (int i = 0; i < 6; i++)
                    //{
                    //    var productName = Console.ReadLine();

                    //    var product = new Product
                    //    {
                    //        Name = productName,
                    //        CategotyId = 1
                    //    };

                    //    var result = await context.Product.AddAsync(product);
                    //}

                    //await context.SaveChangesAsync();
                }

                {// Get list of products

                    //var products = await context.Product.ToArrayAsync();

                    //foreach (var product in products)
                    //{
                    //    Console.WriteLine($"{product.Id} {product.Name}");
                    //}
                }

                {// Update product by Id

                    //var productsUpdate = await context.Product.FirstOrDefaultAsync(x => x.Id == 1);

                    //productsUpdate.Name = "Meat";

                    //await context.SaveChangesAsync();

                    // Update product by Id

                    //var product = new Product
                    //{
                    //    Id = 2,
                    //    Name = "Tea"
                    //};

                    //if (product.Id <= default(int))
                    //{
                    //    throw new InvalidOperationException("Don't have Id");
                    //}

                    //context.Product.Update(product);

                    //await context.SaveChangesAsync();

                    //context.Attach(product).State = EntityState.Modified;
                    //await context.SaveChangesAsync();

                    //var product = await GetProduct(context, 2);
                    //product.Name = "Milk";

                    //var productUpdate = new Product
                    //{
                    //    Id = 4,
                    //    Name = "Water"
                    //};
                    //await UpdateProduct(context, productUpdate);
                    //await context.SaveChangesAsync();
                }

                {// where
                    //var productsQuery = context.Product.Where(x => x.Id > 2).AsEnumerable();

                    //var product = productsQuery.FirstOrDefault();
                }

                {// where
                    //var productsQuery = context.Product.Where(x => x.Id > 2);

                    //var product = productsQuery.Where(x => x.Name == "Cheese").FirstOrDefault();
                }

                {// where
                    //var productsQuery = context.Product.Where(x => x.Id > 2);

                    //var product = await productsQuery.Where(x => x.Id > 14)
                    //    .FirstOrDefaultAsync();
                }

                {
                    //context.Categories.Remove(new Category { Id = 1 });
                    //await context.SaveChangesAsync();
                }

                {
                    //var products = await context
                    //    .Product
                    //    .AsNoTracking()
                    //    .Include(x => x.Category)
                    //    .ToArrayAsync();

                    //var products = await context
                    //    .Product
                    //    .AsNoTracking()
                    //    .Join(context.Categories,
                    //    product => product.CategotyId,
                    //    category => category.Id,
                    //    (product, category) => new Product
                    //    {
                    //        Id = product.Id,
                    //        Name = product.Name,
                    //        CategotyId = product.CategotyId,
                    //        Category = new Category
                    //        {
                    //            Id = category.Id,
                    //            Name = category.Name
                    //        }
                    //    })
                    //    .ToArrayAsync();

                    //var products = await context.Product
                    //    .Where(x => x.Category.Name == "Food")
                    //    .ToArrayAsync();
                }

            }
        }

        private static async Task<Product> GetProduct(MyDBContext context, int productId)
        {
            var product = await context.Products
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == productId);

            return product;
        }

        private static async Task UpdateProduct(MyDBContext context, Product product)
        {
            context.Products.Update(product);

            await context.SaveChangesAsync();
        }

        private static async Task RemoveProduct(MyDBContext context, int productId)
        {
            //context.Product.Remove(new Product() { Id = productId });

            context.Attach(new Product() { Id = productId }).State = EntityState.Deleted;

            await context.SaveChangesAsync();
        }
    }
}
