using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeBankManager.Models.Transaction
{
    public class TransactionCreate
    {
        [Required]
        public DateTimeOffset TransactionDate { get; set; }

        [Required]
        public int AccountId { get; set; }

        [Required]
        public decimal TransactionAmount { get; set; }

        [Required]
        public string TransactionType { get; set; }

        public string TransactionDescription { get; set; }
    }
}
