namespace CoreLayer.Dtos.ResponseDto;
public class CategoryWithProductsDto : CategoryDto
{
    public List<ProductDto> Products { get; set; }
}

