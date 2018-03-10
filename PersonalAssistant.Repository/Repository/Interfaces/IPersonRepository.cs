using PersonalAssistant.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssistant.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person Get(string personalID);
    }
}
