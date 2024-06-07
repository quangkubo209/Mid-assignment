using QuangNN_MidAssignment.DTOs.requestDTO;
using QuangNN_MidAssignment.DTOs.responseDTO;

namespace QuangNN_MidAssignment.services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponseDto>> GetAllCategoriesAsync();
        Task<CategoryResponseDto> GetCategoryByIdAsync(Guid id);
        Task AddCategoryAsync(CategoryRequestDto categoryRequestDto);
        Task UpdateCategoryAsync(Guid id, CategoryRequestDto categoryRequestDto);
        Task DeleteCategoryAsync(Guid id);
    }
}
