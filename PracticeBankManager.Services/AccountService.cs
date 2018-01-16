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
        private readonly Guid _userId;

        public AccountService(Guid userId)
        {
            _userId = userId;
        }

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

        public ICollection<AccountListItem> GetAccounts()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var accounts =
                    ctx
                        .AccountRelationships.Where(ar => ar.UserId == _userId)
                        .Select(
                            a => new AccountListItem()
                            {
                                AccountId = a.Account.AccountId,
                                AccountType = a.Account.AccountType,
                                AccountBalance = a.Account.AccountBalance
                            });

                return accounts.ToList();
            }
        }

        public AccountDetail GetAccount(int accountId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var account =
                    ctx
                        .AccountRelationships
                        .Where(ar => ar.UserId == _userId &&
                                     ar.AccountId == accountId)
                        .Select(
                            a => new AccountDetail()
                            {
                                AccountId = a.Account.AccountId,
                                AccountType = a.Account.AccountType,
                                AccountBalance = a.Account.AccountBalance
                            })
                        .FirstOrDefault();

                return account;
            }
        }
    }
}
