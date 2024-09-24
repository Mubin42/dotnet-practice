using backend.Data;
using backend.Dtos.Stock;
using backend.Mappers;
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
    var stocks = _context.Stocks.ToList().Select(stock => stock.ToStockDto());

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

    return Ok(stock.ToStockDto());
  }

  // POST: api/stocks
  [HttpPost]
  public IActionResult CreateStock([FromBody] CreateStockDto stockDto)
  {

    var stockModel = stockDto.ToStockFromCreateDto();

    _context.Stocks.Add(stockModel);
    _context.SaveChanges();

    return CreatedAtAction(nameof(GetStock), new { id = stockModel.Id }, stockModel.ToStockDto());
  }

  // PUT: api/stocks/5
  [HttpPut("{id}")]
  public IActionResult UpdateStock([FromRoute] int id, [FromBody] UpdateStockDto updateStockDto)
  {

    var stockModel = _context.Stocks.FirstOrDefault(stock => stock.Id == id);

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

    _context.SaveChanges();

    return Ok(stockModel.ToStockDto());
  }

  // DELETE: api/stocks/5
  [HttpDelete("{id}")]
  public IActionResult DeleteStock([FromRoute] int id)
  {
    var stock = _context.Stocks.FirstOrDefault(stock => stock.Id == id);

    if (stock == null)
    {
      return NotFound();
    }

    _context.Stocks.Remove(stock);

    _context.SaveChanges();

    return NoContent();
  }
}
