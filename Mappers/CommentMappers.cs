using backend.Dtos.Comment;
using backend.Models;


namespace backend.Mappers;

public static class CommentMappers
{
  public static CommentDto ToCommentDto(this Comment commentModel)
  {
    return new CommentDto
    {
      Id = commentModel.Id,
      Title = commentModel.Title,
      Body = commentModel.Body,
      CreatedAt = commentModel.CreatedAt,
      StockId = commentModel.StockId
    };
  }
}
