using Common.Application;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Products.Edit
{
    public class EditProductCommand : IBaseCommand
    {
        public EditProductCommand(long productId, string title, string description, IFormFile imageFile,
            long categoryId, long subCategoryId, long seconderySubCategory, SeoData seoData, string slug,
            Dictionary<string, string> specifications)
        {
            ProductId = productId;
            Title = title;
            Description = description;
            ImageFile = imageFile;
            CategoryId = categoryId;
            SubCategoryId = subCategoryId;
            SeconderySubCategory = seconderySubCategory;
            SeoData = seoData;
            Slug = slug;
            Specifications = specifications;
        }

        public long ProductId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public IFormFile? ImageFile { get; private set; }
        public long CategoryId { get; private set; }
        public long SubCategoryId { get; set; }
        public long SeconderySubCategory { get; private set; }
        public SeoData SeoData { get; private set; }
        public string Slug { get; private set; }
        public Dictionary<string, string> Specifications { get; private set; }

    }
}
