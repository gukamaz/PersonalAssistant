using PersonalAssistant.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssistant.DAL.Context
{
    public interface IPersonalAssistantContext : IDisposable
    {
        IDbSet<Budget> Budgets { get; set; }
        IDbSet<Meeting> Meetings { get; set; }
        IDbSet<Person> People { get; set; }
        IDbSet<Transaction> Transactions { get; set; }
    }
}
