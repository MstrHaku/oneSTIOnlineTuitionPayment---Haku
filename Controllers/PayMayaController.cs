using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using oneSTIOnlineTuitionPayment.Data;
using oneSTIOnlineTuitionPayment.DTO;
using oneSTIOnlineTuitionPayment.Models;

namespace oneSTIOnlineTuitionPayment.Controllers;

public class PayMayaController : Controller
{
    private readonly ILogger<PayMayaController> _logger;
    private readonly ApplicationDbContext _context;

    public PayMayaController(ILogger<PayMayaController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult PayMayaMain()
    {
        return View();
    }

    public IActionResult PayMayaOTP()
    {
        return View();
    }

    public IActionResult PayMayaDetails()
    {
        var payment = _context.MayaTable.OrderByDescending(p => p.CreatedAt).FirstOrDefault();
        var vm = new ViewModels
        {
            Maya = payment ?? new MayaModel()
        };
        return View(vm);
    }

    public IActionResult PayMayaSuccess()
    {
        var payment = _context.MayaTable.OrderByDescending(p => p.CreatedAt).FirstOrDefault();
        var vm = new ViewModels
        {
            Maya = payment ?? new MayaModel()
        };
        return View(vm);
    }

    [HttpPost]
    public IActionResult SendData([FromBody] MayaModel availBalance)
    {
        _context.MayaTable.Add(availBalance);
        _context.SaveChanges();

        return Json(new { success = true, message = "Data saved successfully" });
    }

    [HttpPost]
    public IActionResult GetAvailBalance([FromBody] PaymentRequest amount)
    {
        Console.WriteLine("Button has been clicked");

        var latest = _context.MayaTable.OrderByDescending(p => p.CreatedAt).FirstOrDefault();

        // COPY THIS TO GCASH
        var remBalance = _context.RemBalanceTable.OrderByDescending(p => p.CreatedAt).FirstOrDefault();

        if (latest == null)
        {
            Console.WriteLine("Empty Table");
        }
        else
        {
            if (latest.AvailBalance > 0)
            {
                if (latest.Amount > latest.AvailBalance)
                {
                    return BadRequest(new { message = "Amount exceeded!" });
                }
                else
                {
                    var newBalance = latest.AvailBalance - latest.Amount;

                    var sendNewBalance = new MayaModel
                    {
                        AvailBalance = newBalance,
                        Amount = latest.Amount
                    };

                    _context.MayaTable.Add(sendNewBalance);

                    // CHECKS IF THE NEW BALANCE WILL EQUAL TO 0 AND UPDATE THE BALANCE TABLE
                    if (newBalance <= 0)
                    {
                        var balance = 10000;

                        var updatedBalance = new MayaModel
                        {
                            AvailBalance = balance,
                            Amount = latest.Amount
                        };

                        _context.MayaTable.Add(updatedBalance);
                    }


                    // COPY THIS TO GCASH
                    // UPDATE REMAINING BALANCE ON REM BALANCE TABLE
                    var newRemBalance = remBalance.Balance - latest.Amount;

                    var updateRemBalance = new RemBalanceModel
                    {
                        Balance = newRemBalance
                    };

                    _context.RemBalanceTable.Add(updateRemBalance);

                    // CHECKS IF REMAINING BALANCE IS 0 AND UPDATES BALANCE
                    if (newRemBalance <= 0)
                    {
                        var balance = 10000;

                        var updatedRemBalance = new RemBalanceModel
                        {
                            Balance = balance
                        };

                        _context.RemBalanceTable.Add(updatedRemBalance);
                    }

                    // PUT THE DATA TO THE TRANSACTIOM TABLE
                    var transactionTable = new TransactionModel
                    {
                        Amount = latest.Amount,
                        CreatedAt = latest.CreatedAt
                    };

                    _context.TransactionTable.Add(transactionTable);
                    _context.SaveChanges();

                    // PAYMENT SCHED
                    var payments = _context.TransactionTable.OrderBy(p => p.CreatedAt).ToList();

                    var schedules = _context.PaymentSchedTable.OrderBy(p => p.CreatedAt).ToList();

                    int totalPayment = latest.Amount;
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

                    var url = Url.Action("PayMayaSuccess", "PayMaya");
                    return Ok(new { message = "Database saved", url });   
                }
            } 
            else
            {
                var balance = 10000;

                var updatedBalance = new MayaModel
                {
                    AvailBalance = balance,
                    Amount = latest.Amount
                };

                _context.MayaTable.Add(updatedBalance);
                Console.WriteLine("Updated PayMaya balance to 10,000");
            }
            
        }


        return Ok( new { message = "Form received"});
    }
}