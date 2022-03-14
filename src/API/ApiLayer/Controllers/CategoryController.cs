namespace ApiLayer.Controllers;

public class CategoryController : BaseController
{
    private readonly ICategoryService _service;
    private readonly IMapper _mapper;

    public CategoryController(IMapper mapper, ICategoryService service) => (_mapper, _service) = (mapper, service);

    [HttpGet("[action]/{categoryId}")]
    public async Task<IActionResult> GetSingleCategoryWithProducts(int categoryId)
    {
        return CreateActionResult(await _service.GetSingleCategoryWithProducts(categoryId));
    }
}

