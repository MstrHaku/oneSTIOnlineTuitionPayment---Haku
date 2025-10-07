using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

// REMOVE THIS IF YOU HAVE A MODEL FILE INSIDE THE MODEL FOLDER!!!!
//using oneSTIOnlineTuitionPayment.Models;

namespace oneSTIOnlineTuitionPayment.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Balance()
    {
        return View();
    }
}
