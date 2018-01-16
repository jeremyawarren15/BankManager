using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeBankManager.Data
{
    public class Transactions
    {
        [Key]
        public int TransactionId { get; set; }

        [Required]
        public DateTimeOffset TransactionDate { get; set; }

        [Required]
        public int AccountId { get; set; }

        [Required]
        public decimal TransactionAmount { get; set; }

        [Required]
        public string TransactionType { get; set; }

        public string TransactionDescription { get; set; }

        public virtual Account Account { get; set; }
    }
}
