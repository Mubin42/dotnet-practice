using System;
using backend.Data;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository;

public class CommentRepository : ICommentRepository
{

  private readonly AppDBContext _context;
  public CommentRepository(AppDBContext context)
  {
    _context = context;
  }

  public async Task<List<Comment>> GetAllAsync()
  {
    return await _context.Comments.ToListAsync();
  }
}
