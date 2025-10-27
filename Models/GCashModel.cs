using Microsoft.AspNetCore.Identity;

namespace oneSTIOnlineTuitionPayment.Models
{
    public class GCashModel
    {
        public int Id { get; set; }
        public int PhoneNum { get; set; }
        public int Amount { get; set; }
        public int AvailBalance { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}