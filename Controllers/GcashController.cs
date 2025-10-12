using Microsoft.AspNetCore.Mvc;

namespace oneSTIOnlineTuitionPayment_Sai.Controllers
{
    public class GcashController : Controller
    {
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
