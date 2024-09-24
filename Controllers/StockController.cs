using backend.Data;
using backend.Dtos.Stock;
using backend.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
  public async Task<IActionResult> GetStocks()
  {
    var stocks = await _context.Stocks.ToListAsync();

    var stockDto = stocks.Select(stock => stock.ToStockDto());

    return Ok(stocks);
  }

  // GET: api/stocks/5
  [HttpGet("{id}")]
  public async Task<IActionResult> GetStock([FromRoute] int id)
  {
    var stock = await _context.Stocks.FindAsync(id);

    if (stock == null)
    {
      return NotFound();
    }

    return Ok(stock.ToStockDto());
  }

  // POST: api/stocks
  [HttpPost]
  public async Task<IActionResult> CreateStock([FromBody] CreateStockDto stockDto)
  {

    var stockModel = stockDto.ToStockFromCreateDto();

    await _context.Stocks.AddAsync(stockModel);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetStock), new { id = stockModel.Id }, stockModel.ToStockDto());
  }

  // PUT: api/stocks/5
  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] UpdateStockDto updateStockDto)
  {

    var stockModel = await _context.Stocks.FirstOrDefaultAsync(stock => stock.Id == id);

    if (stockModel == null)
    {
      return NotFound();
    }

    stockModel.Symbol = updateStockDto.Symbol;
    stockModel.CompanyName = updateStockDto.CompanyName;
    stockModel.Purchase = updateStockDto.Purchase;
    stockModel.LastDiv = updateStockDto.LastDiv;
    stockModel.Industry = updateStockDto.Industry;
    stockModel.MarketCap = updateStockDto.MarketCap;

    await _context.SaveChangesAsync();

    return Ok(stockModel.ToStockDto());
  }

  // DELETE: api/stocks/5
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteStock([FromRoute] int id)
  {
    var stock = await _context.Stocks.FirstOrDefaultAsync(stock => stock.Id == id);

    if (stock == null)
    {
      return NotFound();
    }

    // Delete is not a async method
    _context.Stocks.Remove(stock);

    await _context.SaveChangesAsync();

    return NoContent();
  }
}
