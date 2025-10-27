namespace oneSTIOnlineTuitionPayment.Models
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}