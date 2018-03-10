using PersonalAssistant.DAL.Context;
using PersonalAssistant.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssistant.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PersonalAssistantContext _context;

        public PersonalAssistantContext Context
        {
            get { return _context; }
        }

        public UnitOfWork(IPersonalAssistantContext context)
        {
            _context = (PersonalAssistantContext)context;
            Budgets = new BudgetRepository(_context);
            Meetings = new MeetingRepository(_context);
            Persons = new PersonRepository(_context);
            Transactions = new TransactionRepository(_context);
        }

        public IBudgetRepository Budgets { get; private set; }
        public IPersonRepository Persons { get; private set; }
        public ITransactionRepository Transactions { get; private set; }
        public IMeetingRepository Meetings { get; private set; }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();

            Budgets.Dispose();
            Persons.Dispose();
            Transactions.Dispose();
            Meetings.Dispose();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
