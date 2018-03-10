using PersonalAssistant.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssistant.DAL.Context
{
    public class PersonalAssistantContext : DbContext, IPersonalAssistantContext
    {
        public PersonalAssistantContext() : base("name=PersonalAssisctantConnection")
        {
        }

        public IDbSet<Budget> Budgets { get; set; }
        public IDbSet<Meeting> Meetings { get; set; }
        public IDbSet<Person> People { get; set; }
        public IDbSet<Transaction> Transactions { get; set; }
    }
}
