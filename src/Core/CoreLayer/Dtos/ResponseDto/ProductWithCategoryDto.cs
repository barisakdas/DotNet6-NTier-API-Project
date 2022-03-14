namespace CoreLayer.Dtos.ResponseDto;
public class ProductWithCategoryDto : ProductDto
{
    public CategoryDto Category { get; set; }
}
