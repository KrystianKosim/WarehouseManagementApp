using System.ComponentModel.DataAnnotations;

namespace AnimalsManagementApp.Model;

public class ProductWarehouse
{
    public ProductWarehouse()
    {
    }

    public ProductWarehouse(int idWarehouse, int idProduct, int idOrder, int amount, decimal price, DateTime createdAt)
    {
        IdWarehouse = idWarehouse;
        IdProduct = idProduct;
        IdOrder = idOrder;
        Amount = amount;
        Price = price;
        CreatedAt = createdAt;
    }

    [Required] public int IdProductWarehouse { get; set; }

    public int IdWarehouse { get; set; }

    public int IdProduct { get; set; }

    public int IdOrder { get; set; }

    public int Amount { get; set; }
    public decimal Price { get; set; }

    public DateTime CreatedAt { get; set; }
}