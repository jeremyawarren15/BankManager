using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticeBankManager.Data;
using PracticeBankManager.Models.Transaction;

namespace PracticeBankManager.Services
{
    public class TransactionService
    {
        private readonly Guid _userId;

        public TransactionService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateTransaction(TransactionCreate model)
        {
            var transaction = new Transactions()
            {
                AccountId = model.AccountId,
                TransactionAmount = model.TransactionAmount,
                TransactionDate = DateTimeOffset.UtcNow,
                TransactionDescription = model.TransactionDescription,
                TransactionType = model.TransactionType
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx
                    .Transactions
                    .Add(transaction);

                if (ctx.SaveChanges() != 1)
                    return false;

                var accountService = new AccountService(_userId);

                return 
                    accountService.UpdateAccountBalance(
                        transaction.AccountId, 
                        transaction.TransactionAmount);
            }
        }

        public ICollection<TransactionListItem> GetTransactions(int accountId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var transactions =
                    ctx
                        .Transactions
                        .Where(t => t.AccountId == accountId)
                        .Select(t =>
                            new TransactionListItem()
                            {
                                TransactionAmount = t.TransactionAmount,
                                TransactionDescription = t.TransactionDescription,
                                TransactionType = t.TransactionType,
                                TransactionDate = t.TransactionDate,
                                TransactionId = t.TransactionId
                            });

                return transactions.ToList();
            }
        }

        public TransactionDetail GetTransaction(int accountId, int transactionId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var accountService = new AccountService(_userId);

                var accounts = accountService.GetAccounts();

                var accountIds = accounts.Select(account => account.AccountId).ToList();

                var transaction =
                    ctx
                        .Transactions
                        .Where(t => accountIds.Contains(t.AccountId) &&
                                    t.AccountId == accountId &&
                                    t.TransactionId == transactionId)
                        .Select(t =>
                            new TransactionDetail()
                            {
                                AccountId = t.AccountId,
                                TransactionAmount = t.TransactionAmount,
                                TransactionDescription = t.TransactionDescription,
                                TransactionDate = t.TransactionDate,
                                TransactionType = t.TransactionType,
                                TransactionId = t.TransactionId
                            });

                return transaction.SingleOrDefault();
            }
        }
    }
}
