using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using oneSTIOnlineTuitionPayment.Models;
using oneSTIOnlineTuitionPayment.Data;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using oneSTIOnlineTuitionPayment.DTO;

namespace oneSTIOnlineTuitionPayment.Controllers;

public class GCashController : Controller
{
    private readonly ILogger<GCashController> _logger;
    private readonly ApplicationDbContext _context;

    public GCashController(ILogger<GCashController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult GCashMain()
    {
        var payment = _context.GCashTable
                        .OrderByDescending(p => p.CreatedAt)
                        .FirstOrDefault();

        var vm = new ViewModels
        {
            GCash = payment ?? new GCashModel()
        };

        return View(vm);
    }

    [HttpPost]
    public IActionResult GetAvailBalance([FromBody] PaymentRequest amount)
    {
        Console.WriteLine("Accessed Gcash Table");

        var gcashTable = _context.GCashTable.OrderByDescending(p => p.CreatedAt).FirstOrDefault();
        var remBalance = _context.RemBalanceTable.OrderByDescending(p => p.CreatedAt).FirstOrDefault();

        if (gcashTable == null)
        {
            Console.WriteLine("Empty Table");
        }
        else
        {   
            if (gcashTable.AvailBalance > 0)
            {
                if (gcashTable.Amount > gcashTable.AvailBalance )
                {
                    return BadRequest(new { message = "Amount exceeded!" });
                }
                else
                {
                    var newBalance = gcashTable.AvailBalance - gcashTable.Amount;

                    var sendNewBalance = new GCashModel
                    {
                        AvailBalance = newBalance,
                        Amount = gcashTable.Amount
                    };

                    _context.GCashTable.Add(sendNewBalance);

                    // CHECKS IF GCASH TABLE AVAILBALANCE COLUMN IS 0
                    if (newBalance <= 0)
                    {
                        var balance = 10000;

                        var updateGcashBalance = new GCashModel
                        {
                            AvailBalance = balance,
                            Amount = gcashTable.Amount
                        };

                        _context.GCashTable.Add(updateGcashBalance);
                    }

                    // CHECK REMBALANCE IN REMBALANCE TABLE
                    var newRemBalance = (remBalance?.Balance ?? 0) - gcashTable.Amount;

                    var updatedRemBalance = new RemBalanceModel
                    {
                        Balance = newRemBalance
                    };

                    _context.RemBalanceTable.Add(updatedRemBalance);

                    if (newRemBalance <= 0)
                    {
                        var balance = 10000;

                        var remBalanceUpdate = new RemBalanceModel
                        {
                            Balance = balance,
                        };

                        _context.RemBalanceTable.Add(remBalanceUpdate);
                    }

                    // PUT THE DATA INTO TRANSACTION TABLE
                    var transactionTable = new TransactionModel
                    {
                        Amount = gcashTable.Amount,
                        CreatedAt = gcashTable.CreatedAt
                    };

                    _context.TransactionTable.Add(transactionTable);
                    _context.SaveChanges();

                    // PAYMENT SCHED
                    var payments = _context.TransactionTable.OrderBy(p => p.CreatedAt).ToList();

                    var schedules = _context.PaymentSchedTable.OrderBy(p => p.CreatedAt).ToList();

                    int totalPayment = gcashTable.Amount;
                    foreach (var s in schedules.ToList())
                    {
                        if (totalPayment >= s.Amount)
                        {
                            totalPayment -= s.Amount;
                            s.Amount = 0;

                            _context.PaymentSchedTable.Remove(s);
                        }
                        else
                        {
                            s.Amount -= totalPayment;
                            totalPayment = 0;

                            _context.PaymentSchedTable.Update(s);
                        }

                        if (totalPayment <= 0)
                            break;
                    }

                    _context.SaveChanges();
                }
            }
            else
            {
                var balance = 10000;

                var updateAvailBalance = new GCashModel
                {
                    AvailBalance = balance,
                    Amount = gcashTable.Amount
                };

                _context.GCashTable.Add(updateAvailBalance);
                _context.SaveChanges();
                Console.WriteLine("Update GCash balance to 10,000");
            }
        }


        return Ok( new { message = "Received form"});
    }
}