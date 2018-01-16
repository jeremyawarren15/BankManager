using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeBankManager.Data
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }

        [DefaultValue(0.00)]
        public decimal AccountBalance { get; set; }

        [Required]
        public string AccountType { get; set; }

        public ICollection<AccountRelationship> AccountRelationships { get; set; }
        public ICollection<Transactions> Transactions { get; set; }
    }
}
