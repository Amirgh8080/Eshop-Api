using Microsoft.Extensions.DependencyInjection;
using Shop.Persentation.Facade.Categories;
using Shop.Persentation.Facade.Comments;

namespace Shop.Persentation.Facade;

public static class FacadeBootstrapper
{
    public static void InitFacadeDependency(this IServiceCollection services)
    {
        services.AddScoped<ICategoryFacade, CategoryFacade>();
        services.AddScoped<ICommentFacade, CommentFacade>();
        services.AddScoped<ICategoryFacade, CategoryFacade>();
        services.AddScoped<ICategoryFacade, CategoryFacade>();
        services.AddScoped<ICategoryFacade, CategoryFacade>();
        services.AddScoped<ICategoryFacade, CategoryFacade>();
        services.AddScoped<ICategoryFacade, CategoryFacade>();
        services.AddScoped<ICategoryFacade, CategoryFacade>();
        services.AddScoped<ICategoryFacade, CategoryFacade>();

    }
}