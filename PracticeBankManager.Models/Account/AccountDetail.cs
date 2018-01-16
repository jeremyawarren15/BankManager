using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeBankManager.Models.Account
{
    public class AccountDetail
    {
        public int AccountId { get; set; }
        public decimal AccountBalance { get; set; }
        public string AccountType { get; set; }
    }
}
