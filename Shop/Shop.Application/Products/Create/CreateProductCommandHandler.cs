using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Shop.Application._Utilities;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductAgg.Repository;
using Shop.Domain.ProductAgg.Services;

namespace Shop.Application.Products.Create
{
    internal class CreateProductCommandHandler : IBaseCommandHandler<CreateProductCommand>
    {
        private readonly IProductRepository _repository;
        private readonly IProductDomainService _domainService;
        private readonly IFileService _localFileService;

        public CreateProductCommandHandler(IProductRepository repository, IProductDomainService domainService, IFileService localFileService)
        {
            _repository = repository;
            _domainService = domainService;
            _localFileService = localFileService;
        }

        public async Task<OperationResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var imageName = await _localFileService.SaveFileAndGenerateName(request.ImageFile, Directories.ProductImages);

            var product = new Product(request.Title, request.Description, request.CategoryId, request.SubCategoryId,
                request.SeconderySubCategory, request.Slug, request.SeoData, imageName, _domainService);
          
             _repository.Add(product);

            var specifications = new List<ProductSpecification>();
            request.Specifications.ToList().ForEach(specification =>
            {
                specifications.Add(new ProductSpecification(specification.Key, specification.Value));
            });
            product.SetSpecification(specifications);

            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
