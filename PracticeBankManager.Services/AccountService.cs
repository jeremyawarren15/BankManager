using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticeBankManager.Data;
using PracticeBankManager.Models.Account;

namespace PracticeBankManager.Services
{
    public class AccountService
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

            if (model.AccountBalance < 0)
                return false;

            using (var ctx = new ApplicationDbContext())
            {
                ctx
                    .Accounts
                    .Add(account);

                if (ctx.SaveChanges() != 1)
                    return false;

                var relationship = new AccountRelationship()
                {
                    AccountId = account.AccountId,
                    UserId = _userId
                };

                ctx
                    .AccountRelationships
                    .Add(relationship);

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

        private Account GetAccount(ApplicationDbContext context, int accountId)
        {
            return
                context
                    .AccountRelationships
                    .Where(ar => ar.UserId == _userId &&
                                 ar.AccountId == accountId)
                    .Select(
                        a => new Account()
                        {
                            AccountId = a.Account.AccountId,
                            AccountType = a.Account.AccountType,
                            AccountBalance = a.Account.AccountBalance
                        })
                    .FirstOrDefault();
        }

        private bool OwnsAccount(ApplicationDbContext context, int accountId)
        {
            var account =
                context
                    .AccountRelationships
                    .Where(ar => ar.UserId == _userId &&
                                 ar.AccountId == accountId);

            return account.ToList().Count() == 1;
        }

        public AccountDetail GetAccount(int accountId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var account = GetAccount(ctx, accountId);

                return
                    new AccountDetail()
                    {
                        AccountId = account.AccountId,
                        AccountType = account.AccountType,
                        AccountBalance = account.AccountBalance
                    };
            }
        }

        public bool UpdateAccountType(int accountId, string accountType)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var account = GetAccount(ctx, accountId);

                account.AccountType = accountType;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool UpdateAccountBalance(int accountId, decimal changeAmount)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var account = GetAccount(ctx, accountId);

                account.AccountBalance += changeAmount;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool AddSecondaryUser(int accountId, string userEmail)
        {
            using (var ctx = new ApplicationDbContext())
            {
                if (!OwnsAccount(ctx, accountId))
                    return false;

                var secondUser =
                    ctx
                        .Users
                        .SingleOrDefault(u => u.Email == userEmail);

                if (secondUser == null)
                    return false;

                var relationship = new AccountRelationship()
                {
                    AccountId = accountId,
                    UserId = Guid.Parse(secondUser.Id)
                };

                ctx
                    .AccountRelationships
                    .Add(relationship);

                return ctx.SaveChanges() == 1;
            }
        }

        public bool RemoveUserFromAccount(int accountId, string userEmail)
        {
            using (var ctx = new ApplicationDbContext())
            {
                if (!OwnsAccount(ctx, accountId))
                    return false;

                var secondUser =
                    ctx
                        .Users
                        .SingleOrDefault(u => u.Email == userEmail);

                if (secondUser == null)
                    return false;

                var relationship =
                    ctx
                        .AccountRelationships
                        .SingleOrDefault(ar => ar.AccountId == accountId &&
                                               ar.User.Email == secondUser.Email);

                if (relationship == null)
                    return false;

                ctx
                    .AccountRelationships
                    .Remove(relationship);

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteAccount(int accountId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var account = GetAccount(ctx, accountId);

                ctx.Accounts.Remove(account);

                if (ctx.SaveChanges() != 1) return false;

                var accountRelationships = 
                    ctx
                        .AccountRelationships
                        .Where(ar => ar.AccountId == accountId)
                        .ToList();

                ctx
                    .AccountRelationships
                    .RemoveRange(accountRelationships);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
