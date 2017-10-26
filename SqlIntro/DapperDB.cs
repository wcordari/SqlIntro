using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;

namespace SqlIntro
{
    class DapperDB
    {
        private readonly string_connectionString;

        public DapperDB(string connectionString)
        {
            _connectionString = connectionString;

        }

        public IEnumerable<Product> GetProducts()
        {
            string _connectionString = null;
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                return conn.Query<Product>("Select * From product");
            }

        }

        public void DeleteProduct(int productid)
        {
            string _connectionString = null;
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                conn.Execute("DELETE FROM Product WHERE ProductId = @id", new {id = productid});
            }
        }

        public void UpdateProduct(Product prod)
        {
            string _connectionString = null;
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();

                conn.Execute("UPDATE Product SET Name = @name WHERE Productid = @id",
                    new {id = prod.ProductId, name = prod.Name});
            }
        }

        public void InsertProduct(Product prod)
        {
            string _connectionString = null;
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();

                var cText = "INSERT into product(" + string.Join(", ", prod.Params.Keys) + ")";

                var valueKeys = prod.Params.Keys.Select(str => "@" + str);
                cText += "values (" + String.Join(", ", valueKeys) + ")";

                cmd.CommandText = cText;

                foreach (var keyValue in prod.Params)
                {
                    cmd.Parameters.AddWithValue("@" + keyValue.Key, keyValue.Value);

                    conn.Execute("INSERT into product (Name) values (@name)", 
                        new {id = prod.ProductId, name = prod.Name});

                }

            }
        }
    }
}
