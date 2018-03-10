using PersonalAssistant.DAL.Context;
using PersonalAssistant.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssistant.Repository
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(PersonalAssistantContext context) : base(context)
        {
        }

        public new PersonalAssistantContext Context
        {
            get { return base.Context as PersonalAssistantContext; }
        }
    }
}
