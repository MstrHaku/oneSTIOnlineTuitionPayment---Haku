using Microsoft.AspNetCore.Mvc;

namespace oneSTIOnlineTuitionPayment.Controllers
{
    public class GcashController : Controller
    {
        private readonly ILogger<GcashController> _logger;

        public GcashController(ILogger<GcashController> logger)
        {
            _logger = logger;
        }

        public IActionResult PhoneNumber()
        {
            return View("Gcash-PhoneNumber");
        }

        public IActionResult OtpPage()
        {
            return View("Gcash-OtpPage");
        }
    }
}


