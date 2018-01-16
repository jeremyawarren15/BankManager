using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeBankManager.Models.Account
{
    public class AccountCreate
    {
        [DefaultValue(0.00)]
        public decimal AccountBalance { get; set; }

        [Required]
        public string AccountType { get; set; }
    }
}
