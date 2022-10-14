using Common.Application;
using Common.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Categories.Add_Child;
using Shop.Application.Categories.Create;
using Shop.Application.Categories.Edit;
using Shop.Persentation.Facade.Categories;
using Shop.Query.Category.DTOs;

namespace Shop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ApiController
    {
        private readonly ICategoryFacade _facade;

        public CategoryController(ICategoryFacade facade)
        {
            _facade = facade;
        }

        [HttpGet]
        public async Task<ApiResult<List<CategoryDto>>> GetCategories()
        {
            var result = await _facade.GetAllCategories();
            return QueryResult(result);
        }

        [HttpGet("{id}")]
        public async Task<ApiResult<CategoryDto>> GetCategoryById(long id)
        {
            var result = await _facade.GetById(id);
            return QueryResult(result);
        }
        [HttpGet("GetChildren/{parentId}")]
        public async Task<ApiResult<List<ChildCategoryDto>>> GetCategoryByParentId(long parentId)
        {
            var result = await _facade.GetCategoriesByParentId(parentId);
            return QueryResult(result);
        }
        [HttpPost]
        public async Task<ApiResult> CreateCategory(CreateCategoryCommand command)
        {
            var result = await _facade.Create(command);
            return CommandResult(result);
        }
        [HttpPut]
        public async Task<ApiResult> EditCategory(EditCategoryCommand command)
        {
            var result = await _facade.Edit(command);
            return CommandResult(result);
        }
        [HttpPost("AddChild")]
        public async Task<ApiResult> AddCategoryChild(AddChildCategoryCommand command)
        {
            var result = await _facade.AddChild(command);
            return CommandResult(result);
        }
        [HttpDelete("{categoryId}")]
        public async Task<ApiResult> DeleteCategory(long categoryId)
        {
            var result = await _facade.Delete(categoryId);
            return CommandResult(result);
        }
    }
}
