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
    private readonly IProductRepository _productRepository;

    public HomeController(ILogger<HomeController> logger, IOrderRepository repository, IProductRepository productRepository)
    {
        _logger = logger;
        _repository = repository;
        _productRepository = productRepository;
    }

    public async Task<IActionResult> Index()
    {
        var inProgressOrders = await InProgressOrders.CreateAsync(_repository);
        
        return View(inProgressOrders);
    }


    public async Task<IActionResult> UpdateOrder([FromQuery]string merchantProductNo)
    {
        var line = new Line(merchantProductNo, "","",0);
        await line.Product.UpdateProductStock(25, _productRepository);
        return RedirectToAction("Index");
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