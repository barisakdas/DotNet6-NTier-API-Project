namespace CoreLayer.Dtos;
public class ProductDto
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedDate { get; set; }
    public int CategoryID { get; set; }
}

