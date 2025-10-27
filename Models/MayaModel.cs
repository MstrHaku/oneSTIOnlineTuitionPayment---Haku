using System.ComponentModel.DataAnnotations;

namespace oneSTIOnlineTuitionPayment.Models
{
    public class MayaModel
    {
        public int Id { get; set; }
        public int phoneNum { get; set; }
        public int AvailBalance { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}