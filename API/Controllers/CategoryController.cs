using API.DTOs.Categories;
using API.Services.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        /// <summary>
        /// Add categories to the system
        /// </summary>
        /// <param name="categoryRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAsync([FromBody] AddCategoryRequest categoryRequest)
        {
            ArgumentNullException.ThrowIfNull(categoryRequest);
            var category = await _categoryService.AddNewAsync(categoryRequest);
            return Ok(category);
        }

        /// <summary>
        /// Get all available categories in the system
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPagedAsync([FromQuery] int page, [FromQuery] int pageSize)
        {
            var categories = await _categoryService.GetPagedAsync(page, pageSize);
            return Ok(categories);
        }

        /// <summary>
        /// Get category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAsync([FromQuery] int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            return Ok(category);
        }
        /// <summary>
        /// Get all category in the system
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        [Authorize]
        public async Task<IActionResult> GetAllAsync()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }


        /// <summary>
        /// Remove categories from system
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync([FromQuery] int id)
        {
            await _categoryService.DeleteAsync(id);
            return Ok();
        }

        /// <summary>
        /// Update categories in system
        /// </summary>
        /// <param name="categoryRequest"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateCategoryRequest categoryRequest)
        {
            ArgumentNullException.ThrowIfNull(categoryRequest);
            var result = await _categoryService.UpdateAsync(categoryRequest);
            return Ok(result);
        }

    }
}
