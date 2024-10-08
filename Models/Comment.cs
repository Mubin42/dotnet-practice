namespace backend.Models;

public class Comment
{
  public int Id { get; set; }

  public string Title { get; set; } = string.Empty;

  public string Body { get; set; } = string.Empty;
  
  public DateTime CreatedAt { get; set; } = DateTime.Now;

  public int? StockId { get; set; }
  
  public Stock? Stock { get; set; }
}
