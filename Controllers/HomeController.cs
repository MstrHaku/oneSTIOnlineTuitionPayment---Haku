using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace oneSTIOnlineTuitionPayment.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Main()
    {
        return View();
    }
}


