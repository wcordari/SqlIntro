using System;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using MySql.Data.MySqlClient;

namespace SqlIntro
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Server=localhost;Database=adventureworks;Uid=root;Pwd=huffmanhigh1;";
            //get connectionString format from connectionstrings.com and change to match your database
            var repo = new ProductRepository(new MySqlConnection(connectionString));
            foreach (var prod in repo.GetProducts())
            {
                Console.WriteLine("Product Name:" + prod.Name + "" + prod.ListPrice + prod.ModifiedDate.DayOfWeek);
                
            }
            
            repo.DeleteProduct(1);
            Console.WriteLine("Should have deleted product");

            var product = new Product
            {
                ProductId = 2,
                Name = "Awesome Amazing Product "
            };

            repo.UpdateProduct(product);
            Console.WriteLine("Should have updated the product" + product.ProductId);

            Console.ReadLine();

        }

       
    }
}
