using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace AdoNet
{
    public class ProductDal
    {
        SqlConnection _conn = new SqlConnection(@"server=(localdb)\mssqllocaldb;initial catalog=ETrade;integrated security=true");

        public List<Product> GetAll()
        {
            ConnControl();

            SqlCommand cmd = new SqlCommand("Select * from  Products", _conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            List<Product> products = new List<Product>();

            while (rdr.Read())
            {
                Product product = new Product
                {
                    Id = Convert.ToInt32(rdr["Id"]),
                    Name = rdr["Name"].ToString(),
                    StockAmount = Convert.ToInt32(rdr["StockAmount"]),
                    UnitPrice = Convert.ToDecimal(rdr["UnitPrice"])

                };
                products.Add(product);

            }
            _conn.Close();
            return products;
        }

        public void Add(Product product)
        {
            ConnControl();
            SqlCommand cmd = new SqlCommand("Insert into Products values(@name,@unitPrice,@stockAmount)", _conn);
            cmd.Parameters.AddWithValue("@name", product.Name);
            cmd.Parameters.AddWithValue("@unitPrice", product.UnitPrice);
            cmd.Parameters.AddWithValue("@stockAmount", product.StockAmount);
            cmd.ExecuteNonQuery();

            _conn.Close();


        }

        public void Update(Product product)
        {
            ConnControl();
            SqlCommand cmd = new SqlCommand("Update Products set Name=@name, UnitPrice=@unitPrice, StockAmount=@stockAmount where Id=@id", _conn);
            cmd.Parameters.AddWithValue("@name", product.Name);
            cmd.Parameters.AddWithValue("@unitPrice", product.UnitPrice);
            cmd.Parameters.AddWithValue("@stockAmount", product.StockAmount);
            cmd.Parameters.AddWithValue("@id", product.Id);
            cmd.ExecuteNonQuery();

            _conn.Close();


        }

        public void Delete(int id)
        {
            ConnControl();
            SqlCommand cmd = new SqlCommand("Delete from Products where Id=@id", _conn);
 
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

            _conn.Close();


        }


        private void ConnControl()
        {
            if (_conn.State == ConnectionState.Closed)
            {
                _conn.Open();

            }
        }
    }
}
