using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using oneSTIOnlineTuitionPayment.Data;
using oneSTIOnlineTuitionPayment.DTO;
using oneSTIOnlineTuitionPayment.Models;

namespace oneSTIOnlineTuitionPayment.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult Main(int Amount, string PaymentMethod)
    {
        // SEND REMAINING BALANCE TO DATABASE
        /*if (Amount > 0)
        {
            var payment = new PaymentModel
            {
                Amount = Amount,
                PaymentMethod = PaymentMethod
            };

            _context.PaymentTable.Add(payment);

            if (PaymentMethod.ToLower() == "gcash")
            {
                amountTransfer("gcash", Amount);
                return RedirectToAction("GCashMain", "GCash");
            }
            else if (PaymentMethod.ToLower() == "paymaya")
            {
                return RedirectToAction("PayMayaMain", "PayMaya");
            }
        }*/

        // CHECK IF REM BALANCE IS 0
        var checkRemBalance = _context.RemBalanceTable.OrderByDescending(p => p.CreatedAt).FirstOrDefault();
        var checkAvailBalMaya = _context.MayaTable.OrderByDescending(p => p.CreatedAt).FirstOrDefault();
        var checkAvailGcash = _context.GCashTable.OrderByDescending(p => p.CreatedAt).FirstOrDefault();
        var checkPaySched = _context.PaymentSchedTable.OrderByDescending(p => p.CreatedAt).FirstOrDefault();

        var balance = 10000;

        if (checkRemBalance == null)
        {
            var remBalance = new RemBalanceModel
            {
                Balance = balance
            };

            _context.RemBalanceTable.Add(remBalance);
        }

        if (checkAvailBalMaya == null)
        {
            var mayaBalance = new MayaModel
            {
                AvailBalance = balance
            };

            _context.MayaTable.Add(mayaBalance);
        }

        if (checkAvailGcash == null)
        {
            var gcashBalance = new GCashModel
            {
                AvailBalance = balance
            };

            _context.GCashTable.Add(gcashBalance);
        }

        if (checkPaySched == null)
        {
            var defaultSchedules = new List<PaymentSchedModel>
            {
                new PaymentSchedModel { Amount = 2000, CreatedAt = DateTime.Parse("2025-10-28") },
                new PaymentSchedModel { Amount = 3000, CreatedAt = DateTime.Parse("2025-11-01") },
                new PaymentSchedModel { Amount = 1000, CreatedAt = DateTime.Parse("2025-11-10") },
                new PaymentSchedModel { Amount = 2000, CreatedAt = DateTime.Parse("2025-12-01") },
                new PaymentSchedModel { Amount = 2000, CreatedAt = DateTime.Parse("2026-01-01") },
            };

            _context.PaymentSchedTable.AddRange(defaultSchedules);
        }

        _context.SaveChanges();

        // SEND REMAINING BALANCE TO DATABASE
        var getRemBalance = _context.RemBalanceTable.OrderByDescending(p => p.CreatedAt).FirstOrDefault();
        var vm = new ViewModels
        {
            RemBalance = getRemBalance ?? new RemBalanceModel()
        };



        return View(vm);
    }

    [HttpPost]
    public IActionResult SendData([FromBody] RemBalanceModel Balance)
    {
        _context.RemBalanceTable.Add(Balance);
        _context.SaveChanges();

        return Json(new { success = true, message = "Data saved successfully!" });
    }

    [HttpPost]
    public IActionResult SubmitPayment([FromBody] PaymentRequest amount)
    {
        var currentBalance = _context.RemBalanceTable.OrderByDescending(r => r.CreatedAt).FirstOrDefault()?.Balance ?? 0;

        if (amount.Amount > currentBalance)
        {
            Console.WriteLine("Exceed Amount");
            return BadRequest(new { message = "Amount exceeded!" });
        }
        else
        {
            Console.WriteLine("Amount not exceeded");
            var payment = new PaymentModel
            {
                Amount = amount.Amount,
                PaymentMethod = amount.PaymentMethod
            };

            _context.PaymentTable.Add(payment);

            if (payment.PaymentMethod?.ToLower() == "gcash")
            {
                amountTransfer("gcash", amount.Amount);
                return Ok(new { redirectUrl = Url.Action("GCashMain", "GCash") });
            }
            else if (payment.PaymentMethod?.ToLower() == "paymaya")
            {
                amountTransfer("maya", amount.Amount);
                return Ok(new { redirectUrl = Url.Action("PayMayaMain", "PayMaya") });
            }
        }

        return Ok(new { message = "Receive form data" });
    }

    // METHODS

    [HttpGet]
    public IActionResult getTransaction()
    {
        var transactions = _context.TransactionTable.AsNoTracking().OrderByDescending(p => p.CreatedAt).Select(p => new { p.Id, p.Amount, DateOnly = p.CreatedAt.ToString("yyyy-MM-dd"), TimeOnly = p.CreatedAt.ToString("hh:mm tt") }).ToList();

        return Json(transactions);
    }

    [HttpGet]
    public IActionResult getPaymentSched()
    {
        var paymentSched = _context.PaymentSchedTable.AsNoTracking().OrderBy(p => p.CreatedAt).Select(p => new { p.Id, Amount = p.Amount, CreatedAt = p.CreatedAt.ToString("yyyy-MM-dd")}).ToList();

        return Json(paymentSched);
    }

    private void amountTransfer(string tableName, int Amount)
    {
        if (tableName.Equals("gcash"))
        {
            var availBalance = _context.GCashTable.OrderByDescending(p => p.CreatedAt).FirstOrDefault();

            var gcash = new GCashModel
            {
                Amount = Amount,
                AvailBalance = availBalance?.AvailBalance ?? 0
            };

            _context.GCashTable.Add(gcash);
            _context.SaveChanges();
        }
        else if (tableName.Equals("maya"))
        {
            var availBalance = _context.MayaTable.OrderByDescending(p => p.CreatedAt).FirstOrDefault();

            var maya = new MayaModel
            {
                Amount = Amount,
                AvailBalance = availBalance?.AvailBalance ?? 0
            };

            _context.MayaTable.Add(maya);
            _context.SaveChanges();
        }
    }
}
