using System;

namespace backend.Dtos.Comment;

public class CommentDto
{
  public int Id { get; set; }

  public string Title { get; set; } = string.Empty;

  public string Body { get; set; } = string.Empty;

  public DateTime CreatedAt { get; set; } = DateTime.Now;

  public int? StockId { get; set; }


}
