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
            using (var conn = new MySqlConnection(connectionString)) ;
            {

                var repo = new DapperDB(conn);
                var products = repo.GetProducts();
                foreach (var prod in repo.GetProducts())
                {
                    Console.WriteLine("Product Name:" + prod.Name + "" + prod.ListPrice + prod.ModifiedDate.DayOfWeek);

                }

                repo.DeleteProduct(1);


                Console.WriteLine("Should have deleted product");

                var product = new Product
                {
                    ProductId = 2,
                    Name = "Awesome Amazing Product"
                };

                repo.UpdateProduct(product);
                Console.WriteLine("Should have updated the product" + product.ProductId);


                {
                    repo.InsertProduct(product);
                    Console.WriteLine("Should INSERT into Product " + product.ProductId);

                    //ProductId = 5,
                    //Name = "Bike",
                    //ProductNumber = "BR=1004",
                    //Color = "Blue",
                    //SafetyStockLevel = 1000,
                    //ReorderPoint = 500,
                    //StandardCost = 500,
                    //ListPrice = 1025.36,


                }





            }


        }

    }
}
    
