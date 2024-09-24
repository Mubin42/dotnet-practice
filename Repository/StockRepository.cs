using System;
using backend.Data;
using backend.Dtos.Stock;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository;

public class StockRepository : IStockRepository
{
    private readonly AppDBContext _context;
    public StockRepository(AppDBContext context)
    {
        _context = context;
    }

    public async Task<Stock> CreateAsync(Stock stockModel)
    {
        await _context.Stocks.AddAsync(stockModel);
        await _context.SaveChangesAsync();

        return stockModel;
    }

    public async Task<Stock?> DeleteAsync(int id)
    {
        var stockModel = await _context.Stocks.FirstOrDefaultAsync(stock => stock.Id == id);

        if (stockModel == null)
        {
            return null;
        }

        _context.Stocks.Remove(stockModel);
        await _context.SaveChangesAsync();

        return stockModel;
    }

    public async Task<List<Stock>> GetAllAsync()
    {
        return await _context.Stocks.ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(int id)
    {
        return await _context.Stocks.FirstOrDefaultAsync(stock => stock.Id == id);
    }

    public async Task<Stock?> UpdateAsync(int id , UpdateStockDto updateStockDto)
    {
        var stockModel = await _context.Stocks.FirstOrDefaultAsync(stock => stock.Id == id);

        if (stockModel == null)
        {
            return null;
        }

        stockModel.Symbol = updateStockDto.Symbol;
        stockModel.CompanyName = updateStockDto.CompanyName;
        stockModel.Purchase = updateStockDto.Purchase;
        stockModel.LastDiv = updateStockDto.LastDiv;
        stockModel.Industry = updateStockDto.Industry;
        stockModel.MarketCap = updateStockDto.MarketCap;

        await _context.SaveChangesAsync();

        return stockModel;
    }
}
