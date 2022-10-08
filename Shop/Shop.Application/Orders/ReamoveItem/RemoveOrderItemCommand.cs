using Common.Application;

namespace Shop.Application.Orders.ReamoveItem;

public record RemoveOrderItemCommand(long UserId, long ItemId) : IBaseCommand;


