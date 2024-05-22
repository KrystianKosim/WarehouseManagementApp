using AnimalsManagementApp.Model;

namespace AnimalsManagementApp.Repositories;

public interface IProductRepository
{
    Product? GetProduct(int idProduct);

}