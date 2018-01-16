using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeBankManager.Models.Account
{
    public class AccountListItem
    {
        public int AccountId { get; set; }

        public decimal AccountBalance { get; set; }

        public string AccountType { get; set; }
    }
}
