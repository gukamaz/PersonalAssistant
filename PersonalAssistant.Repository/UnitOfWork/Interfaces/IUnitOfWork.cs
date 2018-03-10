using PersonalAssistant.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssistant.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IBudgetRepository Budgets { get; }
        IPersonRepository Persons { get; }
        ITransactionRepository Transactions { get; }
        IMeetingRepository Meetings { get; }

        int Save();
    }
}
