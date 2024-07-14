using BillingSystem.Data;
using BillingSystem.Models.Repositories;
using BillingSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BillingSystem.Controllers
{
    public class OpeningInvoicesController : Controller
    {
        private readonly IOpeningInvoicesRepository _openingInvoicesRepository;

        public OpeningInvoicesController(IOpeningInvoicesRepository openingInvoicesRepository)
        {
            _openingInvoicesRepository = openingInvoicesRepository;
        }
        //public async Task<IActionResult> Index()
        //{
        //    OpeningInvoicesListViewModel model = new()
        //    {
        //        OpeningInvoices = (await _openingInvoicesRepository.GetAllOpeningInvoicesAsync()).ToList()
        //    };
        //    return View(model);
        //}
        public IActionResult Index()
        {
            OpeningInvoicesListViewModel model = new()
            {
                OpeningInvoices = (_openingInvoicesRepository.GetAllOpeningInvoices()).ToList()
            };
            return View(model);
        }
    }
}
