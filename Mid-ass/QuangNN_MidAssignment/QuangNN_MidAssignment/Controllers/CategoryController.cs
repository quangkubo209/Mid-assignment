using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuangNN_MidAssignment.DTOs.requestDTO;
using QuangNN_MidAssignment.DTOs.responseDTO;
using QuangNN_MidAssignment.services;

namespace QuangNN_MidAssignment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<CategoryResponseDto>>>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return (new SuccessGeneralResponse<IEnumerable<CategoryResponseDto>>
            {
                Content = categories,
                StatusCode = System.Net.HttpStatusCode.OK
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<CategoryResponseDto>>> GetCategoryById(Guid id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return (new GeneralResponse<CategoryResponseDto>
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Category not found"
                });
            }
            return (new SuccessGeneralResponse<CategoryResponseDto>
            {
                Content = category,
                StatusCode = System.Net.HttpStatusCode.OK
            });
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<GeneralResponse>> AddCategory([FromBody] CategoryRequestDto categoryRequestDto)
        {
            await _categoryService.AddCategoryAsync(categoryRequestDto);
            return new SuccessGeneralResponse
            {
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = "Category created successfully"
            };
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<GeneralResponse>> UpdateCategory(Guid id, [FromBody] CategoryRequestDto categoryRequestDto)
        {
            var existingCategory = await _categoryService.GetCategoryByIdAsync(id);
            if (existingCategory == null)
            {
                return (new GeneralResponse
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Category not found"
                });
            }

            await _categoryService.UpdateCategoryAsync(id, categoryRequestDto);
            return (new SuccessGeneralResponse
            {
                StatusCode = System.Net.HttpStatusCode.NoContent,
                Message = "Category updated successfully"
            });
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<GeneralResponse>> DeleteCategory(Guid id)
        {
            var existingCategory = await _categoryService.GetCategoryByIdAsync(id);
            if (existingCategory == null)
            {
                return (new GeneralResponse
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Category not found"
                });
            }

            await _categoryService.DeleteCategoryAsync(id);
            return (new SuccessGeneralResponse
            {
                StatusCode = System.Net.HttpStatusCode.NoContent,
                Message = "Category deleted successfully"
            });
        }
    }
}
