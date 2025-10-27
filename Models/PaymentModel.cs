namespace oneSTIOnlineTuitionPayment.Models
{
    public class PaymentModel
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public required string PaymentMethod { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}