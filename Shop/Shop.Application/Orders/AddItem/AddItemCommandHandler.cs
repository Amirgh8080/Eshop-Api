using Common.Application;
using Shop.Domain.OrderAgg;
using Shop.Domain.OrderAgg.Repository;
using Shop.Domain.SellerAgg.Repository;

namespace Shop.Application.Orders.AddItem;

public class AddItemCommandHandler : IBaseCommandHandler<AddOrderItemCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ISellerRepository _sellerReposictory;

    public AddItemCommandHandler(IOrderRepository orderRepository, ISellerRepository sellerReposictory)
    {
        _orderRepository = orderRepository;
        _sellerReposictory = sellerReposictory;
    }

    public async Task<OperationResult> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
    {
        var inventory = await _sellerReposictory.GetInventoryById(request.InventoryId);
        if (inventory == null)
            return OperationResult.NotFound();
        if (inventory.Count < request.Count)
            return OperationResult.Error("تعداد محصولات موجود کمتر از حد درخواستی است.");

        var order = await _orderRepository.GetCurrentUserOrder(request.UserId);
        if (order == null)
            order = new Order(request.UserId);

        order.AddItem(new OrderItem(request.InventoryId, request.Count, inventory.Price));

        if (ItemCountBiggerThanInventoryCount(inventory,order))
            return OperationResult.Error("تعداد محصولات موجود کمتر از حد درخواستی است.");

        await _orderRepository.Save();

        return OperationResult.Success();
    }

    private bool ItemCountBiggerThanInventoryCount(InventoryResult inventory, Order order)
    {
        var orderItem = order.Items.First(oi => oi.InverntoryId == inventory.Id);
        if (orderItem.Count > inventory.Count)
            return true;

        return false;
    }
}

