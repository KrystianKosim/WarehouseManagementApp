using AnimalsManagementApp.Model;

namespace AnimalsManagementApp.Repositories;

public interface IWarehouseRepository
{
    Warehouse? GetWarehouse(int idWarehouse);

}