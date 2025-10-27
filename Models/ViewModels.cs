using System.ComponentModel.DataAnnotations;

namespace oneSTIOnlineTuitionPayment.Models
{
    public class ViewModels
    {
        public RemBalanceModel? RemBalance { get; set; }
        public TransactionModel? Transaction { get; set; }
        public PaymentSchedModel? PaymentSched { get; set; }
        public PaymentModel? Payment { get; set; }
        public MayaModel? Maya { get; set; }
        public GCashModel? GCash { get; set; }
    }
}