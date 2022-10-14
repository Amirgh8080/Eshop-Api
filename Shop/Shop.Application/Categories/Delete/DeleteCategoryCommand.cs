using Common.Application;
using Shop.Domain.CategoryAgg;

namespace Shop.Application.Categories.Delete;

public record DeleteCategoryCommand(long CategoryId) : IBaseCommand;

public class DeleteCategoryCommandHandler:IBaseCommandHandler<DeleteCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<OperationResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var result = await _categoryRepository.DeleteCategory(request.CategoryId);
        if(result)
            return OperationResult.Success();

        return OperationResult.Error("امکان حذف این دسته بندی وجود ندارد");
    }
}