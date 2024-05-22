using AnimalsManagementApp.Model;
using Microsoft.Data.SqlClient;


namespace AnimalsManagementApp.Repositories;

public class WarehouseProductRepository : IWarehouseProductRepository
{
    private IConfiguration _configuration;

    public WarehouseProductRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public int AddProductToWarehouse(ProductWarehouse productWarehouse)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();

        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText =
            "INSERT INTO Product_Warehouse (IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt) VALUES (@IdWarehouse, @IdProduct, @IdOrder, @Amount, @Price, @CreatedAt);";
        cmd.Parameters.AddWithValue("@IdWarehouse", productWarehouse.IdWarehouse);
        cmd.Parameters.AddWithValue("@IdProduct", productWarehouse.IdProduct);
        cmd.Parameters.AddWithValue("@IdOrder", productWarehouse.IdOrder);
        cmd.Parameters.AddWithValue("@Amount", productWarehouse.Amount);
        cmd.Parameters.AddWithValue("@Price", productWarehouse.Price);
        cmd.Parameters.AddWithValue("@CreatedAt", productWarehouse.CreatedAt);
        
        
        var affectedCount = cmd.ExecuteNonQuery();
        return affectedCount;
    }

    public ProductWarehouse? GetWarehouse(int idOrder)
    {
        {
            using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
            con.Open();

            using var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText =
                "SELECT IdProductWarehouse, IdWarehouse, IdProduct,IdOrder,Amount,Price,CreatedAt FROM Product_Warehouse WHERE IdOrder = @IdOrder";
            cmd.Parameters.AddWithValue("@IdOrder", idOrder);

            var dr = cmd.ExecuteReader();

            if (!dr.Read()) return null;

            var productWarehouse = new ProductWarehouse()
            {
                IdProductWarehouse = (int)dr["IdProductWarehouse"],
                IdWarehouse = (int)dr["IdWarehouse"],
                IdProduct = (int)dr["IdProduct"],
                IdOrder = (int)dr["IdOrder"],
                Amount = (int)dr["Amount"],
                Price = (decimal)dr["Price"],
                CreatedAt = DateTime.Parse(dr["CreatedAt"].ToString() ?? string.Empty)
            };

            return productWarehouse;
        }
    }
}