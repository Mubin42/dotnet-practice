using System;
using backend.Data;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/stocks")]
public class StockController : ControllerBase
{
  private readonly AppDBContext _context;
  public StockController(AppDBContext context)
  {
    _context = context;
  }

  // GET: api/stocks
  [HttpGet]
  public IActionResult GetStocks()
  {
    var stocks = _context.Stocks.ToList();

    return Ok(stocks);
  }

  // GET: api/stocks/5
  [HttpGet("{id}")]
  public IActionResult GetStock([FromRoute] int id)
  {
    var stock = _context.Stocks.Find(id);

    if (stock == null)
    {
      return NotFound();
    }

    return Ok(stock);
  }
}
