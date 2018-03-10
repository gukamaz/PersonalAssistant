using PersonalAssistant.DAL.Context;
using PersonalAssistant.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssistant.Repository
{
    class BudgetRepository : Repository<Budget>, IBudgetRepository
    {
        public BudgetRepository(PersonalAssistantContext context) : base(context)
        {
        }

        public new PersonalAssistantContext Context
        {
            get { return base.Context as PersonalAssistantContext; }
        }

        public Budget GetCurrentByPerson(int id)
        {
            var now = DateTime.Now;
            return Get(c => c.PersonID == id 
                        && c.Year == now.Year 
                        && c.Month == now.Month);
        }
    }
}
