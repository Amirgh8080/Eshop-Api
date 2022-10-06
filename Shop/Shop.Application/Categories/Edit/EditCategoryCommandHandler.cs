
using Common.Application;
using Shop.Domain.CategoryAgg;
using Shop.Domain.CategoryAgg.Services;

namespace Shop.Application.Categories.Edit
{
    public class EditCategoryCommandHandler : IBaseCommandHandler<EditCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryDomainService _categoryDomainService;
        public async Task<OperationResult> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
        {
            var category =await _categoryRepository.GetTracking(request.Id);

            if (category == null)
                return OperationResult.NotFound();

            category.Edit(request.Title, request.Slug, request.SeoData,_categoryDomainService);

            await _categoryRepository.Save();

            return OperationResult.Success();
        }
    }

}
