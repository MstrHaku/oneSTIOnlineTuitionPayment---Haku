using System.ComponentModel.DataAnnotations;

namespace oneSTIOnlineTuitionPayment.Models
{
    public class RemBalanceModel
    {
        public int Id { get; set; }

        [Required]
        public int Balance { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }   

}