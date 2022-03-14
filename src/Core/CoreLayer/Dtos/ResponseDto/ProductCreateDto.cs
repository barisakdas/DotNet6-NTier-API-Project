namespace CoreLayer.Dtos.ResponseDto;
public class ProductCreateDto
{
    public string Name { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public int CategoryID { get; set; }
}

