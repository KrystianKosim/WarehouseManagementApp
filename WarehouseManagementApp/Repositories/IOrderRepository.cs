using AnimalsManagementApp.Model;

namespace AnimalsManagementApp.Repositories;

public interface IOrderRepository
{
    Order? GetOrder(int idProduct, int amount);
    void UpdateFullfilledAt(int orderIdOrder, DateTime fulfilledAt);
}