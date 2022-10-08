using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Shop.Application._Utilities;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductAgg.Repository;
using Shop.Domain.ProductAgg.Services;

namespace Shop.Application.Products.Edit
{
    internal class EditProductCommandHandler : IBaseCommandHandler<EditProductCommand>
    {
        private readonly IProductRepository _repository;
        private readonly IProductDomainService _domainService;
        private readonly IFileService _localFileService;

        public EditProductCommandHandler(IProductRepository repository, IProductDomainService domainService, IFileService localFileService)
        {
            _repository = repository;
            _domainService = domainService;
            _localFileService = localFileService;
        }

        public async Task<OperationResult> Handle(EditProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetTracking(request.ProductId);

            if (product == null)
                return OperationResult.NotFound();

            product.Edit(request.Title, request.Description, request.CategoryId, request.SubCategoryId,
                request.SeconderySubCategory, request.Slug, request.SeoData, _domainService);

            var oldImage = product.ImageName;

            if (request.ImageFile != null)
            {
                var imageName = await _localFileService
                    .SaveFileAndGenerateName(request.ImageFile, Directories.ProductImages);

                product.SetProductImage(imageName);
            }

            var specifications = new List<ProductSpecification>();
            request.Specifications.ToList().ForEach(specification =>
            {
                specifications.Add(new ProductSpecification(specification.Key, specification.Value));
            });
            product.SetSpecification(specifications);

            await _repository.Save();
            RemoveOldImage(request.ImageFile, oldImage);
            return OperationResult.Success();
        }

        public void RemoveOldImage(IFormFile imageFile,string oldImage)
        {
            if (imageFile != null)
            {
                _localFileService.DeleteFile(Directories.ProductImages,oldImage);
            }
        }
    }
}
