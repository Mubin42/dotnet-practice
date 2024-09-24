using System;
using backend.Dtos.Stock;
using backend.Models;

namespace backend.Interfaces;

public interface IStockRepository
{
  Task<List<Stock>> GetAllAsync();
  Task<Stock?> GetByIdAsync(int id); // FirstOrDefaultAsync can return null
  Task<Stock> CreateAsync(Stock stockModel);
  Task<Stock?> UpdateAsync(int id, UpdateStockDto stockDto);
  Task<Stock?> DeleteAsync(int id);
}
