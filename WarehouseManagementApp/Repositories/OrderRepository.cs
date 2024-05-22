using AnimalsManagementApp.Model;
using Microsoft.Data.SqlClient;

namespace AnimalsManagementApp.Repositories;

public class OrderRepository : IOrderRepository
{
    private IConfiguration _configuration;

    public OrderRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Order? GetOrder(int idProduct, int amount)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();

        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText =
            "SELECT IdOrder, IdProduct, Amount, CreatedAt,FulfilledAt FROM Order WHERE IdProduct = @IdProduct AND Amount = @Amount";
        cmd.Parameters.AddWithValue("@IdProduct", idProduct);
        cmd.Parameters.AddWithValue("@Amount", amount);

        var dr = cmd.ExecuteReader();

        if (!dr.Read()) return null;

        var order = new Order()
        {
            IdOrder = (int)dr["IdOrder"],
            IdProduct = (int)dr["IdProduct"],
            Amount = (int)dr["Amount"],
            CreatedAt =  DateTime.Parse(dr["CreatedAt"].ToString()),
            FulfilledAt = DateTime.Parse(dr["FulfilledAt"].ToString() ?? string.Empty),
        };

        return order;
    }

    public void UpdateFullfilledAt(int idOrder, DateTime fulfilledAt)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();

        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText =
            "UPDATE Order SET FulfilledAt = @FulfilledAt WHERE IdOrder = @IdOrder";
        cmd.Parameters.AddWithValue("@FulfilledAt", fulfilledAt);
        cmd.Parameters.AddWithValue("@IdOrder", idOrder);

        cmd.ExecuteNonQuery();
    }
}