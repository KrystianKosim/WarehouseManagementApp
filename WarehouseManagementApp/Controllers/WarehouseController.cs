using AnimalsManagementApp.Model;
using AnimalsManagementApp.Services;
using Microsoft.AspNetCore.Mvc;


namespace AnimalsManagementApp.Controller;

[Route("api/[controller]")]
[ApiController]
public class WarehouseController : ControllerBase
{
    private IWarehouseService _warehouseService;

    public WarehouseController(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }

    
    [HttpPost]
    public IActionResult AddProductToWarehouse(AddProductToWarehouseCommand addProductToWarehouseCommand)
    {
        var students = _warehouseService.AddProductToWarehouse(addProductToWarehouseCommand);
        return Ok(students);
    }
}