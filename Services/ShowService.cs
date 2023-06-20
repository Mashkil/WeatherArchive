using Microsoft.EntityFrameworkCore;
using TestApp.Data;
using TestApp.Data.Entities;

namespace TestApp.Services
{
    public class ShowService
    {
        private readonly TestAppDbContext _dbContext;
        public ShowService(TestAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<WeatherData>> GetData(int year, int month)
        {
            return await _dbContext.WeatherDatas.Where(e => e.Date.Year == year && e.Date.Month == month).ToListAsync();
        }
    }
}
