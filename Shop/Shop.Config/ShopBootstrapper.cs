using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application._Utilities;
using Shop.Application.Categories;
using Shop.Application.Products;
using Shop.Application.Products.AddImage;
using Shop.Application.Sellers;
using Shop.Application.Users;
using Shop.Domain.CategoryAgg.Services;
using Shop.Domain.CommentAgg;
using Shop.Domain.ProductAgg.Services;
using Shop.Domain.SellerAgg.Repository;
using Shop.Domain.SellerAgg.Services;
using Shop.Domain.UserAgg.Repository;
using Shop.Domain.UserAgg.Services;
using Shop.Infrastructure;
using Shop.Infrastructure.Persistent.Ef.CommentAgg;
using Shop.Infrastructure.Persistent.Ef.SellerAgg;
using Shop.Persentation.Facade;
using Shop.Query.Category.GetById;

namespace Shop.Config
{
    public static class ShopBootstrapper
    {
        public static void RegisterShopDependency(this IServiceCollection services, string connectionString)
        {
            InfrastructureBootstrapper.Init(services,connectionString);

            services.AddMediatR(typeof(Directories).Assembly);
            services.AddMediatR(typeof(GetCategoryByIdQuery).Assembly);

            services.AddTransient<IProductDomainService, ProductDomainService>();
            services.AddTransient<ICategoryDomainService, CategoryDomainService>();
            services.AddTransient<ISellerDomainService, SellerDomainService>();
            services.AddTransient<IUserDomainService, UserDomainService>();

            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<ISellerRepository, SellerRepository>();



            services.AddValidatorsFromAssembly(typeof(AddProductImageCommandValidator).Assembly);


            services.InitFacadeDependency();
        }
    }
}