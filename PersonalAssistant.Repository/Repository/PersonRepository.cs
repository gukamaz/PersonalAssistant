using PersonalAssistant.DAL.Context;
using PersonalAssistant.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssistant.Repository
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(PersonalAssistantContext context) : base(context)
        {
        }

        public new PersonalAssistantContext Context
        {
            get { return base.Context as PersonalAssistantContext; }
        }

        public Person Get(string personalID)
        {
            return Get(p => p.PersonalID == personalID);
        }
    }
}
