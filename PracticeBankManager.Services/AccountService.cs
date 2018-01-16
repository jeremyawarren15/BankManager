using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticeBankManager.Data;
using PracticeBankManager.Models.Account;

namespace PracticeBankManager.Services
{
    class AccountService
    {
        public bool CreateAccount(AccountCreate model)
        {
            var account = new Account()
            {
                AccountBalance = model.AccountBalance,
                AccountType = model.AccountType
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx
                    .Accounts
                    .Add(account);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
