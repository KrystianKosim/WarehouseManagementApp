using AnimalsManagementApp.Model;

namespace AnimalsManagementApp.Services;

public interface IWarehouseService
{
    int AddProductToWarehouse(AddProductToWarehouseCommand addProductToWarehouseCommand);
}