using PersonalAssistant.DAL.Context;
using PersonalAssistant.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssistant.Repository
{
    class MeetingRepository : Repository<Meeting>, IMeetingRepository
    {
        public MeetingRepository(PersonalAssistantContext context) : base(context)
        {
        }

        public new PersonalAssistantContext Context
        {
            get { return base.Context as PersonalAssistantContext; }
        }

        public IQueryable<Meeting> GetByPerson(int id)
        {
            return GetRange(r => r.PersonID == id);
        }

        public IQueryable<Meeting> GetByPerson(string personID)
        {
            return GetRange(r => r.Person.PersonalID == personID);
        }
    }
}
