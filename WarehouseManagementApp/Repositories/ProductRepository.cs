using AnimalsManagementApp.Model;
using Microsoft.Data.SqlClient;

namespace AnimalsManagementApp.Repositories;

public class ProductRepository : IProductRepository
{
    
    private IConfiguration _configuration;
    
    public ProductRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    public Product? GetProduct(int idProduct)
    { 
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();
     
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT IdProduct, Name, Description, Price FROM Product WHERE IdProduct = @IdProduct";
        cmd.Parameters.AddWithValue("@IdProduct", idProduct);
        
        var dr = cmd.ExecuteReader();
        
        if (!dr.Read()) return null;
        
        var product = new Product()
        {
            IdProduct = (int)dr["IdProduct"],
            Name = dr["Name"].ToString(),
            Description = dr["Description"].ToString(),
            Price = (int)dr["Price"]
        };
        
        return product;
    }
}