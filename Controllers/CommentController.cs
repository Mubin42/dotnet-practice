using System;
using backend.Interfaces;
using backend.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/comments")]
[ApiController]
public class CommentController : ControllerBase
{
  private readonly ICommentRepository _commentRepo;

  public CommentController(ICommentRepository commentRepo)
  {
    _commentRepo = commentRepo;
  }

  [HttpGet]
  public async Task<IActionResult> GetAllComments()
  {
    var comments = await _commentRepo.GetAllAsync();
    var commentDto = comments.Select(comment => comment.ToCommentDto());

    return Ok(commentDto);
  }
}
