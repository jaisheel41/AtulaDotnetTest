namespace AtulaDotnetTest.Models;
public class ProductDTO
{
    public int Id { get; set; }
    public string Sku { get; set; }
    public string Name { get; set; }
    public List<int> CategoryIds { get; set; } = new List<int>();
}
