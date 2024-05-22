using AnimalsManagementApp.Model;
using AnimalsManagementApp.Repositories;
using WarehouseManagementApp.Exceptions;

namespace AnimalsManagementApp.Services;

public class WarehouseService : IWarehouseService
{
    private readonly IWarehouseProductRepository _warehouseProductRepository;
    private readonly IProductRepository _productRepository;
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IOrderRepository _orderRepository;

    public WarehouseService(IWarehouseProductRepository warehouseProductRepository,
        IProductRepository productRepository, IWarehouseRepository warehouseRepository,
        IOrderRepository orderRepository)
    {
        _warehouseProductRepository = warehouseProductRepository;
        _productRepository = productRepository;
        _warehouseRepository = warehouseRepository;
        _orderRepository = orderRepository;
    }

    public int AddProductToWarehouse(AddProductToWarehouseCommand addProductToWarehouseCommand)
    {
        var product = CheckIfProductExists(addProductToWarehouseCommand);

        CheckIfWarehouseExists(addProductToWarehouseCommand);

        CheckIfAmountIsCorrect(addProductToWarehouseCommand);

        var order = CheckIfOrderExists(addProductToWarehouseCommand);

        CheckIfCreatedAtIsCorrect(addProductToWarehouseCommand, order);

        CheckIfProductWarehouseExists(order);

        _orderRepository.UpdateFullfilledAt(order.IdOrder, DateTime.Now);

        AddProductWarehouse(addProductToWarehouseCommand, product, order);

        return GetWarehousePrimaryKey(order);
    }

    private int GetWarehousePrimaryKey(Order order)
    {
        var warehouse = _warehouseProductRepository.GetWarehouse(order.IdOrder);
        if (warehouse == null)
        {
            throw new Exception("Something went wrong");
        }

        return warehouse.IdProductWarehouse;
    }

    private void AddProductWarehouse(AddProductToWarehouseCommand addProductToWarehouseCommand, Product product,
        Order order)
    {
        var productWarehousePrice = product.Price * order.Amount;
        var productWarehouse = new ProductWarehouse(addProductToWarehouseCommand.IdWarehouse,
            addProductToWarehouseCommand.IdProduct,
            order.IdOrder,
            order.Amount,
            productWarehousePrice,
            DateTime.Now);
        _warehouseProductRepository.AddProductToWarehouse(productWarehouse);
    }

    private void CheckIfProductWarehouseExists(Order order)
    {
        var productWarehouse = _warehouseProductRepository.GetWarehouse(order.IdOrder);

        if (productWarehouse != null)
        {
            throw new ValidationException("Order is done!");
        }
    }

    private static void CheckIfCreatedAtIsCorrect(AddProductToWarehouseCommand addProductToWarehouseCommand,
        Order? order)
    {
        if (order != null && order.CreatedAt >= addProductToWarehouseCommand.CreatedAt)
        {
            throw new ValidationException("CreatedAt value is not later than order CreatedAt value!");
        }
    }

    private Order CheckIfOrderExists(AddProductToWarehouseCommand addProductToWarehouseCommand)
    {
        var order = _orderRepository.GetOrder(addProductToWarehouseCommand.IdProduct,
            addProductToWarehouseCommand.Amount);
        if (order == null)
        {
            throw new ValidationException("Order for given product and with given amount doesn't exists!");
        }

        return order;
    }

    private static void CheckIfAmountIsCorrect(AddProductToWarehouseCommand addProductToWarehouseCommand)
    {
        if (addProductToWarehouseCommand.Amount <= 0)
        {
            throw new ValidationException("Amount have to be greater than 0!");
        }
    }

    private void CheckIfWarehouseExists(AddProductToWarehouseCommand addProductToWarehouseCommand)
    {
        var warehouse = _warehouseRepository.GetWarehouse(addProductToWarehouseCommand.IdWarehouse);
        if (warehouse == null)
        {
            throw new ValidationException("Warehouse doesn't exists!");
        }
    }

    private Product CheckIfProductExists(AddProductToWarehouseCommand addProductToWarehouseCommand)
    {
        var product = _productRepository.GetProduct(addProductToWarehouseCommand.IdProduct);
        if (product == null)
        {
            throw new ValidationException("Product doesn't exists!");
        }

        return product;
    }
}