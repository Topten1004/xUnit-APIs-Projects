using Sales.Common.Extension;
using Sales.Domain.Entities;
using Sales.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sales.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly IGenericServiceAsync<Sale> _service;
        private readonly ISalesService _salesService;

        public SalesController(IGenericServiceAsync<Sale> service, ISalesService salesService)
        {
            _service = service;
            _salesService = salesService;
        }

        [HttpGet]
        public IActionResult GetAllSales()
        {
            try
            {
                return Ok(_service.GetAll().Result.OrderBy(x => x.OrderDate));
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Fail to get data from DB {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetSalesCount()
        {
            try
            {
                return Ok(await _service.CountAll().ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Fail to get data from DB {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetSaleById(string id)
        {
            try
            {
                return Ok(await _service.GetById(new Guid(id)).ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Fail to get data from DB {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCountries()
        {
            return Ok(await _salesService.SelectDistinctCountries().ConfigureAwait(false));
        }

        [HttpGet]
        public IActionResult CountFiltered(DateTime? beginDate = null, DateTime? endDate = null, string country = "")
        {
            try
            {
                DateTime dtBegin = beginDate ?? DateTime.MinValue;
                DateTime dtEnd = endDate ?? DateTime.Now;

                if (string.IsNullOrEmpty(country) || country == "undefined")
                {
                    return Ok(_service.CountFiltered(x => x.OrderDate > dtBegin && x.OrderDate < dtEnd));
                }
                else
                {
                    return Ok(_service.CountFiltered(x => x.OrderDate > dtBegin && x.OrderDate < dtEnd && x.Country == country));
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Fail to get data from DB {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetSalesByRangeDateAndCountry(DateTime? beginDate = null, DateTime? endDate = null, string country = "", int page = 0, int size = 10)
        {
            try
            {
                DateTime dtBegin = beginDate ?? DateTime.MinValue;
                DateTime dtEnd = endDate ?? DateTime.Now;

                var teste = _service.GetWhere(x => x.OrderDate > dtBegin && x.OrderDate < dtEnd, page, size).OrderBy(x => x.OrderDate);

                if (string.IsNullOrEmpty(country) || country == "undefined")
                {
                    return Ok(_service.GetWhere(x => x.OrderDate > dtBegin && x.OrderDate < dtEnd, page, size).OrderBy(x => x.OrderDate));
                }
                else
                {
                    return Ok(_service.GetWhere(x => x.OrderDate > dtBegin && x.OrderDate < dtEnd && x.Country == country, page, size).OrderBy(x => x.OrderDate));
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Fail to get data from DB {ex.Message}");
            }
        }

        [HttpPost, DisableRequestSizeLimit]
        public ActionResult UploadFile()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("CSV");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
                    var fullPath = Path.Combine(pathToSave, filename.Replace("\"", " ").Trim());

                    if (!Directory.Exists(pathToSave))
                    {
                        Directory.CreateDirectory(pathToSave);
                    }

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    List<Sale> lstSales = new List<Sale>();
                    _service.SaveAsync(); // clean EF cache
                    using (StreamReader reader = new System.IO.StreamReader(fullPath))
                    {
                        int i = 0;
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            if (i > 0)
                            {
                                var values = line.Split(',');
                                try
                                {
                                    lstSales.Add(new Sale
                                    {
                                        Id = Guid.NewGuid(),
                                        Region = values[0],
                                        Country = values[1],
                                        ItemType = values[2],
                                        SalesChannel = values[3],
                                        OrderPriority = values[4],
                                        OrderDate = values[5].GetDateTimeParsed(),
                                        OrderId = values[6],
                                        ShipDate = values[7].GetDateTimeParsed(),
                                        UnitsSold = Convert.ToInt32(values[8]),
                                        UnitPrice = values[9].GetDecimalParsed(),
                                        UnitCost = values[10].GetDecimalParsed(),
                                        TotalRevenue = values[11].GetDecimalParsed(),
                                        TotalCost = values[12].GetDecimalParsed(),
                                        TotalProfit = values[13].GetDecimalParsed()
                                    });
                                }
                                catch
                                {

                                }
                            }
                            i++;
                        }
                    }

                    Directory.Delete(pathToSave, true);
                    _service.AddRange(lstSales);
                    _service.SaveAsync();
                }
                return Ok("Upload Successful.");
            }
            catch (System.Exception ex)
            {
                return Ok("Upload Failed: " + ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSale(string id, Sale sale)
        {
            try
            {
                await _service.Update(sale).ConfigureAwait(false);
                await _service.SaveAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $" {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(string id)
        {
            try
            {
                await _service.Remove(_service.GetById(new Guid(id)).Result).ConfigureAwait(false);
                await _service.SaveAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $" {ex.Message}");
            }
        }
    }
}