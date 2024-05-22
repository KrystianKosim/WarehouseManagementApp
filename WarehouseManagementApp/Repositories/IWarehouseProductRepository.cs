using AnimalsManagementApp.Model;

namespace AnimalsManagementApp.Repositories;

public interface IWarehouseProductRepository
{
    int AddProductToWarehouse(ProductWarehouse productWarehouse);
    ProductWarehouse? GetWarehouse(int orderIdOrder);
}