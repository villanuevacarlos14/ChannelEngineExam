using System.Diagnostics;
using Domain;
using Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IOrderRepository _repository;

    public HomeController(ILogger<HomeController> logger, IOrderRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<IActionResult> Index()
    {
        var inProgressOrders = await InProgressOrders.CreateAsync(_repository);
        
        return View(inProgressOrders);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}