using AnimalsManagementApp.Model;
using Microsoft.Data.SqlClient;

namespace AnimalsManagementApp.Repositories;

public class WarehouseRepository : IWarehouseRepository
{
    private IConfiguration _configuration;
    
    public WarehouseRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public Warehouse? GetWarehouse(int idWarehouse)
    { 
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();
     
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT IdWarehouse, Name, Address FROM Warehouse WHERE IdWarehouse = @IdWarehouse";
        cmd.Parameters.AddWithValue("@IdWarehouse", idWarehouse);
        
        var dr = cmd.ExecuteReader();
        
        if (!dr.Read()) return null;
        
        var warehouse = new Warehouse()
        {
            IdWarehouse = (int)dr["IdWarehouse"],
            Name = dr["Name"].ToString(),
            Address = dr["Address"].ToString()
        };
        
        return warehouse;
    }
}