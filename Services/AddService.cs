using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using TestApp.Data;
using TestApp.Data.Entities;

namespace TestApp.Services
{
    public class AddService
    {
        private readonly TestAppDbContext _dbContext;
        IWebHostEnvironment _appEnvironment;
        public AddService(TestAppDbContext dbContext, IWebHostEnvironment appEnvironment)
        {
            _dbContext = dbContext;
            _appEnvironment = appEnvironment;
        }

        public async Task AddToDatabase(IFormFile file)
        {
            try
            {
                if (!file.FileName.Contains(".xlsx") || !file.FileName.Contains(".xls"))
                    throw new Exception();

                string path = _appEnvironment.WebRootPath + "\\Files\\" + file.FileName;

                using (var fs = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(fs);
                }

                IWorkbook workbook = null;

                FileStream fileStream = new(path, FileMode.Open, FileAccess.Read);

                if (path.IndexOf(".xlsx") > 0)
                    workbook = new XSSFWorkbook(fileStream);

                else if (path.IndexOf(".xls") > 0)
                    workbook = new HSSFWorkbook(fileStream);


                for (int i = 0; i < workbook.NumberOfSheets; i++)
                {
                    ISheet sheet = workbook.GetSheetAt(i);
                    int rowCount = sheet.LastRowNum;

                    for (int k = 4; k <= rowCount; k++)
                    {
                        IRow currentRow = sheet.GetRow(k);

                        var date = currentRow.GetCell(0).StringCellValue.Trim(); // можно реализовать через for (int i=0; i<currentRow.LastCellNum.cell)
                        var time = currentRow.GetCell(1).StringCellValue.Trim();
                        var temp = currentRow.GetCell(2).ToString();
                        var airHumidity = currentRow.GetCell(3).ToString();
                        var dewPoint = currentRow.GetCell(4).ToString();
                        var pressureMmHg = currentRow.GetCell(5).ToString();
                        var windDirection = currentRow.GetCell(6).ToString();
                        var windSpeed = currentRow.GetCell(7).ToString();
                        var cloudy = currentRow.GetCell(8).ToString();
                        var cloudCover = currentRow.GetCell(9).ToString();
                        var horizontalVisibility = currentRow.GetCell(10).ToString();
                        string? weatherEvents = currentRow.GetCell(11)?.ToString();

                        _dbContext.WeatherDatas.Add(new WeatherData
                        {
                            Date = DateTime.Parse(date),
                            Time = DateTime.Parse(time),
                            TemperatureC = Convert.ToDouble(temp),
                            AirHumidity = Convert.ToDouble(airHumidity),
                            DewPoint = Convert.ToDouble(dewPoint),
                            PressureMmHg = Convert.ToInt32(pressureMmHg),
                            WindDirection = windDirection,
                            WindSpeed = string.IsNullOrWhiteSpace(windSpeed) ? null : Convert.ToInt32(windSpeed),
                            Cloudy = string.IsNullOrWhiteSpace(cloudy) ? null : Convert.ToInt32(cloudy),
                            CloudCover = string.IsNullOrWhiteSpace(cloudCover) ? null : Convert.ToInt32(cloudCover),
                            HorizontalVisibility = string.IsNullOrWhiteSpace(horizontalVisibility) ? null : Convert.ToInt32(horizontalVisibility),
                            WeatherEvents = weatherEvents
                        });

                        await _dbContext.SaveChangesAsync();
                    }
                }
            }
            catch (Exception)
            {

            }
        }

    }
}
