using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SqlIntro
{
    public class ProductRepository : IDisposable
    {
        private readonly MySqlConnection _conn;

        public ProductRepository(MySqlConnection conn)
        {
            _conn = conn;
            _conn.Open();
        }
        /// <summary>
        /// Reads all the products from the products table
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> GetProducts()
        {

            var cmd = _conn.CreateCommand();
            cmd.CommandText = "select * from Product";
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                yield return new Product
                {
                    Name = dr["Name"].ToString(),
                    ListPrice = (double)dr["ListPrice"]
                };
            }
        }

        /// <summary>
        /// Deletes a Product from the database
        /// </summary>
        /// <param name="id"></param>
        public void DeleteProduct(int id)
        {
            var cmd = _conn.CreateCommand();
            cmd.CommandText = "Delete from product where productid = 1 "; //Write a delete statement that deletes by id
            cmd.ExecuteNonQuery();

        }
        /// <summary>
        /// Updates the Product in the database
        /// </summary>
        /// <param name="prod"></param>
        public void UpdateProduct(Product prod)
        {
            //This is annoying and unnecessarily tedious for large objects.
            //More on this in the future...  Nothing to do here..
            var cmd = _conn.CreateCommand();
            cmd.CommandText = "update product set color = @name where id = 1";
            cmd.Parameters.AddWithValue("@name", prod.Name);
            cmd.Parameters.AddWithValue("@id", prod.ProductId);
            cmd.ExecuteNonQuery();
        }
        /// <summary>
        /// Inserts a new Product into the database
        /// </summary>
        /// <param name="prod"></param>
        public void InsertProduct(Product prod)
        {
            var cmd = _conn.CreateCommand();
            cmd.CommandText = "Insert product set name = @name where id =@id";
            var param = cmd.CreateParameter();
            param.ParameterName = "name";
            param.Value = prod.Name;
            cmd.Parameters.AddWithValue("@name", prod.Name);
            cmd.ExecuteNonQuery();
        }

        public void Dispose()
        {
            _conn.Dispose();
        }
    }
}
