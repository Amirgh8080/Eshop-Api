
using Common.Application;
using Shop.Domain.OrderAgg.Repository;

namespace Shop.Application.Orders.ReamoveItem
{
    public class RemoveOrderItemCommandHandler : IBaseCommandHandler<RemoveOrderItemCommand>
    {
        private readonly IOrderRepository _reposoitory;

        public RemoveOrderItemCommandHandler(IOrderRepository reposoitory)
        {
            _reposoitory = reposoitory;
        }

        public async Task<OperationResult> Handle(RemoveOrderItemCommand request, CancellationToken cancellationToken)
        {
            var order = await _reposoitory.GetCurrentUserOrder(request.UserId);
            if (order == null)
                return OperationResult.NotFound();

            order.RemoveItem(request.ItemId);
            await _reposoitory.Save();
            return OperationResult.Success();
        }
    }
   
}
