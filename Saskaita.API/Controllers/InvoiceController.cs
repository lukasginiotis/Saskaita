using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Saskaita.API.Application.Services;
using Saskaita.Domain.Models;
using Saskaita.Repository;

namespace Saskaita.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoiceController : ControllerBase
    {
        public IInvoiceService _invoiceService;
        public ICountryRepository _countryRepository;

        public InvoiceController(IInvoiceService invoiceService, ICountryRepository countryRepository)
        {
            _invoiceService = invoiceService;
            _countryRepository = countryRepository;
        }

        [HttpPost(Name = "CreateInvoice")]
        public ActionResult Post([FromBody] InvoiceRequest requestBody)
        {
            InvoiceRequestValidator validator = new(_countryRepository);

            ValidationResult result = validator.Validate(requestBody);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors.First().ErrorMessage);
            }
            return new FileContentResult(_invoiceService.CreateInvoice(requestBody).Stream.ToArray(), "application/pdf")
            {
                FileDownloadName = "invoice.pdf"
            };
        }
    }
}