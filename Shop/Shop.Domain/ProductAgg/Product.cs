using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.Utils;
using Common.Domain.ValueObjects;
using Shop.Domain.ProductAgg.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.ProductAgg
{
    public class Product:AggregateRoot
    {
       

        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ImageName { get; private set; }
        public long CategoryId { get; private set; }
        public long SubCategoryId { get; set; }
        public long SeconderySubCategory { get; private set; }
        public SeoData SeoData { get; private set; }
        public string Slug { get; private set; }
        public List<ProductImage> Images { get; private set; }
        public List<ProductSpecification> Specifications { get; private set; }

        private Product()
        {

        }

        public Product(string title, string description, int categoryId, int subCategoryId,
           int seconderySubCategory, string slug, SeoData seoData, string imageName, IProductDomainService service)
        {
            Guard(title, slug, description, imageName, service);

            Title = title;
            Description = description;
            CategoryId = categoryId;
            SubCategoryId = subCategoryId;
            SeconderySubCategory = seconderySubCategory;
            Slug = slug.ToSlug();
            SeoData = seoData;
            ImageName = imageName;
        }

        public void Edit(string title, string description, int categoryId, int subCategoryId,
         int seconderySubCategory, string slug, SeoData seoData, string imageName,
         IProductDomainService service)
        {
            Guard(title,slug,description,imageName, service);

            Title = title;
            Description = description;
            CategoryId = categoryId;
            SubCategoryId = subCategoryId;
            SeconderySubCategory = seconderySubCategory;
            Slug = slug.ToSlug();
            SeoData = seoData;
            ImageName = imageName;
        }

        public void AddImage(ProductImage image)
        {
            image.ProductId = Id;
            Images.Add(image);
        }
        public void RemoveImage(long imageId)
        {
            var image = Images.FirstOrDefault(i => i.Id == imageId);
            if (image == null)
                return;
            Images.Remove(image);
        }
        public void SetSpecification(List<ProductSpecification> specifications)
        {
            specifications.ForEach(i=>i.ProductId=Id);
            Specifications = specifications;
        }

        public void Guard(string title,string slug, string description, string imageName,
            IProductDomainService service)
        {
            NullOrEmptyDomainDataException.CheckString(title, nameof(title));
            NullOrEmptyDomainDataException.CheckString(description, nameof(description));
            NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
            NullOrEmptyDomainDataException.CheckString(slug, nameof(slug));


            if (slug != Slug)
                if (service.SlugIsExist(slug.ToSlug())
                    throw new SlugIsDuplicateException();

            
        }
    }
}
