using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeBankManager.Models.Transaction
{
    public class TransactionDetail
    {
        public int TransactionId { get; set; }
        public DateTimeOffset TransactionDate { get; set; }
        public int AccountId { get; set; }
        public decimal TransactionAmount { get; set; }
        public string TransactionType { get; set; }
        public string TransactionDescription { get; set; }
    }
}
