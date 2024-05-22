using System.ComponentModel.DataAnnotations;

namespace AnimalsManagementApp.Model;

public class AddProductToWarehouseCommand
{
    [Required]  public int IdProduct { get; set; }

    [Required] public int IdWarehouse { get; set; }

    [Required] public int Amount { get; set; }

    public DateTime CreatedAt { get; set; }
}