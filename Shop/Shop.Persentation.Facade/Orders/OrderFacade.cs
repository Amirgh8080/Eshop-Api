using System.Runtime.InteropServices.ComTypes;
using Common.Application;
using MediatR;
using Shop.Application.Orders.AddItem;
using Shop.Application.Orders.Checkout;
using Shop.Application.Orders.DecreaseItemCount;
using Shop.Application.Orders.IncreaseItemCount;
using Shop.Application.Orders.ReamoveItem;
using Shop.Query.Orders.DTOs;
using Shop.Query.Orders.GetByFilter;
using Shop.Query.Orders.GetById;
using Shop.Query.Orders.GetCurrent;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Shop.Persentation.Facade.Orders;

public class OrderFacade:IOrderFacade
{
    private IMediator _mediator;

    public OrderFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> AddOrderItem(AddOrderItemCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> OrderCheckOut(CheckoutOrderCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> RemoveOrderItem(RemoveOrderItemCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> IncreaseItemCount(IncreaseOrderItemCountCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> DecreaseItemCount(DecreaseOrderItemCountCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OrderDto?> GetOrderById(long orderId)
    {
        return await _mediator.Send(new GetOrderByIdQuery(orderId));
    }

    public async Task<OrderFilterResult> GetOrdersByFilter(OrderFilterParams filterParams)
    {
        return await _mediator.Send(new GetOrdersByFilterQuery(filterParams));
    }

    public async Task<OrderDto?> GetCurrentOrder(long userId)
    {
        return await _mediator.Send(new GetCurrentUserOrderQuery(userId));
    }
}