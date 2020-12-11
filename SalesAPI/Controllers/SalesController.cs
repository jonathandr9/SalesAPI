using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SalesApi.Domain.Models;
using SalesApi.Domain.Services;
using SalesAPI.WebApi.Dto;

namespace SalesAPI.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _saleService;
        private readonly IMapper _mapper;

        public SalesController(ISaleService saleService,
            IMapper mapper)
        {
            _saleService = saleService;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult RegisterSale(SalePost salePost)
        {
            var sale = _mapper.Map<Sale>(salePost);

            Sale saleResult = _saleService.RegisterSale(sale);

            return Ok(saleResult);
        }
        
        [HttpGet]
        public IActionResult SearchSale(string saleID)
        {           
            Sale saleResult = _saleService.SearchSale(saleID);

            return Ok(saleResult);
        }
    }
}
