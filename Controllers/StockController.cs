using backend.Data;
using backend.Dtos.Stock;
using backend.Interfaces;
using backend.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/stocks")]
public class StockController : ControllerBase
{

  private readonly IStockRepository _stockRepo;

  public StockController(AppDBContext context, IStockRepository stockRepo)
  {
    _stockRepo = stockRepo;
  }

  // GET: api/stocks
  [HttpGet]
  public async Task<IActionResult> GetStocks()
  {
    var stocks = await _stockRepo.GetAllAsync();

    var stockDto = stocks.Select(stock => stock.ToStockDto());

    return Ok(stocks);
  }

  // GET: api/stocks/5
  [HttpGet("{id}")]
  public async Task<IActionResult> GetStock([FromRoute] int id)
  {
    var stock = await _stockRepo.GetByIdAsync(id);

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

    await _stockRepo.CreateAsync(stockModel);

    return CreatedAtAction(nameof(GetStock), new { id = stockModel.Id }, stockModel.ToStockDto());
  }

  // PUT: api/stocks/5
  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] UpdateStockDto updateStockDto)
  {

    var stockModel = await _stockRepo.UpdateAsync(id, updateStockDto);

    if (stockModel == null)
    {
      return NotFound();
    }

    return Ok(stockModel.ToStockDto());
  }

  // DELETE: api/stocks/5
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteStock([FromRoute] int id)
  {
    var stock = await _stockRepo.DeleteAsync(id);

    if (stock == null)
    {
      return NotFound();
    }

    return NoContent();
  }
}
